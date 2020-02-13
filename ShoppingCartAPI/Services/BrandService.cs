using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ShoppingCartAPI.Configurations;
using ShoppingCartAPI.DTO.Requestes;
using ShoppingCartAPI.DTO.Responses;
using ShoppingCartAPI.Enums;
using ShoppingCartAPI.Helper;
using ShoppingCartAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public class BrandService : IBrandService
    {
        private readonly MyOptions _options;
        public BrandService(IOptions<MyOptions> options)
        {
            _options = options.Value;
        }
        public async Task<string> AddBrand(BrandRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@BrandName", request.Name);
                var res = new BrandResponse();
                await sqlCon.ExecuteAsync(
                    "spAddBrand",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.CreateSuccess);
            }
            catch (Exception ex)
            {
                var res = new BrandResponse();
                return res.Messages = ex.Message;
            }

        }

        public async Task<string> DeleteBrand(int id)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@BrandId", id);
                var res = new BrandResponse();
                await sqlCon.ExecuteAsync(
                    "spDeleteBrand",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                    );
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.DeleteSuccess);
            }
            catch (Exception ex)
            {

                var res = new BrandResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<BrandResponse> GetBrand(int brandId)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@BrandId", brandId);
                return await sqlCon.QuerySingleOrDefaultAsync<BrandResponse>(
                    "spGetBrandById",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<IEnumerable<BrandResponse>> GetBrands()
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<BrandResponse>(
                    "spGetBrands",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateBrand(BrandRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@BrandId", request.Id);
                parameters.Add("@BrandName", request.Name);
                var res = new BrandResponse();
                await sqlCon.ExecuteAsync(
                    "spUpdateBrand",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.UpdateSuccess);
            }
            catch (Exception ex)
            {
                var res = new BrandResponse();
                return res.Messages = ex.Message;
            }
        }
    }
}