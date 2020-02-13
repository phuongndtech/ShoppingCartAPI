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

namespace ShoppingCartAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MyOptions _options;
        public CategoryService(IOptions<MyOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> AddCategory(CategoryRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@categoryName", request.Name);
                var res = new CategoryResponse();
                await sqlCon.ExecuteAsync(
                    "spAddCategory",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.CreateSuccess);
            }
            catch (Exception ex)
            {
                var res = new CategoryResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<string> DeleteCategory(int id)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                var res = new CategoryResponse();
                await sqlCon.ExecuteAsync(
                    "spDeleteCategory",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                    );
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.DeleteSuccess);
            }
            catch (Exception ex)
            {

                var res = new CategoryResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<CategoryResponse>(
                    "spGetCategory",
                    null,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryResponse> GetCategory(int id)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@id", id);
                return await sqlCon.QuerySingleOrDefaultAsync<CategoryResponse>(
                    "spGetCategoryById",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UpdateCategory(CategoryRequest request)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@id", request.Id);
                parameters.Add("@categoryName", request.Name);
                var res = new CategoryResponse();
                await sqlCon.ExecuteAsync(
                    "spUpdateCategory",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.UpdateSuccess);
            }
            catch (Exception ex)
            {
                var res = new CategoryResponse();
                return res.Messages = ex.Message;
            }
        }
    }
}
