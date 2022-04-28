using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using Avalonia.ReactiveUI;
using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Siapel.UI.Views.Pages;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel()
        {

        }

        public object SelectedPage
        {
            get => _selectedCategory;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedCategory, value);
                SetCurrentPage();
            }
        }

        private void SetCurrentPage()
        {
            if (SelectedPage is NavigationViewItem nvi)
            {
                var menuPage = $"Siapel.UI.Views.Pages.{nvi.Content}View";
                if (Type.GetType(menuPage) != null)
                {
                    var pg = Activator.CreateInstance(Type.GetType(menuPage));
                    CurrentPage = (IControl)pg;
                }
            }
        }

        public IControl CurrentPage
        {
            get => _currentPage;
            set => this.RaiseAndSetIfChanged(ref _currentPage, value);
        }

        

        private object _selectedCategory;
        private IControl _currentPage = new HomeView();
        
    }

}
