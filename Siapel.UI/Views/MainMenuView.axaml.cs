using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using System;
using System.Collections.Generic;

namespace Siapel.UI.Views
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();

            var nv = this.FindControl<NavigationView>("MainMenuNav");
            nv.SelectionChanged += MainMenuSelectionChanged;
            nv.SelectedItem = nv.MenuItems.ElementAt(0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void MainMenuSelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
        {
            if (e.IsSettingsSelected)
            {

            }
            else if (e.SelectedItem is NavigationViewItem nvi)
            {                
                var menuPage = $"Siapel.UI.Views.Pages.{nvi.Tag}";
                
                if (Type.GetType(menuPage) != null)
                {
                    var pg = Activator.CreateInstance(Type.GetType(menuPage));
                    (sender as NavigationView).Content = pg;
                }                
            }
        }
    }
}
