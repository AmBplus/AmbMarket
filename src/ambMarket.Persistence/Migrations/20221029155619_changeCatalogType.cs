using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ambMarket.Persistence.Migrations
{
    public partial class changeCatalogType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "CatalogTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "CatalogTypes");
        }
    }
}
