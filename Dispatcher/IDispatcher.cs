namespace Dispatcher
{
    public interface IDispatcher<BaseRequest, BaseResponse>
    {
        TResponse Send<TRequest, TResponse>(TRequest request)
            where TRequest : BaseRequest
            where TResponse : BaseResponse;
    }
}
