using System;
using System.Collections.Generic;

namespace Dispatcher
{
    public delegate object ServiceFactory(Type serviceType);

    public static class ServiceFactoryExtensions
    {
        public static T GetInstance<T>(this ServiceFactory serviceFactory) =>
            (T)serviceFactory(typeof(T));

        public static IEnumerable<T> GetInstances<T>(this ServiceFactory serviceFactory) =>
            (IEnumerable<T>)serviceFactory(typeof(IEnumerable<T>));
    }
}