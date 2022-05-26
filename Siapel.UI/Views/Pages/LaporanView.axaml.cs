using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels;

namespace Siapel.UI.Views.Pages
{
    public partial class LaporanView : ReactiveUserControl<LaporanViewModel>
    {
        public LaporanView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
