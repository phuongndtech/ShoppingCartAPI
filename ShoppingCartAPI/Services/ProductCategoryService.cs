using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ShoppingCartAPI.Configurations;
using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Enums;
using ShoppingCartAPI.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;

namespace ShoppingCartAPI.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly MyOptions _options;
        public ProductCategoryService(IOptions<MyOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> AddProductCategory(ProductCategoryRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productId", request.ProductId);
                dynamicParameters.Add("@categoryId", request.CategoryId);
                var res = new ProductCategoryResponse();
                await sqlCon.ExecuteAsync(
                    "spAddProductCategory",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.CreateSuccess);
            }
            catch (Exception ex)
            {
                var res = new ProductCategoryResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<string> DeleteProductCategory(int productId, int categoryId)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productId", productId);
                dynamicParameters.Add("@categoryId", categoryId);
                var res = new ProductCategoryResponse();
                await sqlCon.ExecuteAsync(
                    "spDeleteProductCategory",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                    );
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.DeleteSuccess);
            }
            catch (Exception ex)
            {

                var res = new ProductCategoryResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<IEnumerable<ProductCategoryResponse>> GetProductCategory(int categoryId)
        {
            try
            {
                var productCategoryResponse = new List<ProductCategoryResponse>();
                var listProduct = new List<ProductResponse>();
                var brandResponseCategory = new List<BrandResponseCategory>();
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@categoryId", categoryId);
                var res = (await sqlCon.QueryAsync<ProductCategoryModel>(
                    "spGetProductCategoryById",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure)).ToList();

                foreach (var item in res)
                {
                    var productResponseItem = new ProductResponse();
                    productResponseItem.ProductName = item.ProductName;
                    productResponseItem.Price = item.Price;
                    productResponseItem.Description = item.Description;
                    productResponseItem.ImageFileName = item.ImageFile;
                    productResponseItem.BrandName = item.BrandName;
                    productResponseItem.BrandId = item.BrandId;
                    listProduct.Add(productResponseItem);
                }

                return res
                        .GroupBy(r => r.BrandName)
                        .Select(g => new ProductCategoryResponse()
                        {

                            BrandResponses = new List<BrandResponseCategory>()
                            {
                                new BrandResponseCategory()
                                {
                                     BrandName = g.Key,
                                     BrandId = g.Select(x=>x.BrandId).FirstOrDefault(),
                                     Products = g
                                            .Select(x => new ProductResponse()
                                            {
                                                ProductId = x.ProductId,
                                                ProductName = x.ProductName,
                                                Price = x.Price,
                                                ImageFileName = x.ImageFile,
                                                Description = x.Description,
                                                CategoryName = x.CategoryName
                                            }).Distinct().ToList()
                                }
                            }
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductCategoryModel>> GetProductCategories()
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<ProductCategoryModel>(
                    "spGetProductCategory",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateProductCategory(ProductCategoryRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productId", request.ProductId);
                dynamicParameters.Add("@categoryId", request.CategoryId);
                var res = new ProductCategoryResponse();
                await sqlCon.ExecuteAsync(
                    "spUpdateProduct",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.UpdateSuccess);
            }
            catch (Exception ex)
            {
                var res = new ProductCategoryResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<IEnumerable<ProductCategoryModel>> ProductCategoryDetail(int id)
        {
            using var sqlCon = new SqlConnection(_options.connectionString);
            await sqlCon.OpenAsync();
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@categoryId", id);
            return await sqlCon.QueryAsync<ProductCategoryModel>(
                "spGetProductCategoryById",
                dynamicParameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}