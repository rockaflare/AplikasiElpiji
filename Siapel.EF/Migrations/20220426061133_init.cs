using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    Harga = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pangkalan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pangkalan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pembayaran",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nama = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pembayaran", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pengeluaran",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Jenis = table.Column<string>(type: "TEXT", nullable: false),
                    Keterangan = table.Column<string>(type: "TEXT", nullable: false),
                    Jumlah = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pengeluaran", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pembelian",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TanggalBeli = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Jumlah = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pembelian", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pembelian_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokGudang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    TabungIsi = table.Column<int>(type: "INTEGER", nullable: false),
                    TabungKosong = table.Column<int>(type: "INTEGER", nullable: false),
                    DalamTruk = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokGudang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokGudang_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StokReal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Jumlah = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokReal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StokReal_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TabungBocor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Titipan = table.Column<int>(type: "INTEGER", nullable: false),
                    Ambil = table.Column<int>(type: "INTEGER", nullable: false),
                    Keterangan = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabungBocor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabungBocor_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitipTabung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PangkalanId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Jumlah = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitipTabung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitipTabung_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitipTabung_Pangkalan_PangkalanId",
                        column: x => x.PangkalanId,
                        principalTable: "Pangkalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penjualan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TanggalJual = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    PangkalanId = table.Column<int>(type: "INTEGER", nullable: false),
                    Jumlah = table.Column<int>(type: "INTEGER", nullable: false),
                    PembayaranId = table.Column<int>(type: "INTEGER", nullable: false),
                    TanggalLunas = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false),
                    Keterangan = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penjualan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penjualan_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Penjualan_Pangkalan_PangkalanId",
                        column: x => x.PangkalanId,
                        principalTable: "Pangkalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Penjualan_Pembayaran_PembayaranId",
                        column: x => x.PembayaranId,
                        principalTable: "Pembayaran",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Harga", "Nama" },
                values: new object[] { 1, 195000, "12 KG" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Harga", "Nama" },
                values: new object[] { 2, 92000, "5.5 KG" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "Harga", "Nama" },
                values: new object[] { 3, 250000, "50 KG" });

            migrationBuilder.InsertData(
                table: "Pangkalan",
                columns: new[] { "Id", "Nama", "Status" },
                values: new object[] { 1, "Pangkalpaha", "Aktif" });

            migrationBuilder.InsertData(
                table: "Pangkalan",
                columns: new[] { "Id", "Nama", "Status" },
                values: new object[] { 2, "Rajinpangkalpandai", "Aktif" });

            migrationBuilder.InsertData(
                table: "Pangkalan",
                columns: new[] { "Id", "Nama", "Status" },
                values: new object[] { 3, "APMS", "Aktif" });

            migrationBuilder.InsertData(
                table: "Pembayaran",
                columns: new[] { "Id", "Nama" },
                values: new object[] { 1, "Tunai" });

            migrationBuilder.InsertData(
                table: "Pembayaran",
                columns: new[] { "Id", "Nama" },
                values: new object[] { 2, "Transfer" });

            migrationBuilder.InsertData(
                table: "Pembayaran",
                columns: new[] { "Id", "Nama" },
                values: new object[] { 3, "Invoice" });

            migrationBuilder.CreateIndex(
                name: "IX_Pembelian_ItemId",
                table: "Pembelian",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Penjualan_ItemId",
                table: "Penjualan",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Penjualan_PangkalanId",
                table: "Penjualan",
                column: "PangkalanId");

            migrationBuilder.CreateIndex(
                name: "IX_Penjualan_PembayaranId",
                table: "Penjualan",
                column: "PembayaranId");

            migrationBuilder.CreateIndex(
                name: "IX_StokGudang_ItemId",
                table: "StokGudang",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StokReal_ItemId",
                table: "StokReal",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TabungBocor_ItemId",
                table: "TabungBocor",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TitipTabung_ItemId",
                table: "TitipTabung",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TitipTabung_PangkalanId",
                table: "TitipTabung",
                column: "PangkalanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pembelian");

            migrationBuilder.DropTable(
                name: "Pengeluaran");

            migrationBuilder.DropTable(
                name: "Penjualan");

            migrationBuilder.DropTable(
                name: "StokGudang");

            migrationBuilder.DropTable(
                name: "StokReal");

            migrationBuilder.DropTable(
                name: "TabungBocor");

            migrationBuilder.DropTable(
                name: "TitipTabung");

            migrationBuilder.DropTable(
                name: "Pembayaran");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Pangkalan");
        }
    }
}
