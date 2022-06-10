using FluentAvalonia.UI.Controls;
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
    public class PemasukanViewModel : ReactiveObject, IRoutableViewModel
    {//TODO
        private IDataService<Pemasukan> _dataService;
        private IDataService<TransaksiLog> _transaksiLogService;
        private IDataService<StokAwal> _stokAwalService;
        private ObservableCollection<Pemasukan> _pemasukan { get; } = new ObservableCollection<Pemasukan>();
        private List<StokAwal> _stokAwalList { get; } = new List<StokAwal>();
        private List<TransaksiLog> _transaksiLogList { get; } = new List<TransaksiLog>();
        public string? UrlPathSegment => "Pemasukan";

        public IScreen HostScreen { get; }
        public IEnumerable<Pemasukan> Pemasukan => _pemasukan;

        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> DeleteItem { get; }

        public PemasukanViewModel(IScreen screen, IDataService<Pemasukan> dataService, IDataService<TransaksiLog> transaksiLogService, IDataService<StokAwal> stokAwalService)
        {
            HostScreen = screen;
            _dataService = dataService;
            _stokAwalService = stokAwalService;
            _transaksiLogService = transaksiLogService;
            LoadItem = ReactiveCommand.CreateFromTask(PemasukanUpdater);
            LoadItem.Execute();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteConfirmation);
        }

        private async Task PemasukanUpdater()
        {
            _pemasukan.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _pemasukan.Add(item);
                }
            }
            PopulateStockSources();
        }

        private Pemasukan _selectedPemasukan;
        public Pemasukan SelectedPemasukan
        {
            get => _selectedPemasukan;
            set => this.RaiseAndSetIfChanged(ref _selectedPemasukan, value);
        }

        private async void DeleteItemAsync()
        {
            await _dataService.Delete(SelectedPemasukan);
            await UpdateTransaksiLog(SelectedPemasukan.Item, SelectedPemasukan.Jumlah, SelectedPemasukan.Tanggal, DateTime.UtcNow, 0);
            //await _transaksiLogService.Create(new TransaksiLog { Item = SelectedPemasukan.Item, SisaStok = GetLastStock(SelectedPemasukan.Item, SelectedPemasukan.Tanggal) - SelectedPemasukan.Jumlah, Tanggal = SelectedPemasukan.Tanggal, Created = DateTime.UtcNow });
            await LoadItem.Execute();
        }

        private async Task DeleteConfirmation()
        {
            var dialog = new ContentDialog()
            {
                Title = "Hapus item",
                Content = "Anda yakin ingin menghapus?",
                PrimaryButtonText = "Ok"
            };

            if (SelectedPemasukan != null)
            {
                dialog.CloseButtonText = "Batal";
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    DeleteItemAsync();
                }
            }
            else
            {
                dialog.Content = "Tidak ada item dipilih";
                await dialog.ShowAsync();
            }
        }
        private int? GetLastStock(string item, DateTimeOffset? tanggal)
        {
            int? resultStock = 0;
            if (_transaksiLogList.Count > 0)
            {
                var transaksiResult = _transaksiLogList.Where(x => x.Tanggal == tanggal && x.Item == item).OrderByDescending(x => x.Created).Select(x => x.SisaStok).FirstOrDefault();
                if (transaksiResult != null && transaksiResult > 0)
                {
                    resultStock = transaksiResult;
                }
                else
                {
                    resultStock = _transaksiLogList.Where(x => x.Tanggal == tanggal?.AddDays(-1) && x.Item == item).OrderByDescending(x => x.Created).Select(x => x.SisaStok).FirstOrDefault();
                }
            }
            else
            {
                resultStock = _stokAwalList.First(x => x.Item == item).Jumlah;
            }
            return resultStock;
        }
        private async void PopulateStockSources()
        {
            _stokAwalList.Clear();
            _transaksiLogList.Clear();
            var stoklist = await _stokAwalService.GetAll();
            foreach (var item in stoklist)
            {
                _stokAwalList.Add(item);
            }

            var transloglist = await _transaksiLogService.GetAll();
            foreach (var item in transloglist)
            {
                _transaksiLogList.Add(item);
            }
        }
        private async Task UpdateTransaksiLog(string item, int? sisastok, DateTimeOffset? tanggal, DateTime createdat, int operationType)
        {
            int? calculatedStok = 0;
            var transaksiLogBetween = _transaksiLogList.Where(x => x.Item == item && x.Tanggal >= tanggal).GroupBy(x => x.Tanggal).Select(t => new TransaksiLog()
            {
                Item = t.Select(x => x.Item).First(),
                Tanggal = t.Select(x => x.Tanggal).First()
            }).ToList();
            if (transaksiLogBetween.Count > 0)
            {
                foreach (var tab in transaksiLogBetween)
                {
                    switch (operationType)
                    {
                        case 0:
                            calculatedStok = GetLastStock(item, tab.Tanggal) - sisastok;
                            break;
                        case 1:
                            calculatedStok = GetLastStock(item, tab.Tanggal) + sisastok;
                            break;
                        default:
                            break;
                    }
                    await _transaksiLogService.Create(new TransaksiLog { Item = item, SisaStok = calculatedStok, Tanggal = tab.Tanggal, Created = createdat });
                }
            }
            else
            {
                switch (operationType)
                {
                    case 0:
                        calculatedStok = GetLastStock(item, tanggal) - sisastok;
                        break;
                    case 1:
                        calculatedStok = GetLastStock(item, tanggal) + sisastok;
                        break;
                    default:
                        break;
                }
                await _transaksiLogService.Create(new TransaksiLog { Item = item, SisaStok = calculatedStok, Tanggal = tanggal, Created = createdat });
            }
        }
        public async void AddCommand()
        {
            var vm = new PemasukanFieldViewModel(this.HostScreen, "Tambah Pemasukan");

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (Pemasukan)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);
                        await UpdateTransaksiLog(model.Item, model.Jumlah, model.Tanggal, DateTime.UtcNow, 1);
                        //await _transaksiLogService.Create(new TransaksiLog { Item = model.Item, SisaStok = GetLastStock(model.Item, model.Tanggal) + model.Jumlah, Tanggal = model.Tanggal, Created = DateTime.UtcNow});
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new PemasukanViewModel(this.HostScreen, _dataService, _transaksiLogService, _stokAwalService));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }
    }
}
