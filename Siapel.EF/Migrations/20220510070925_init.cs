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
                name: "Pangkalan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nama = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Perma = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pangkalan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Harga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PangkalanId = table.Column<int>(type: "INTEGER", nullable: false),
                    TbLimaPuluh = table.Column<int>(type: "INTEGER", nullable: false),
                    TbDuaBelas = table.Column<int>(type: "INTEGER", nullable: false),
                    TbLimaSetengah = table.Column<int>(type: "INTEGER", nullable: false),
                    TanggalUbah = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Harga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Harga_Pangkalan_PangkalanId",
                        column: x => x.PangkalanId,
                        principalTable: "Pangkalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaksi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PangkalanId = table.Column<int>(type: "INTEGER", nullable: false),
                    Item = table.Column<string>(type: "TEXT", nullable: false),
                    Harga = table.Column<int>(type: "INTEGER", nullable: false),
                    JenisBayar = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    TanggalLunas = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaksi_Pangkalan_PangkalanId",
                        column: x => x.PangkalanId,
                        principalTable: "Pangkalan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pangkalan",
                columns: new[] { "Id", "Nama", "Perma", "Status" },
                values: new object[] { 1, "UMUM", true, "Aktif" });

            migrationBuilder.InsertData(
                table: "Pangkalan",
                columns: new[] { "Id", "Nama", "Perma", "Status" },
                values: new object[] { 2, "WIJAYA", false, "Aktif" });

            migrationBuilder.InsertData(
                table: "Pangkalan",
                columns: new[] { "Id", "Nama", "Perma", "Status" },
                values: new object[] { 3, "NIHAYAH", false, "Aktif" });

            migrationBuilder.CreateIndex(
                name: "IX_Harga_PangkalanId",
                table: "Harga",
                column: "PangkalanId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksi_PangkalanId",
                table: "Transaksi",
                column: "PangkalanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Harga");

            migrationBuilder.DropTable(
                name: "Transaksi");

            migrationBuilder.DropTable(
                name: "Pangkalan");
        }
    }
}
