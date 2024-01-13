using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Category(Name, ImageUrl) Values('Bebidas','bebidas.jpg')");
            migrationBuilder.Sql("Insert into Category(Name, ImageUrl) Values('Lanches','lanches.jpg')");
            migrationBuilder.Sql("Insert into Category(Name, ImageUrl) Values('Sobremesas','sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Category");
        }
    }
}
