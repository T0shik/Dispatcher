using Core.Infrastructure;
using Dispatcher;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Infrastructure
{
    public class BasePage : PageModel
    {
        private IDispatcher<Request, Response> _dispatcher;

        protected IDispatcher<Request, Response> Dispatcher => _dispatcher ??
            (_dispatcher = HttpContext.RequestServices.GetService<IDispatcher<Request, Response>>());
    }
}
