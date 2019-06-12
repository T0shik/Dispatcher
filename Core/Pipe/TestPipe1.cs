using Core.Infrastructure;
using Dispatcher;

namespace Core.Pipe
{
    public class TestPipe1 : IGlobalPipe<Request, Response>
    {
        public TResponse Process<TRequest, TResponse>(TRequest request, ServiceHandler<TResponse> next)
            where TRequest : Request
            where TResponse : Response
        {
            request.RequestId = 10;

            var response = next();

            response.Time = 599;

            return response;
        }
    }
}
