using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class TransaksiViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableCollection<Transaksi> _transaksi { get; } = new ObservableCollection<Transaksi>();
        private readonly ITransaksiDataService _dataService;
        public string? UrlPathSegment => "Transaksi";

        public IScreen HostScreen { get; }

        private ReactiveCommand<Unit, Unit> LoadItem { get; }

        public TransaksiViewModel(IScreen screen, ITransaksiDataService dataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            LoadItem = ReactiveCommand.CreateFromTask(TransaksiUpdater);
            LoadItem.Execute();
        }

        private async Task TransaksiUpdater()
        {
            _transaksi.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _transaksi.Add(item);
                }
            }
        }

        private Transaksi _selectedTransaksi = new Transaksi();
        public Transaksi SelectedTransaksi
        {
            get => _selectedTransaksi;
            set => this.RaiseAndSetIfChanged(ref _selectedTransaksi, value);
        }


    }
}
