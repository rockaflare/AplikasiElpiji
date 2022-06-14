using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class removetransaksilog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabungBocor");

            migrationBuilder.DropTable(
                name: "TransaksiLogs");

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 14, 14, 55, 54, 645, DateTimeKind.Local).AddTicks(5533));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 2,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 3,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabungBocor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ambil = table.Column<int>(type: "INTEGER", nullable: false),
                    Item = table.Column<string>(type: "TEXT", nullable: false),
                    Keterangan = table.Column<string>(type: "TEXT", nullable: false),
                    Tanggal = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Titipan = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabungBocor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransaksiLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Item = table.Column<string>(type: "TEXT", nullable: false),
                    SisaStok = table.Column<int>(type: "INTEGER", nullable: true),
                    Tanggal = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaksiLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 11, 10, 31, 48, 366, DateTimeKind.Local).AddTicks(7309));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 2,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 3,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));
        }
    }
}
