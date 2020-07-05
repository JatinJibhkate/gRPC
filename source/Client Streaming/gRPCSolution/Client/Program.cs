using ComputeAverage;
using Grpc.Core;
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

            var inputStream = new[] { 1, 10, 30, 22, 4, 50, 123, 2343, 23 };
            var client = new ComputeAverageService.ComputeAverageServiceClient(channel);

            //get stream instance
            var stream = client.ComputeAverageCalculation();
            foreach (var input in inputStream)
            {
                //write data on request stream
                await stream.RequestStream.WriteAsync(new Request { Argument = new NumberData { Number = input } });
            }

            //signal request stream end
            await stream.RequestStream.CompleteAsync();

            //get the response
            var response = await stream.ResponseAsync;

            Console.WriteLine($"Compute average for input stream {string.Join(",", inputStream)} = {response.Result.Number}");

            await channel.ShutdownAsync();
            Console.ReadKey();
        }
    }
}
