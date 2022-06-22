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
    public class PemasukanViewModel : ReactiveObject, IRoutableViewModel
    {//TODO
        private IDataService<Pemasukan> _dataService;
        private ObservableCollection<Pemasukan> _pemasukan { get; } = new ObservableCollection<Pemasukan>();
        public string? UrlPathSegment => "Pemasukan";

        public IScreen HostScreen { get; }
        public IEnumerable<Pemasukan> Pemasukan => _pemasukan;

        private ReactiveCommand<Unit, Unit> LoadItem { get; }
        private ReactiveCommand<Unit, Unit> DeleteItem { get; }

        public PemasukanViewModel(IScreen screen, IDataService<Pemasukan> dataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            LoadItem = ReactiveCommand.CreateFromTask(PemasukanItemLoad);
            LoadItem.Execute();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteConfirmation);
        }

        private async Task PemasukanItemLoad()
        {
            _pemasukan.Clear();
            if (_dataService != null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _pemasukan.Add(item);
                }
            }
        }

        private Pemasukan _selectedPemasukan;
        public Pemasukan SelectedPemasukan
        {
            get => _selectedPemasukan;
            set => this.RaiseAndSetIfChanged(ref _selectedPemasukan, value);
        }

        private async void DeleteItemAsync()
        {
            await _dataService.Delete(SelectedPemasukan);         
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

            if (SelectedPemasukan != null)
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
            var vm = new PemasukanFieldViewModel(this.HostScreen, "Tambah Pemasukan");

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (Pemasukan)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);                
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new PemasukanViewModel(this.HostScreen, _dataService));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }
    }
}
