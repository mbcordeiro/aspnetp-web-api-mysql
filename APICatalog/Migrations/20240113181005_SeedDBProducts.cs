using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class SeedDBProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Product(Name,Description,Price,ImageUrl,Stock,RegistrationDate,CategoryId)" +
                "Values('Coca-Cola Diet','Refrigerante de Cola 350 ml',5.45,'cocacola.jpg',50,now(),1)");
            migrationBuilder.Sql("Insert into Product(Name,Description,Price,ImageUrl,Stock,RegistrationDate,CategoryId)" +
                "Values('Lanche de Atum','Lanche de Atum com maionese',8.50,'atum.jpg',10,now(),2)");
            migrationBuilder.Sql("Insert into Product(Name,Description,Price,ImageUrl,Stock,RegistrationDate,CategoryId)" +
                "Values('Pudim 100 g','Pudim de leite condensado 100g',6.75,'pudim.jpg',20,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Product");
        }
    }
}
