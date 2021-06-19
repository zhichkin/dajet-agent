using System;

namespace DaJet.Agent.MessageHandlers
{
    public sealed class ШтрихкодыУпаковокЗаказовКлиентовMessageHandler : IMessageHandler
    {
        MessageJsonSerializer _serializer = new MessageJsonSerializer();

        public ШтрихкодыУпаковокЗаказовКлиентовMessageHandler()
        {
            var knownTypes = _serializer.Binder.KnownTypes;
            knownTypes.Add("jcfg:InformationRegisterRecordSet.ШтрихкодыУпаковокЗаказовКлиентов", typeof(ШтрихкодыУпаковокЗаказовКлиентов));
        }

        public void ProcessMessage(string messageType, string messageBody)
        {
            if (messageBody == null || messageBody.Length > 100000) return;

            if (messageType != "РегистрСведений.ШтрихкодыУпаковокЗаказовКлиентов") return;

            object message = _serializer.FromJson(messageBody);

            if (!(message is RecordSet recordSet)) return;

            if (recordSet.TypeName != "jcfg:InformationRegisterRecordSet.ШтрихкодыУпаковокЗаказовКлиентов") return;

            string logMessage = string.Empty;

            foreach (var item in recordSet.Records)
            {
                if (!(item is ШтрихкодыУпаковокЗаказовКлиентов record)) break;

                logMessage += record.ЗаказКлиента + ","
                    + record.ШтрихкодУпаковки + ","
                    + record.ШтрихкодВложеннойУпаковки
                    + Environment.NewLine;
            }

            Console.WriteLine(logMessage);

            //filePath = @"C:\Users\User\Desktop\GitHub\dajet-agent\src\json1.csv";
            //using (StreamWriter writer = File.CreateText(filePath))
            //{
            //    writer.Write(logMessage);
            //}
        }
    }
}