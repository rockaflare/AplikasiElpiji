using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class seedharga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Harga",
                columns: new[] { "Id", "PangkalanId", "TanggalUbah", "TbDuaBelas", "TbLimaPuluh", "TbLimaSetengah" },
                values: new object[] { 1, 1, new DateTime(2022, 5, 25, 10, 13, 0, 69, DateTimeKind.Local).AddTicks(7793), 195000, 1012000, 93000 });

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 1,
                column: "Perma",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 2,
                column: "Perma",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 3,
                column: "Perma",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Harga",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 1,
                column: "Perma",
                value: true);

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 2,
                column: "Perma",
                value: false);

            migrationBuilder.UpdateData(
                table: "Pangkalan",
                keyColumn: "Id",
                keyValue: 3,
                column: "Perma",
                value: false);
        }
    }
}
