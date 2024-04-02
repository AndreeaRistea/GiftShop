using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Models.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GiftShopOnline.Services;

public class CategoryService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CategoryService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    /*public async Task <Category> CreateCategoryAsync (CategoryDto categoryDto)
    {
        var newCategory = new Category
        {
            CategoryName = categoryDto.Name,

        };

        if (categoryDto.CoverImageFile != null && categoryDto.CoverImageFile.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                await categoryDto.CoverImageFile.CopyToAsync(ms);
                newCategory.CoverImage = ms.ToArray();
            }
        }
        _unitOfWork.Categories.Add(newCategory);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<Category>(newCategory);
    }*/


    public async Task<bool> DeleteCategoryAsync(Guid categoryId)
    {
        var categoryToDelete = await _unitOfWork.Categories
            .FirstOrDefaultAsync(c => c.Id.Equals(categoryId));

        if (categoryToDelete == null) { return false; }

        _unitOfWork.Categories.Remove(categoryToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task <List<CategoryDto>> GetAllCategoriesAsync ()
    {
        var categories= await _unitOfWork.Categories.ToListAsync();
        var cateogryDtos = categories.Select(category => new CategoryDto
        {
            CategoryId = category.Id,
            Name = category.CategoryName,
            CoverImage = category.CoverImage,
        }).ToList();
        
        return cateogryDtos;

    }
}

