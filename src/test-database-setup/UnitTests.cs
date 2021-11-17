using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Text;

namespace Test.DaJet.Database.Setup
{
    [TestClass] public class UnitTests
    {
        [TestMethod] public void RunExeFile()
        {
            const string ExeFilePath = @"C:\Users\User\Desktop\GitHub\dajet-agent\src\dajet-database-setup\bin\Debug\netcoreapp3.1\dajet-dbsetup.exe";

            const string ExeFileArgs = "--ms zhichkin --db cerberus";

            //const string ExeFileArgs = "--ms zhichkin --db test_node_1";
            //const string ExeFileArgs = "--pg 127.0.0.1 --db test_node_2 --usr postgres --pwd postgres";

            Console.WriteLine();
            Console.WriteLine(ExeFileArgs);
            Console.WriteLine();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            RunExeInternal(ExeFilePath, ExeFileArgs);
            watch.Stop();
            Console.WriteLine();
            Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds} ms");
        }

        public void RunExeInternal(string filename, string arguments = null)
        {
            Process process = new Process();

            process.StartInfo.FileName = filename;

            if (!string.IsNullOrEmpty(arguments))
            {
                process.StartInfo.Arguments = arguments;
            }

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            var stdError = new StringBuilder();
            var stdOutput = new StringBuilder();
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.ErrorDataReceived += (sender, args) => { stdError.AppendLine(args.Data); };
            process.OutputDataReceived += (sender, args) => { stdOutput.AppendLine(args.Data); };
            
            try
            {
                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing " + Format(filename, arguments) + ": " + e.Message, e);
            }

            Console.WriteLine($"Exit code = {process.ExitCode}");
            Console.WriteLine($"StdErr: {stdError}");
            Console.WriteLine($"StdOut: {stdOutput}");
        }

        private string Format(string filename, string arguments)
        {
            return "'" + filename +
                ((string.IsNullOrEmpty(arguments)) ? string.Empty : " " + arguments) +
                "'";
        }
    }
}