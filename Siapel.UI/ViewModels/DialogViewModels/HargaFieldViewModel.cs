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
    public class HargaFieldViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _title;
        public string? UrlPathSegment => _title;
        public IScreen HostScreen { get; }
        private Harga _harga;
        private List<Pangkalan> _pangkalanList;
        public List<Pangkalan> PangkalanList => _pangkalanList;
        public HargaFieldViewModel(IScreen screen, string title, List<Pangkalan> pangkalan, Harga harga = null)
        {
            HostScreen = screen;
            _harga = harga;
            _title = title;
            _pangkalanList = pangkalan;
            SetField();
            var okEnabled = this.WhenAnyValue(x => x.HargaLimaPuluh, x => x.HargaDuaBelas, x => x.HargaLimaSetengah, (lp, db, ls) => !string.IsNullOrWhiteSpace(lp.ToString()) && !string.IsNullOrWhiteSpace(db.ToString()) && !string.IsNullOrEmpty(ls.ToString()));
            Save = ReactiveCommand.Create(
                () => _harga != null ? EditHarga() : new Harga { Pangkalan = Pangkalan, TbLimaPuluh = HargaLimaPuluh, TbDuaBelas = HargaDuaBelas, TbLimaSetengah = HargaLimaSetengah, TanggalUbah = DateTime.Now });
            Cancel = ReactiveCommand.Create(() => { });
            
        }

        //private Pangkalan _selectedPangkalan;
        //public Pangkalan SelectedPangkalan
        //{
        //    get => _selectedPangkalan;
        //    set => this.RaiseAndSetIfChanged(ref _selectedPangkalan, value);
        //}

        private Pangkalan _pangkalan;
        public Pangkalan Pangkalan
        {
            get => _pangkalan;
            set => this.RaiseAndSetIfChanged(ref _pangkalan, value);
        }
        private int _hargaLimaPuluh;
        public int HargaLimaPuluh
        {
            get => _hargaLimaPuluh;
            set => this.RaiseAndSetIfChanged(ref _hargaLimaPuluh, value);
        }
        private int _hargaDuaBelas;
        public int HargaDuaBelas
        {
            get => _hargaDuaBelas;
            set => this.RaiseAndSetIfChanged(ref _hargaDuaBelas, value);
        }
        private int _hargaLimaSetengah;
        public int HargaLimaSetengah
        {
            get => _hargaLimaSetengah;
            set => this.RaiseAndSetIfChanged(ref _hargaLimaSetengah, value);
        }

        private Harga EditHarga()
        {
            _harga.Pangkalan = _pangkalan;
            _harga.TbLimaPuluh = _hargaLimaPuluh;
            _harga.TbDuaBelas = _hargaDuaBelas;
            _harga.TbLimaSetengah = _hargaLimaSetengah;

            return _harga;
        }

        private void SetField()
        {
            if (_harga != null)
            {
                _pangkalan = _harga.Pangkalan;
                _hargaLimaPuluh = _harga.TbLimaPuluh;
                _hargaDuaBelas = _harga.TbDuaBelas;
                _hargaLimaSetengah = _harga.TbLimaSetengah;
            }
        }

        public ReactiveCommand<Unit, Harga> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
