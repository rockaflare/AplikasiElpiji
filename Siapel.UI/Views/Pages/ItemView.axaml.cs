using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Siapel.UI.Views.Pages
{
    public partial class ItemView : UserControl
    {
        public ItemView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
