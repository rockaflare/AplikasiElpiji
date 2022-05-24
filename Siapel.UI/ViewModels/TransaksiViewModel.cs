using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.UI.ViewModels.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class TransaksiViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableCollection<Transaksi> _transaksi { get; } = new ObservableCollection<Transaksi>();
        private readonly ITransaksiDataService _dataService;
        private readonly IPangkalanDataService _pangkalanService;
        private readonly IDataService<Harga> _hargaService;
        public string? UrlPathSegment => "Transaksi";

        public IScreen HostScreen { get; }
        public IEnumerable<Transaksi> Transaksi => _transaksi;

        private ReactiveCommand<Unit, Unit> LoadItem { get; }

        public TransaksiViewModel(IScreen screen, ITransaksiDataService dataService, IPangkalanDataService pangkalanDataService, IDataService<Harga> hargaDataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            _pangkalanService = pangkalanDataService;
            _hargaService = hargaDataService;
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

        public async void AddCommand()
        {
            var pangkalans = await _pangkalanService.GetAll();
            var hargas = await _hargaService.GetAll();
            var vm = new TransaksiFieldViewModel(this.HostScreen, "Transaksi Baru", new List<Pangkalan>(pangkalans), new List<Harga>(hargas));

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (Transaksi)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new TransaksiViewModel(this.HostScreen, _dataService, _pangkalanService, _hargaService));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }

        public async void UpdateCommand()
        {
            if (SelectedTransaksi != null)
            {
                var pangkalans = await _pangkalanService.GetAll();
                var hargas = await _hargaService.GetAll();
                var vm = new TransaksiFieldViewModel(this.HostScreen, "Edit Transaksi", new List<Pangkalan>(pangkalans), new List<Harga>(hargas), SelectedTransaksi);

                Observable.Merge(
                    vm.Save,
                    vm.Cancel.Select(_ => (Transaksi)null))
                    .Take(1)
                    .Subscribe(async model =>
                    {
                        if (model != null)
                        {
                            await _dataService.Update(model);
                        }

                        await HostScreen.Router.NavigateAndReset.Execute(new TransaksiViewModel(this.HostScreen, _dataService, _pangkalanService, _hargaService));
                    });

                await HostScreen.Router.Navigate.Execute(vm);
            }
        }
    }
}
