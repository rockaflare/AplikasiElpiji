using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.EF.DataServices.Core;
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
        private readonly ObservableCollection<Pangkalan> _pangkalan;
        public string? UrlPathSegment => Lah;
        public IScreen HostScreen { get; }

        public IEnumerable<Pangkalan> Pangkalans => _pangkalan;
        

        public PangkalanViewModel(IScreen screen)
        {
            HostScreen = screen;
            _pangkalan = new ObservableCollection<Pangkalan>();
        }
    }
}
