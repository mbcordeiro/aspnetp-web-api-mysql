using APICatalog.Context;
using APICatalog.DTOs;
using APICatalog.Models;
using APICatalog.Pagination;
using APICatalog.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> Get([FromQuery] ProductsParameters productsParameters)
        {
            var products = _unitOfWork.ProductRepository.GetProducts(productsParameters);
            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (products is null)
            {
                return NotFound("Products Not Found");
            }
            var productsDto = _mapper.Map<List<ProductDTO>>(products);
            return productsDto;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var product = _unitOfWork.ProductRepository
                .GetById(p => p.ProductId == id);
            if (product is null)
            {
                return NotFound("Product Not Found");
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        [HttpGet("price")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductsByPrice()
        {
            var products = _unitOfWork.ProductRepository.GetProductsByPrice().ToList();
            if (products is null)
            {
                return NotFound("Products Not Found");
            }
            var productsDto = _mapper.Map<List<ProductDTO>>(products);
            return productsDto;
        }

        [HttpPost]
        public ActionResult Post(ProductDTO productDto)
        {
            if(productDto is null)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();
            var dto = _mapper.Map<ProductDTO>(product);
            return new CreatedAtRouteResult("Get Product",
                new { id = dto.ProductId }, dto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId) 
            {
                return BadRequest(); 
            }
            var product = _mapper.Map<Product>(productDto);
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
