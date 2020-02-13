using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCartAPI.Data
{
    public partial class MyShopMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}