using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.UI.ViewModels.DialogViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
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

        private StokAwal _selectedStokAwal;
        public StokAwal SelectedStokAwal
        {
            get => _selectedStokAwal;
            set => this.RaiseAndSetIfChanged(ref _selectedStokAwal, value);
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
        public async void UpdateCommand()
        {
            if (SelectedStokAwal != null)
            {
                var oldJumlah = SelectedStokAwal.Jumlah;
                var vm = new StokAwalFieldViewModel(this.HostScreen, "Edit StokAwal", SelectedStokAwal);

                Observable.Merge(
                    vm.Save,
                    vm.Cancel.Select(_ => (StokAwal)null))
                    .Take(1)
                    .Subscribe(async model =>
                    {
                        if (model != null)
                        {
                            await _dataService.Update(model);
                        }
                        await HostScreen.Router.NavigateAndReset.Execute(new StokAwalViewModel(this.HostScreen, _dataService));
                    });

                await HostScreen.Router.Navigate.Execute(vm);
            }
            else
            {
                var dialog = new ContentDialog()
                {
                    Title = "Update item",
                    Content = "Tidak ada item dipilih!",
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }

    }
}
