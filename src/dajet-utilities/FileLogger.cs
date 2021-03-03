using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace DaJet.Utilities
{
    public static class FileLogger
    {
        private static string _filePath;
        private static string GetFilePath()
        {
            if (_filePath != null)
            {
                return _filePath;
            }

            Assembly asm = Assembly.GetExecutingAssembly();
            string appCatalogPath = Path.GetDirectoryName(asm.Location);
            _filePath = Path.Combine(appCatalogPath, "dajet-rabbitmq-producer.log");

            return _filePath;
        }
        public static int LogSize { get; set; }
        public static void Log(string text)
        {
            string filePath = GetFilePath();
            FileInfo file = new FileInfo(filePath);

            try
            {
                if (file.Exists && file.Length > LogSize)
                {
                    file.Delete();
                }
            }
            catch (Exception ex)
            {
                text += Environment.NewLine + ExceptionHelper.GetErrorText(ex);
            }

            using (StreamWriter writer = new StreamWriter(GetFilePath(), true, Encoding.UTF8))
            {
                writer.WriteLine("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text);
            }
        }
    }
}