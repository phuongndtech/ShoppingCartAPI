using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }

        [HttpGet]
        [Route("Id")]
        public async Task<CategoryResponse> GetCategory(int id)
        {
            return await _categoryService.GetCategory(id);
        }

        [HttpPost]
        public async Task<string> AddCategory([FromBody]CategoryRequest request)
        {
            return await _categoryService.AddCategory(request);
        }

        [HttpPut]
        public async Task<string> UpdateCategory([FromBody]CategoryRequest request)
        {
            return await _categoryService.UpdateCategory(request);
        }

        [HttpDelete]
        public async Task<string> DeleteCategory(int id)
        {
            return await _categoryService.DeleteCategory(id);
        }
    }
}