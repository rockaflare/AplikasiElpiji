using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Splat;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Siapel.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } = new RoutingState();
        private readonly IDataService<Harga> _hargaService;
        private readonly IDataService<Pemasukan> _pemasukanService;
        private readonly IPangkalanDataService _pangkalanService;
        private readonly ITransaksiDataService _transaksiService;
        public MainWindowViewModel(IDataService<Harga> hargaService, IDataService<Pemasukan> pemasukanService, IPangkalanDataService pangkalanDataService, ITransaksiDataService transaksiDataService)
        {
            _hargaService = hargaService;
            _pemasukanService = pemasukanService;
            _pangkalanService = pangkalanDataService;
            _transaksiService = transaksiDataService;
        }


        public object SelectedPage
        {
            get => _selectedCategory;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedCategory, value);
                SetCurrentPage();
            }
        }
        private object _selectedCategory;
        
        private void SetCurrentPage()
        {
            if (SelectedPage is NavigationViewItem nvi)
            {
                switch (nvi.Tag)
                {
                    case "Home":
                        ShowHome();
                        break;
                    case "Harga":
                        ShowHarga();
                        break;
                    case "Pangkalan":
                        ShowPangkalan();
                        break;
                    case "Pemasukan":
                        ShowPemasukan();
                        break;
                    case "Transaksi":
                        ShowTransaksi();
                        break;
                    case "Laporan":
                        ShowLaporan();
                        break;
                    case "Master":
                        break;
                    default:
                        ShowDefaultPage();
                        break;
                }
            }
        }

        

        private void ShowHome()
        {
            Router.Navigate.Execute(new HomeViewModel(this));
        }
        private void ShowHarga()
        {
            Router.Navigate.Execute(new HargaViewModel(this, _hargaService, _pangkalanService));
        }
        private void ShowPangkalan()
        {
            Router.Navigate.Execute(new PangkalanViewModel(this, _pangkalanService));
        }

        private void ShowTransaksi()
        {
            Router.Navigate.Execute(new TransaksiViewModel(this, _transaksiService, _pangkalanService, _hargaService));
        }
        private void ShowPemasukan()
        {
            Router.Navigate.Execute(new PemasukanViewModel(this, _pemasukanService));
        }
        private void ShowLaporan()
        {
            Router.Navigate.Execute(new LaporanViewModel(this, _transaksiService));
        }



        private void ShowDefaultPage()
        {
            Router.Navigate.Execute(new NotFoundPageDefaultViewModel(this));
        }

    }
}
