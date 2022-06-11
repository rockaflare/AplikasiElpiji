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
    public class TabungBocorFieldViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _title;
        private List<string> _itemList;
        public string? UrlPathSegment => _title;
        public IScreen HostScreen { get; }
        public List<string> ItemList => _itemList;

        public TabungBocorFieldViewModel(IScreen screen, string title)
        {
            HostScreen = screen;
            _title = title;
            _itemList = new List<string>() { "50 KG", "12 KG", "5,5 KG" };
            SetField();
            Save = ReactiveCommand.Create(
                () => new TabungBocor { Tanggal = Tanggal?.Date, Item = Item, Titipan = Titipan, Ambil = Ambil, Keterangan = Keterangan}
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
        private int? _titipan;
        public int? Titipan
        {
            get => _titipan;
            set => this.RaiseAndSetIfChanged(ref _titipan, value);
        }
        private int? _ambil;
        public int? Ambil
        {
            get => _titipan;
            set => this.RaiseAndSetIfChanged(ref _ambil, value);
        }
        private string _keterangan;
        public string Keterangan
        {
            get => _keterangan;
            set => this.RaiseAndSetIfChanged(ref _keterangan, value);
        }

        private void SetField()
        {
            _tanggal = DateTimeOffset.Now;
        }

        public ReactiveCommand<Unit, TabungBocor> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
