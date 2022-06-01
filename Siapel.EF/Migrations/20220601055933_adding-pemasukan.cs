using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class addingpemasukan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pemasukan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tanggal = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    InLimaPuluh = table.Column<int>(type: "INTEGER", nullable: false),
                    InDuaBelas = table.Column<int>(type: "INTEGER", nullable: false),
                    InLimaSetengah = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pemasukan", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 6, 1, 13, 59, 32, 697, DateTimeKind.Local).AddTicks(5203));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pemasukan");

            migrationBuilder.UpdateData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalUbah",
                value: new DateTime(2022, 5, 25, 10, 13, 0, 69, DateTimeKind.Local).AddTicks(7793));
        }
    }
}
