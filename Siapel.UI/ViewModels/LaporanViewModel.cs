using QuestPDF.Fluent;
using QuestPDF.Previewer;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.UI.Documents;
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
    public class LaporanViewModel : ReactiveObject, IRoutableViewModel
    {
        //TODO : Tambah laporan IN OUT
        private List<string> _jenisLaporan;
        private ITransaksiDataService _transaksiDataService;
        private IDataService<StokAwal> _stokAwalDataService;
        private IDataService<Pemasukan> _pemasukanDataService;
        private IDataService<TabungBocor> _tabungBocorDataService;
        private ObservableCollection<Transaksi> _transaksi { get; } = new ObservableCollection<Transaksi>();
        private ObservableCollection<StokAwal> _stokawal { get; } = new ObservableCollection<StokAwal>();
        private ObservableCollection<Pemasukan> _pemasukan { get; } = new ObservableCollection<Pemasukan>();
        private ObservableCollection<TabungBocor> _tabungbocor { get; } = new ObservableCollection<TabungBocor>();
        private List<object> _invoiceList { get; } = new List<object>();
        private List<object> _invoiceGrandTotalList { get; } = new List<object>();
        private List<object> _laporanHarianLimaPuluh { get; } = new List<object>();
        private List<object> _laporanHarianDuaBelas { get; } = new List<object>();
        private List<object> _laporanHarianLimaSetengah { get; } = new List<object>();
        private List<object> _laporanInOutStok { get; } = new List<object>();
        private List<object> _laporanBulananLimaPuluh { get; } = new List<object>();
        private List<object> _laporanBulananDuaBelas { get; } = new List<object>();
        private List<object> _laporanBulananLimaSetengah { get; } = new List<object>();
        private List<LaporanHarian> _totalOfTransaksiList { get; } = new List<LaporanHarian>();
        private List<LaporanBulanan> _totalOfBulananList { get; } = new List<LaporanBulanan>();
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> SaveItem { get; }
        private ReactiveCommand<Unit, Unit> LaporanSelector { get; }
        private string _invoiceGrandTotal;
        private string _invoiceGrandTotalLp;
        private string _invoiceGrandTotalDb;
        private string _invoiceGrandTotalLs;
        string Title = "Laporan";
        public string? UrlPathSegment => Title;
        public IScreen HostScreen { get; }
        public IEnumerable<Transaksi> Transaksi => _transaksi;
        public IEnumerable<StokAwal> StokAwal => _stokawal;
        public IEnumerable<Pemasukan> Pemasukan => _pemasukan;
        public IEnumerable<TabungBocor> TabungBocor => _tabungbocor;
        public List<string> JenisLaporan => _jenisLaporan;
        public List<object> InvoiceList => _invoiceList;
        public List<object> InvoiceGrandTotalList => _invoiceGrandTotalList;
        public List<object> LaporanHarianLimaPuluh => _laporanHarianLimaPuluh;
        public List<object> LaporanHarianDuaBelas => _laporanHarianDuaBelas;
        public List<object> LaporanHarianLimaSetengah => _laporanHarianLimaSetengah;
        public List<object> LaporanInOutStok => _laporanInOutStok;
        public List<object> LaporanBulananLimaPuluh => _laporanBulananLimaPuluh;
        public List<object> LaporanBulananDuaBelas => _laporanBulananDuaBelas;
        public List<object> LaporanBulananLimaSetengah => _laporanBulananLimaSetengah;
        public List<LaporanHarian> TotalOfTransaksiList => _totalOfTransaksiList;
        public List<LaporanBulanan> TotalOfBulananList => _totalOfBulananList;
        

        public LaporanViewModel(IScreen screen, ITransaksiDataService transaksiDataService = null, IDataService<StokAwal> stokAwalDataService = null, IDataService<Pemasukan> pemasukanDataService = null, IDataService<TabungBocor> tabungBocorDataService = null)
        {
            HostScreen = screen;
            _transaksiDataService = transaksiDataService;
            _stokAwalDataService = stokAwalDataService;
            _pemasukanDataService = pemasukanDataService;
            _tabungBocorDataService = tabungBocorDataService;
            _jenisLaporan = new List<string>() { "Invoice", "Harian", "Bulanan"};
            SelectedTanggalLaporan = DateTimeOffset.Now.AddDays(-1);
            LoadItem = ReactiveCommand.CreateFromTask(LaporanUpdater);
            LoadItem.Execute();

            SaveItem = ReactiveCommand.Create(GenerateLaporanCommand);
            LaporanSelector = ReactiveCommand.Create(SelectLaporan);

            this.WhenAnyValue(x => x.SelectedLaporan, x => x.SelectedTanggalLaporan).Select(_ => Unit.Default).InvokeCommand(LaporanSelector);
            this.WhenAnyValue(x => x.SaveDestinationPath).Select(_ => Unit.Default).InvokeCommand(SaveItem);
        }

        private async Task LaporanUpdater()
        {
            _transaksi.Clear();
            if (_transaksiDataService != null)
            {
                var dataList = await _transaksiDataService.GetAll();
                foreach (var item in dataList)
                {
                    _transaksi.Add(item);
                }                
            }
            _stokawal.Clear();
            if (_stokAwalDataService != null)
            {
                var dataList = await _stokAwalDataService.GetAll();
                foreach (var item in dataList)
                {
                    _stokawal.Add(item);
                }
            }
            _pemasukan.Clear();
            if (_pemasukanDataService != null)
            {
                var dataList = await _pemasukanDataService.GetAll();
                foreach (var item in dataList)
                {
                    _pemasukan.Add(item);
                }
            }
            _tabungbocor.Clear();
            if (_tabungBocorDataService != null)
            {
                var dataList = await _tabungBocorDataService.GetAll();
                foreach (var item in dataList)
                {
                    _tabungbocor.Add(item);
                }
            }
        }
        private bool _isInvoice;
        public bool IsInvoice
        {
            get => _isInvoice;
            set => this.RaiseAndSetIfChanged(ref _isInvoice, value);
        }
        private void InvoiceCreator()
        {
            if (_transaksi != null)
            {
                var listtes = _transaksi
                    .Where(x => x.JenisBayar == "Invoice" && x.Status != "Lunas")
                    .GroupBy(t => new { t.Pangkalan, t.Tanggal })
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Tanggal = n.Select(x => x.Tanggal.ToString("dd MMMM yyyy")).First(),
                        Jml50Kg = n.Where(x => x.Item == "50 KG").Sum(x => x.Jumlah),
                        Tab50Kg = n.Where(x => x.Item == "50 KG").Sum(x => x.Total).ToString("Rp #,#"),
                        Jml12Kg = n.Where(x => x.Item == "12 KG").Sum(x => x.Jumlah),
                        Tab12Kg = n.Where(x => x.Item == "12 KG").Sum(x => x.Total).ToString("Rp #,#"),
                        Jml5Kg = n.Where(x => x.Item == "5,5 KG").Sum(x => x.Jumlah),
                        Tab5Kg = n.Where(x => x.Item == "5,5 KG").Sum(x => x.Total).ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")
                        
                    })
                    .OrderBy(t => t.Pangkalan)
                    .ToList();
                _invoiceList.Clear();
                foreach (var item in listtes)
                {
                    _invoiceList.Add(item);
                }
                var grandTotalList = _transaksi
                    .Where(x => x.JenisBayar == "Invoice" && x.Status != "Lunas")
                    .GroupBy(t => new { t.Pangkalan })
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Tab50Kg = n.Where(x => x.Item == "50 KG").Sum(x => x.Total).ToString("Rp #,#"),
                        Tab12Kg = n.Where(x => x.Item == "12 KG").Sum(x => x.Total).ToString("Rp #,#"),
                        Tab5Kg = n.Where(x => x.Item == "5,5 KG").Sum(x => x.Total).ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#"),
                        TotalInt = n.Sum(c => c.Total)
                    })
                    .OrderBy(t => t.Pangkalan)
                    .ToList();
                _invoiceGrandTotalList.Clear();
                foreach (var item in grandTotalList)
                {
                    _invoiceGrandTotalList.Add(item);
                }

                var grandTotalInvoice = _transaksi                    
                    .Where(x => x.JenisBayar == "Invoice" && x.Status != "Lunas")
                    .Sum(t => t.Total)
                    ;
                var grandTotalLP = _transaksi
                    .Where(x => x.JenisBayar == "Invoice" && x.Status != "Lunas" && x.Item == "50 KG")
                    .Sum(t => t.Total)
                    ;
                var grandTotalDB = _transaksi
                    .Where(x => x.JenisBayar == "Invoice" && x.Status != "Lunas" && x.Item == "12 KG")
                    .Sum(t => t.Total)
                    ;
                var grandTotalLS = _transaksi
                    .Where(x => x.JenisBayar == "Invoice" && x.Status != "Lunas" && x.Item == "5,5 KG")
                    .Sum(t => t.Total)
                    ;
                _invoiceGrandTotal = grandTotalInvoice != null ? grandTotalInvoice.ToString("Rp #,#") : "";
                _invoiceGrandTotalLp = grandTotalLP != null ? grandTotalLP.ToString("Rp #,#") : "";
                _invoiceGrandTotalDb = grandTotalDB != null ? grandTotalDB.ToString("Rp #,#") : "";
                _invoiceGrandTotalLs = grandTotalLS != null ? grandTotalLS.ToString("Rp #,#") : "";
            }
        }
        private void LapHarianCreator()
        {
            if (_transaksi != null && _pemasukan != null && _stokawal != null && _tabungbocor != null)
            {
                var limaPuluhList = _transaksi
                    .Where(x => x.Item == "50 KG" && x.Tanggal == SelectedTanggalLaporan.Date)
                    .GroupBy(t => t.Pangkalan)
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                        Tunai = n.Where(x => x.JenisBayar == "Tunai").Sum(x => x.Total).ToString("Rp #,#"),
                        Transfer = n.Where(x => x.JenisBayar == "Transfer").Sum(x => x.Total).ToString("Rp #,#"),
                        Invoice = n.Where(x => x.JenisBayar == "Invoice").Sum(x => x.Total).ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")
                    })
                    .ToList();
                _laporanHarianLimaPuluh.Clear();
                foreach (var item in limaPuluhList)
                {
                    _laporanHarianLimaPuluh.Add(item);
                }
                var duaBelasList = _transaksi
                   .Where(x => x.Item == "12 KG" && x.Tanggal == SelectedTanggalLaporan.Date)
                   .GroupBy(t => t.Pangkalan)
                   .Select(n => new
                   {
                       Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                       Jumlah = n.Sum(x => x.Jumlah),
                       Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                       Tunai = n.Where(x => x.JenisBayar == "Tunai").Sum(x => x.Total).ToString("Rp #,#"),
                       Transfer = n.Where(x => x.JenisBayar == "Transfer").Sum(x => x.Total).ToString("Rp #,#"),
                       Invoice = n.Where(x => x.JenisBayar == "Invoice").Sum(x => x.Total).ToString("Rp #,#"),
                       TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")

                   })
                   .OrderBy(t => t.Pangkalan)
                   .ToList();
                _laporanHarianDuaBelas.Clear();
                foreach (var item in duaBelasList)
                {
                    _laporanHarianDuaBelas.Add(item);
                }
                var limaSetengahList = _transaksi
                    .Where(x => x.Item == "5,5 KG" && x.Tanggal == SelectedTanggalLaporan.Date)
                    .GroupBy(t => t.Pangkalan)
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                        Tunai = n.Where(x => x.JenisBayar == "Tunai").Sum(x => x.Total).ToString("Rp #,#"),
                        Transfer = n.Where(x => x.JenisBayar == "Transfer").Sum(x => x.Total).ToString("Rp #,#"),
                        Invoice = n.Where(x => x.JenisBayar == "Invoice").Sum(x => x.Total).ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")

                    })
                    .OrderBy(t => t.Pangkalan)
                    .ToList();
                _laporanHarianLimaSetengah.Clear();
                foreach (var item in limaSetengahList)
                {
                    _laporanHarianLimaSetengah.Add(item);
                }
                var totalOfTransaksiList = _transaksi
                    .Where(x => x.Tanggal == SelectedTanggalLaporan.Date)
                    .GroupBy(t => t.Item)
                    .Select(n => new LaporanHarian()
                    {
                        Item = n.Select(x => x.Item).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                        Tunai = n.Where(x => x.JenisBayar == "Tunai").Sum(x => x.Total).ToString("Rp #,#"),
                        Transfer = n.Where(x => x.JenisBayar == "Transfer").Sum(x => x.Total).ToString("Rp #,#"),
                        Invoice = n.Where(x => x.JenisBayar == "Invoice").Sum(x => x.Total).ToString("Rp #,#"),
                        Total = n.Sum(c => c.Total).ToString("Rp #,#"),
                        TunaiInt = n.Where(x => x.JenisBayar == "Tunai").Sum(x => x.Total),
                        TransferInt = n.Where(x => x.JenisBayar == "Transfer").Sum(x => x.Total),
                        InvoiceInt = n.Where(x => x.JenisBayar == "Invoice").Sum(x => x.Total),
                        TotalInt = n.Sum(x => x.Total)
                    })
                    .ToList();
                _totalOfTransaksiList.Clear();
                foreach (var item in totalOfTransaksiList)
                {
                    _totalOfTransaksiList.Add(item);
                }
                List<StokAwal> stokAwals = new List<StokAwal>(StokAwal);
                List<Pemasukan> pemasukans = new List<Pemasukan>(Pemasukan);
                List<Transaksi> transaksis = new List<Transaksi>(Transaksi);
                List<TabungBocor> tabungBocors = new List<TabungBocor>(TabungBocor);
                InOutService inOutService = new InOutService(stokAwals, pemasukans, transaksis, tabungBocors, SelectedTanggalLaporan.Date);
                var inoutlist = inOutService.GetInOutStokList();
                _laporanInOutStok.Clear();
                foreach (var item in inoutlist)
                {
                    _laporanInOutStok.Add(item);
                }
            }
        }
        private void LapBulananCreator()
        {
            if (_transaksi != null)
            {
                var bulan = SelectedTanggalLaporan.Month;
                var limaPuluhList = _transaksi
                    .Where(x => x.Item == "50 KG" && x.Tanggal.Month == bulan && x.Status == "Lunas" && x.TanggalLunas.Value.Month == bulan)
                    .OrderBy(t => t.Tanggal)
                    .GroupBy(t => t.Pangkalan)
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Tanggal = n.Select(x => x.Tanggal.ToString("dd MMMM yyyy")).First(),
                        Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")
                    })
                    .ToList();
                _laporanBulananLimaPuluh.Clear();
                foreach (var item in limaPuluhList)
                {
                    _laporanBulananLimaPuluh.Add(item);
                }

                var duaBelasList = _transaksi
                    .Where(x => x.Item == "12 KG" && x.Tanggal.Month == bulan && x.Status == "Lunas" && x.TanggalLunas.Value.Month == bulan)
                    .OrderBy(t => t.Tanggal)
                    .GroupBy(t => t.Pangkalan)
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Tanggal = n.Select(x => x.Tanggal.ToString("dd MMMM yyyy")).First(),
                        Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")
                    })
                    .ToList();
                _laporanBulananDuaBelas.Clear();
                foreach (var item in duaBelasList)
                {
                    _laporanBulananDuaBelas.Add(item);
                }

                var limaSetengahList = _transaksi
                    .Where(x => x.Item == "5,5 KG" && x.Tanggal.Month == bulan && x.Status == "Lunas" && x.TanggalLunas.Value.Month == bulan)
                    .OrderBy(t => t.Tanggal)
                    .GroupBy(t => t.Pangkalan)
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan.Nama).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Tanggal = n.Select(x => x.Tanggal.ToString("dd MMMM yyyy")).First(),
                        Harga = n.Select(x => x.Harga).First().ToString("Rp #,#"),
                        TotalSemua = n.Sum(c => c.Total).ToString("Rp #,#")
                    })
                    .ToList();
                _laporanBulananLimaSetengah.Clear();
                foreach (var item in limaSetengahList)
                {
                    _laporanBulananLimaSetengah.Add(item);
                }
                var totalOfBulananList = _transaksi
                    .Where(x => x.Tanggal.Month == bulan && x.Status == "Lunas" && x.TanggalLunas.Value.Month == bulan)
                    .GroupBy(t => t.Item)
                    .Select(n => new LaporanBulanan()
                    {
                        Item = n.Select(x => x.Item).First(),
                        Jumlah = n.Sum(x => x.Jumlah),
                        Total = n.Sum(x => x.Total).ToString("Rp #,#")
                    })
                    .ToList();
                _totalOfBulananList.Clear();
                foreach (var item in totalOfBulananList)
                {
                    _totalOfBulananList.Add(item);
                }
            }
        }
        private string _selectedLaporan;
        public string SelectedLaporan
        {
            get => _selectedLaporan;
            set => this.RaiseAndSetIfChanged(ref _selectedLaporan, value);
        }
        private DateTimeOffset _selectedTanggalLaporan;
        public DateTimeOffset SelectedTanggalLaporan
        {
            get => _selectedTanggalLaporan;
            set => this.RaiseAndSetIfChanged(ref _selectedTanggalLaporan, value);
        }

        private string _saveDestinationPath;
        public string SaveDestinationPath
        {
            get => _saveDestinationPath;
            set => this.RaiseAndSetIfChanged(ref _saveDestinationPath, value);
        }

        private void SelectLaporan()
        {
            if (SelectedLaporan != null)
            {
                if (SelectedLaporan == "Invoice")
                {
                    InvoiceCreator();
                }
                else if(SelectedLaporan == "Harian")
                {
                    LapHarianCreator();
                }
                else if (SelectedLaporan == "Bulanan")
                {
                    LapBulananCreator();
                }
            }
        }

        public void GenerateLaporanCommand()
        {            
            if (SelectedLaporan != null && !string.IsNullOrWhiteSpace(SaveDestinationPath))
            {
                var invoiceDocument = new InvoiceDocument(InvoiceList, InvoiceGrandTotalList, SelectedTanggalLaporan.Date.ToLongDateString(), _invoiceGrandTotal, _invoiceGrandTotalLp, _invoiceGrandTotalDb, _invoiceGrandTotalLs);
                var harianDocument = new NewLaporanHarianDocument(LaporanHarianLimaPuluh, LaporanHarianDuaBelas, LaporanHarianLimaSetengah, SelectedTanggalLaporan.Date.ToLongDateString(), TotalOfTransaksiList, LaporanInOutStok);
                var bulananDocument = new LaporanBulananDocument(LaporanBulananLimaPuluh, LaporanBulananDuaBelas, LaporanBulananLimaSetengah, SelectedTanggalLaporan.ToString("MMMM yyyy"), TotalOfBulananList);

                switch (SelectedLaporan)
                {
                    case "Invoice":
                        invoiceDocument.GeneratePdf(SaveDestinationPath);
                        break;
                    case "Harian":
                        harianDocument.GeneratePdf(SaveDestinationPath);
                        break;
                    case "Bulanan":
                        bulananDocument.GeneratePdf(SaveDestinationPath);
                        break;
                    default:
                        break;
                }
            }
        }

        
    }
}
