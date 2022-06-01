using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF.DataServices.Core;
using Siapel.UI.DependencyInjection;
using Siapel.UI.ViewModels;
using Siapel.UI.Views;
using Splat;
using Splat.Autofac;
using Autofac;
using Siapel.EF;
using Siapel.UI.Views.Pages;
using Siapel.UI.Views.Pages.Dialogs;
using Siapel.UI.ViewModels.DialogViewModels;
using Microsoft.EntityFrameworkCore;

namespace Siapel.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var builder = Bootstrapper.Register();
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
                    

                    var hargaservice = scope.Resolve<IDataService<Harga>>(new NamedParameter("contextFactory", new SiapelDbContextFactory()));
                    var pangkalanservice = scope.Resolve<IPangkalanDataService>(new NamedParameter("contextFactory", new SiapelDbContextFactory()));
                    var transaksiservice = scope.Resolve<ITransaksiDataService>(new NamedParameter("contextFactory", new SiapelDbContextFactory()));
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = new MainWindowViewModel(hargaservice, pangkalanservice, transaksiservice),
                    };
                    SiapelDbContextFactory contextFactory = scope.Resolve<SiapelDbContextFactory>();
                    using (SiapelDbContext context = contextFactory.CreateDbContext())
                    {
                        context.Database.Migrate();
                    }

                }
                autoFacResolver.SetLifetimeScope(container);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
