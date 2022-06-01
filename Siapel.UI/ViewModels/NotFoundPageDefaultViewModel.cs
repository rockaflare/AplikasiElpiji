using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class NotFoundPageDefaultViewModel : ReactiveObject, IRoutableViewModel
    {
        string Lah = "Halaman tidak ditemukan / belum terimplementasi.";

        public string? UrlPathSegment => Lah;

        public IScreen HostScreen { get; }

        public NotFoundPageDefaultViewModel(IScreen screen) => HostScreen = screen;
    }
}
