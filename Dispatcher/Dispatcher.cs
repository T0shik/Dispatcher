using System.Linq;

namespace Dispatcher
{
    public class Dispatcher<BaseRequest, BaseResponse> : IDispatcher<BaseRequest, BaseResponse>
    {
        private ServiceFactory _serviceFactory;

        public Dispatcher(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public TResponse Send<TRequest, TResponse>(TRequest request)
            where TRequest : BaseRequest
            where TResponse : BaseResponse
        {
            var service = _serviceFactory.GetInstance<IService<TRequest, TResponse>>();

            var pipes = _serviceFactory.GetInstances<IGlobalPipe<BaseRequest, BaseResponse>>();

            TResponse handle() => service.Do(request);

            return pipes
                .Reverse()
                .Aggregate((ServiceHandler<TResponse>)handle, (next, pipe) => () => pipe.Process(request, next))();
        }
    }
}
