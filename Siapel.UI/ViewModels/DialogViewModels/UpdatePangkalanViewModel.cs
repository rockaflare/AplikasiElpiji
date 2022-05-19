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
    public class UpdatePangkalanViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Tambah Pangkalan";
        public IScreen HostScreen { get; }
        private Pangkalan _pangkalan;
        public UpdatePangkalanViewModel(IScreen hostScreen, Pangkalan pangkalan)
        {
            HostScreen = hostScreen;
            _pangkalan = pangkalan;
        }

        public ReactiveCommand<Unit, Pangkalan> Save { get; }
        public ReactiveCommand<Unit, Pangkalan> Cancel { get; }
    }
}
