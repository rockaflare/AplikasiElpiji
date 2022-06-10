using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using System;
using System.Collections.Generic;
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
        private readonly IDataService<TransaksiLog> _transaksiLogService;
        private readonly IDataService<Pemasukan> _pemasukanService;
        private readonly ITransaksiDataService _transaksiDataService;
        private List<StokAwal> _stokAwal { get; } = new List<StokAwal>();
        private List<TransaksiLog> _transaksiLog { get; } = new List<TransaksiLog>();
        private List<Pemasukan> _pemasukan { get; } = new List<Pemasukan>();
        private List<Transaksi> _transaksi { get; } = new List<Transaksi>();

        private int _stokAwalValue;
        private List<object> _stokInOut { get; } = new List<object>();
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> LoadStokAwal { get; }

        public string? UrlPathSegment => "In Out";
        public IScreen HostScreen { get; }
        public List<object> StokInOut => _stokInOut;



        public InOutViewModel(IScreen screen, IDataService<StokAwal> stokAwalService, IDataService<Pemasukan> pemasukanService, ITransaksiDataService transaksiDataService, IDataService<TransaksiLog> transaksiLogService)
        {
            HostScreen = screen;
            _stokAwalService = stokAwalService;
            _pemasukanService = pemasukanService;
            _transaksiDataService = transaksiDataService;
            _transaksiLogService = transaksiLogService;
            _selectedTanggal = DateTimeOffset.Now;
            LoadItem = ReactiveCommand.CreateFromTask(LoadAllItem);
            LoadItem.Execute();
            SelectedTanggal = DateTimeOffset.Now;
            this.WhenAnyValue(x => x.SelectedTanggal).Subscribe(_ => SummaLummaDumma());
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
            var transaksiLogs = await _transaksiLogService.GetAll();
            var pemasukans = await _pemasukanService.GetAll();
            var transaksis = await _transaksiDataService.GetAll();

            _stokAwal.Clear();
            _transaksiLog.Clear();
            _pemasukan.Clear();
            _transaksi.Clear();

            foreach (var item in stokAwals)
            {
                _stokAwal.Add(item);
            }
            foreach (var item in transaksiLogs)
            {
                _transaksiLog.Add(item);
            }
            foreach (var item in pemasukans)
            {
                _pemasukan.Add(item);
            }
            foreach (var item in transaksis)
            {
                _transaksi.Add(item);
            }
        }

        private int? GetStokAwalDefault(int? masuk, int? keluar, int? lastStok)
        {
            int? resultStokAwal = lastStok + keluar - masuk;
            return resultStokAwal;
        }

        private int? GetSumPemasukan(string item, DateTimeOffset tanggal)
        {
            int? resultPemasukan = 0;
            if (_pemasukan.Any())
            {
                var pemasukanSum = _pemasukan.Where(x => x.Item == item && x.Tanggal == tanggal).Sum(x => x.Jumlah);
                if (pemasukanSum > 0)
                {
                    resultPemasukan = pemasukanSum;
                }                
            }
            return resultPemasukan;
        }
        private int? GetSumPenjualan(string item, DateTimeOffset tanggal)
        {
            int? resultPenjualan = 0;
            if (_transaksi.Any())
            {
                var penjualanSum = _transaksi.Where(x => x.Item == item && x.Tanggal == tanggal).Sum(x => x.Jumlah);
                if (penjualanSum > 0)
                {
                    resultPenjualan = penjualanSum;
                }               
            }
            return resultPenjualan;
        }

        private int? GetLastStok(string item, DateTimeOffset tanggal)
        {
            int? resultLastStok = 0;
            if (_transaksiLog.Any())
            {
                int? tLogResult = _transaksiLog.Where(x => x.Tanggal == tanggal && x.Item == item).OrderByDescending(x => x.Created).Select(x => x.SisaStok).FirstOrDefault();
                if (tLogResult != null)
                {
                    resultLastStok = tLogResult;
                }                
            }
            return resultLastStok;
        }

        private void SummaLummaDumma()
        {
            var sakhir = GetLastStok("50 KG", SelectedTanggal.Date);
            var klr = GetSumPenjualan("50 KG", SelectedTanggal.Date);
            var msk = GetSumPemasukan("50 KG", SelectedTanggal.Date);
            var sawal = GetStokAwalDefault(msk, klr, sakhir);
        }
    }
}
