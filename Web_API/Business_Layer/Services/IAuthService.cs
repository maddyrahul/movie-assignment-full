using Data_Access_Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(RegisterModelDTO registerDto);
        Task<string> LoginUser(LoginModelDTO loginDto);
    }
}
