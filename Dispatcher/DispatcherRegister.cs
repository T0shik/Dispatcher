using Dispatcher;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DispatcherRegister
    {
        public static IServiceCollection AddDispatcher<TRequest, TResponse>(this IServiceCollection @this, Type[] pipes)
        {
            @this.AddTransient<ServiceFactory>(x => x.GetService);
            @this.AddTransient<IDispatcher<TRequest, TResponse>, Dispatcher<TRequest, TResponse>>();

            var assembly = typeof(TRequest).GetTypeInfo().Assembly;

            var serviceBase = typeof(IService<,>);

            @this.AddServices<TRequest, TResponse>(assembly, serviceBase);

            if (pipes != null && pipes.Count() > 0)
                foreach (var pipe in pipes)
                    @this.AddGlobalPipe<TRequest, TResponse>(pipe);

            return @this;
        }

        public static IServiceCollection AddGlobalPipe<TRequest, TResponse>(this IServiceCollection @this, Type pipeType)
        {
            var pipeBase = typeof(IGlobalPipe<,>);
            var assembly = typeof(TRequest).GetTypeInfo().Assembly;
            var pipe = assembly.DefinedTypes.FirstOrDefault(x => x.GetTypeInfo() == pipeType);
            var pipeInterface = pipe.GetInterfaces().FirstOrDefault(x => x.GetTypeInfo().GetGenericTypeDefinition() == pipeBase);

            @this.AddTransient(pipeInterface, pipe);

            return @this;
        }

        private static IServiceCollection AddServices<TRequest, TResponse>(this IServiceCollection @this, Assembly assembly, Type targetType)
        {
            var services = assembly.DefinedTypes
                .Where(x => !x.GetTypeInfo().IsAbstract && !x.GetTypeInfo().IsInterface)
                .Where(x => x.GetTypeInfo().ImplementedInterfaces.Count() > 0)
                .Where(x => x.GetTypeInfo().ImplementedInterfaces.FirstOrDefault().GetTypeInfo().GetGenericTypeDefinition() == targetType);

            foreach (var service in services)
            {
                var serviceInterface = service.GetTypeInfo().GetInterfaces()
                    .FirstOrDefault(type => type.GetTypeInfo().IsGenericType && (type.GetGenericTypeDefinition() == targetType));

                @this.AddTransient(serviceInterface, service);
            }

            return @this;
        }
    }
}
