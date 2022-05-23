using ReactiveUI;
using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
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
        public List<Pangkalan> PangkalanList => _pangkalanList;

        public TransaksiFieldViewModel(IScreen screen, string title, List<Pangkalan> pangkalan, Transaksi transaksi = null)
        {
            HostScreen = screen;
            _title = title;
            _pangkalanList = pangkalan;
            _transaksi = transaksi;
            var okEnabled = this.WhenAnyValue(x => x.Item, x => !string.IsNullOrWhiteSpace(x));
            Save = ReactiveCommand.Create(
                () => _transaksi != null ? EditTransaksi() : new Transaksi { Tanggal = DateOnly.Parse(DateTime.Now.ToLongDateString()) });
            Cancel = ReactiveCommand.Create(() => { });
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
        private DateOnly _tanggal;
        public DateOnly Tanggal
        {
            get => _tanggal;
            set => this.RaiseAndSetIfChanged(ref _tanggal, value);
        }
        private Pangkalan _selectedPangkalan;
        public Pangkalan SelectedPangkalan
        {
            get => _selectedPangkalan;
            set => this.RaiseAndSetIfChanged(ref _selectedPangkalan, value);
        }
        private string _item;
        public string Item
        {
            get => _item;
            set => this.RaiseAndSetIfChanged(ref _item, value);
        }

        private Transaksi EditTransaksi()
        {
            _transaksi.Tanggal = _tanggal;
            return _transaksi;
        }
        public ReactiveCommand<Unit, Transaksi> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
