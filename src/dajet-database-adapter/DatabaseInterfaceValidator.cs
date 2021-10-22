using DaJet.Metadata.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DaJet.Database.Adapter
{
    public interface IDatabaseInterfaceValidator
    {
        bool IncomingInterfaceIsValid(ApplicationObject queue, out List<string> errors);
        bool OutgoingInterfaceIsValid(ApplicationObject queue, out List<string> errors);
        bool InterfaceIsValid(Type template, ApplicationObject queue, out List<string> errors);
    }
    public sealed class DatabaseInterfaceValidator : IDatabaseInterfaceValidator
    {
        public bool IncomingInterfaceIsValid(ApplicationObject queue, out List<string> errors)
        {
            return InterfaceIsValid(typeof(DatabaseIncomingMessage), queue, out errors);
        }
        public bool OutgoingInterfaceIsValid(ApplicationObject queue, out List<string> errors)
        {
            return InterfaceIsValid(typeof(DatabaseOutgoingMessage), queue, out errors);
        }
        public bool InterfaceIsValid(Type template, ApplicationObject queue, out List<string> errors)
        {
            errors = new List<string>();

            if (!QueueNameIsValid(template, queue, in errors))
            {
                return false;
            }

            ValidateInterface(queue, template, in errors);

            return (errors.Count == 0);
        }
        private bool QueueNameIsValid(Type template, ApplicationObject queue, in List<string> errors)
        {
            TableAttribute table = template.GetCustomAttribute<TableAttribute>();
            
            if (table == null || string.IsNullOrWhiteSpace(table.Name))
            {
                errors.Add($"TableAttribute is not defined for template type \"{template.FullName}\".");
                return false;
            }

            string[] names = table.Name.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (names.Length != 2)
            {
                errors.Add($"Bad template metadata object name format: {nameof(table.Name)}");
            }
            string baseType = names[0];
            string typeName = names[1];

            if ((baseType == "Справочник" && !(queue is Catalog)) ||
                (baseType == "РегистрСведений" && !(queue is InformationRegister)))
            {
                errors.Add($"The base type \"{baseType}\" does not match the metadata object \"{queue.Name}\".");
                return false;
            }

            if (queue.Name != typeName)
            {
                errors.Add($"The name of metadata object \"{queue.Name}\" does not match \"{typeName}\".");
                return false;
            }

            if (string.IsNullOrWhiteSpace(queue.TableName))
            {
                errors.Add($"The metadata object \"{queue.Name}\" does not have a database table defined.");
                return false;
            }

            return true;
        }
        private void ValidateInterface(ApplicationObject queue, Type template, in List<string> errors)
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