using ComputeAverage;
using Grpc.Core;
using Grpc.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputeAverage.ComputeAverageService;

namespace Grpc.Server
{
    public class ComputeAverageServiceEx : ComputeAverageServiceBase
    {
        public async override Task<Response> ComputeAverageCalculation(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            List<Request> input = await requestStream.ToListAsync();
            return new Response{Result = new NumberData { Number = input.Sum(x => x.Argument.Number) / input.Count } };
        }
    }
}
