using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class HomeViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Home - Halaman Utama";

        public IScreen HostScreen { get; }

        public HomeViewModel(IScreen screen) => HostScreen = screen;
    }
}
