using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class HargaViewModel : ReactiveObject, IRoutableViewModel
    {
        string Lah = "Harga Tabung";
        public string? UrlPathSegment => Lah;

        public IScreen HostScreen { get; }

        public HargaViewModel(IScreen screen) => HostScreen = screen;
    }
}
