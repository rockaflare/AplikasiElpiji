using ReactiveUI;
using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public PangkalanViewModel(IScreen screen) 
        {
            HostScreen = screen;
            Pangkalans = new ObservableCollection<Pangkalan>(GetPangkalan());
        }
        public ObservableCollection<Pangkalan> Pangkalans { get; }
        private IEnumerable<Pangkalan> GetPangkalan() => new[]
        {
            new Pangkalan { Id = 1, Nama = "UMUM", Status = "Aktif", Perma = true },
            new Pangkalan { Id = 2, Nama = "WIJAYA", Status = "Aktif", Perma = false },
            new Pangkalan { Id = 3, Nama = "NIHAYAH", Status = "Aktif", Perma = false },
        };
    }
}
