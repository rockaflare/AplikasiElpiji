using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class tabungbocortable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Tanggal",
                table: "TransaksiLogs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");

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
                    Keterangan = table.Column<string>(type: "TEXT", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabungBocor");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "Tanggal",
                table: "TransaksiLogs",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT",
                oldNullable: true);

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
    }
}
