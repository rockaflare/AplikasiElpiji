using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF;
using Siapel.EF.DataServices.Core;
using Siapel.EF.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.DependencyInjection
{
    public class ServicesBootstrapper
    {
        public static void RegisterServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            RegisterCommonServices(services, resolver);
        }

        private static  void RegisterCommonServices(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
        {
            services.RegisterLazySingleton<IDataService<Pangkalan>>(() => new PangkalanDataService(resolver.GetRequiredService<SiapelDbContextFactory>()));
        }
    }
}
