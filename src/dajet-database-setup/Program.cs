using DaJet.Database.Adapter;
using DaJet.Metadata;
using DaJet.Metadata.Model;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DaJet.Database.Setup
{
    public static class Program
    {
        private const string SERVER_IS_NOT_DEFINED_ERROR = "Server address is not defined.";
        private const string DATABASE_IS_NOT_DEFINED_ERROR = "Database name is not defined.";

        private static readonly IDatabaseConfigurator DbConfigurator = new DatabaseConfigurator();
        private static readonly IDatabaseInterfaceValidator DbValidator = new DatabaseInterfaceValidator();

        public static int Main(string[] args)
        {
            //args = new string[] { "--ms", "zhichkin", "--db", "cerberus" };
            //args = new string[] { "--ms", "zhichkin", "--db", "test_node_1" };
            //args = new string[] { "--pg", "127.0.0.1", "--db", "test_node_2", "--usr", "postgres", "--pwd", "postgres" };

            RootCommand command = new RootCommand()
            {
                new Option<string>("--ms", "Microsoft SQL Server address or name"),
                new Option<string>("--pg", "PostgresSQL server address or name"),
                new Option<string>("--db", "Database name"),
                new Option<string>("--usr", "User name (Windows authentication is used if not defined)"),
                new Option<string>("--pwd", "User password if SQL Server authentication is used")
            };
            command.Description = "DaJet Agent Database Setup Utility 1.0";
            command.Handler = CommandHandler.Create<string, string, string, string, string>(ExecuteCommand);
            return command.Invoke(args);
        }
        private static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static int ExecuteCommand(string ms, string pg, string db, string usr, string pwd)
        {
            if (string.IsNullOrWhiteSpace(ms) && string.IsNullOrWhiteSpace(pg))
            {
                ShowErrorMessage(SERVER_IS_NOT_DEFINED_ERROR);
                return 1;
            }

            if (string.IsNullOrWhiteSpace(db))
            {
                ShowErrorMessage(DATABASE_IS_NOT_DEFINED_ERROR);
                return 1;
            }

            IMetadataService metadataService = ConfigureMetadataService(ms, pg, db, usr, pwd);

            DbConfigurator
                .UseDatabaseProvider(metadataService.DatabaseProvider)
                .UseConnectionString(metadataService.ConnectionString);

            if (!TryOpenInfoBase(metadataService, out InfoBase infoBase, out string errorMessage))
            {
                ShowErrorMessage(errorMessage);
                return 1;
            }

            ShowInfoBaseInfo(infoBase);

            int exitCode = SetupOutgoingQueue(infoBase);
            if (exitCode == 0)
            {
                exitCode = SetupIncomingQueue(infoBase);
            }

            return exitCode;
        }
        private static IMetadataService ConfigureMetadataService(string ms, string pg, string db, string usr, string pwd)
        {
            IMetadataService metadataService = new MetadataService();

            if (!string.IsNullOrWhiteSpace(ms))
            {
                metadataService
                    .UseDatabaseProvider(DatabaseProvider.SQLServer)
                    .ConfigureConnectionString(ms, db, usr, pwd);
            }
            else if (!string.IsNullOrWhiteSpace(pg))
            {
                metadataService
                    .UseDatabaseProvider(DatabaseProvider.PostgreSQL)
                    .ConfigureConnectionString(pg, db, usr, pwd);
            }

            return metadataService;
        }
        private static bool TryOpenInfoBase(IMetadataService metadataService, out InfoBase infoBase, out string errorMessage)
        {
            errorMessage = null;

            try
            {
                infoBase = metadataService.OpenInfoBase();
            }
            catch (Exception error)
            {
                infoBase = null;
                errorMessage = ExceptionHelper.GetErrorText(error);
            }

            return (errorMessage == null);
        }
        private static void ShowInfoBaseInfo(InfoBase infoBase)
        {
            ConfigInfo config = infoBase.ConfigInfo;

            Console.WriteLine();
            Console.WriteLine("Name = " + config.Name);
            Console.WriteLine("Alias = " + config.Alias);
            Console.WriteLine("Comment = " + config.Comment);
            Console.WriteLine("Version = " + config.Version);
            Console.WriteLine("YearOffset = " + infoBase.YearOffset.ToString());
            Console.WriteLine("ConfigVersion = " + config.ConfigVersion);
            Console.WriteLine();
        }

        private static int SetupOutgoingQueue(InfoBase infoBase)
        {
            Type template = typeof(DatabaseOutgoingMessage);

            TableAttribute table = template.GetCustomAttribute<TableAttribute>();
            if (table == null || string.IsNullOrWhiteSpace(table.Name))
            {
                ShowErrorMessage($"TableAttribute is not defined for template type \"{template.FullName}\".");
                return 1;
            }

            ApplicationObject queue = DbConfigurator.FindMetadataObjectByName(infoBase, table.Name);
            if (queue == null)
            {
                ShowErrorMessage($"Metadata object \"{table.Name}\" is not found.");
                return 1;
            }

            if (!DbValidator.OutgoingInterfaceIsValid(queue, out List<string> errors))
            {
                ShowErrorMessage($"{table.Name}");
                foreach (string error in errors)
                {
                    ShowErrorMessage(error);
                }
                return 1;
            }

            int exitCode = 0;

            try
            {
                DbConfigurator.ConfigureOutgoingQueue(queue);
                ShowSuccessMessage($"Outgoing queue \"{table.Name}\" [{queue.TableName}] configured successfully.");
            }
            catch (Exception error)
            {
                exitCode = 1;
                ShowErrorMessage($"Failed to configure outgoing queue \"{table.Name}\" [{queue.TableName}].");
                ShowErrorMessage(ExceptionHelper.GetErrorText(error));
            }

            return exitCode;
        }

        private static int SetupIncomingQueue(InfoBase infoBase)
        {
            Type template = typeof(DatabaseIncomingMessage);

            TableAttribute table = template.GetCustomAttribute<TableAttribute>();
            if (table == null || string.IsNullOrWhiteSpace(table.Name))
            {
                ShowErrorMessage($"TableAttribute is not defined for template type \"{template.FullName}\".");
                return 1;
            }

            ApplicationObject queue = DbConfigurator.FindMetadataObjectByName(infoBase, table.Name);
            if (queue == null)
            {
                ShowErrorMessage($"Metadata object \"{table.Name}\" is not found.");
                return 1;
            }

            if (!DbValidator.IncomingInterfaceIsValid(queue, out List<string> errors))
            {
                ShowErrorMessage($"{table.Name}");
                foreach (string error in errors)
                {
                    ShowErrorMessage(error);
                }
                return 1;
            }

            int exitCode = 0;

            try
            {
                if (DbConfigurator.IncomingQueueSequenceExists())
                {
                    DbConfigurator.DropIncomingQueueTrigger(queue);
                }
                else
                {
                    DbConfigurator.ConfigureIncomingQueue(queue);
                }
                ShowSuccessMessage($"Incoming queue \"{table.Name}\" [{queue.TableName}] configured successfully.");
            }
            catch (Exception error)
            {
                exitCode = 1;
                ShowErrorMessage($"Failed to configure incoming queue \"{table.Name}\" [{queue.TableName}].");
                ShowErrorMessage(ExceptionHelper.GetErrorText(error));
            }

            return exitCode;
        }
    }
}