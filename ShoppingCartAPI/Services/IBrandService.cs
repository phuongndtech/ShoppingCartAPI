using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandResponse>> GetBrands();
        Task<BrandResponse> GetBrand(int brandId);
        Task<string> AddBrand(BrandRequest request);
        Task<string> UpdateBrand(BrandRequest request);
        Task<string> DeleteBrand(int id);
    }
}