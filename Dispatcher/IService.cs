namespace Dispatcher
{
    public interface IService<TRequest, TResponse>
    {
        TResponse Do(TRequest request);
    }
}
