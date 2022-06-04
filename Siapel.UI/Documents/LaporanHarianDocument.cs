using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siapel.UI.Documents
{
    public class LaporanHarianDocument : IDocument
    {
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
                            text.Span("1 Juni 2022");
                        });
                    });
                });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(15).Column(column =>
            {
                column.Spacing(5);

                column.Item().Text("Tunai");
                column.Item().Element(ComposeTabelTunai);

                column.Item().Text("Transfer");
                column.Item().Element(ComposeTableTransfer);

                column.Item().Text("Invoice");
                column.Item().Element(ComposeTableInvoice);

                column.Item().Text("");
                column.Item().Element(ComposeTableTotalPenjualan);
            });
        }

        void ComposeTabelTunai(IContainer container)
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
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().RowSpan(2).ExtendHorizontal().Element(CellStyle).Text("No.");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Tuan/ Toko");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Harga");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("50 Kg");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("12 Kg");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("5,5 Kg");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Element(CellStyle).Text("Total penjualan : ");

                    footer.Cell().Element(CellStyle).Text("Qty"); //diganti sum jumlah
                    footer.Cell().Element(CellStyle).Text("Total"); //diganti sum total

                    footer.Cell().Element(CellStyle).Text("Qty");
                    footer.Cell().Element(CellStyle).Text("Total");

                    footer.Cell().Element(CellStyle).Text("Qty");
                    footer.Cell().Element(CellStyle).Text("Total");

                    footer.Cell().Element(CellStyle).Text("Total");

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

            });
        }

        void ComposeTableTransfer(IContainer container)
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
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().RowSpan(2).ExtendHorizontal().Element(CellStyle).Text("No.");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Tuan/ Toko");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Harga");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("50 Kg");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("12 Kg");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("5,5 Kg");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Element(CellStyle).Text("Total penjualan : ");

                    footer.Cell().Element(CellStyle).Text("Qty"); //diganti sum jumlah
                    footer.Cell().Element(CellStyle).Text("Total"); //diganti sum total

                    footer.Cell().Element(CellStyle).Text("Qty");
                    footer.Cell().Element(CellStyle).Text("Total");

                    footer.Cell().Element(CellStyle).Text("Qty");
                    footer.Cell().Element(CellStyle).Text("Total");

                    footer.Cell().Element(CellStyle).Text("Total");

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

            });
        }

        void ComposeTableInvoice(IContainer container)
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
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.ConstantColumn(35);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().RowSpan(2).ExtendHorizontal().Element(CellStyle).Text("No.");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Tuan/ Toko");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Harga");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("50 Kg");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("12 Kg");
                    header.Cell().ColumnSpan(2).Element(CellStyle).Text("5,5 Kg");
                    header.Cell().RowSpan(2).Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");

                    header.Cell().Element(CellStyle).Text("Qty");
                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(3).Element(CellStyle).Text("Total penjualan : ");

                    footer.Cell().Element(CellStyle).Text("Qty"); //diganti sum jumlah
                    footer.Cell().Element(CellStyle).Text("Total"); //diganti sum total

                    footer.Cell().Element(CellStyle).Text("Qty");
                    footer.Cell().Element(CellStyle).Text("Total");

                    footer.Cell().Element(CellStyle).Text("Qty");
                    footer.Cell().Element(CellStyle).Text("Total");

                    footer.Cell().Element(CellStyle).Text("Total");

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
                    header.Cell().Element(CellStyle).Text("Total Penjualan Keseluruhan");

                    header.Cell().Element(CellStyle).Text("50 Kg");
                    header.Cell().Element(CellStyle).Text("12 Kg");
                    header.Cell().Element(CellStyle).Text("5,5 Kg");

                    header.Cell().Element(CellStyle).Text("Total");


                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten2);
                });

                table.Footer(footer =>
                {
                    footer.Cell().ColumnSpan(4).Element(CellStyle).Text("Total penjualan : ");

                    footer.Cell().Element(CellStyle).Text("Total"); // diganti sum total

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten4);
                });

            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    }
}
