using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class removetransaksilogonly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabungBocor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Item = table.Column<string>(type: "TEXT", nullable: false),
                    Titipan = table.Column<int>(type: "INTEGER", nullable: false),
                    Ambil = table.Column<int>(type: "INTEGER", nullable: false),
                    Keterangan = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabungBocor", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 14, 15, 3, 34, 583, DateTimeKind.Local).AddTicks(2773));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabungBocor");

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 14, 14, 55, 54, 645, DateTimeKind.Local).AddTicks(5533));
        }
    }
}
