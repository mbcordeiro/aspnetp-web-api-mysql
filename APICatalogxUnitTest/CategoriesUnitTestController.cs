using APICatalog.Context;
using APICatalog.Controllers;
using APICatalog.DTOs;
using APICatalog.DTOs.Mappings;
using APICatalog.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace APICatalogxUnitTest
{
    public class CategoriesUnitTestController
    {
        private IMapper mapper;
        private IUnitOfWork repository;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString =
           "Server=localhost;DataBase=CatalogDB;Uid=root;Pwd=root";

        static CategoriesUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString,
                 ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public CategoriesUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = config.CreateMapper();
            var context = new AppDbContext(dbContextOptions);
            //DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            //db.Seed(context);
            repository = new UnitOfWork(context);
        }

        [Fact]
        public void GetCategoryById_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriesController(repository, mapper);
            var catId = 2;

            //Act  
            var data = controller.Get(catId);

            //Assert  
            Assert.IsType<CategoryDTO>(data);
        }

        [Fact]
        public void GetCategoryById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new CategoriesController(repository, mapper);
            var catId = 9999;

            //Act  
            var data = controller.Get(catId);

            //Assert  
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact]
        public void Post_Category_AddValidData_Return_CreatedResult()
        {
            //Arrange  
            var controller = new CategoriesController(repository, mapper);

            var cat = new CategoryDTO() { Name = "Teste Unitario Inclusao", ImageUrl = "testecatInclusao.jpg" };

            //Act  
            var data = controller.Post(cat);

            //Assert  
            Assert.IsType<CreatedAtRouteResult>(data);
        }

        [Fact]
        public void Put_Category_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriesController(repository, mapper);
            var catId = 3;

            //Act  
            var existingPost = controller.Get(catId);
            var result = existingPost.Should().BeAssignableTo<CategoryDTO>().Subject;

            var catDto = new CategoryDTO();
            catDto.CategoryId = catId;
            catDto.Name = "Categoria Atualizada - Testes 1";
            catDto.ImageUrl = result.ImageUrl;

            var updatedData = controller.Put(catId, catDto);

            //Assert  
            Assert.IsType<OkResult>(updatedData);
        }

        [Fact]
        public void Delete_Category_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriesController(repository, mapper);
            var catId = 4;

            //Act  
            var data = controller.Delete(catId);

            //Assert  
            Assert.IsType<CategoryDTO>(data);
        }
    }
}
