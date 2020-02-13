using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ShoppingCartAPI.Configurations;
using ShoppingCartAPI.DTO;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Enums;
using ShoppingCartAPI.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public class ProductService : IProductService
    {
      
        private readonly MyOptions _options;
        public ProductService(IOptions<MyOptions> options)
        {
            _options = options.Value;
        }
        public async Task<string> AddProduct(ProductRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@brandId", request.BrandId);
                dynamicParameters.Add("@productName", request.Name);
                dynamicParameters.Add("@price", request.Price);
                dynamicParameters.Add("@description", request.Description);
                dynamicParameters.Add("@imageFileName", request.ImageFileName);
                dynamicParameters.Add("@isHot", request.IsHot);
                var res = new ProductResponse();
                await sqlCon.ExecuteAsync(
                    "spAddProduct",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.CreateSuccess);
            }
            catch (Exception ex)
            {
                var res = new ProductResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<string> DeleteProduct(int id)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                var res = new ProductResponse();
                await sqlCon.ExecuteAsync(
                    "spDeleteProduct",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                    );
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.DeleteSuccess);
            }
            catch (Exception ex)
            {

                var res = new ProductResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<ProductResponse> GetProduct(int id)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                return await sqlCon.QuerySingleOrDefaultAsync<ProductResponse>(
                    "spGetProductById",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductResponse>> GetProductByBrand(int brandId)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@brandId", brandId);
                return await sqlCon.QueryAsync<ProductResponse>(
                    "spGetProductByBrand",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductResponse>> GetProductHot()
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<ProductResponse>(
                    "spGetProductHot",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductResponse>> GetProducts()
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<ProductResponse>(
                    "spGetProduct",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductResponse>> SearchProducts(string name)
        {
            using var sqlCon = new SqlConnection(_options.connectionString);
            await sqlCon.OpenAsync();
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@name", name);
            return await sqlCon.QueryAsync<ProductResponse>(
                "spGetProductByName",
                dynamicParameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<string> UpdateProduct(int id, ProductRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                dynamicParameters.Add("@brandId", request.BrandId);
                dynamicParameters.Add("@productName", request.Name);
                dynamicParameters.Add("@price", request.Price);
                dynamicParameters.Add("@description", request.Description);
                dynamicParameters.Add("@imageFileName", request.ImageFileName);
                dynamicParameters.Add("@isHot", request.IsHot);
                var res = new ProductResponse();
                await sqlCon.ExecuteAsync(
                    "spUpdateProduct",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.UpdateSuccess);
            }
            catch (Exception ex)
            {
                var res = new ProductResponse();
                return res.Messages = ex.Message;
            }
        }
    }
}