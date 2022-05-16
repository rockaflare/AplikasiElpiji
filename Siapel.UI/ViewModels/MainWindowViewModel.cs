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
        private readonly IDataService<Pangkalan> _dataService;
        public MainWindowViewModel()
        {
            
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
                    case "Transaksi":
                        ShowTransaksi();
                        break;
                    default:
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
            Router.Navigate.Execute(new HargaViewModel(this));
        }
        private void ShowPangkalan()
        {
            Router.Navigate.Execute(new PangkalanViewModel(this, _dataService));
        }

        private void ShowTransaksi()
        {
            Router.Navigate.Execute(new TransaksiViewModel(this));
        }
    }
}
