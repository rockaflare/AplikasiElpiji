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
        ViewModelBase content;
        public MainMenuViewModel()
        {
            Content = new HomeViewModel();
        }
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
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
                switch (nvi.Tag)
                {
                    case "Harga":
                        Content = new HargaViewModel();
                        break;
                    default:
                        break;
                }
                //var menuPage = $"Siapel.UI.Views.Pages.{nvi.Tag}View";
                //if (Type.GetType(menuPage) != null)
                //{
                //    var pg = Activator.CreateInstance(Type.GetType(menuPage));
                //    CurrentPage = (IControl)pg;
                //}
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
