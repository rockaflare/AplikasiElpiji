using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;

namespace Siapel.UI.DependencyInjection
{
    public static class ReadonlyDependencyResolverExtensions
    {
        public static TService GetRequiredService<TService>(this IReadonlyDependencyResolver resolver)
        {
            var service = resolver.GetService<TService>();
            if (service is null)
            {
                throw new InvalidOperationException($"Failed to resolve object type{typeof(TService)}");
            }
            return service;
        }

        public static object GetRequiredService(this IReadonlyDependencyResolver resolver, Type type)
        {
            var service = resolver.GetService(type);
            if (service is null)
            {
                throw new InvalidOperationException($"Failed to resolve object type {type}");
            }
            return service;
        }
    }
}
