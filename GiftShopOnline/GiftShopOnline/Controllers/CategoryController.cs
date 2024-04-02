using AutoMapper;
using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Interfaces;
using GiftShopOnline.Models.Category;
using GiftShopOnline.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        //public CategoryController(CategoryService categoryService)
        //{
        //    _categoryService = categoryService;
        //}

        public CategoryController(UnitOfWork context, IMapper mapper, CategoryService categoryService)
        {
            _uow = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        //{
        //    return await _uow.Categories.ToListAsync();
        //}

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            if (category.CoverImageFile != null && category.CoverImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await category.CoverImageFile.CopyToAsync(ms);
                    category.CoverImage = ms.ToArray();
                }
            }
           
            _uow.Categories.Add(category);
            await _uow.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }


        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(Guid id)
        {
            var category = await _uow.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _uow.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _uow.Categories.Remove(category);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
