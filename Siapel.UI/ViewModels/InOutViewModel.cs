using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class InOutViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IDataService<StokAwal> _stokAwalService;
        private readonly IDataService<Pemasukan> _pemasukanService;
        private readonly ITransaksiDataService _transaksiDataService;

        private int _stokAwal;
        private int _stokMasuk;
        private int _stokKeluar;
        private List<object> _stokInOut { get; } = new List<object>();
        public string? UrlPathSegment => "In Out";

        public IScreen HostScreen { get; }
        public List<object> StokInOut => _stokInOut;

        public InOutViewModel(IScreen screen)
        {
            HostScreen = screen;
        }

        

        private async Task GetStokInOut()
        {
            var stokAwal = await _stokAwalService.GetAll();
            var pemasukanTotal = await _pemasukanService.GetAll();
            var transaksiTotal = await _transaksiDataService.GetAll();

            
        }

    }
}
