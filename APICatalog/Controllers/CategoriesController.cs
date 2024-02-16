using APICatalog.Context;
using APICatalog.DTOs;
using APICatalog.Models;
using APICatalog.Pagination;
using APICatalog.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalog.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromQuery] CategoriesParameters categoriesParameters)
        {
            var categories = await _unitOfWork.CategoryRepostory.GetCategories(categoriesParameters);
            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (categories is null)
            {
                return NotFound("Categories Not Found");
            }
            var categoriesDto = _mapper.Map<List<ProductDTO>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}", Name ="GetCategories")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _unitOfWork.CategoryRepostory.GetById(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var category = await _unitOfWork.CategoryRepostory.GetCategoriesByProducts();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(category);
            return categoriesDto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoryDTO categoryDto)
        {
            if (categoryDto is null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.CategoryRepostory.Add(category);
            await _unitOfWork.Commit();
            var dto = _mapper.Map<CategoryDTO>(category);
            return new CreatedAtRouteResult("Get Category",
                new { id = dto.CategoryId }, dto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDto);
            _unitOfWork.CategoryRepostory.Update(category);
            await _unitOfWork.Commit();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepostory.GetById(c => c.CategoryId == id);
            if (category is null)
            {
                return NotFound("Category Not Found");
            }
            _unitOfWork.CategoryRepostory.Delete(category);
            await _unitOfWork.Commit();
            return NoContent();
        }
    }
}
