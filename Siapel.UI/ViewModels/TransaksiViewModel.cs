using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class TransaksiViewModel : ReactiveObject, IRoutableViewModel
    {
        public string? UrlPathSegment => "Transaksi";

        public IScreen HostScreen { get; }

        public TransaksiViewModel(IScreen screen)
        {
            HostScreen = screen;
        }
    }
}
