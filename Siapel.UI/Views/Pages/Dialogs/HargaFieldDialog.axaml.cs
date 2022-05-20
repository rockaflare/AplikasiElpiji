using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels.DialogViewModels;

namespace Siapel.UI.Views.Pages.Dialogs
{
    public partial class HargaFieldDialog : ReactiveUserControl<HargaFieldViewModel>
    {
        public HargaFieldDialog()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
