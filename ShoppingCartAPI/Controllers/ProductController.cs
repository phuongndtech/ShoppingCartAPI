using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.DTO;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResponse>> GetProducts()
        {
            return await _productService.GetProducts();
        }

        [HttpGet]
        [Route("Id")]
        public async Task<ProductResponse> GetProduct(int id)
        {
            return await _productService.GetProduct(id);
        }

        [HttpGet]
        [Route("brandId")]
        public async Task<IEnumerable<ProductResponse>> GetProductByBrand(int brandId)
        {
            return await _productService.GetProductByBrand(brandId);
        }

        [HttpGet]
        [Route("Name")]
        public async Task<IEnumerable<ProductResponse>> SearchProduct(string name)
        {
            return await _productService.SearchProducts(name);
        }

        [HttpPost]
        public async Task<string> AddProduct([FromBody]ProductRequest request)
        {
            return await _productService.AddProduct(request);
        }

        [HttpPut]
        public async Task<string> UpdateProduct(int id,[FromBody]ProductRequest request)
        {
            return await _productService.UpdateProduct(id, request);
        }

        [HttpDelete]
        public async Task<string> DeleteProduct(int id)
        {
            return await _productService.DeleteProduct(id);
        }

        [HttpGet]
        [Route("GetProductHot")]
        public async Task<IEnumerable<ProductResponse>> GetProductHot()
        {
            return await _productService.GetProductHot();
        }
    }
}