using DaJet.Metadata.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DaJet.Database.Adapter
{
    public interface IDatabaseInterfaceValidator
    {
        bool IncomingQueueInterfaceIsValid(ApplicationObject queue, out List<string> errors);
        bool OutgoingQueueInterfaceIsValid(ApplicationObject queue, out List<string> errors);
    }
    public sealed class DatabaseInterfaceValidator : IDatabaseInterfaceValidator
    {
        private const string INCOMING_QUEUE_NAME = "DaJetExchangeВходящаяОчередь";
        private const string OUTGOING_QUEUE_NAME = "DaJetExchangeИсходящаяОчередь";

        public bool IncomingQueueInterfaceIsValid(ApplicationObject queue, out List<string> errors)
        {
            errors = new List<string>();

            if (!(queue is InformationRegister))
            {
                errors.Add($"The metadata object \"{queue.Name}\" is not an information register.");
            }

            if (queue.Name != INCOMING_QUEUE_NAME)
            {
                errors.Add($"The name of metadata object \"{queue.Name}\" does not match \"{INCOMING_QUEUE_NAME}\".");
            }

            if (string.IsNullOrWhiteSpace(queue.TableName))
            {
                errors.Add($"The metadata object \"{queue.Name}\" does not have a database table defined.");
            }

            ValidateDatabaseInterface(queue, typeof(DatabaseIncomingMessage), in errors);

            return (errors.Count == 0);
        }

        public bool OutgoingQueueInterfaceIsValid(ApplicationObject queue, out List<string> errors)
        {
            errors = new List<string>();

            if (!(queue is InformationRegister))
            {
                errors.Add($"The metadata object \"{queue.Name}\" is not an information register.");
            }

            if (queue.Name != OUTGOING_QUEUE_NAME)
            {
                errors.Add($"The name of metadata object \"{queue.Name}\" does not match \"{OUTGOING_QUEUE_NAME}\".");
            }

            if (string.IsNullOrWhiteSpace(queue.TableName))
            {
                errors.Add($"The metadata object \"{queue.Name}\" does not have a database table defined.");
            }

            ValidateDatabaseInterface(queue, typeof(DatabaseOutgoingMessage), in errors);

            return (errors.Count == 0);
        }

        private void ValidateDatabaseInterface(ApplicationObject queue, Type template, in List<string> errors)
        {
            foreach (PropertyInfo info in template.GetProperties())
            {
                ColumnAttribute column = info.GetCustomAttribute<ColumnAttribute>();
                if (column == null)
                {
                    errors.Add($"The property \"{info.Name}\" does not have attribute Column applied.");
                    continue;
                }

                bool found = false;

                foreach (MetadataProperty property in queue.Properties)
                {
                    if (property.Fields.Count == 0)
                    {
                        errors.Add($"The property \"{property.Name}\" does not have a database field defined.");
                    }
                    else if (property.Fields.Count > 1)
                    {
                        errors.Add($"The property \"{property.Name}\" has too many database fields defined.");
                    }
                    else if (string.IsNullOrWhiteSpace(property.Fields[0].Name))
                    {
                        errors.Add($"The property \"{property.Name}\" has empty database field name.");
                    }

                    if (property.Name == column.Name)
                    {
                        found = true; break;
                    }
                }

                if (!found)
                {
                    errors.Add($"The property \"{info.Name}\" [{column.Name}] does not match database interface.");
                }
            }
        }
    }
}