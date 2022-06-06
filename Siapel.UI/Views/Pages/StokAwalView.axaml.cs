using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels;

namespace Siapel.UI.Views.Pages
{
    public partial class StokAwalView : ReactiveUserControl<StokAwalViewModel>
    {
        public StokAwalView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
