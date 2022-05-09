using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using ReactiveUI;
using Siapel.UI.ViewModels;
using System;
using System.Collections.Generic;

namespace Siapel.UI.Views
{
    public partial class MainMenuView : ReactiveWindow<MainMenuViewModel>
    {
        public MainMenuView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
