using Core.Infrastructure;
using Core.Pipe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDispatcher<Request, Response>(new[] {
                typeof(TestPipe1),
                typeof(TestPipe3),
                typeof(TestPipe2),
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}
