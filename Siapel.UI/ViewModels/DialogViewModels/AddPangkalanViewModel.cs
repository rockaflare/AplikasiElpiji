using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF.DataServices.Core;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace Siapel.UI.ViewModels.DialogViewModels
{
    public class AddPangkalanViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Tambah Pangkalan";
        public IScreen HostScreen { get; }
        private Pangkalan _pangkalan;

        public AddPangkalanViewModel(IScreen screen, Pangkalan pangkalan = null)
        {
            HostScreen = screen;
            _pangkalan = pangkalan;
            SetField();
            var okEnabled = this.WhenAnyValue(x => x.NamaPangkalan, x => !string.IsNullOrWhiteSpace(x));
            Save = ReactiveCommand.Create(
                () => _pangkalan != null ? EditPangkalan() : new Pangkalan { Nama = NamaPangkalan, Status = Status, Perma = true }, okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }

        private string _namaPangkalan;

        public string NamaPangkalan
        {
            get => _namaPangkalan;
            set => this.RaiseAndSetIfChanged(ref _namaPangkalan, value);
        }

        private string _status;
        public string Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value);
        }

        readonly ObservableAsPropertyHelper<Pangkalan> _pangkalanUpdate;
        public Pangkalan PangkalanUpdate => _pangkalanUpdate.Value;
        
        private Pangkalan EditPangkalan()
        {
            _pangkalan.Nama = _namaPangkalan;
            _pangkalan.Status = _status;

            return _pangkalan;
        }

        private void SetField()
        {
            if (_pangkalan != null)
            {
                _namaPangkalan = _pangkalan.Nama;
                _status = _pangkalan.Status;
            }
        }

        public ReactiveCommand<Unit, Pangkalan> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
