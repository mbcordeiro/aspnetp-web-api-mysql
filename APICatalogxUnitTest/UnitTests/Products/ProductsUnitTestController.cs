using APICatalog.Context;
using APICatalog.Controllers;
using APICatalog.DTOs;
using APICatalog.DTOs.Mappings;
using APICatalog.Pagination;
using APICatalog.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APICatalogxUnitTest.UnitTests.Products
{
    public class ProductsUnitTestController : IClassFixture<ProductsUnitTestController>
    {
        public IUnitOfWork repository;
        public IMapper mapper;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }
        public static string connectionString =
          "Server=localhost;DataBase=apicatalogodb;Uid=root;Pwd=root";
        static ProductsUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
               .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
               .Options;
        }

        public ProductsUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = config.CreateMapper();
            var context = new AppDbContext(dbContextOptions);
            repository = new UnitOfWork(context);
        }

        private readonly ProductsController _controller;

        public ProductsUnitTestController(ProductsUnitTestController controller)
        {
            _controller = new ProductsController(controller.repository, controller.mapper);
        }

        [Fact]
        public async Task GetProductsById_OKResult()
        {
            //Arrange
            var prodId = 2;

            //Act
            var data = await _controller.Get(prodId);

            ////Assert (xunit)
            //var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //Assert (fluentassertions)
            data.Should().BeOfType<OkObjectResult>()
                       .Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetProductsById_Return_NotFound()
        {
            //Arrange  
            var prodId = 999;

            // Act  
            var data = await _controller.Get(prodId);

            // Assert  
            data.Result.Should().BeOfType<NotFoundObjectResult>()
                        .Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetProductsById_Return_BadRequest()
        {
            //Arrange  
            int prodId = -1;

            // Act  
            var data = await _controller.Get(prodId);

            // Assert  
            data.Result.Should().BeOfType<BadRequestObjectResult>()
                       .Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task GetProducts_Return_ListOfProdutoDTO()
        {
            // Act  
            var query = new ProductsParameters();
            var data = await _controller.Get(query);

            // Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<ProductDTO>>()
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetProducts_Return_BadRequestResult()
        {
            // Act  
            var query = new ProductsParameters();
            var data = await _controller.Get(query);

            //Assert
            data.Result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task PostProduct_Return_CreatedStatusCode()
        {
            // Arrange  
            var newProductDTO = new ProductDTO
            {
                Name = "Novo Produto",
                Description = "Descrição do Novo Produto",
                Price = 10.99m,
                ImageUrl = "imagemfake1.jpg",
                CategoryId = 2
            };

            // Act  
            var data = await _controller.Post(newProductDTO);

            // Assert  
            var createdResult = data.Should().BeOfType<CreatedAtRouteResult>();
            createdResult.Subject.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task PostProduct_Return_BadRequest()
        {
            ProductDTO product = null;

            // Act              
            var data = await _controller.Post(product);

            // Assert  
            var badRequestResult = data.Should().BeOfType<BadRequestResult>();
            badRequestResult.Subject.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task PutProduct_Return_OkResult()
        {
            //Arrange  
            var prodId = 14;

            var updatedProdutoDto = new ProductDTO
            {
                ProductId = prodId,
                Name = "Produto Atualizado - Testes",
                Description = "Minha Descricao",
                ImageUrl = "imagem1.jpg",
                CategoryId = 2
            };

            // Act
            var result = await _controller.Put(prodId, updatedProdutoDto);

            // Assert  
            result.Should().NotBeNull(); 
            result.Should().BeOfType<OkObjectResult>(); 
        }

        [Fact]
        public async Task PutProduct_Return_BadRequest()
        {
            //Arrange
            var prodId = 1000;

            var meuProduto = new ProductDTO
            {
                ProductId = prodId,
                Name = "Produto Atualizado - Testes",
                Description = "Minha Descricao",
                ImageUrl = "imagem1.jpg",
                CategoryId = 2
            };

            //Act              
            var data = await _controller.Put(prodId, meuProduto);

            // Assert  
            data.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);

        }

        [Fact]
        public async Task DeleteProductById_Return_OkResult()
        {
            var prodId = 3;

            // Act
            var result = await _controller.Delete(prodId);

            // Assert  
            result.Should().NotBeNull(); 
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task DeleteProductById_Return_NotFound()
        {
            // Arrange  
            var prodId = 999;

            // Act
            var result = await _controller.Delete(prodId);

            // Assert  
            result.Should().NotBeNull(); 
            result.Should().BeOfType<NotFoundObjectResult>(); 

        }
    }
}
