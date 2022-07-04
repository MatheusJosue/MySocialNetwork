using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        Task<List<ApplicationUser>> ListUsers();
        Task<ApplicationUser> GetUserById(string userId);
        Task<int> UpdateUser(ApplicationUser user);
        Task<bool> DeleteUser(string userId);
        Task<bool> SignUp(SignUpDTO signUpDTO);
        Task<SsoDTO> SignIn(SignInDTO signInDTO);
        Task<ApplicationUser> GetCurrentUser();
    }
}
