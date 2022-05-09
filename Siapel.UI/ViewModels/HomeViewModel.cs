﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class HomeViewModel : ReactiveObject, IRoutableViewModel
    {
        string Lah = "Home View";

        public string? UrlPathSegment => Lah;

        public IScreen HostScreen { get; }

        public HomeViewModel(IScreen screen) => HostScreen = screen;
    }
}
