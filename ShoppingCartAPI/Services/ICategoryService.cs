using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetCategories();
        Task<CategoryResponse> GetCategory(int id);
        Task<string> AddCategory(CategoryRequest request);
        Task<string> UpdateCategory(CategoryRequest request);
        Task<string> DeleteCategory(int id);
    }
}
