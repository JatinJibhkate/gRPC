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
        
        static void Main(string[] args)
        {
            //from Grpc.Core
            Channel channel = new Channel(serverAddress, ChannelCredentials.Insecure);
            channel.ConnectAsync().ContinueWith(task =>
            {
                if(task.Status == TaskStatus.RanToCompletion ||
                   task.Status == TaskStatus.Canceled)
                {
                    Console.WriteLine("The client connected to the server successfully!");
                }
            });

            var client = new Sample.SampleService.SampleServiceClient(channel);
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
