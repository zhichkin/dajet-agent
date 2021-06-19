using System;
using System.Collections.Generic;
using System.Linq;

namespace DaJet.Json
{
    public interface ISerializationBinder
    {
        Type GetType(string typeCode);
        string GetTypeCode(Type type);
        Dictionary<string, Type> KnownTypes { get; }
    }
    public sealed class JsonSerializationBinder : ISerializationBinder
    {
        public Dictionary<string, Type> KnownTypes { get; } = new Dictionary<string, Type>()
        {
            { "jxs:string", typeof(string) },
            { "jxs:decimal", typeof(decimal) },
            { "jxs:boolean", typeof(bool) },
            { "jxs:dateTime", typeof(DateTime) }
        };

        public JsonSerializationBinder() { }
        public Type GetType(string typeCode)
        {
            if (string.IsNullOrWhiteSpace(typeCode)) return null;

            if (typeCode.StartsWith("jcfg:EnumRef")) return typeof(EnumRef);
            else if (typeCode.StartsWith("jcfg:CatalogRef")) return typeof(CatalogRef);
            else if (typeCode.StartsWith("jcfg:DocumentRef")) return typeof(DocumentRef);

            return KnownTypes.SingleOrDefault(i => i.Key == typeCode).Value;
        }
        public string GetTypeCode(Type type)
        {
            return KnownTypes.SingleOrDefault(i => i.Value == type).Key;
        }
    }
}
public sealed class EnumRef
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
public sealed class CatalogRef
{
    public string Name { get; set; } = string.Empty;
    public Guid Value { get; set; } = Guid.Empty;
}
public sealed class DocumentRef
{
    public string Name { get; set; } = string.Empty;
    public Guid Value { get; set; } = Guid.Empty;
}
// jxs:string
// jxs:decimal
// jxs:boolean
// jxs:dateTime
// jcfg:EnumRef.ТестовоеПеречисление - строка
// jcfg:CatalogRef.ТестовыйСправочник
// jcfg:DocumentRef.ТестовыйДокумент
// jcfg:CatalogObject.ТестовыйСправочник
// jcfg:InformationRegisterRecordSet.ТестовыйРегистр