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
        }

        private async Task JalaninAjaDulu()
        {            
            var dataList = await dataService.GetAll();
            _pangkalan = new ObservableCollection<Pangkalan>(dataList);
        }

        public async void AddCommand()
        {
            var dialog = new ContentDialog()
            {
                Title = "Tambah Pangkalan",
                PrimaryButtonText = "Simpan",
                CloseButtonText = "Batal"
            };

            var vm = new AddPangkalanViewModel(dialog);

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
                });

            dialog.Content = new AddPangkalan()
            {
                DataContext = vm
            };
            

            _ = await dialog.ShowAsync();

            
        }
    }
}
