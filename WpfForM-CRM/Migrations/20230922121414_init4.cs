using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfForM_CRM.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Shops_ShopId",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shops",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Shops_ShopId",
                table: "Category",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Shops_ShopId",
                table: "Category");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shops",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Shops_ShopId",
                table: "Category",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
