using ComputeAverage;
using Grpc.Core;
using Grpc.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcServer
{
    class Program
    {
        const int serverPort = 50501;
        static void Main(string[] args)
        {
            //from Grpc.Core
            Server server = null;
            try
            {
                //Grpc server instance with insecure connection on localhost
                //insecure as out client is also going to run on same machine
                //this is just for a demo purpose and real practice it will replaced by 
                //meaningful entity
                server = new Server
                {
                    Services = { ComputeAverageService.BindService(new ComputeAverageServiceEx())},
                    Ports = { new ServerPort("localhost", serverPort, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine($"The server is actively listening on port {serverPort}...");
                Console.ReadKey();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Unable to start a server!\r\nMore details - {ex.Message}.");
            }
            finally
            {
                server?.ShutdownAsync().Wait();
            }
        }
    }
}
