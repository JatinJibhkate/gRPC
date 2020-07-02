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
        const string hostMachine = "localhost";
        const int port = 50501;


        static void Main(string[] args)
        {
            //from Grpc.Core
            Channel channel = new Channel(hostMachine, port, ChannelCredentials.Insecure);
            
            //this is for our undertanding, however, in reality we dont need to call connect
            //whenever we call any client call gRPC internals makes sure to create TCP channel first
            //before making further call
            channel.ConnectAsync().ContinueWith(task =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected to the server successfully!");
                }
            });

            //var client = new Sample.SampleService.SampleServiceClient(channel);
            var client = new Calcultor.CalcultorService.CalcultorServiceClient(channel);
            var request = new Calcultor.CalculateRequest
            {
                Calculate = new Calcultor.Calculate { Arg1 = 20, Arg2 = 10 }
            };

            //add call
            var result = client.Add(request);
            Console.WriteLine($"Add 20 + 10 = {result.Result}");

            //subtract call
            result = client.Substract(request);
            Console.WriteLine($"Substract 20 - 10 = {result.Result}");

            //multiply call
            result = client.Multiply(request);
            Console.WriteLine($"Multiply 20 * 10 = {result.Result}");

            //division call
            result = client.Division(request);
            Console.WriteLine($"Division 20 / 10 = {result.Result}");

            #region Divide by zero
            //error not yet handled gracefully and application will crash at this point
            //try
            //{
            //    request.Calculate.Arg2 = 0;
            //    //division with exception
            //    result = client.Division(request);
            //    Console.WriteLine($"Division 20 / 0 = {result.Result}");
            //}
            //catch(DivideByZeroException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            #endregion

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
