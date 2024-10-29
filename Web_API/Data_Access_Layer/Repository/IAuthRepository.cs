using Data_Access_Layer.DTOs;
using Data_Access_Layer.Models;


namespace Data_Access_Layer.Repository
{
    public interface IAuthRepository
    {
        Task<bool> UserExists(string username);
        Task<bool> CreateUser(RegisterModelDTO registerDto);
        Task<bool> CheckPassword(LoginModelDTO loginDto);
        Task<string> GetUserRole(string username);
        Task<ApplicationUser> GetUserByName(string username);
    }
}
