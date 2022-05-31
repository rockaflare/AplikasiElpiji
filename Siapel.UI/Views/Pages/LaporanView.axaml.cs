using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Siapel.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;

namespace Siapel.UI.Views.Pages
{
    public partial class LaporanView : ReactiveUserControl<LaporanViewModel>
    {
        public LaporanView()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);

            List<FileDialogFilter>? GetFilters()
            {
                
                return new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Name = "Pdf files (.pdf)", Extensions = new List<string> {"pdf"}
                    },
                    new FileDialogFilter
                    {
                        Name = "All files",
                        Extensions = new List<string> {"*"}
                    }
                };
            }
            var results = this.FindControl<TextBox>("ReportSavePath");

            string lastSelectedDirectory = null;

            this.FindControl<Button>("LaporanFileDialog").Click += async delegate
            {
                var result = await new SaveFileDialog()
                {
                    Title = "Simpan File",
                    Filters = GetFilters(),
                    Directory = lastSelectedDirectory,
                    InitialFileName = $"Laporan-{DateTime.Now.ToString("dd-MM-yyyy")}.pdf"
                }.ShowAsync(GetWindow());

                results.Text = result;
            };
        }

        Window GetWindow() => (Window)this.VisualRoot;
    }
}
