using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaJet.Benchmarks
{
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class SqlStreamingBenchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                //AddJob(Job.Dry.WithGcServer(true).WithGcForce(true).WithId("ServerForce"));
                AddJob(Job.Dry.WithGcServer(true).WithGcForce(false).WithId("Server"));
                //AddJob(Job.Dry.WithGcServer(false).WithGcForce(true).WithId("Workstation"));
                //AddJob(Job.Dry.WithGcServer(false).WithGcForce(false).WithId("WorkstationForce"));
            }
        }

        private const string ConnectionString = "Data Source=zhichkin;Initial Catalog=dajet_agent;Integrated Security=True";

        DatabaseMessageProducer producer = new DatabaseMessageProducer(ConnectionString);

        private List<byte[]> MessageMemoryStore;
        [Params(10)] public int MessageCount;
        
        private string TestMessage = new string('ё', 1024 * 1024);
        private byte[] TestMessageBytes;
        private string TestMessageUTF8;

        [GlobalSetup] public void GlobalSetup()
        {
            TestMessageBytes = Encoding.UTF8.GetBytes(TestMessage);
            TestMessageUTF8 = Encoding.UTF8.GetString(TestMessageBytes);

            MessageMemoryStore = new List<byte[]>(MessageCount);

            for (int i = 0; i < MessageCount; i++)
            {
                MessageMemoryStore.Add(Encoding.UTF8.GetBytes(TestMessage));
            }
        }
        [GlobalCleanup] public void GlobalCleanup()
        {
            Console.WriteLine("TestMessage size = " + Encoding.UTF8.GetBytes(TestMessage).Length.ToString() + " bytes.");
        }

        [Benchmark(Description = "Benchmark 0")] public void InsertMessage0()
        {
            for (int i = 0; i < MessageCount; i++)
            {
                producer.InsertMessage(TestMessageUTF8);
            }
        }
        [Benchmark(Description = "Benchmark 1")] public void InsertMessage1()
        {
            ReadOnlyMemory<byte> message;

            for (int i = 0; i < MessageCount; i++)
            {
                message = MessageMemoryStore[i];

                byte[] body = message.ToArray();

                string json = Encoding.UTF8.GetString(body);

                producer.InsertMessage(json);
            }
        }
        [Benchmark(Description = "Benchmark 2")] public void InsertMessage2()
        {
            ReadOnlyMemory<byte> message;

            for (int i = 0; i < MessageCount; i++)
            {
                message = MessageMemoryStore[i];

                string json = Encoding.UTF8.GetString(message.Span);

                producer.InsertMessage(json);
            }
        }
        [Benchmark(Description = "Benchmark 3")] public void InsertMessage3()
        {
            ReadOnlySpan<byte> span;

            for (int i = 0; i < MessageCount; i++)
            {
                span = MessageMemoryStore[i];
                producer.InsertMessage(Encoding.UTF8.GetString(span));
            }
        }
        [Benchmark(Description = "Benchmark 4")] public void InsertMessage4()
        {
            for (int i = 0; i < MessageCount; i++)
            {
                producer.InsertMessage(MessageMemoryStore[i]);
            }
        }
    }
}