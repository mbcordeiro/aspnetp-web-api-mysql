using APICatalog.Context;
using APICatalog.Models;
using APICatalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return Ok(_unitOfWork.CategoryRepostory.Get().ToList());
        }

        [HttpGet("{id:int}", Name ="GetCategories")]
        public ActionResult<Category> Get(int id)
        {
            var category = _unitOfWork.CategoryRepostory.GetById(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(category);
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            return Ok(_unitOfWork.CategoryRepostory.GetCategoriesByProducts().ToList());
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
            {
                return BadRequest();
            }
            _unitOfWork.CategoryRepostory.Add(category);
            _unitOfWork.Commit();
            return new CreatedAtRouteResult("Get Category",
                new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }
            _unitOfWork.CategoryRepostory.Update(category);
            _unitOfWork.Commit();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _unitOfWork.CategoryRepostory.GetById(c => c.CategoryId == id);
            if (category is null)
            {
                return NotFound("Category Not Found");
            }
            _unitOfWork.CategoryRepostory.Delete(category);
            _unitOfWork.Commit();
            return NoContent();
        }
    }
}
