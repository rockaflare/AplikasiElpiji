using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels.DialogViewModels;
using System.Collections.Generic;

namespace Siapel.UI.Views.Pages.Dialogs
{
    public partial class TransaksiFieldDialog : ReactiveUserControl<TransaksiFieldViewModel>
    {
        public TransaksiFieldDialog()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
