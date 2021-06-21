using DaJet.Metadata;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DaJet.Agent.MessageHandlers
{
    public static class ШтрихкодыУпаковокЗаказовКлиентовLogger
    {
        private static string _filePath;
        private static object _syncLog = new object();
        private static string GetFilePath(string direction)
        {
            if (_filePath != null)
            {
                return _filePath;
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            string catalogPath = Path.GetDirectoryName(assembly.Location);
            _filePath = Path.Combine(catalogPath, "ШтрихкодыУпаковокЗаказовКлиентов_" + direction + ".csv");

            return _filePath;
        }
        public static int LogSize { get; set; } = 1073741824; // 1 Gb
        public static void Log(string direction, string text)
        {
            lock (_syncLog)
            {
                LogSyncronized(direction, text);
            }
        }
        private static void LogSyncronized(string direction, string text)
        {
            string filePath = GetFilePath(direction);
            FileInfo file = new FileInfo(filePath);

            try
            {
                if (file.Exists && file.Length > LogSize)
                {
                    return; //file.Delete();
                }
            }
            catch (Exception ex)
            {
                text += Environment.NewLine + ExceptionHelper.GetErrorText(ex);
            }

            using (StreamWriter writer = new StreamWriter(GetFilePath(direction), true, Encoding.UTF8))
            {
                writer.WriteLine("{0},{1}", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), text);
            }
        }
    }
}