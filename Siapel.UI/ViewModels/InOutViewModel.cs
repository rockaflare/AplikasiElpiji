using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class InOutViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly IDataService<StokAwal> _stokAwalService;
        private readonly IDataService<Pemasukan> _pemasukanService;
        private readonly ITransaksiDataService _transaksiDataService;
        private StokAwal _stokAwal;
        private List<object> _stokInOut { get; } = new List<object>();
        private ReactiveCommand<Unit, Unit> LoadItem { get; }

        public string? UrlPathSegment => "In Out";
        public IScreen HostScreen { get; }
        public List<object> StokInOut => _stokInOut;


        public InOutViewModel(IScreen screen, IDataService<StokAwal> stokAwalService, IDataService<Pemasukan> pemasukanService, ITransaksiDataService transaksiDataService)
        {
            HostScreen = screen;
            _stokAwalService = stokAwalService;
            _pemasukanService = pemasukanService;
            _transaksiDataService = transaksiDataService;
            _selectedTanggal = DateTimeOffset.Now;
            LoadItem = ReactiveCommand.CreateFromTask(GetStokAwal);
            LoadItem.Execute();
        }

        private DateTimeOffset _selectedTanggal;
        public DateTimeOffset SelectedTanggal
        {
            get => _selectedTanggal;
            set => this.RaiseAndSetIfChanged(ref _selectedTanggal, value);
        }

        private async Task GetStokAwal()
        {
            var stokAwal = await _stokAwalService.GetAll();
            
        }

    }
}
