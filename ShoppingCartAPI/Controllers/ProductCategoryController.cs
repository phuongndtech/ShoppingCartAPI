using Microsoft.AspNetCore.Mvc;
using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategoryModel>> GetProductCategories()
        {
            return await _productCategoryService.GetProductCategories();
        }

        [HttpGet]
        [Route("categoryId")]
        public async Task<IEnumerable<ProductCategoryResponse>> GetProductCategory(int categoryId)
        {
            return await _productCategoryService.GetProductCategory(categoryId);
        }

        [HttpGet]
        [Route("id")]
        public async Task<IEnumerable<ProductCategoryModel>> ProductCategoryDetail(int id)
        {
            return await _productCategoryService.ProductCategoryDetail(id);
        }
        [HttpPost]
        public async Task<string> AddProductCategory([FromBody]ProductCategoryRequest request)
        {
            return await _productCategoryService.AddProductCategory(request);
        }

        [HttpPut]
        public async Task<string> UpdateProductCategory([FromBody]ProductCategoryRequest request)
        {
            return await _productCategoryService.UpdateProductCategory(request);
        }

        [HttpPost]
        [Route("DeleteProductCategory")]
        public async Task<string> DeleteProductCategory([FromBody]ProductCategoryRequest request)
        {
            return await _productCategoryService.DeleteProductCategory(request.ProductId, request.CategoryId);
        }
    }
}