using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Siapel.UI.Views.Pages
{
    public partial class PembayaranView : UserControl
    {
        public PembayaranView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
