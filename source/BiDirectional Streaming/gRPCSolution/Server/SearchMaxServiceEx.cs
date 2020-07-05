using Grpc.Core;
using SearchMax;
using System.Threading.Tasks;
using static SearchMax.SearchMaxService;

namespace Grpc.Server
{
    public class SearchMaxServiceEx : SearchMaxServiceBase
    {
        public override async Task SearchMaxNumber(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            //read data from the request stream instance
            var max = 0;

            //read stream request
            while (await requestStream.MoveNext())
            {
                var input = requestStream.Current.Argument.Number;
                if(max == 0 || max < input)
                {
                    max = input;

                    //send data using stream response
                    await responseStream.WriteAsync(new Response { Result = new NumberData { Number = max } });
                }
            }
        }
    }
}
