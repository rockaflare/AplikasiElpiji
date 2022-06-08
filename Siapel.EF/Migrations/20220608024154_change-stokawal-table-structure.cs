using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class changestokawaltablestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitDuaBelas",
                table: "StokAwal");

            migrationBuilder.DropColumn(
                name: "InitLimaPuluh",
                table: "StokAwal");

            migrationBuilder.RenameColumn(
                name: "InitLimaSetengah",
                table: "StokAwal",
                newName: "Jumlah");

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "StokAwal",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 8, 10, 41, 54, 341, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Item", "Jumlah", "Tanggal" },
                values: new object[] { "50 KG", 20, new DateTimeOffset(new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "StokAwal",
                columns: new[] { "Id", "CanEdit", "Item", "Jumlah", "Tanggal" },
                values: new object[] { 2, false, "12 KG", 220, new DateTimeOffset(new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "StokAwal",
                columns: new[] { "Id", "CanEdit", "Item", "Jumlah", "Tanggal" },
                values: new object[] { 3, false, "5,5 KG", 355, new DateTimeOffset(new DateTime(2022, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 8, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StokAwal",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Item",
                table: "StokAwal");

            migrationBuilder.RenameColumn(
                name: "Jumlah",
                table: "StokAwal",
                newName: "InitLimaSetengah");

            migrationBuilder.AddColumn<int>(
                name: "InitDuaBelas",
                table: "StokAwal",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitLimaPuluh",
                table: "StokAwal",
                type: "INTEGER",
                nullable: true);

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
                columns: new[] { "InitDuaBelas", "InitLimaPuluh", "InitLimaSetengah", "Tanggal" },
                values: new object[] { 50, 20, 100, new DateTimeOffset(new DateTime(2022, 6, 7, 15, 52, 12, 528, DateTimeKind.Unspecified).AddTicks(1262), new TimeSpan(0, 8, 0, 0, 0)) });
        }
    }
}
