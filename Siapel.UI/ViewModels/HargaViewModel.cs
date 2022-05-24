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
using Avalonia.ReactiveUI;
using FluentAvalonia.UI.Controls;

namespace Siapel.UI.ViewModels
{
    public class HargaViewModel : ReactiveObject, IRoutableViewModel
    {
        private ObservableCollection<Harga> _harga { get; } = new ObservableCollection<Harga>();
        private readonly IDataService<Harga> _dataService;
        private readonly IPangkalanDataService _pangkalanService;
        string Lah = "Harga Tabung";
        public string? UrlPathSegment => Lah;

        public IScreen HostScreen { get; }
        public IEnumerable<Harga> Harga => _harga;
        
        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> DeleteItem { get; }

        public HargaViewModel(IScreen screen, IDataService<Harga> dataService, IPangkalanDataService pangkalanService)
        {
            HostScreen = screen;
            _dataService = dataService;
            _pangkalanService = pangkalanService;
            LoadItem = ReactiveCommand.CreateFromTask(HargaUpdater);
            LoadItem.Execute();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteConfirmation);
        }

        private async Task HargaUpdater()
        {
            _harga.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _harga.Add(item);
                }
            }
        }

        private Harga _selectedHarga;
        public Harga SelectedHarga
        {
            get => _selectedHarga;
            set => this.RaiseAndSetIfChanged(ref _selectedHarga, value);
        }

        private async void DeleteItemAsync()
        {
            await _dataService.Delete(SelectedHarga);
            await LoadItem.Execute();
        }

        private async Task DeleteConfirmation()
        {
            var dialog = new ContentDialog()
            {
                Title = "Hapus item",
                Content = "Anda yakin ingin menghapus?",
                PrimaryButtonText = "Ok",
                CloseButtonText = "Batal"
            };

            if (SelectedHarga != null)
            {
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
            var pangkalans = await _pangkalanService.GetAll();
            var vm = new HargaFieldViewModel(this.HostScreen, "Tambah Harga", new List<Pangkalan>(pangkalans));

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (Harga)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new HargaViewModel(this.HostScreen, _dataService, _pangkalanService));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }
        public async void UpdateCommand()
        {
            if (SelectedHarga != null)
            {
                var pangkalans = await _pangkalanService.GetAll();
                var vm = new HargaFieldViewModel(this.HostScreen, "Edit Harga", new List<Pangkalan>(pangkalans), SelectedHarga);

                Observable.Merge(
                    vm.Save,
                    vm.Cancel.Select(_ => (Harga)null))
                    .Take(1)
                    .Subscribe(async model =>
                    {
                        if (model != null)
                        {
                            await _dataService.Update(model);
                        }

                        await HostScreen.Router.NavigateAndReset.Execute(new HargaViewModel(this.HostScreen, _dataService, _pangkalanService));
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
