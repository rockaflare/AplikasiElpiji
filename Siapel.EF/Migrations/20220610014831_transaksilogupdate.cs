using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class transaksilogupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SisaStok",
                table: "TransaksiLogs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TransaksiLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 10, 9, 48, 30, 698, DateTimeKind.Local).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 2,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 3,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "TransaksiLogs");

            migrationBuilder.AlterColumn<int>(
                name: "SisaStok",
                table: "TransaksiLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 8, 13, 30, 38, 430, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 2,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 3,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));
        }
    }
}
