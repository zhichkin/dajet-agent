using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DaJet.ProtoBuf
{
    public sealed class UnionJsonConverter : JsonConverter<Union>
    {
        public override Union Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Union union = null;

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                union = new Union();
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                return union;
            }

            string propertyName = null;
            string propertyType = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    union = new Union();
                }
                else if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                }
                else if (reader.TokenType == JsonTokenType.Null)
                {
                    break;
                }
                else if (reader.TokenType == JsonTokenType.String)
                {
                    if (propertyName == "type")
                    {
                        propertyType = reader.GetString();
                    }
                    else if (propertyName == "value")
                    {
                        if (union != null)
                        {
                            if (propertyType == "Строка")
                            {
                                union.String = reader.GetString()!;
                            }
                        }
                    }
                }
                else if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
            }

            return union;
        }
        public override void Write(Utf8JsonWriter writer, Union value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}