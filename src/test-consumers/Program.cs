using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace test_consumers
{
    class Program
    {
        static void Main(string[] args)
        {
            Consumer consumer = new Consumer();
            consumer.StartConsuming();
            Console.WriteLine("Consumer is running ...");
            _ = Console.ReadLine();
        }
    }
}