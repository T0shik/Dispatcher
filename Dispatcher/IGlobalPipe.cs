namespace Dispatcher
{
    public delegate TResponse ServiceHandler<TResponse>();

    public interface IGlobalPipe<BaseRequest, BaseResponse>
    {
        TResponse Process<TRequest, TResponse>(TRequest request, ServiceHandler<TResponse> next)
            where TRequest : BaseRequest
            where TResponse : BaseResponse;
    }
}
