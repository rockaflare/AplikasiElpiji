using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF;
using Siapel.EF.DataServices.Core;
using Siapel.EF.Services;
using Siapel.UI.ViewModels;
using Siapel.UI.ViewModels.DialogViewModels;
using Siapel.UI.Views;
using Siapel.UI.Views.Pages;
using Siapel.UI.Views.Pages.Dialogs;
using Splat;
using Splat.Autofac;
using System;

namespace Siapel.UI
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HomeView>().As<IViewFor<HomeViewModel>>();
            builder.RegisterType<HargaView>().As<IViewFor<HargaViewModel>>();
            builder.RegisterType<PangkalanView>().As<IViewFor<PangkalanViewModel>>();
            builder.RegisterType<TransaksiView>().As<IViewFor<TransaksiViewModel>>();
            builder.RegisterType<AddPangkalan>().As<IViewFor<AddPangkalanViewModel>>();

            builder.Register((c, p) => new PangkalanDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<Pangkalan>>();

            var autoFacResolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(autoFacResolver);

            
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();

            Locator.CurrentMutable.RegisterConstant(new AvaloniaActivationForViewFetcher(), typeof(IActivationForViewFetcher));
            Locator.CurrentMutable.RegisterConstant(new AutoDataTemplateBindingHook(), typeof(IPropertyBindingHook));
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                //var contextFactory = new SiapelDbContextFactory();
                var service = scope.Resolve<IDataService<Pangkalan>>(new NamedParameter("contextFactory", new SiapelDbContextFactory()));
            }
            autoFacResolver.SetLifetimeScope(container);

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
                //.UseReactiveUI();
        }
    }
}
