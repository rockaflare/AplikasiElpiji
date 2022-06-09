using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels;
using Siapel.UI.ViewModels.DialogViewModels;

namespace Siapel.UI.Views.Pages.Dialogs
{
    public partial class StokAwalFieldDialog : ReactiveUserControl<StokAwalFieldViewModel>
    {
        public StokAwalFieldDialog()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
