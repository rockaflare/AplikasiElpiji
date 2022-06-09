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
        private IEnumerable<StokAwal> _stokAwal;
        private IEnumerable<TransaksiLog> _transaksiLog;
        private IEnumerable<Pemasukan> _pemasukan;
        private IEnumerable<Transaksi> _transaksi;

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
            _stokAwal = await _stokAwalService.GetAll();
            _transaksiLog = await _transaksiLogService.GetAll();
            _pemasukan = await _pemasukanService.GetAll();
            _transaksi = await _transaksiDataService.GetAll();
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
                var tLogResult = _transaksiLog.OrderByDescending(x => x.Tanggal).Where(x => x.Tanggal <= tanggal).FirstOrDefault().SisaStok;
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
