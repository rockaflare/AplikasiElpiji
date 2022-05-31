﻿using QuestPDF.Fluent;
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
using System.Reactive.Linq;
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
        private ReactiveCommand<Unit, Unit> SaveItem { get; }
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
            SelectedTanggalLaporan = DateTimeOffset.Now;
            LoadItem = ReactiveCommand.CreateFromTask(LaporanUpdater);
            LoadItem.Execute();

            SaveItem = ReactiveCommand.Create(GenerateLaporanCommand);

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
            if (_transaksi != null)
            {
                var listtes = _transaksi
                    .Where(x =>  x.JenisBayar == "Invoice" && x.Status != "Lunas" )
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
                        TotalSemua = n.Sum(c => c.Total)
                    })                    
                    .ToList();

                foreach (var item in listtes)
                {
                    _invoiceList.Add(item);
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

        private void GenerateInvoice()
        {
            
        }

        public void GenerateLaporanCommand()
        {
            var document = new InvoiceDocument(InvoiceList, SelectedTanggalLaporan.Date.ToLongDateString());
            if (!string.IsNullOrWhiteSpace(SaveDestinationPath))
            {
                document.GeneratePdf(SaveDestinationPath);
            }            
        }

        
    }
}