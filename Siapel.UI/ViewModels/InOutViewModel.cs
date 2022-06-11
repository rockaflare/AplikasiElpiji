﻿using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
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
        private readonly IDataService<TransaksiLog> _transaksiLogService;
        private readonly IDataService<Pemasukan> _pemasukanService;
        private readonly IDataService<TabungBocor> _tabungBocorService;
        private readonly ITransaksiDataService _transaksiDataService;
        private List<StokAwal> _stokAwal { get; } = new List<StokAwal>();
        private List<TransaksiLog> _transaksiLog { get; } = new List<TransaksiLog>();
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



        public InOutViewModel(IScreen screen, IDataService<StokAwal> stokAwalService, IDataService<Pemasukan> pemasukanService, ITransaksiDataService transaksiDataService, IDataService<TransaksiLog> transaksiLogService, IDataService<TabungBocor> tabungBocorSerevice)
        {
            HostScreen = screen;
            _stokAwalService = stokAwalService;
            _pemasukanService = pemasukanService;
            _transaksiDataService = transaksiDataService;
            _transaksiLogService = transaksiLogService;
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
            var transaksiLogs = await _transaksiLogService.GetAll();
            var pemasukans = await _pemasukanService.GetAll();
            var transaksis = await _transaksiDataService.GetAll();
            var tabungbocors = await _tabungBocorService.GetAll();

            _stokAwal.Clear();
            _transaksiLog.Clear();
            _pemasukan.Clear();
            _transaksi.Clear();
            _tabungBocor.Clear();

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
            foreach (var item in tabungbocors)
            {
                _tabungBocor.Add(item);
            }
        }

        private int? GetStokAwalDefault(int? masuk, int? keluar, int? lastStok, int? titipan, int? ambil)
        {
            int? resultStokAwal = lastStok + ambil + keluar - masuk - titipan;
            return resultStokAwal;
        }
        private int? GetSumTitipanBocor(string item, DateTimeOffset tanggal)
        {
            int? resultTitipan = 0;
            if (_tabungBocor.Any())
            {
                var titipanSum = _tabungBocor.Where(x => x.Item == item && x.Tanggal == tanggal).Sum(x => x.Titipan);
                if (titipanSum > 0)
                {
                    resultTitipan = titipanSum;
                }
            }
            return resultTitipan;
        }
        private int? GetSumAmbilBocor(string item, DateTimeOffset tanggal)
        {
            int? resultTitipan = 0;
            if (_tabungBocor.Any())
            {
                var titipanSum = _tabungBocor.Where(x => x.Item == item && x.Tanggal == tanggal).Sum(x => x.Ambil);
                if (titipanSum > 0)
                {
                    resultTitipan = titipanSum;
                }
            }
            return resultTitipan;
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
            if (_transaksiLog.Count > 0)
            {
                int? tLogResult = _transaksiLog.Where(x => x.Tanggal == tanggal && x.Item == item).OrderByDescending(x => x.Created).Select(x => x.SisaStok).FirstOrDefault();
                if (tLogResult != null && tLogResult > 0)
                {
                    resultLastStok = tLogResult;
                }
                else
                {
                    resultLastStok = _stokAwal.FirstOrDefault(x => x.Item == item).Jumlah;
                }
            }
            return resultLastStok;
        }

        private void CreateInOut()
        {
            string[] items = { "50 KG", "12 KG", "5,5 KG" };
            _stokInOut.Clear();
            foreach (var i in items)
            {
                _stokInOut.Add(new 
                {
                    Item = i,
                    StokAkhir = GetLastStok(i, SelectedTanggal.Date),
                    TitipanBocor = GetSumTitipanBocor(i, SelectedTanggal.Date),
                    AmbilBocor = GetSumAmbilBocor(i, SelectedTanggal.Date),
                    Penjualan = GetSumPenjualan(i, SelectedTanggal.Date),
                    Masuk = GetSumPemasukan(i, SelectedTanggal.Date),
                    StokAwal = GetStokAwalDefault(GetSumPemasukan(i, SelectedTanggal.Date), GetSumPenjualan(i, SelectedTanggal.Date), GetLastStok(i, SelectedTanggal.Date), GetSumTitipanBocor(i, SelectedTanggal.Date), GetSumAmbilBocor(i, SelectedTanggal.Date))
                });
            }
        }
    }
}
