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
    public class TransaksiViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableCollection<Transaksi> _transaksi { get; } = new ObservableCollection<Transaksi>();
        private ObservableCollection<Transaksi> _transaksiFilter { get; } = new ObservableCollection<Transaksi>();
        private readonly ITransaksiDataService _dataService;
        private readonly IPangkalanDataService _pangkalanService;
        private readonly IDataService<Harga> _hargaService;
        private List<string> _jenisPembayaran;
        private List<string> _jenisTabung;
        private List<Pangkalan> _listPangkalan;
        public string? UrlPathSegment => "Transaksi";

        public IScreen HostScreen { get; }
        public IEnumerable<Transaksi> Transaksi => _transaksi;
        public IEnumerable<Transaksi> TransaksiFilter => _transaksiFilter;
        public List<Pangkalan> Pangkalans => _listPangkalan;
        public List<string> JenisPembayaranList => _jenisPembayaran;
        public List<string> JenisItemList => _jenisTabung;
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> DeleteItem { get; }
        private ReactiveCommand<Unit, Unit> PangkalanCbx { get; }
        private ReactiveCommand<Unit, Unit> FilterTransaksi { get; }

        public TransaksiViewModel(IScreen screen, ITransaksiDataService dataService, IPangkalanDataService pangkalanDataService, IDataService<Harga> hargaDataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            _pangkalanService = pangkalanDataService;
            _hargaService = hargaDataService;
            _jenisPembayaran = new List<string>() { "Tunai", "Transfer", "Invoice" };
            _jenisTabung = new List<string>() { "50 KG", "12 KG", "5,5 KG" };
            PangkalanCbx = ReactiveCommand.CreateFromTask(GetPangkalan);
            PangkalanCbx.Execute();
            LoadItem = ReactiveCommand.CreateFromTask(TransaksiUpdater);
            LoadItem.Execute();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteConfirmation);
            FilterTransaksi = ReactiveCommand.Create(TransaksiFilterUpdater);

            this.WhenAnyValue(x => x.SelectedPangkalanFilter, x => x.SelectedItemFilter, x => x.StartDate, x => x.EndDate, x => x.SelectedPembayaranFilter).Select(_ => Unit.Default).InvokeCommand(FilterTransaksi);
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

        private void TransaksiFilterUpdater()
        {
            var filtered = Transaksi.Where(
                x => (SelectedPangkalanFilter == null ? true : x.Pangkalan.Id == SelectedPangkalanFilter.Id)
                && (SelectedItemFilter == null ? true : x.Item == SelectedItemFilter)
                && (StartDate == null ? true : x.Tanggal >= StartDate)
                && (EndDate == null ? true : x.Tanggal <= EndDate)
                && (SelectedPembayaranFilter == null ? true : x.JenisBayar == SelectedPembayaranFilter)
                ).OrderByDescending(x => x.Tanggal).ToList();
            var unfiltered = _transaksi.OrderByDescending(x => x.Tanggal);            
            if (filtered.Count > 0)
            {
                _transaksiFilter.Clear();
                foreach (var item in filtered)
                {
                    _transaksiFilter.Add(item);
                }
            }
            else
            {
                _transaksiFilter.Clear();
                foreach (var item in unfiltered)
                {
                    _transaksiFilter.Add(item);
                }
            }
        }
        private async Task GetPangkalan()
        {
            var entities = await _pangkalanService.GetAll();
            var pangkalanentities = entities.Where(x => x.Perma == true).ToList();
            _listPangkalan = new List<Pangkalan>(pangkalanentities);
        }

        private Transaksi _selectedTransaksi;
        public Transaksi SelectedTransaksi
        {
            get => _selectedTransaksi;
            set => this.RaiseAndSetIfChanged(ref _selectedTransaksi, value);
        }
        private Pangkalan _selectedPangkalanFilter;
        public Pangkalan SelectedPangkalanFilter
        {
            get => _selectedPangkalanFilter;
            set => this.RaiseAndSetIfChanged(ref _selectedPangkalanFilter, value);
        }
        private string _selectedItemFilter;
        public string SelectedItemFilter
        {
            get => _selectedItemFilter;
            set => this.RaiseAndSetIfChanged(ref _selectedItemFilter, value);
        }
        private DateTimeOffset? _startDate;
        public DateTimeOffset? StartDate
        {
            get => _startDate;
            set => this.RaiseAndSetIfChanged(ref _startDate, value);
        }
        private DateTimeOffset? _endDate;
        public DateTimeOffset? EndDate
        {
            get => _endDate;
            set => this.RaiseAndSetIfChanged(ref _endDate, value);
        }
        private string _selectedPembayaranFilter;
        public string SelectedPembayaranFilter
        {
            get => _selectedPembayaranFilter;
            set => this.RaiseAndSetIfChanged(ref _selectedPembayaranFilter, value);
        }

        private async void DeleteItemAsync()
        {
            await _dataService.Delete(SelectedTransaksi);
            await LoadItem.Execute();
            await FilterTransaksi.Execute();
        }

        private async Task DeleteConfirmation()
        {
            var dialog = new ContentDialog()
            {
                Title = "Hapus item",
                Content = "Anda yakin ingin menghapus?",
                PrimaryButtonText = "Ok"                
            };

            if (SelectedTransaksi != null)
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
        public void ResetFilter()
        {
            SelectedPangkalanFilter = null;
            SelectedItemFilter = null;
            StartDate = null;
            EndDate = null;
            SelectedPembayaranFilter = null;
        }

        public async void AddCommand()
        {
            var hargas = await _hargaService.GetAll();
            var vm = new TransaksiFieldViewModel(this.HostScreen, "Transaksi Baru", new List<Pangkalan>(Pangkalans), new List<Harga>(hargas));

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
                var hargas = await _hargaService.GetAll();
                var vm = new TransaksiFieldViewModel(this.HostScreen, "Edit Transaksi", new List<Pangkalan>(Pangkalans), new List<Harga>(hargas), SelectedTransaksi);

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
            else
            {
                var dialog = new ContentDialog()
                {
                    Title = "Update item",
                    Content = "Tidak ada item dipilih!",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }
    }
}
