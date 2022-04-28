using Avalonia.Controls;
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
            Categories = new List<CategoryBase>();

            Categories.Add(new Category { Name = "Home", Icon = Symbol.Home });
            Categories.Add(new Category { Name = "Item", Icon = Symbol.List });

            SelectedCategory = Categories[0];
        }

        public List<CategoryBase> Categories { get; }

        public object SelectedCategory
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
            if (SelectedCategory is Category cat)
            {
                var index = Categories.IndexOf(cat) + 1;
                var menuPage = $"Siapel.UI.Views.Pages.{cat.Name}View";
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

    public abstract class CategoryBase { }
    public class Category : CategoryBase
    {
        public string Name { get; set; }
        public Symbol Icon { get; set; }
    }
}
