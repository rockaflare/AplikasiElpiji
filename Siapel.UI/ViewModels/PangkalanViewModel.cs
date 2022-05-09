using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class PangkalanViewModel : ReactiveObject, IRoutableViewModel
    {
        string Lah = "Pangkalan";
        public string? UrlPathSegment => Lah;

        public IScreen HostScreen { get; }

        public PangkalanViewModel(IScreen screen) => HostScreen = screen;
    }
}
