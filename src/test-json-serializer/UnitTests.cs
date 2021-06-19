using DaJet.Agent.MessageHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace test_json_serializer
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod1()
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