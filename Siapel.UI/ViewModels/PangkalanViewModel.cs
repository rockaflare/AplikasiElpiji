using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF.DataServices.Core;
using Siapel.EF.Services;
using Siapel.UI.ViewModels.DialogViewModels;
using Siapel.UI.Views.Pages.Dialogs;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class PangkalanViewModel : ReactiveObject, IRoutableViewModel
    {
        string Lah = "Pangkalan";
        private ObservableCollection<Pangkalan> _pangkalan { get; } = new ObservableCollection<Pangkalan>();
        private readonly IPangkalanDataService _dataService;
        public string? UrlPathSegment => Lah;
        public IScreen HostScreen { get; }

        public IEnumerable<Pangkalan> Pangkalans => _pangkalan;
        public ReactiveCommand<Unit, Unit> DeleteItem { get; }
        public ReactiveCommand<Unit, Unit> LoadItem { get; }

        public PangkalanViewModel(IScreen screen, IPangkalanDataService dataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            LoadItem = ReactiveCommand.CreateFromTask(JalaninAjaDulu);
            LoadItem.Execute();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteConfirmation);

            _hasilPencarian = this
                .WhenAnyValue(x => x.CariPangkalan)
                .Throttle(TimeSpan.FromMilliseconds(800))
                .Select(cari => cari?.Trim())
                .DistinctUntilChanged()
                //.Where(cari => !string.IsNullOrWhiteSpace(cari))
                .SelectMany(PangkalanSearch)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToProperty(this, x => x.HasilPencarian);

            _hasilPencarian.ThrownExceptions.Subscribe(error => { });
        }

        private async Task JalaninAjaDulu()
        {
            _pangkalan.Clear();
            if (_dataService!=null)
            {
                var dataList = await _dataService.GetAll();
                foreach (var item in dataList)
                {
                    _pangkalan.Add(new Pangkalan { Id = item.Id, Nama = item.Nama, Perma = item.Perma, Status = item.Status });
                }
            }            
        }

        private Pangkalan _selectedPangkalan = new Pangkalan();

        public Pangkalan SelectedPangkalan
        {
            get => _selectedPangkalan;
            private set => this.RaiseAndSetIfChanged(ref _selectedPangkalan, value);
        }

        private string _cariPangkalan;
        public string CariPangkalan
        {
            get => _cariPangkalan;
            set => this.RaiseAndSetIfChanged(ref _cariPangkalan, value);
        }

        private readonly ObservableAsPropertyHelper<IEnumerable<Pangkalan>> _hasilPencarian;
        public IEnumerable<Pangkalan> HasilPencarian => _hasilPencarian.Value;
        //not yet implemented
        private async Task<IEnumerable<Pangkalan>> PangkalanSearch(string term)
        {
            var dataList = term != null ? Pangkalans.Where(p => p.Nama.ToLower().Contains(term)).ToList() : Pangkalans;
            return await Task.FromResult(dataList);
        }

        private async void DeleteItemAsync()
        {
            await _dataService.Delete(SelectedPangkalan);
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
            
            if (SelectedPangkalan != null)
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
            var vm = new AddPangkalanViewModel(this.HostScreen);

            Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (Pangkalan)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Create(model);
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new PangkalanViewModel(this.HostScreen, _dataService));                    
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }

        public async void UpdateCommand()
        {
            if (SelectedPangkalan != null)
            {
                var vm = new AddPangkalanViewModel(this.HostScreen, SelectedPangkalan);

                Observable.Merge(
                vm.Save,
                vm.Cancel.Select(_ => (Pangkalan)null))
                .Take(1)
                .Subscribe(async model =>
                {
                    if (model != null)
                    {
                        await _dataService.Update(model);
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new PangkalanViewModel(this.HostScreen, _dataService));
                });

                await HostScreen.Router.NavigateAndReset.Execute(vm);
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
