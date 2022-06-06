using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class StokAwalViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableCollection<StokAwal> _stokAwal { get; } = new ObservableCollection<StokAwal>();
        private readonly IDataService<StokAwal> _dataService;
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        public string? UrlPathSegment => "Stok Awal";

        public IScreen HostScreen { get; }
        public IEnumerable<StokAwal> StokAwal => _stokAwal;

        public StokAwalViewModel(IScreen screen, IDataService<StokAwal> dataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            LoadItem = ReactiveCommand.CreateFromTask(StokAwalUpdater);
            LoadItem.Execute();
        }

        private async Task StokAwalUpdater()
        {
            _stokAwal.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _stokAwal.Add(item);
                }
            }
        }

    }
}
