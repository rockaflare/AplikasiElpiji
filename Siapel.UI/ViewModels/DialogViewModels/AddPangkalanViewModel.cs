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
        private string _title;
        public string? UrlPathSegment => _title;
        public IScreen HostScreen { get; }
        private Pangkalan _pangkalan;

        public AddPangkalanViewModel(IScreen screen, string title, Pangkalan pangkalan = null)
        {
            HostScreen = screen;
            _pangkalan = pangkalan;
            _title = title;
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
