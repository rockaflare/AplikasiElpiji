using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class LaporanViewModel : ReactiveObject, IRoutableViewModel
    {
        string Title = "Laporan";
        public string? UrlPathSegment => Title;

        public IScreen HostScreen { get; }
        public LaporanViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
