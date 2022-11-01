using ReactiveUI;
using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels.DialogViewModels
{
    public class TransaksiFieldViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _title;
        public string? UrlPathSegment => _title;
        public IScreen HostScreen { get; }
        private Transaksi _transaksi;
        private List<Pangkalan> _pangkalanList;
        private List<Harga> _hargaList;
        private List<string> _itemList;
        private List<string> _jenisBayar;
        public List<Pangkalan> PangkalanList => _pangkalanList;
        public List<Harga> HargaList => _hargaList;
        public List<string> ItemList => _itemList;
        public List<string> JenisBayarList => _jenisBayar;

        public TransaksiFieldViewModel(IScreen screen, string title, List<Pangkalan> pangkalan, List<Harga> harga, Transaksi transaksi = null)
        {
            HostScreen = screen;
            _title = title;
            _pangkalanList = pangkalan;
            _transaksi = transaksi;
            _hargaList = harga;
            _itemList = new List<string>() { "50 KG", "12 KG", "5,5 KG", "Lainnya" };
            _jenisBayar = new List<string>() { "Tunai", "Transfer", "Invoice" };
            SetField();
            var okEnabled = this.WhenAnyValue(x => x.Item, x => x.TipeBayar, (i, t) => !string.IsNullOrWhiteSpace(i) && !string.IsNullOrWhiteSpace(t));
            Save = ReactiveCommand.Create(
                () => _transaksi != null ? EditTransaksi() : new Transaksi { Tanggal = Tanggal.Date, Pangkalan = Pangkalan, Item = Item, Harga = Harga.Value, Jumlah = JumlahItem.Value, JenisBayar = TipeBayar, Total = Total.Value, Status = Status, TanggalLunas = TanggalLunas?.Date}, okEnabled);
            Cancel = ReactiveCommand.Create(() => { });

            ExecuteHargaItem = ReactiveCommand.Create(GetHargaPangkalan);
            CalculateCommand = ReactiveCommand.Create(CalculateTotal);
            SetPaymentStatus = ReactiveCommand.Create(PaymentStatusSetter);
            

            this.WhenAnyValue(x => x.Pangkalan, x => x.Item).Select(_ => Unit.Default).InvokeCommand(ExecuteHargaItem);
            this.WhenAnyValue(x => x.JumlahItem, x => x.Pangkalan, x => x.Item).Select(_ => Unit.Default).InvokeCommand(CalculateCommand);
            this.WhenAnyValue(x => x.TipeBayar, x => x.TanggalLunas).Select(_ => Unit.Default).InvokeCommand(SetPaymentStatus);
            
        }
        private int _pangkalanIndex;
        public int PangkalanIndex
        {
            get => _pangkalanIndex;
            set => this.RaiseAndSetIfChanged(ref _pangkalanIndex, value);
        }

        private Pangkalan _pangkalan;
        public Pangkalan Pangkalan
        {
            get => _pangkalan;
            set => this.RaiseAndSetIfChanged(ref _pangkalan, value);
        }
        private DateTimeOffset _tanggal;
        public DateTimeOffset Tanggal
        {
            get => _tanggal;
            set => this.RaiseAndSetIfChanged(ref _tanggal, value);
        }
        private string _item;
        public string Item
        {
            get => _item;
            set => this.RaiseAndSetIfChanged(ref _item, value);
        }
        private int? _harga;
        public int? Harga
        {
            get => _harga;
            set => this.RaiseAndSetIfChanged(ref _harga, value);
        }
        private ReactiveCommand<Unit, Unit> ExecuteHargaItem { get; set; }
        private void GetHargaPangkalan()
        {
            int? hargaResult = null;
            if (Pangkalan != null)
            {
                var defHarga = HargaList.First();
                var getHarga = HargaList.FirstOrDefault(p => p.Pangkalan.Id == _pangkalan.Id, defHarga);
                if (getHarga != null)
                {
                    switch (_item)
                    {
                        case "50 KG":
                            hargaResult = getHarga.TbLimaPuluh;
                            break;
                        case "12 KG":
                            hargaResult = getHarga.TbDuaBelas;
                            break;
                        case "5,5 KG":
                            hargaResult = getHarga.TbLimaSetengah;
                            break;
                        default:
                            break;
                    }
                }
            }
            Harga = hargaResult;
        }
        private int? _jumlahItem;
        public int? JumlahItem
        {
            get => _jumlahItem;
            set => this.RaiseAndSetIfChanged(ref _jumlahItem, value);
        }
        private ReactiveCommand<Unit, Unit> CalculateCommand { get; set; }
        private void CalculateTotal()
        {
            if (_jumlahItem != null && _harga != null)
            {
                Total = _jumlahItem * _harga;
            }
        }
        private string _tipeBayar;
        public string TipeBayar
        {
            get => _tipeBayar;
            set => this.RaiseAndSetIfChanged(ref _tipeBayar, value);
        }
        private int? _total;
        public int? Total
        {
            get => _total;
            set => this.RaiseAndSetIfChanged(ref _total, value);
        }
        private ReactiveCommand<Unit, Unit> SetPaymentStatus { get; set; }
        private void PaymentStatusSetter()
        {
            string resultStatus = Status;
            if (TipeBayar != null)
            {
                if (_transaksi == null)
                {
                    if (TipeBayar != "Invoice")
                    {
                        resultStatus = "Lunas";
                        TanggalLunas = DateTimeOffset.Now;
                    }
                    else
                    {
                        resultStatus = "Belum Lunas";
                        TanggalLunas = null;
                    }
                }
                else
                {
                    if (TipeBayar == "Invoice" && TanggalLunas != null)
                    {
                        resultStatus = "Lunas";
                    }
                }
            }
            Status = resultStatus;
        }
        private string _status;
        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }
        private DateTimeOffset? _tanggalLunas;
        public DateTimeOffset? TanggalLunas
        {
            get => _tanggalLunas;
            set => this.RaiseAndSetIfChanged(ref _tanggalLunas, value);
        }
        private bool _canEditTipeBayar;
        public bool CanEditTipeBayar
        {
            get => _canEditTipeBayar;
            set => this.RaiseAndSetIfChanged(ref _canEditTipeBayar, value);
        }
        private Transaksi EditTransaksi()
        {
            _transaksi.Tanggal = _tanggal;
            _transaksi.Pangkalan = _pangkalan;
            _transaksi.Item = _item;
            _transaksi.Harga = _harga.Value;
            _transaksi.Jumlah = _jumlahItem.Value;
            _transaksi.JenisBayar = _tipeBayar;
            _transaksi.Total = _total.Value;
            _transaksi.Status = _status;
            _transaksi.TanggalLunas = _tanggalLunas;           

            return _transaksi;
        }

        private void SetField()
        {
            if (_transaksi != null)
            {
                _tanggal = _transaksi.Tanggal;
                _pangkalan = _transaksi.Pangkalan;
                _item = _transaksi.Item;
                _harga = _transaksi.Harga;
                _jumlahItem = _transaksi.Jumlah;
                _tipeBayar = _transaksi.JenisBayar;
                _total = _transaksi.Total;
                _status = _transaksi.Status;
                _tanggalLunas = _transaksi.TanggalLunas;
                _canEditTipeBayar = false;
                _pangkalanIndex = PangkalanList.FindIndex(a => a.Nama == _pangkalan.Nama);
            }
            else
            {
                _canEditTipeBayar = true;
                _pangkalanIndex = -1;
                Tanggal = DateTimeOffset.Now;
                TanggalLunas = null;
            }
        }
        public ReactiveCommand<Unit, Transaksi> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
