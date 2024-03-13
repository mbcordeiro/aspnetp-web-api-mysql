using APICatalog.Context;
using APICatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogxUnitTest.UnitTests
{
    class DBUnitTestsMockInitializer
    {
        public DBUnitTestsMockInitializer()
        { }
        public void Seed(AppDbContext context)
        {
            context.Categories.Add
            (new Category { CategoryId = 999, Name = "Bebidas999", ImageUrl = "bebidas999.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 2, Name = "Sucos", ImageUrl = "sucos1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 3, Name = "Doces", ImageUrl = "doces1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 4, Name = "Salgados", ImageUrl = "Salgados1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 5, Name = "Tortas", ImageUrl = "tortas1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 6, Name = "Bolos", ImageUrl = "bolos1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 7, Name = "Lanches", ImageUrl = "lanches1.jpg" });

            context.SaveChanges();
        }
    }
}
