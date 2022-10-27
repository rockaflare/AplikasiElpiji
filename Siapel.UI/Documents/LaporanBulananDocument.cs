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
    public class LaporanBulananDocument : IDocument
    {
        private List<object>? _lapLimaPuluhList;
        private List<object>? _lapDuaBelasList;
        private List<object>? _lapLimaSetengahList;
        private string? _bulan;

        public LaporanBulananDocument(List<object>? lapLimaPuluhList = null, List<object>? lapDuaBelasList = null, List<object>? lapLimaSetengahList = null, string bulan = null)
        {
            _lapLimaPuluhList = lapLimaPuluhList;
            _lapDuaBelasList = lapDuaBelasList;
            _lapLimaSetengahList = lapLimaSetengahList;
            _bulan = bulan;
        }

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
                        column.Item().AlignCenter().Text("Laporan Bulanan").Style(titleStyle);

                        column.Item().Text(text =>
                        {
                            text.Span("Bulan : ").FontSize(9).SemiBold();
                            text.Span(_bulan).FontSize(9);
                        });
                    });
                });
        }
        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(15).Column(column =>
            {
                column.Spacing(5);

                column.Item().Text("50 KG").FontSize(9);
                column.Item().Element(ComposeTableLimaPuluh);

                column.Item().Text("12 KG").FontSize(9);
                column.Item().Element(ComposeTableDuaBelas);

                column.Item().Text("5,5 KG").FontSize(9);
                column.Item().Element(ComposeTableLimaSetengah);

                //column.Item().Text("").FontSize(9);
                //column.Item().Element(ComposeTableTotalPenjualan);

                //column.Item().Text("").FontSize(9);
                //column.Item().Element(ComposeTableInOutStok);
            });
        }
        void ComposeTableLimaPuluh(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(8).NormalWeight();
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
                });

                table.Header(header =>
                {
                    header.Cell().ExtendHorizontal().Element(CellStyle).Text("No.").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tanggal").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Nama").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Harga").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Qty").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                if (_lapLimaPuluhList != null)
                {
                    foreach (var item in _lapLimaPuluhList)
                    {
                        var nomor = _lapLimaPuluhList.IndexOf(item) + 1;
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var tanggal = item.GetType().GetProperty("Tanggal").GetValue(item);
                        var harga = item.GetType().GetProperty("Harga").GetValue(item);
                        var jumlah = item.GetType().GetProperty("Jumlah").GetValue(item);
                        var total = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tanggal).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }
            });
        }
        void ComposeTableDuaBelas(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(8).NormalWeight();
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
                });

                table.Header(header =>
                {
                    header.Cell().ExtendHorizontal().Element(CellStyle).Text("No.").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tanggal").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Nama").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Harga").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Qty").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                if (_lapDuaBelasList != null)
                {
                    foreach (var item in _lapDuaBelasList)
                    {
                        var nomor = _lapLimaSetengahList.IndexOf(item) + 1;
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var tanggal = item.GetType().GetProperty("Tanggal").GetValue(item);
                        var harga = item.GetType().GetProperty("Harga").GetValue(item);
                        var jumlah = item.GetType().GetProperty("Jumlah").GetValue(item);
                        var total = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tanggal).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }
            });
        }
        void ComposeTableLimaSetengah(IContainer container)
        {
            var textStyle = TextStyle.Default.FontSize(8).NormalWeight();
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
                });

                table.Header(header =>
                {
                    header.Cell().ExtendHorizontal().Element(CellStyle).Text("No.").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tanggal").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Nama").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Harga").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Qty").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                if (_lapLimaSetengahList != null)
                {
                    foreach (var item in _lapLimaSetengahList)
                    {
                        var nomor = _lapLimaSetengahList.IndexOf(item) + 1;
                        var pangkalan = item.GetType().GetProperty("Pangkalan").GetValue(item);
                        var tanggal = item.GetType().GetProperty("Tanggal").GetValue(item);
                        var harga = item.GetType().GetProperty("Harga").GetValue(item);
                        var jumlah = item.GetType().GetProperty("Jumlah").GetValue(item);
                        var total = item.GetType().GetProperty("TotalSemua").GetValue(item);

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tanggal).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }
            });
        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    }
}
