using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Siapel.EF.Migrations
{
    public partial class transaksiaddjumlah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Jumlah",
                table: "Transaksi",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jumlah",
                table: "Transaksi");
        }
    }
}
