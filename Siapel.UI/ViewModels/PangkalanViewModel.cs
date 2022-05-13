using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Siapel.Domain.Models;
using Siapel.Domain.Services;
using Siapel.EF.DataServices.Core;
using Siapel.EF.Services;
using Siapel.UI.ViewModels.DialogViewModels;
using Siapel.UI.Views.Pages.Dialogs;
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
        private readonly IDataService<Pangkalan> dataService;
        public string? UrlPathSegment => Lah;
        public IScreen HostScreen { get; }

        public IEnumerable<Pangkalan> Pangkalans => _pangkalan;
        

        public PangkalanViewModel(IScreen screen)
        {
            HostScreen = screen;
            dataService = new GenericDataService<Pangkalan>(new EF.SiapelDbContextFactory());
            JalaninAjaDulu();
            DeleteItem = ReactiveCommand.CreateFromTask(DeleteItemAsync);
        }

        private async Task JalaninAjaDulu()
        {            
            var dataList = await dataService.GetAll();
            _pangkalan = new ObservableCollection<Pangkalan>(dataList);
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
            await dataService.Delete(SelectedPangkalan);
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
                        await dataService.Create(model);
                    }

                    await HostScreen.Router.NavigateAndReset.Execute(new PangkalanViewModel(this.HostScreen));
                });

            await HostScreen.Router.Navigate.Execute(vm);
        }
    }
}
