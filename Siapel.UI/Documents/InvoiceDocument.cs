﻿using QuestPDF.Drawing;
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
        private List<object>? _invoiceRekapData;
        private string _tanggal;
        private string _invoiceGrandTotal;
        private string _invoiceGrandTotalLP;
        private string _invoiceGrandTotalDB;
        private string _invoiceGrandTotalLS;
        public InvoiceDocument(List<object>? invoiceData = null, List<object>? invoiceRekapData = null, string tanggal = null, string invoiceGrandTotal = null, string invoiceGrandTotalLP = null, string invoiceGrandTotalDB = null, string invoiceGrandTotalLS = null)
        {
            _invoiceData = invoiceData;
            _invoiceRekapData = invoiceRekapData;
            _tanggal = tanggal;
            _invoiceGrandTotal = invoiceGrandTotal;
            _invoiceGrandTotalLP = invoiceGrandTotalLP;
            _invoiceGrandTotalDB = invoiceGrandTotalDB;
            _invoiceGrandTotalLS = invoiceGrandTotalLS;
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
            var titleStyle = TextStyle.Default.FontSize(16).SemiBold();

            container
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Invoice").Style(titleStyle);

                        column.Item().Text(text =>
                        {
                            text.Span("Tanggal : ").FontSize(9).SemiBold();
                            text.Span(_tanggal).FontSize(9);
                        });
                    });
                });
        }
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(30).Column(column =>
            {
                column.Spacing(5);

                column.Item().Text("Invoice Detail").FontSize(9);
                column.Item().Element(ComposeInvoiceDetailTable);
                column.Item().Text("");
                column.Item().Text("Invoice Rekap").FontSize(9);
                column.Item().Element(ComposeInvoiceTotalTable);
            });
        }
        void ComposeInvoiceDetailTable(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(9).NormalWeight();
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
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().RowSpan(2).ExtendHorizontal().Element(CellStyle).Text("Tanggal").FontSize(9);
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Tuan/ Toko").FontSize(9);

                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("50 KG").FontSize(9);
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("12 KG").FontSize(9);
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("5,5 KG").FontSize(9);

                    header.Cell().RowSpan(2).Element(CellStyle).Text("Total").FontSize(9);

                    header.Cell().Element(CellStyle).Text("Qty").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);
                    
                    header.Cell().Element(CellStyle).Text("Qty").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);
                    
                    header.Cell().Element(CellStyle).Text("Qty").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);

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
                        var totalsemua = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).ExtendHorizontal().Text(tanggal).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(jml50).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tab50).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(jml12).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tab12).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(jml5).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tab5).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(totalsemua).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

            });
        }
        void ComposeInvoiceTotalTable(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(9).NormalWeight();
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Tuan/ Toko").FontSize(9);

                    header.Cell().Element(CellStyle).Text("50 KG").FontSize(9);
                    header.Cell().Element(CellStyle).Text("12 KG").FontSize(9);
                    header.Cell().Element(CellStyle).Text("5,5 KG").FontSize(9);

                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                });

                if (_invoiceRekapData != null)
                {

                    foreach (var item in _invoiceRekapData)
                    {
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var tab50 = item.GetType().GetProperty("Tab50Kg").GetValue(item);
                        var tab12 = item.GetType().GetProperty("Tab12Kg").GetValue(item);
                        var tab5 = item.GetType().GetProperty("Tab5Kg").GetValue(item);
                        var totalsemua = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(tab50).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(tab12).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(tab5).Style(textStyle);

                        table.Cell().Element(CellStyle).Text(totalsemua).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

                table.Footer(footer =>
                {
                    footer.Cell().Element(CellStyle).Text("Grand Total : ").FontSize(9);
                    footer.Cell().Element(CellStyle).Text(_invoiceGrandTotalLP).FontSize(9);
                    footer.Cell().Element(CellStyle).Text(_invoiceGrandTotalDB).FontSize(9);
                    footer.Cell().Element(CellStyle).Text(_invoiceGrandTotalLS).FontSize(9);
                    footer.Cell().Element(CellStyle).Text(_invoiceGrandTotal).FontSize(9);

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                });

            });
        }
    }
}
