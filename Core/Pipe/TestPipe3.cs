using Core.Infrastructure;
using Dispatcher;

namespace Core.Pipe
{
    public class TestPipe3 : IGlobalPipe<Request, Response>
    {
        public TResponse Process<TRequest, TResponse>(TRequest request, ServiceHandler<TResponse> next)
            where TRequest : Request
            where TResponse : Response
        {
            request.RequestId = 2;

            var response = next();

            response.Time++;

            return response;
        }
    }
}
