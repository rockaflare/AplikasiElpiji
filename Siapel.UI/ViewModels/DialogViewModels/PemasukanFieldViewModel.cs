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
    public class PemasukanFieldViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _title;
        private List<string> _itemList;
        private Pemasukan _pemasukan;
        public string? UrlPathSegment => _title;
        public IScreen HostScreen { get; }
        public List<string> ItemList => _itemList;

        public PemasukanFieldViewModel(IScreen screen, string title, Pemasukan pemasukan = null)
        {
            HostScreen = screen;
            _title = title;
            _pemasukan = pemasukan;
            _itemList = new List<string>() { "50 KG", "12 KG", "5,5 KG" };
            SetField();
            //var okEnabled = this.WhenAnyValue(x => x.JumlahLimaPuluh, x => x.JumlahDuaBelas, x => x.JumlahLimaSetengah, (lp, db, ls) => !string.IsNullOrWhiteSpace(lp.ToString()) && !string.IsNullOrWhiteSpace(db.ToString()) && !string.IsNullOrEmpty(ls.ToString()));
            Save = ReactiveCommand.Create(
                () => _pemasukan != null ? EditPemasukan() : new Pemasukan { Tanggal = Tanggal?.Date, Item = Item, Jumlah = Jumlah }
                );
            Cancel = ReactiveCommand.Create(() => { });
        }

        private DateTimeOffset? _tanggal;
        public DateTimeOffset? Tanggal
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
        private int? _jumlah;
        public int? Jumlah
        {
            get => _jumlah;
            set => this.RaiseAndSetIfChanged(ref _jumlah, value);
        }

        private Pemasukan EditPemasukan()
        {
            _pemasukan.Tanggal = _tanggal;
            _pemasukan.Item = _item;
            _pemasukan.Jumlah = _jumlah;

            return _pemasukan;
        }

        private void SetField()
        {
            if (_pemasukan != null)
            {
                _tanggal = _pemasukan.Tanggal;
                _item = _pemasukan.Item;
                _jumlah = _pemasukan.Jumlah;
            }
            else
            {
                _tanggal = DateTimeOffset.Now;
            }
        }

        public ReactiveCommand<Unit, Pemasukan> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
