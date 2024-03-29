﻿using ReactiveUI;
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
        public string? UrlPathSegment => _title;
        public IScreen HostScreen { get; }
        public List<string> ItemList => _itemList;

        public PemasukanFieldViewModel(IScreen screen, string title)
        {
            HostScreen = screen;
            _title = title;
            _itemList = new List<string>() { "50 KG", "12 KG", "5,5 KG" };
            SetField();
            var okEnabled = this.WhenAnyValue(x => x.Item, x => x.Jumlah, (db, ls) => !string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(ls.ToString()));
            Save = ReactiveCommand.Create(
                () => new Pemasukan { Tanggal = Tanggal?.Date, Item = Item, Jumlah = Jumlah }, okEnabled
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

        private void SetField()
        {            
            _tanggal = DateTimeOffset.Now;            
        }

        public ReactiveCommand<Unit, Pemasukan> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
