using Siapel.Domain.Models;
using Siapel.Domain.Services.MasterServices;
using Siapel.EF.DataServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        private ItemDataService itemDataService;
        public ItemViewModel(ItemDataService dataService)
        {

        }

        public ObservableCollection<Item> Items { get; }
    }
}
