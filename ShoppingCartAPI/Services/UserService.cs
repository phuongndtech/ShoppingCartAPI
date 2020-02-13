using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingCartAPI.Configurations;
using ShoppingCartAPI.DTO;
using ShoppingCartAPI.Enums;
using ShoppingCartAPI.Helper;
using ShoppingCartAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Services
{
    public class UserService : IUserSevice
    {
        private readonly MyOptions _options;
        private readonly AppSettings _appSettings;
    
        public UserService(IOptions<MyOptions> options, IOptions<AppSettings> appSettings)
        {
            _options = options.Value;
            _appSettings = appSettings.Value;
        }

        public async Task<string> CreateUser(UserRequest userRequest)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userName", userRequest.UserName);
                dynamicParameters.Add("@password", userRequest.Password);
                dynamicParameters.Add("@fullName", userRequest.FullName);
                var res = new UserResponse();
                await sqlCon.ExecuteAsync(
                       "spAddUser",
                       dynamicParameters,
                       commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.CreateSuccess);
            }
            catch (Exception ex)
            {
                var response = new UserResponse();
                return response.Messages = ex.Message;
            }

        }

        public async Task<string> DeteleUser(int userId)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userId", userId);
                var res = new UserResponse();
                await sqlCon.ExecuteAsync(
                    "spDeleteUser",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure
                    );
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.DeleteSuccess);
            }
            catch (Exception ex)
            {
                var res = new UserResponse();
                return res.Messages = ex.Message;
            }
        }

        public async Task<User> GetUser(int userId)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userId", userId);
                return await sqlCon.QuerySingleOrDefaultAsync<User>(
                    "spGetUserById",
                    dynamicParameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                return await sqlCon.QueryAsync<User>(
                    "spGetUsers",
                    null,
                    commandType: CommandType.StoredProcedure); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UpdateUser(int userId, UserRequest userRequest)
        {
            try
            {
                using var sqlCon = new SqlConnection(_options.connectionString);
                await sqlCon.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@userId", userId);
                parameters.Add("@userName", userRequest.UserName);
                parameters.Add("@password", userRequest.Password);
                parameters.Add("@fullName", userRequest.FullName);
                var res = new UserResponse();
                await sqlCon.ExecuteAsync(
                    "spUdateUser",
                    parameters,
                    commandType: CommandType.StoredProcedure);
                return res.Messages = Enumhelper.GetEnumDescription(StatusHandlersEnum.StatusHandle.UpdateSuccess);
            }
            catch (Exception ex)
            {
                var res = new UserResponse();
                return res.Messages = ex.Message;
            }
        }
        public async Task<User> Authenticate(string username, string password)
        {
           var users = await GetUsers();
           var user = users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
           if (user == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user.WithoutPassword();
        }
    }
}