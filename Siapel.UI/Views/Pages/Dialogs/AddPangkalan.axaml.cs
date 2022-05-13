using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels.DialogViewModels;

namespace Siapel.UI.Views.Pages.Dialogs
{
    public partial class AddPangkalan : ReactiveUserControl<AddPangkalanViewModel>
    {
        public AddPangkalan()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
