using Grpc.Core;
using Primenumberdecomposition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        const string serverAddress = "127.0.0.1:50501";
        
        static async Task Main(string[] args)
        {
            //from Grpc.Core
            Channel channel = new Channel(serverAddress, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith(task =>
            {
                if(task.Status == TaskStatus.RanToCompletion ||
                   task.Status == TaskStatus.Canceled)
                {
                    Console.WriteLine("The client connected to the server successfully!");
                }
            });

            int inputNumber = 200;

            //create an instance of number service stub client
            var client = new NumberService.NumberServiceClient(channel);

            //call the prime number decomposition
            var response = client.GetPrimeNumberDecomposition(new Request { NumberData = new NumberData { Number = inputNumber } });

            StringBuilder builder = new StringBuilder();
            while (await response.ResponseStream.MoveNext())
            {
                builder.Append(response.ResponseStream.Current.NumberData.Number.ToString() + "*");
            }

            Console.WriteLine($"Prime number decomposition for number { inputNumber } = { builder.ToString().TrimEnd('*') }");

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
