using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Models;
using ShoppingCartAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IEnumerable<BrandResponse>> GetBrands()
        {
            return await _brandService.GetBrands();
        }

        [HttpGet]
        [Route("Id")]
        public async Task<BrandResponse> GetBrand(int id)
        {
            return await _brandService.GetBrand(id);
        }

        [HttpPost]
        public async Task<string> AddBrand([FromBody]BrandRequest request)
        {
            return await _brandService.AddBrand(request);
        }

        [HttpPut]
        public async Task<string> UpdateBrand([FromBody]BrandRequest request)
        {
            return await _brandService.UpdateBrand(request);
        }

        [HttpDelete]
        public async Task<string> DeleteBrand(int id)
        {
            return await _brandService.DeleteBrand(id);
        }
    }
}