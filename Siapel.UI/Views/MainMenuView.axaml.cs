using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using System.Collections.Generic;

namespace Siapel.UI.Views
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        //private List<NavigationViewItem> GetNavigationViewItems()
        //{
        //    return new List<NavigationViewItem>
        //    {
        //        new NavigationViewItem
        //        {
        //            Content = "Home",
        //            Tag = typeof(MainMenuView),
        //            Icon = new IconSourceElement { IconSource = (IconSource)this.FindResource("HomeIcon")},
        //            Classes = {"MainMenuNav"}
        //        },
        //        new NavigationViewItem
        //        {
        //            Content = "Home",
        //            Tag = typeof(MainMenuView),
        //            Icon = new IconSourceElement { IconSource = (IconSource)this.FindResource("HomeIcon")},
        //            Classes = {"MainMenuNav"}
        //        },
        //        new NavigationViewItem
        //        {
        //            Content = "Home",
        //            Tag = typeof(MainMenuView),
        //            Icon = new IconSourceElement { IconSource = (IconSource)this.FindResource("HomeIcon")},
        //            Classes = {"MainMenuNav"}
        //        },
        //    };
        //}
    }
}
