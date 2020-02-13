using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryModel>> GetProductCategories();
        Task<IEnumerable<ProductCategoryResponse>> GetProductCategory(int categoryId);
        Task<string> AddProductCategory(ProductCategoryRequest request);
        Task<string> UpdateProductCategory(ProductCategoryRequest request);
        Task<string> DeleteProductCategory(int productId, int categoryId);
        Task<IEnumerable<ProductCategoryModel>> ProductCategoryDetail(int id);
    }
}