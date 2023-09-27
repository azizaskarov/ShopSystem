using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfForM_CRM.Migrations
{
    /// <inheritdoc />
    public partial class AddproductEntityUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "SellingPrice");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Barcode");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OriginalPrice",
                table: "Products",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "Products",
                newName: "Description");
        }
    }
}
