using Grpc.Core;
using SearchMax;
using System;
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
                if(task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected to the server successfully!");
                }
            });

            var client = new SearchMaxService.SearchMaxServiceClient(channel);
            var stream = client.SearchMaxNumber();

            //execute this when you are done
            var responseTask = Task.Run(async () => 
            {
                StringBuilder maxSeries = new StringBuilder();
                while(await stream.ResponseStream.MoveNext())
                {
                    maxSeries.AppendLine(stream.ResponseStream.Current.Result.Number.ToString());
                }
                Console.WriteLine($"Max numbers = {string.Join(",", maxSeries.ToString().Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries)) }");
            });

            //input numbers
            var inputCollection = new[] { 2, 5, 3, 6, 21, 23, 56, 35, 45, 34, 88, 123 };

            //send input stream
            foreach (var input in inputCollection)
            {
                await stream.RequestStream.WriteAsync(new Request { Argument = new NumberData { Number = input } });
            }

            //notify end of request stream
            await stream.RequestStream.CompleteAsync();

            await responseTask;

            await channel.ShutdownAsync();
            Console.ReadKey();
        }
    }
}
