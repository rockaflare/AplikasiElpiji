using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels.DialogViewModels;
using System.Collections.Generic;

namespace Siapel.UI.Views.Pages.Dialogs
{
    public partial class AddPangkalan : ReactiveUserControl<AddPangkalanViewModel>
    {
        private List<string> StatusList;
        public AddPangkalan()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
            StatusList = new List<string> { "Aktif", "Non-Aktif" };
            ComboBox statusCb = this.Find<ComboBox>("StatusCb");
            statusCb.Items = StatusList;
        }
    }
}
