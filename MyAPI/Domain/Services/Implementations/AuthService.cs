using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using MyAPI.Domain.Services.Interfaces;
using MyAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserRepository userRepository, IConfiguration configuration, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<ApplicationUser>> ListUsers()
        {
            List<ApplicationUser> listUsers = await _userRepository.ListUsers();

            return listUsers;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            ApplicationUser user = await _userRepository.GetUser(userId);

            if (user == null)
                throw new ArgumentException("Usuário não existe!");

            return user;
        }

        public async Task<int> UpdateUser(ApplicationUser user)
        {
            ApplicationUser findUser = await _userRepository.GetUser(user.Id);
            if (findUser == null)
                throw new ArgumentException("Usuário não encontrado");

            findUser.Email = user.Email;
            findUser.UserName = user.UserName;

            return await _userRepository.UpdateUser(findUser);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            ApplicationUser findUser = await _userRepository.GetUser(userId);
            if (findUser == null)
                throw new ArgumentException("Usuário não encontrado");

            await _userRepository.DeleteUser(userId);

            return true;
        }

        public async Task<bool> SignUp(SignUpDTO signUpDTO)
        {
            if (!signUpDTO.Email.Contains('@'))
            {
                throw new ArgumentException("Email Invalido!");
            }

            if (signUpDTO.Email == null)
            {
                throw new ArgumentException("O Campo email é obrigatório");
            }

            if (signUpDTO.Username == null)
            {
                throw new ArgumentException("O Campo nome é obrigatório");
            }

            if (signUpDTO.Password == null)
            {
                throw new ArgumentException("O Campo de senha é obrigatório");
            }

            if (signUpDTO.ConfirmPassword == null)
            {
                throw new ArgumentException("O Campo de confirmação de senha é obrigatório");
            }

            var userExists = await _userManager.FindByNameAsync(signUpDTO.Username);

            if (userExists != null)
                throw new ArgumentException("Username already exists!");

            userExists = await _userManager.FindByEmailAsync(signUpDTO.Email);
            if (userExists != null)
                throw new ArgumentException("Email already exists!");

            ApplicationUser user;

            user = new ApplicationUser()
            {
                Email = signUpDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = signUpDTO.Username
            };
                                
            var result = await _userManager.CreateAsync(user, signUpDTO.Password);

            if (!result.Succeeded)
                throw new ArgumentException("Cadastro do usuário falhou.");

            return true;
        }

                            //recebendo usuario e senha para login atraves do SignInDTO
        public async Task<SsoDTO> SignIn(SignInDTO signInDTO)
        {
            var user = await _userManager.FindByNameAsync(signInDTO.Username);
            if (user == null)
                throw new ArgumentException("Usuário não encontrado.");

            if (!await _userManager.CheckPasswordAsync(user, signInDTO.Password))
                throw new ArgumentException("Senha inválida.");

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var ssoDTO = new SsoDTO(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
            ssoDTO.me = user;

            return ssoDTO;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User); // Get user id:

            ApplicationUser user = await _userRepository.GetUser(userId);

            return user;
        }
    }
}
