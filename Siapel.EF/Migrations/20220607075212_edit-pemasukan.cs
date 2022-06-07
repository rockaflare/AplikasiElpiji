using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class editpemasukan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDuaBelas",
                table: "Pemasukan");

            migrationBuilder.DropColumn(
                name: "InLimaPuluh",
                table: "Pemasukan");

            migrationBuilder.RenameColumn(
                name: "InLimaSetengah",
                table: "Pemasukan",
                newName: "Jumlah");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Pemasukan",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 7, 15, 52, 12, 528, DateTimeKind.Local).AddTicks(1193));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 6, 7, 15, 52, 12, 528, DateTimeKind.Unspecified).AddTicks(1262), new TimeSpan(0, 8, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item",
                table: "Pemasukan");

            migrationBuilder.RenameColumn(
                name: "Jumlah",
                table: "Pemasukan",
                newName: "InLimaSetengah");

            migrationBuilder.AddColumn<int>(
                name: "InDuaBelas",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InLimaPuluh",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 7, 15, 32, 56, 47, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                column: "Tanggal",
                value: new DateTimeOffset(new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)));
        }
    }
}
