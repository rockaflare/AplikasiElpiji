using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class addstokawaltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InLimaSetengah",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "InLimaPuluh",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "InDuaBelas",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "StokAwal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    InitLimaPuluh = table.Column<int>(type: "INTEGER", nullable: true),
                    InitDuaBelas = table.Column<int>(type: "INTEGER", nullable: true),
                    InitLimaSetengah = table.Column<int>(type: "INTEGER", nullable: true),
                    CanEdit = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokAwal", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 6, 14, 50, 15, 587, DateTimeKind.Local).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nama",
                value: "DEFAULT");

            migrationBuilder.InsertData(
                table: "StokAwal",
                columns: new[] { "Id", "CanEdit", "InitDuaBelas", "InitLimaPuluh", "InitLimaSetengah", "Tanggal" },
                values: new object[] { 1, false, 50, 20, 100, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StokAwal");

            migrationBuilder.AlterColumn<int>(
                name: "InLimaSetengah",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InLimaPuluh",
                table: "Pemasukan",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InDuaBelas",
                table: "Pemasukan",
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
                value: new DateTime(2022, 6, 1, 13, 59, 32, 697, DateTimeKind.Local).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nama",
                value: "UMUM");
        }
    }
}
