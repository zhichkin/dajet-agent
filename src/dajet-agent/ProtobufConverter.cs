using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace DaJet.ProtoBuf
{
    internal static class ProtobufConverter
    {
        internal static byte[] ConvertJsonToProtobuf(Assembly assembly, in string messageType, in string messageBody)
        {
            return Array.Empty<byte>();

            //Type type = assembly.GetType($"EntityModel.{messageType}");

            //if (type == null)
            //{
            //    return null;
            //}

            //JsonSerializerOptions options = new JsonSerializerOptions()
            //{
            //    Converters = { new UnionJsonConverter() }
            //};

            //object entity = JsonSerializer.Deserialize(messageBody, type, options);

            //using (MemoryStream memory = new MemoryStream())
            //{
            //    Serializer.Serialize(memory, entity);

            //    //string outputFile = "C:\\test\\message.bin";
            //    //using (Stream writer = File.Create(outputFile))
            //    //{
            //    //    writer.Write(memory.ToArray());
            //    //}

            //    return memory.ToArray();
            //}
        }
        internal static void ReadProtobufFromFile()
        {
            //string filePath = "C:\\temp\\proto\\json\\message.bin";
            //object entity;
            //using (FileStream file = File.OpenRead(filePath))
            //{
            //    // typeof(РегистрСведений.ИсторияСтатусовЗаказовКлиентов)
            //    string typeName = "РегистрСведений.ИсторияСтатусовЗаказовКлиентов";
            //    Type type = Type.GetType(typeName)!;
            //    if (type == null)
            //    {
            //        Console.WriteLine($"Type \"{typeName}\" is not found.");
            //        return;
            //    }

            //    entity = Serializer.Deserialize(type, file);
            //}

            //if (entity is РегистрСведений.ИсторияСтатусовЗаказовКлиентов)
            //{
            //    Console.WriteLine(entity);
            //}
        }
    }
}