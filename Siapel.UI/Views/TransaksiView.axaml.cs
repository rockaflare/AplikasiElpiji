using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Siapel.UI.Views
{
    public partial class TransaksiView : UserControl
    {
        public TransaksiView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
