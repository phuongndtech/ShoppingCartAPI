using Microsoft.AspNetCore.Http;
using ShoppingCartAPI.DTO;
using ShoppingCartAPI.DTO.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProducts();
        Task<ProductResponse> GetProduct(int id);
        Task<string> AddProduct(ProductRequest request);
        Task<string> UpdateProduct(int id,ProductRequest request);
        Task<string> DeleteProduct(int id);
        Task<IEnumerable<ProductResponse>> SearchProducts(string name);
        Task<IEnumerable<ProductResponse>> GetProductByBrand(int brandId);
        Task<IEnumerable<ProductResponse>> GetProductHot();
    }
}
