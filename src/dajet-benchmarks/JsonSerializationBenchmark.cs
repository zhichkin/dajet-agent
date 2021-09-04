using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Microsoft.IO;
using System;
using System.Buffers;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DaJet.Benchmarks
{
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class JsonSerializationBenchmark
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

        private static RecyclableMemoryStreamManager StreamManager = new RecyclableMemoryStreamManager();

        [Params(1000, 10000, 100000)] public int TestSize;
        private string[] TestValues;

        [GlobalSetup] public void GlobalSetup()
        {
            TestValues = new string[1024];

            for (int i = 0; i < 1024; i++)
            {
                TestValues[i] = i.ToString().PadLeft(4);
            }
        }

        [Benchmark(Description = "MemoryStream")] public void SerializeWithMemoryStream()
        {
            for (int i = 0; i < TestSize; i++)
            {
                Serialize1();
            }
        }
        private void Serialize1()
        {
            JsonWriterOptions options = new JsonWriterOptions()
            {
                Indented = false,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            using (MemoryStream stream = new MemoryStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    foreach (string item in TestValues)
                    {
                        writer.WriteString(item, item);
                    }
                    writer.WriteEndObject();
                    writer.Flush();
                }
                //byte[] result = stream.ToArray();
            }
        }

        [Benchmark(Description = "ArrayPool")] public void SerializeWithArrayPool()
        {
            JsonWriterOptions options = new JsonWriterOptions()
            {
                Indented = false,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            byte[] buffer = ArrayPool<byte>.Shared.Rent(10240);

            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    for (int i = 0; i < TestSize; i++)
                    {
                        Serialize2(writer);
                        
                        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(buffer, 0, (int)writer.BytesCommitted);

                        writer.Reset();
                        stream.Position = 0;
                    }
                }
            }

            ArrayPool<byte>.Shared.Return(buffer);
        }
        private void Serialize2(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            foreach (string item in TestValues)
            {
                writer.WriteString(item, item);
            }
            writer.WriteEndObject();
            
            writer.Flush();
        }

        [Benchmark(Description = "RecyclableStream")] public void SerializeWithRecyclableStream()
        {
            JsonWriterOptions options = new JsonWriterOptions()
            {
                Indented = false,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            using (MemoryStream stream = StreamManager.GetStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    for (int i = 0; i < TestSize; i++)
                    {
                        Serialize2(writer);

                        ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(stream.GetBuffer(), 0, (int)writer.BytesCommitted);

                        writer.Reset();
                        stream.Position = 0;
                    }
                }
            }
        }
    }
}