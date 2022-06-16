using Autofac;
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

            builder.RegisterType<PageNotFoundDefault>().As<IViewFor<NotFoundPageDefaultViewModel>>();
            builder.RegisterType<HomeView>().As<IViewFor<HomeViewModel>>();
            builder.RegisterType<HargaView>().As<IViewFor<HargaViewModel>>();
            builder.RegisterType<StokAwalView>().As<IViewFor<StokAwalViewModel>>();
            builder.RegisterType<PangkalanView>().As<IViewFor<PangkalanViewModel>>();
            builder.RegisterType<TransaksiView>().As<IViewFor<TransaksiViewModel>>();
            builder.RegisterType<PemasukanView>().As<IViewFor<PemasukanViewModel>>();
            builder.RegisterType<LaporanView>().As<IViewFor<LaporanViewModel>>();
            builder.RegisterType<InOutView>().As<IViewFor<InOutViewModel>>();
            builder.RegisterType<TabungBocorView>().As<IViewFor<TabungBocorViewModel>>();

            builder.RegisterType<MainWindowViewModel>().AsSelf();

            builder.RegisterType<SiapelDbContextFactory>().AsSelf();

            builder.RegisterType<AddPangkalan>().As<IViewFor<AddPangkalanViewModel>>();
            builder.RegisterType<HargaFieldDialog>().As<IViewFor<HargaFieldViewModel>>();
            builder.RegisterType<TransaksiFieldDialog>().As<IViewFor<TransaksiFieldViewModel>>();
            builder.RegisterType<PemasukanFieldDialog>().As<IViewFor<PemasukanFieldViewModel>>();
            builder.RegisterType<StokAwalFieldDialog>().As<IViewFor<StokAwalFieldViewModel>>();
            builder.RegisterType<TabungBocorFieldDialog>().As<IViewFor<TabungBocorFieldViewModel>>();


            builder.Register((c, p) => new PangkalanDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IPangkalanDataService>();
            builder.Register((c, p) => new TransaksiDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<ITransaksiDataService>();
            builder.Register((c, p) => new HargaDataService(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<Harga>>();
            builder.Register((c, p) => new GenericDataService<Pemasukan>(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<Pemasukan>>();
            builder.Register((c, p) => new GenericDataService<StokAwal>(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<StokAwal>>();
            builder.Register((c, p) => new GenericDataService<TabungBocor>(p.Named<SiapelDbContextFactory>("contextFactory"))).As<IDataService<TabungBocor>>();



            return builder;
        }
    }
}
