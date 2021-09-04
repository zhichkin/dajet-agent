using DaJet.Agent.MessageHandlers;
using Microsoft.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace test_json_serializer
{
    [TestClass] public class UnitTests
    {
        [TestMethod] public void TestMethod1()
        {
            string filePath = @"C:\Users\User\Desktop\GitHub\dajet-agent\src\json1.json";
            string json = File.ReadAllText(filePath);

            MessageJsonSerializer serializer = new MessageJsonSerializer();
            var knownTypes = serializer.Binder.KnownTypes;
            knownTypes.Add("jcfg:InformationRegisterRecordSet.Ўтрихкоды”паковок«аказов лиентов", typeof(Ўтрихкоды”паковок«аказов лиентов));

            object message = null;
            try
            {
                message = serializer.FromJson(json);
            }
            catch { throw; }

            if (!(message is RecordSet recordSet)) return;

            Console.WriteLine(recordSet.TypeName);

            string logMessage = string.Empty;

            foreach (var item in recordSet.Records)
            {
                if (!(item is Ўтрихкоды”паковок«аказов лиентов record)) break;

                logMessage += record.«аказ лиента + ","
                    + record.Ўтрихкод”паковки + ","
                    + record.Ўтрихкод¬ложенной”паковки
                    + Environment.NewLine;
            }

            Console.WriteLine(logMessage);

            filePath = @"C:\Users\User\Desktop\GitHub\dajet-agent\src\json1.csv";
            using (StreamWriter writer = File.CreateText(filePath))
            {
                writer.Write(logMessage);
            }
        }

        [TestMethod] public void TestArrayPool()
        {
            List<string> values = new List<string>() { "ABCD", "A", "AB", "ABC" };

            JsonWriterOptions options = new JsonWriterOptions()
            {
                Indented = false,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            byte[] buffer = ArrayPool<byte>.Shared.Rent(100);

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    foreach (string value in values)
                    {
                        SerializeToJson(writer, value);
                        
                        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(buffer, 0, (int)writer.BytesCommitted);
                        
                        Console.WriteLine(Encoding.UTF8.GetString(span) + " = " + writer.BytesCommitted.ToString() + " bytes");

                        writer.Reset();
                        stream.Position = 0;
                    }
                }
            }

            ArrayPool<byte>.Shared.Return(buffer);
        }
        private void SerializeToJson(Utf8JsonWriter writer, string value)
        {
            writer.WriteStartObject();
            writer.WriteString(value, value);
            writer.WriteEndObject();
            writer.Flush();
        }


        private static RecyclableMemoryStreamManager StreamManager = new RecyclableMemoryStreamManager();
        [TestMethod] public void TestRecyclableStream()
        {
            List<string> values = new List<string>() { "ABCD", "A", "AB", "ABC" };

            JsonWriterOptions options = new JsonWriterOptions()
            {
                Indented = false,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            using (MemoryStream stream = StreamManager.GetStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    foreach (string value in values)
                    {
                        SerializeToJson(writer, value);

                        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(stream.GetBuffer(), 0, (int)writer.BytesCommitted);

                        Console.WriteLine(Encoding.UTF8.GetString(span) + " = " + writer.BytesCommitted.ToString() + " bytes");

                        writer.Reset();
                        stream.Position = 0;
                    }
                }
            }
        }
    }
}
//public Module()
//{
//    _serializer = new OneCSharpJsonSerializer();
//    var knownTypes = _serializer.Binder.KnownTypes;
//    knownTypes.Add(1, typeof(Language));
//    knownTypes.Add(2, typeof(Namespace));
//}
//public void Persist(Entity model)
//{
//    string json = _serializer.ToJson(model);

//    string filePath = GetModuleFilePath();
//    using (StreamWriter writer = File.CreateText(filePath))
//    {
//        writer.Write(json);
//    }
//}
//private void ReadModuleFromFile()
//{
//    string filePath = GetModuleFilePath();
//    string json = File.ReadAllText(filePath);
//    if (string.IsNullOrWhiteSpace(json)) return;

//    Entity entity = _serializer.FromJson(json);
//    BuildTreeView(entity);
//}