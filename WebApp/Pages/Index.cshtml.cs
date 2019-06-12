using Core.Services;
using WebApp.Infrastructure;

namespace WebApp.Pages
{
    public class IndexModel : BasePage
    {
        public TestResponse TestResponse { get; private set; }

        public void OnGet()
        {
            TestResponse = Dispatcher.Send<TestRequest, TestResponse>(new TestRequest { Index = 2 });
        }
    }
}