using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF;
using Siapel.EF.DataServices.Core;
using Siapel.UI.DependencyInjection;
using Siapel.UI.ViewModels;
using Siapel.UI.ViewModels.DialogViewModels;
using Siapel.UI.Views;
using Siapel.UI.Views.Pages;
using Siapel.UI.Views.Pages.Dialogs;
using Splat;
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
            RegisterDependencies();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        { 
            Locator.CurrentMutable.Register(() => new HomeView(), typeof(IViewFor<HomeViewModel>));
            Locator.CurrentMutable.Register(() => new HargaView(), typeof(IViewFor<HargaViewModel>));
            Locator.CurrentMutable.Register(() => new PangkalanView(), typeof(IViewFor<PangkalanViewModel>));
            Locator.CurrentMutable.Register(() => new TransaksiView(), typeof(IViewFor<TransaksiViewModel>));
            Locator.CurrentMutable.Register(() => new AddPangkalan(), typeof(IViewFor<AddPangkalanViewModel>));

            //SplatRegistrations.RegisterLazySingleton<IDataService<Pangkalan>, PangkalanDataService>();
            //SplatRegistrations.SetupIOC();

            //Locator.CurrentMutable.RegisterLazySingleton(() => new PangkalanDataService(Locator.Current.GetRequiredService<SiapelDbContextFactory>()), typeof(IDataService<Pangkalan>));

            

            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }

        private static void RegisterDependencies() => Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);
    }
}
