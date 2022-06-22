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
    public class TabungBocorViewModel : ReactiveObject, IRoutableViewModel
    {
        private IDataService<TabungBocor> _dataService;
        private IDataService<StokAwal> _stokAwalService;
        private ObservableCollection<TabungBocor> _tabungBocor { get; } = new ObservableCollection<TabungBocor>();
        private List<StokAwal> _stokAwalList { get; } = new List<StokAwal>();
        public string? UrlPathSegment => "Tabung Bocor";
        public IScreen HostScreen { get; }
        public IEnumerable<TabungBocor> TabungBocor => _tabungBocor;

        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> DeleteItem { get; }

        public TabungBocorViewModel(IScreen screen, IDataService<TabungBocor> dataService, IDataService<StokAwal> stokAwalService)
        {
            HostScreen = screen;
            _dataService = dataService;
            _stokAwalService = stokAwalService;
            LoadItem = ReactiveCommand.CreateFromTask(TabungBocorItemLoad);
            LoadItem.Execute();

            DeleteItem = ReactiveCommand.CreateFromTask(DeleteConfirmation);
        }

        private async Task TabungBocorItemLoad()
        {
            _tabungBocor.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _tabungBocor.Add(item);
                }
            }
        }

        private TabungBocor _selectedTabungBocor;
        public TabungBocor SelectedTabungBocor
        {
            get => _selectedTabungBocor;
            set => this.RaiseAndSetIfChanged(ref _selectedTabungBocor, value);
        }

        private async void DeleteItemAsync()
        {
            await _dataService.Delete(SelectedTabungBocor);
            await LoadItem.Execute();
        }

        private async Task DeleteConfirmation()
        {
            var dialog = new ContentDialog()
            {
                Title = "Hapus item",
                Content = "Anda yakin ingin menghapus?",
                PrimaryButtonText = "Ok"
            };

            if (SelectedTabungBocor != null)
            {
                dialog.CloseButtonText = "Batal";
                var result = await dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    DeleteItemAsync();
                }
            }
            else
            {
                dialog.Content = "Tidak ada item dipilih";
                await dialog.ShowAsync();
            }
        }
        public async void AddCommand()
        {
            var vm = new TabungBocorFieldViewModel(this.HostScreen, "Tambah Tabung Bocor");

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (TabungBocor)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new TabungBocorViewModel(this.HostScreen, _dataService, _stokAwalService));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }
    }
}
