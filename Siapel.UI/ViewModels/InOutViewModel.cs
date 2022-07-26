using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.UI.Services;
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
    public class InOutViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IDataService<StokAwal> _stokAwalService;
        private readonly IDataService<Pemasukan> _pemasukanService;
        private readonly IDataService<TabungBocor> _tabungBocorService;
        private readonly ITransaksiDataService _transaksiDataService;
        private List<StokAwal> _stokAwal { get; } = new List<StokAwal>();
        private List<Pemasukan> _pemasukan { get; } = new List<Pemasukan>();
        private List<Transaksi> _transaksi { get; } = new List<Transaksi>();
        private List<TabungBocor> _tabungBocor { get; } = new List<TabungBocor>();

        private int _stokAwalValue;
        private ObservableCollection<object> _stokInOut { get; } = new ObservableCollection<object>();
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> LoadInOut { get; }

        public string? UrlPathSegment => "In Out";
        public IScreen HostScreen { get; }
        public IEnumerable<object> StokInOut => _stokInOut;



        public InOutViewModel(IScreen screen, IDataService<StokAwal> stokAwalService, IDataService<Pemasukan> pemasukanService, ITransaksiDataService transaksiDataService, IDataService<TabungBocor> tabungBocorSerevice)
        {
            HostScreen = screen;
            _stokAwalService = stokAwalService;
            _pemasukanService = pemasukanService;
            _transaksiDataService = transaksiDataService;
            _tabungBocorService = tabungBocorSerevice;
            _selectedTanggal = DateTimeOffset.Now;
            LoadItem = ReactiveCommand.CreateFromTask(LoadAllItem);
            LoadItem.Execute();
            SelectedTanggal = DateTimeOffset.Now;
            LoadInOut = ReactiveCommand.Create(CreateInOut);
            
            this.WhenAnyValue(x => x.SelectedTanggal).Select(_ => Unit.Default).InvokeCommand(LoadInOut);
        }

        private DateTimeOffset _selectedTanggal;
        public DateTimeOffset SelectedTanggal
        {
            get => _selectedTanggal;
            set => this.RaiseAndSetIfChanged(ref _selectedTanggal, value);
        }

        private async Task LoadAllItem()
        {
            var stokAwals = await _stokAwalService.GetAll();
            var pemasukans = await _pemasukanService.GetAll();
            var transaksis = await _transaksiDataService.GetAll();
            var tabungbocors = await _tabungBocorService.GetAll();

            _stokAwal.Clear();
            _pemasukan.Clear();
            _transaksi.Clear();
            _tabungBocor.Clear();

            foreach (var item in stokAwals)
            {
                _stokAwal.Add(item);
            }
            foreach (var item in pemasukans)
            {
                _pemasukan.Add(item);
            }
            foreach (var item in transaksis)
            {
                _transaksi.Add(item);
            }
            foreach (var item in tabungbocors)
            {
                _tabungBocor.Add(item);
            }
        }

        private void CreateInOut()
        {
            InOutService inOutService = new InOutService(_stokAwal, _pemasukan, _transaksi, _tabungBocor, SelectedTanggal.Date);
            var inOutList = inOutService.GetInOutStokList();
            _stokInOut.Clear();
            foreach (var item in inOutList)
            {
                _stokInOut.Add(item);
            }
        }

        public void BackDateCommand()
        {
            SelectedTanggal = SelectedTanggal.AddDays(-1);
        }
        public void ForwardDateCommand()
        {
            SelectedTanggal = SelectedTanggal.AddDays(1);
        }
    }
}
