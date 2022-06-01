using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels.DialogViewModels;

namespace Siapel.UI.Views.Pages.Dialogs
{
    public partial class PemasukanFieldDialog : ReactiveUserControl<PemasukanFieldViewModel>
    {
        public PemasukanFieldDialog()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
