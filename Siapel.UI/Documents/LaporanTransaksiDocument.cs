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
    public class LaporanTransaksiDocument : IDocument
    {
        private List<Transaksi>? _listTransaksi;
        private string? _tanggal;

        public LaporanTransaksiDocument(List<Transaksi>? listTransaksi, string? tanggal)
        {
            _listTransaksi = listTransaksi;
            _tanggal = tanggal;
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
                        column.Item().AlignCenter().Text("Laporan Transaksi").Style(titleStyle);

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

                column.Item().Text("Transaksi Detail").FontSize(9);
                column.Item().Element(ComposeTransaksiTable);
            });
        }

        void ComposeTransaksiTable(IContainer container)
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();   
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("No.").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tanggal").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tuan/Toko").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tabung").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Harga").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Jumlah").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Pembayaran").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Total").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Status").FontSize(9);
                    header.Cell().Element(CellStyle).Text("Tanggal Lunas").FontSize(9);

                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                });

                if (_listTransaksi != null)
                {

                    foreach (var item in _listTransaksi)
                    {
                        var nomor = _listTransaksi.IndexOf(item) + 1;
                        var tanggal = item.Tanggal.ToString("dd-MMM-yyyy");
                        var pangkalan = item.Pangkalan.Nama;
                        var tabung = item.Item;
                        var harga = item.Harga.ToString("Rp #,#");
                        var jumlah = item.Jumlah;
                        var pembayaran = item.JenisBayar;
                        var total = item.Total.ToString("Rp #,#");
                        var status = item.Status;
                        var tanggalunas = item.TanggalLunas.Value.ToString("dd-MMM-yyyy");

                        table.Cell().Element(CellStyle).Text(nomor).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tanggal).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pangkalan).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tabung).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(harga).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(jumlah).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(pembayaran).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(total).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(status).Style(textStyle);
                        table.Cell().Element(CellStyle).Text(tanggalunas).Style(textStyle);

                        IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);
                    }
                }

            });
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    }
}
