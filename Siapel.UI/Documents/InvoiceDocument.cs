using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
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
        private List<object>? _invoiceData;
        private string? _tanggal;
        public InvoiceDocument(List<object>? invoiceData = null, string? tanggal = null)
        {
            _invoiceData = invoiceData;
            _tanggal = tanggal;
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                    {
                        page.Margin(50);
                        page.Size(PageSizes.A4);

                        page.Header().Element(ComposeHeader);
                        page.Content().Element(ComposeContent);
                    }
                );
        }
        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(36).SemiBold();

            container
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Invoice").Style(titleStyle);

                        column.Item().Text(text =>
                        {
                            text.Span("Tanggal : ").SemiBold();
                            text.Span(_tanggal);
                        });
                    });
                });
        }
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(30).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTables);
            });
        }
        void ComposeTables(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(11).NormalWeight();
            container.Table(table =>
            {
                IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                {
                    return container
                        .Border(1)
                        .BorderColor(Colors.Grey.Lighten1)
                        .Background(backgroundColor)
                        .PaddingVertical(2)
                        .PaddingHorizontal(4)
                        .AlignCenter()
                        .AlignMiddle()
                        .ShowOnce()
                        ;
                }

                

                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.ConstantColumn(30);
                    columns.RelativeColumn();
                    columns.ConstantColumn(30);
                    columns.RelativeColumn();
                    columns.ConstantColumn(30);
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().RowSpan(2).ExtendHorizontal().Element(CellStyle).Text("Tanggal");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Tuan/ Toko");

                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("50 KG");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("12 KG");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("5,5 KG");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");
                    
                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");
                    
                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                });

                if (_invoiceData != null)
                {
                    
                    foreach (var item in _invoiceData)
                    {
                        var tanggal = item.GetType().GetProperty("Tanggal").GetValue(item);
                        
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var jml50 = item.GetType().GetProperty("Jml50Kg").GetValue(item);
                        var tab50 = item.GetType().GetProperty("Tab50Kg").GetValue(item);
                        var jml12 = item.GetType().GetProperty("Jml12Kg").GetValue(item);
                        var tab12 = item.GetType().GetProperty("Tab12Kg").GetValue(item);
                        var jml5 = item.GetType().GetProperty("Jml5Kg").GetValue(item);
                        var tab5 = item.GetType().GetProperty("Tab5Kg").GetValue(item);

                        table.Cell().Element(CellStyle).ExtendHorizontal().Text(tanggal).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(jml50).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tab50).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(jml12).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tab12).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(jml5).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tab5).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

            });
        }
    }
}
