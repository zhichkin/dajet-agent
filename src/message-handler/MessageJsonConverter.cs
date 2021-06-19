using DaJet.Agent.MessageHandlers;
using System;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaJet.Json.Converters
{
    public sealed class MessageJsonConverter : JsonConverter<object>
    {
        private readonly IReferenceResolver _resolver;
        private readonly ISerializationBinder _binder;

        private readonly byte[] TYPE = Encoding.UTF8.GetBytes("#type");
        private readonly byte[] VALUE = Encoding.UTF8.GetBytes("#value");

        public MessageJsonConverter(ISerializationBinder binder, IReferenceResolver resolver) : base()
        {
            _binder = binder;
            _resolver = resolver;
        }
        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return ReadObject(ref reader);
        }
        private object ReadObject(ref Utf8JsonReader reader)
        {
            string typeCode = null;
            Type objectType = null;
            object objectValue = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.ValueTextEquals(TYPE))
                    {
                        reader.Read();
                        typeCode = reader.GetString();
                        objectType = _binder.GetType(typeCode);
                    }
                    else if (reader.ValueTextEquals(VALUE))
                    {
                        if (objectType != null)
                        {
                            objectValue = ReadRecordSet(ref reader, objectType, typeCode);
                        }
                    }
                }
            }
            return objectValue;
        }
        private object ReadObjectValue(ref Utf8JsonReader reader)
        {
            string typeCode = string.Empty;
            Type valueType = null;
            object objectValue = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.ValueTextEquals(TYPE))
                    {
                        reader.Read();
                        typeCode = reader.GetString();
                        valueType = _binder.GetType(typeCode);
                    }
                    else if (reader.ValueTextEquals(VALUE))
                    {
                        reader.Read();
                        if (valueType == null) objectValue = null;
                        else if (valueType == typeof(bool)) objectValue = reader.GetBoolean();
                        else if (valueType == typeof(string)) objectValue = reader.GetString();
                        else if (valueType == typeof(decimal)) objectValue = reader.GetDecimal();
                        else if (valueType == typeof(DateTime)) objectValue = reader.GetDateTime();
                        else if (valueType == typeof(EnumRef)) objectValue = reader.GetString();
                        else if (valueType == typeof(CatalogRef)) objectValue = reader.GetGuid();
                        else if (valueType == typeof(DocumentRef)) objectValue = reader.GetGuid();
                    }
                }
                else if (reader.TokenType == JsonTokenType.EndObject) break;
            }

            if (valueType == typeof(EnumRef))
            {
                return new EnumRef()
                {
                    Name = typeCode.Replace("jcfg:EnumRef.", string.Empty),
                    Value = (string)objectValue
                };
            }
            else if (valueType == typeof(CatalogRef))
            {
                return new CatalogRef()
                {
                    Name = typeCode.Replace("jcfg:CatalogRef.", string.Empty),
                    Value = (Guid)objectValue
                };
            }
            else if (valueType == typeof(DocumentRef))
            {
                return new DocumentRef()
                {
                    Name = typeCode.Replace("jcfg:DocumentRef.", string.Empty),
                    Value = (Guid)objectValue
                };
            }

            return objectValue;
        }
        
        private object ReadRecordSet(ref Utf8JsonReader reader, Type recordType, string typeFullName)
        {
            RecordSet recordSet = new RecordSet()
            {
                TypeName = typeFullName
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    if (propertyName == "Filter")
                    {
                        ReadRecordSetFilter(ref reader, recordSet);
                    }
                    else if (propertyName == "Record")
                    {
                        ReadRecordSetRecords(ref reader, recordSet, recordType);
                    }
                }
            }
            return recordSet;
        }
        private void ReadRecordSetFilter(ref Utf8JsonReader reader, RecordSet recordSet)
        {
            string filterName = string.Empty;
            object filterValue = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    filterName = string.Empty;
                    filterValue = null;
                }
                else if (reader.TokenType == JsonTokenType.EndObject)
                {
                    if (!string.IsNullOrWhiteSpace(filterName))
                    {
                        recordSet.Filter.TryAdd(filterName, filterValue);
                    }
                }
                else if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.ValueTextEquals("Name"))
                    {
                        filterName = (string)ReadObjectValue(ref reader);
                    }
                    else if (reader.ValueTextEquals("Value"))
                    {
                        filterValue = ReadObjectValue(ref reader);
                    }
                }
                else if (reader.TokenType == JsonTokenType.EndArray) break;
            }
        }
        private void ReadRecordSetRecords(ref Utf8JsonReader reader, RecordSet recordSet, Type recordType)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    object record = ReadRecordSetRecord(ref reader, recordSet, recordType);

                    recordSet.Records.Add(record);
                }
                else if (reader.TokenType == JsonTokenType.EndArray) break;
            }
        }
        private object ReadRecordSetRecord(ref Utf8JsonReader reader, RecordSet recordSet, Type recordType)
        {
            object record = Activator.CreateInstance(recordType);
            
            string propertyName = null;
            PropertyInfo propertyInfo = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    propertyInfo = recordType.GetProperty(propertyName);
                }
                else if (reader.TokenType == JsonTokenType.Null)
                {
                    propertyInfo.SetValue(record, null);
                }
                else if (reader.TokenType == JsonTokenType.True)
                {
                    propertyInfo.SetValue(record, true);
                }
                else if (reader.TokenType == JsonTokenType.False)
                {
                    propertyInfo.SetValue(record, false);
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    decimal numericValue = reader.GetDecimal();
                    propertyInfo.SetValue(record, numericValue);
                }
                else if (reader.TokenType == JsonTokenType.String)
                {
                    string stringValue = reader.GetString();
                    propertyInfo.SetValue(record, stringValue);
                }
                else if (reader.TokenType == JsonTokenType.StartObject)
                {
                    object objectValue = ReadObjectValue(ref reader);
                    propertyInfo.SetValue(record, objectValue);
                }
                else if (reader.TokenType == JsonTokenType.EndObject) break;
            }

            return record;
        }
    }
}