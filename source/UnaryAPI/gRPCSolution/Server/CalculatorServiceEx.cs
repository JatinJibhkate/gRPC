using Calcultor;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Calcultor.CalcultorService;

namespace Grpc.Server
{
    public class CalculatorServiceEx : CalcultorServiceBase
    {
        public override Task<CalculateResponse> Add(CalculateRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalculateResponse { Result = request.Calculate.Arg1 + request.Calculate.Arg2 });
        }

        public override Task<CalculateResponse> Substract(CalculateRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalculateResponse { Result = request.Calculate.Arg1 - request.Calculate.Arg2 });
        }

        public override Task<CalculateResponse> Multiply(CalculateRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalculateResponse { Result = request.Calculate.Arg1 * request.Calculate.Arg2 });
        }

        public override Task<CalculateResponse> Division(CalculateRequest request, ServerCallContext context)
        {
            if(request.Calculate.Arg2 == 0)
            {
                throw new DivideByZeroException("Arg2 cannot zero");
            }
            return Task.FromResult(new CalculateResponse { Result = request.Calculate.Arg1 / request.Calculate.Arg2 });
        }
    }
}
