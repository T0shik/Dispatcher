using Core.Infrastructure;
using Dispatcher;

namespace Core.Services
{
    public class TestRequest : Request
    {
        public int Index { get; set; }
    }

    public class TestResponse : Response
    {
        public int Index { get; set; }
    }

    public class TestService : IService<TestRequest, TestResponse>
    {
        public TestResponse Do(TestRequest request)
        {
            return new TestResponse { Index = request.Index * request.RequestId };
        }
    }
}
