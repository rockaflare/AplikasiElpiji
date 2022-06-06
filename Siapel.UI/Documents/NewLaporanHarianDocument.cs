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
    public class NewLaporanHarianDocument : IDocument
    {
        private List<object>? _lapLimaPuluhList;
        private List<object>? _lapDuaBelasList;
        private List<object>? _lapLimaSetengah;
        private List<LaporanHarian>? _listOfTotals;
        private string? _tanggal;

        public NewLaporanHarianDocument(List<object>? lapLimaPuluhList = null, List<object>? lapDuaBelasList = null, List<object>? lapLimaSetengah = null, string? tanggal = null, List<LaporanHarian>? listOfTotals = null)
        {
            _lapLimaPuluhList = lapLimaPuluhList;
            _lapDuaBelasList = lapDuaBelasList;
            _lapLimaSetengah = lapLimaSetengah;
            _listOfTotals = listOfTotals;
            _tanggal = tanggal;
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4.Landscape());

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                }
                );
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(26).SemiBold();

            container
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().AlignCenter().Text("Laporan Harian").Style(titleStyle);

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
            container.PaddingVertical(15).Column(column =>
            {
                column.Spacing(5);

                column.Item().Text("50 KG");
                column.Item().Element(ComposeTableLimaPuluh);

                column.Item().Text("12 KG");
                column.Item().Element(ComposeTableDuaBelas);

                column.Item().Text("5,5 KG");
                column.Item().Element(ComposeTableLimaSetengah);

                column.Item().Text("");
                column.Item().Element(ComposeTableTotalPenjualan);
            });
        }

        void ComposeTableLimaPuluh(IContainer container)
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
                    columns.ConstantColumn(30);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().ExtendHorizontal().Element(CellStyle).Text("No.");
                    header.Cell().Element(CellStyle).Text("Tuan/ Toko");
                    header.Cell().Element(CellStyle).Text("Harga");
                    header.Cell().Element(CellStyle).Text("Jumlah");
                    header.Cell().Element(CellStyle).Text("Tunai");
                    header.Cell().Element(CellStyle).Text("Transfer");
                    header.Cell().Element(CellStyle).Text("Invoice");
                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                if (_lapLimaPuluhList != null)
                {
                    foreach (var item in _lapLimaPuluhList)
                    {
                        var nomor = _lapLimaPuluhList.IndexOf(item) + 1;
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var jumlah = item.GetType().GetProperty("Jumlah").GetValue(item);
                        var harga = item.GetType().GetProperty("Harga").GetValue(item);
                        var tunai = item.GetType().GetProperty("Tunai").GetValue(item);
                        var transfer = item.GetType().GetProperty("Transfer").GetValue(item);
                        var invoice = item.GetType().GetProperty("Invoice").GetValue(item);
                        var total = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tunai).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(transfer).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(invoice).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Element(CellStyle).Text("Total penjualan : ");
                    if (_listOfTotals != null && _listOfTotals.Count > 0)
                    {
                        var jumlah = _listOfTotals.Where(x => x.Item == "50 KG").Select(x => x.Jumlah).First();
                        var tunai = _listOfTotals.Where(x => x.Item == "50 KG").Select(x => x.Tunai).First();
                        var transfer = _listOfTotals.Where(x => x.Item == "50 KG").Select(x => x.Transfer).First();
                        var invoice = _listOfTotals.Where(x => x.Item == "50 KG").Select(x => x.Invoice).First();
                        var total = _listOfTotals.Where(x => x.Item == "50 KG").Select(x => x.Total).First();

                        footer.Cell().Element(CellStyle).Text(jumlah);
                        footer.Cell().Element(CellStyle).Text(tunai);
                        footer.Cell().Element(CellStyle).Text(transfer);
                        footer.Cell().Element(CellStyle).Text(invoice);
                        footer.Cell().Element(CellStyle).Text(total);
                    }
                    else
                    {
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                    }

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

            });
        }

        void ComposeTableDuaBelas(IContainer container)
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
                    columns.ConstantColumn(30);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().ExtendHorizontal().Element(CellStyle).Text("No.");
                    header.Cell().Element(CellStyle).Text("Tuan/ Toko");
                    header.Cell().Element(CellStyle).Text("Harga");
                    header.Cell().Element(CellStyle).Text("Jumlah");
                    header.Cell().Element(CellStyle).Text("Tunai");
                    header.Cell().Element(CellStyle).Text("Transfer");
                    header.Cell().Element(CellStyle).Text("Invoice");
                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                if (_lapDuaBelasList != null)
                {
                    foreach (var item in _lapDuaBelasList)
                    {
                        var nomor = _lapLimaPuluhList.IndexOf(item) + 1;
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var jumlah = item.GetType().GetProperty("Jumlah").GetValue(item);
                        var harga = item.GetType().GetProperty("Harga").GetValue(item);
                        var tunai = item.GetType().GetProperty("Tunai").GetValue(item);
                        var transfer = item.GetType().GetProperty("Transfer").GetValue(item);
                        var invoice = item.GetType().GetProperty("Invoice").GetValue(item);
                        var total = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tunai).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(transfer).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(invoice).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Element(CellStyle).Text("Total penjualan : ");
                    if (_listOfTotals != null && _listOfTotals.Count > 0)
                    {
                        var jumlah = _listOfTotals.Where(x => x.Item == "12 KG").Select(x => x.Jumlah).First();
                        var tunai = _listOfTotals.Where(x => x.Item == "12 KG").Select(x => x.Tunai).First();
                        var transfer = _listOfTotals.Where(x => x.Item == "12 KG").Select(x => x.Transfer).First();
                        var invoice = _listOfTotals.Where(x => x.Item == "12 KG").Select(x => x.Invoice).First();
                        var total = _listOfTotals.Where(x => x.Item == "12 KG").Select(x => x.Total).First();

                        footer.Cell().Element(CellStyle).Text(jumlah);
                        footer.Cell().Element(CellStyle).Text(tunai);
                        footer.Cell().Element(CellStyle).Text(transfer);
                        footer.Cell().Element(CellStyle).Text(invoice);
                        footer.Cell().Element(CellStyle).Text(total);
                    }
                    else
                    {
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                    }

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

            });
        }

        void ComposeTableLimaSetengah(IContainer container)
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
                    columns.ConstantColumn(30);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().ExtendHorizontal().Element(CellStyle).Text("No.");
                    header.Cell().Element(CellStyle).Text("Tuan/ Toko");
                    header.Cell().Element(CellStyle).Text("Harga");
                    header.Cell().Element(CellStyle).Text("Jumlah");
                    header.Cell().Element(CellStyle).Text("Tunai");
                    header.Cell().Element(CellStyle).Text("Transfer");
                    header.Cell().Element(CellStyle).Text("Invoice");
                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                if (_lapLimaSetengah != null)
                {
                    foreach (var item in _lapLimaSetengah)
                    {
                        var nomor = _lapLimaPuluhList.IndexOf(item) + 1;
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var jumlah = item.GetType().GetProperty("Jumlah").GetValue(item);
                        var harga = item.GetType().GetProperty("Harga").GetValue(item);
                        var tunai = item.GetType().GetProperty("Tunai").GetValue(item);
                        var transfer = item.GetType().GetProperty("Transfer").GetValue(item);
                        var invoice = item.GetType().GetProperty("Invoice").GetValue(item);
                        var total = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tunai).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(transfer).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(invoice).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Element(CellStyle).Text("Total penjualan : ");
                    if (_listOfTotals != null && _listOfTotals.Count > 0)
                    {
                        var jumlah = _listOfTotals.Where(x => x.Item == "5,5 KG").Select(x => x.Jumlah).First();
                        var tunai = _listOfTotals.Where(x => x.Item == "5,5 KG").Select(x => x.Tunai).First();
                        var transfer = _listOfTotals.Where(x => x.Item == "5,5 KG").Select(x => x.Transfer).First();
                        var invoice = _listOfTotals.Where(x => x.Item == "5,5 KG").Select(x => x.Invoice).First();
                        var total = _listOfTotals.Where(x => x.Item == "5,5 KG").Select(x => x.Total).First();

                        footer.Cell().Element(CellStyle).Text(jumlah);
                        footer.Cell().Element(CellStyle).Text(tunai);
                        footer.Cell().Element(CellStyle).Text(transfer);
                        footer.Cell().Element(CellStyle).Text(invoice);
                        footer.Cell().Element(CellStyle).Text(total);
                    }
                    else
                    {
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                        footer.Cell().Element(CellStyle).Text("");
                    }

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

            });
        }

        void ComposeTableTotalPenjualan(IContainer container)
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
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {                    
                    if (_listOfTotals != null && _listOfTotals.Count > 0)
                    {
                        var tunai = _listOfTotals.Sum(x => x.TunaiInt).ToString("Rp #,#");
                        var transfer = _listOfTotals.Sum(x => x.TransferInt).ToString("Rp #,#");
                        var invoice = _listOfTotals.Sum(x => x.InvoiceInt).ToString("Rp #,#");
                        var total = _listOfTotals.Sum(x => x.TotalInt).ToString("Rp #,#");

                        header.Cell().RowSpan(2).Element(CellStyle).Text("Total Penjualan Keseluruhan");

                        header.Cell().Element(CellStyle).Text("Tunai");
                        header.Cell().Element(CellStyle).Text("Transfer");
                        header.Cell().Element(CellStyle).Text("Invoice");

                        header.Cell().RowSpan(2).Element(CellStyle).Text(total);

                        header.Cell().Element(CellStyle).Text(tunai);
                        header.Cell().Element(CellStyle).Text(transfer);
                        header.Cell().Element(CellStyle).Text(invoice);

                        
                    }                


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });
            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    }
}
