using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class stokawalseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 6, 15, 45, 26, 385, DateTimeKind.Local).AddTicks(6900));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 6, 14, 50, 15, 587, DateTimeKind.Local).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
