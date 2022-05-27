using QuestPDF.Fluent;
using QuestPDF.Previewer;
using ReactiveUI;
using Siapel.UI.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.ViewModels
{
    public class LaporanViewModel : ReactiveObject, IRoutableViewModel
    {
        string Title = "Laporan";
        public string? UrlPathSegment => Title;

        public IScreen HostScreen { get; }
        public LaporanViewModel(IScreen screen)
        {
            HostScreen = screen;
            
        }

        public void GenerateLaporanCommand()
        {
            var document = new InvoiceDocument();
            document.GeneratePdf("Invoice-Tes-1.pdf");
        }
        string path = "/image-0.png";
        public string ImagePathLoc => "/Assets/avalonia-logo.ico";
    }
}
