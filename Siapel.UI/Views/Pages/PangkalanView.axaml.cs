using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Siapel.UI.Views.Pages
{
    public partial class PangkalanView : UserControl
    {
        public PangkalanView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
