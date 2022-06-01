using Autofac;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF;
using Siapel.EF.DataServices.Core;
using Siapel.UI.ViewModels;
using Siapel.UI.ViewModels.DialogViewModels;
using Siapel.UI.Views.Pages;
using Siapel.UI.Views.Pages.Dialogs;
using Splat;
using Splat.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.DependencyInjection
{
    public static class Bootstrapper
    {
        public static ContainerBuilder Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<HomeView>().As<IViewFor<HomeViewModel>>();
            builder.RegisterType<HargaView>().As<IViewFor<HargaViewModel>>();
            builder.RegisterType<PangkalanView>().As<IViewFor<PangkalanViewModel>>();
            builder.RegisterType<TransaksiView>().As<IViewFor<TransaksiViewModel>>();
            builder.RegisterType<LaporanView>().As<IViewFor<LaporanViewModel>>();
                        
            builder.RegisterType<MainWindowViewModel>().AsSelf();

            builder.RegisterType<SiapelDbContextFactory>().AsSelf();

            builder.RegisterType<AddPangkalan>().As<IViewFor<AddPangkalanViewModel>>();
            builder.RegisterType<HargaFieldDialog>().As<IViewFor<HargaFieldViewModel>>();
            builder.RegisterType<TransaksiFieldDialog>().As<IViewFor<TransaksiFieldViewModel>>();

            builder.Register((c, p) => new PangkalanDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<Pangkalan>>();
            builder.Register((c, p) => new PangkalanDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IPangkalanDataService>();
            builder.Register((c, p) => new TransaksiDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<ITransaksiDataService>();
            builder.Register((c, p) => new HargaDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<Harga>>();

            

            return builder;
        }
    }
}
