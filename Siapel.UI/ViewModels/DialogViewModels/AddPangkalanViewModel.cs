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
        private readonly IDataService<Pangkalan> _dataService;

        public AddPangkalanViewModel(IScreen screen, Pangkalan pangkalan = null)
        {
            HostScreen = screen;
            _dataService = new PangkalanDataService(new EF.SiapelDbContextFactory());
            SetUpdateField(pangkalan);
            var okEnabled = this.WhenAnyValue(x => x.NamaPangkalan, x => !string.IsNullOrWhiteSpace(x));
            Save = ReactiveCommand.Create(
                () => new Pangkalan { Nama = NamaPangkalan, Status = Status, Perma = true }, okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
        }

        private void SetUpdateField(Pangkalan pangkalan)
        {
            if (pangkalan != null)
            {
                _namaPangkalan = pangkalan.Nama;
                _status = pangkalan.Status;
            }            
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

        private void Tes()
        {
            
        }

        public ReactiveCommand<Unit, Pangkalan> Save { get; }
        public ReactiveCommand<Unit, Unit> Cancel { get; }
    }
}
