using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _unitOfWork.ProductRepository.Get().ToList();
            if (products is null)
            {
                return NotFound("Products Not Found");
            }
            return products;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _unitOfWork.ProductRepository
                .GetById(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Product Not Found");
            }
            return product;
        }

        [HttpGet("price")]
        public ActionResult<IEnumerable<Product>> GetProductsByPrice()
        {
            var products = _unitOfWork.ProductRepository.GetProductsByPrice().ToList();
            if (products is null)
            {
                return NotFound("Products Not Found");
            }
            return products;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if(product is null)
            {
                return BadRequest();
            }
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("Get Product",
                new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId) 
            {
                return BadRequest(); 
            }
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = 
                _unitOfWork.ProductRepository.GetById(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Product Not Found");
            }
            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();
            return NoContent();
        }
    }
}
