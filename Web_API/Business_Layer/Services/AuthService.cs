using Data_Access_Layer.DTOs;
using Data_Access_Layer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<bool> RegisterUser(RegisterModelDTO registerDto)
        {
            var userExists = await _authRepository.UserExists(registerDto.Username);
            if (userExists) return false;

            return await _authRepository.CreateUser(registerDto);
        }

        /* public async Task<string> LoginUser(LoginModelDTO loginDto)
         {
             var passwordValid = await _authRepository.CheckPassword(loginDto);
             if (!passwordValid) return null;

             var role = await _authRepository.GetUserRole(loginDto.Username);
             var authClaims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, loginDto.Username),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(ClaimTypes.Role, role)
             };

             return GenerateToken(authClaims);
         }*/
        public async Task<string> LoginUser(LoginModelDTO loginDto)
        {
            // Fetch the user object based on the username
            var user = await _authRepository.GetUserByName(loginDto.Username);

            // Validate password
            var passwordValid = await _authRepository.CheckPassword(loginDto);
            if (!passwordValid) return null;

            // Get user's role
            var role = await _authRepository.GetUserRole(loginDto.Username);

            // Create claims list, include NameIdentifier (user.Id)
            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, loginDto.Username),
        new Claim(ClaimTypes.NameIdentifier, user.Id),  // Add this line for userId
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, role)
    };

            // Generate JWT token
            return GenerateToken(authClaims);
        }

        private string GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
