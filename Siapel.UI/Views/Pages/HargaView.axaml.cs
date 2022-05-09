using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Siapel.UI.Views.Pages
{
    public partial class HargaView : UserControl
    {
        public HargaView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
