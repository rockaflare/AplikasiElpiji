using FluentAvalonia.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.Services
{
    public class MessageBoxService
    {
        public async void ShowWarning(string message, string caption)
        {
            var dialog = new ContentDialog()
            {
                Title = caption,
                Content = message,
                PrimaryButtonText = "Ok"
            };

            var result = await dialog.ShowAsync();
        }
    }
}
