using QuestPDF.Fluent;
using QuestPDF.Previewer;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.UI.Documents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class LaporanViewModel : ReactiveObject, IRoutableViewModel
    {
        private List<string> _jenisLaporan;
        private ITransaksiDataService _transaksiDataService;
        private ObservableCollection<Transaksi> _transaksi { get; } = new ObservableCollection<Transaksi>();
        private List<object> _invoiceList { get; } = new List<object>();
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        string Title = "Laporan";
        public string? UrlPathSegment => Title;
        public IScreen HostScreen { get; }
        public IEnumerable<Transaksi> Transaksi => _transaksi;
        public List<string> JenisLaporan => _jenisLaporan;
        public List<object> InvoiceList => _invoiceList;
        
        public LaporanViewModel(IScreen screen, ITransaksiDataService transaksiDataService = null)
        {
            HostScreen = screen;
            _transaksiDataService = transaksiDataService;
            _jenisLaporan = new List<string>() { "Invoice", "Harian"};
            LoadItem = ReactiveCommand.CreateFromTask(LaporanUpdater);
            LoadItem.Execute();
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
            if (_transaksi != null)
            {
                var listtes = _transaksi
                    .GroupBy(t => t.Pangkalan)
                    .Select(n => new
                    {
                        Pangkalan = n.Select(x => x.Pangkalan),
                        Tanggal = n.Select(x => x.Tanggal),
                        Tab50Kg = n.Where(x => x.Item == "50 KG").Sum(x => x.Total),
                        Tab12Kg = n.Where(x => x.Item == "12 KG").Sum(x => x.Total),
                        Tab5Kg = n.Where(x => x.Item == "5,5 KG").Sum(x => x.Total),
                        TotalSemua = n.Sum(c => c.Total)
                    }).ToList();

                foreach (var item in listtes)
                {
                    _invoiceList.Add(item);
                }
            }
        }

        private void GenerateInvoice()
        {
            
        }

        public void GenerateLaporanCommand()
        {
            var document = new InvoiceDocument();
            document.GeneratePdf("Invoice-Tes-1.pdf");
        }

        public string ImagePathLoc => "/Assets/avalonia-logo.ico";
    }
}
