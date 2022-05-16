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
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class PangkalanViewModel : ReactiveObject, IRoutableViewModel
    {
        string Lah = "Pangkalan";
        private ObservableCollection<Pangkalan> _pangkalan;
        private readonly IDataService<Pangkalan> _dataService;
        public string? UrlPathSegment => Lah;
        public IScreen HostScreen { get; }

        public IEnumerable<Pangkalan> Pangkalans => _pangkalan;
        

        public PangkalanViewModel(IScreen screen, IDataService<Pangkalan> dataService)
        {
            HostScreen = screen;
            _dataService = dataService;
            JalaninAjaDulu();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteItemAsync);
        }

        private async Task JalaninAjaDulu()
        {
            if (_dataService!=null)
            {
                var dataList = await _dataService.GetAll();
                _pangkalan = new ObservableCollection<Pangkalan>(dataList);
            }            
        }

        private Pangkalan _selectedPangkalan = new Pangkalan();

        public Pangkalan SelectedPangkalan
        {
            get => _selectedPangkalan;
            private set => this.RaiseAndSetIfChanged(ref _selectedPangkalan, value);
        }

        public ReactiveCommand<Unit, Unit> DeleteItem { get; }

        private async Task DeleteItemAsync()
        {
            await _dataService.Delete(SelectedPangkalan);
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
    }
}
