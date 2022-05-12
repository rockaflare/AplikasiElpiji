using FluentAvalonia.UI.Controls;
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
    public class AddPangkalanViewModel : ViewModelBase
    {
        private readonly ContentDialog _dialog;
        private readonly IDataService<Pangkalan> _dataService;

        public AddPangkalanViewModel(ContentDialog dialog)
        {
            _dialog = dialog;
            _dataService = new PangkalanDataService(new EF.SiapelDbContextFactory());
            var okEnabled = this.WhenAnyValue(x => x.NamaPangkalan, x => !string.IsNullOrWhiteSpace(x));
            Save = ReactiveCommand.Create(
                () => new Pangkalan { Nama = NamaPangkalan, Status = Status, Perma = true }, okEnabled);
            Cancel = ReactiveCommand.Create(() => { });
            dialog.Closed += DialogOnClosed;
        }

        private void DialogOnClosed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            _dialog.Closed -= DialogOnClosed;

            switch (args.Result)
            {
                case ContentDialogResult.None:
                    Cancel.Execute();
                    break;
                case ContentDialogResult.Primary:
                    Save.Execute();
                    break;
                case ContentDialogResult.Secondary:
                    break;
                default:
                    break;
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

        public ReactiveCommand<Unit, Pangkalan> Save { get; set; }
        public ReactiveCommand<Unit, Unit> Cancel { get; set; }
    }
}
