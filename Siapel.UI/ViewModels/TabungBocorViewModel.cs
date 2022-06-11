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
    public class TabungBocorViewModel : ReactiveObject, IRoutableViewModel
    {
        private IDataService<TabungBocor> _dataService;
        private IDataService<TransaksiLog> _transaksiLogService;
        private IDataService<StokAwal> _stokAwalService;
        private ObservableCollection<TabungBocor> _tabungBocor { get; } = new ObservableCollection<TabungBocor>();
        private List<StokAwal> _stokAwalList { get; } = new List<StokAwal>();
        private List<TransaksiLog> _transaksiLogList { get; } = new List<TransaksiLog>();
        public string? UrlPathSegment => "Tabung Bocor";
        public IScreen HostScreen { get; }
        public IEnumerable<TabungBocor> TabungBocor => _tabungBocor;

        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> DeleteItem { get; }

        public TabungBocorViewModel(IScreen screen, IDataService<TabungBocor> dataService, IDataService<TransaksiLog> transaksiLogService, IDataService<StokAwal> stokAwalService)
        {
            HostScreen = screen;
            _dataService = dataService;
            _stokAwalService = stokAwalService;
            _transaksiLogService = transaksiLogService;
            LoadItem = ReactiveCommand.CreateFromTask(TabungBocorUpdater);
            LoadItem.Execute();
        }

        private async Task TabungBocorUpdater()
        {
            _tabungBocor.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _tabungBocor.Add(item);
                }
            }
        }

        private TabungBocor _selectedTabungBocor;
        public TabungBocor SelectedTabungBocor
        {
            get => _selectedTabungBocor;
            set => this.RaiseAndSetIfChanged(ref _selectedTabungBocor, value);
        }

        public async void AddCommand()
        {
            var vm = new TabungBocorFieldViewModel(this.HostScreen, "Tambah Tabung Bocor");

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (TabungBocor)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);
                        //await UpdateTransaksiLog(model.Item, model.Jumlah, model.Tanggal, DateTime.UtcNow, 1);
                        
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new TabungBocorViewModel(this.HostScreen, _dataService, _transaksiLogService, _stokAwalService));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }
    }
}
