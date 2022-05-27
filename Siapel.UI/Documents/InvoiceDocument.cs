using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Siapel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.Documents
{
    public class InvoiceDocument : IDocument
    {
        private IEnumerable<Transaksi>? _invoiceData;
        public InvoiceDocument(IEnumerable<Transaksi>? invoiceData = null, string? tanggal = null)
        {
            _invoiceData = invoiceData;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                    {
                        page.Margin(50);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                    }
                );
        }
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(21).SemiBold();

            container
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Invoice").Style(titleStyle);

                        column.Item().Text(text =>
                        {
                            text.Span("Tanggal : ").SemiBold();
                            text.Span("22 Mei 2022");
                        });
                    });
                });
        }
        void ComposeContent(IContainer container)
        {

        }
    }
}
