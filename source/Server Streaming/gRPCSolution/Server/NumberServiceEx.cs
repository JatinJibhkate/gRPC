using Grpc.Core;
using Primenumberdecomposition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Primenumberdecomposition.NumberService;

namespace Grpc.Server
{
    public class NumberServiceEx : NumberServiceBase
    {
        public override async Task GetPrimeNumberDecomposition(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            //prime number decomposition algorithm
            int factor = 2;
            int number = request.NumberData.Number;
            while (number > 1)
            {
                // if factor evenly divides into number
                if (number % factor == 0)
                {

                    // this is a factor
                    await responseStream.WriteAsync(new Response
                    {
                        NumberData = new NumberData
                        {
                            Number = factor
                        }
                    });

                    // divide number by factor so that we have the rest of the number left.
                    number = number / factor;
                }
                else
                {
                    factor = factor + 1;
                }
            }
        }
    }
}
