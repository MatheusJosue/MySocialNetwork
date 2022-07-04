using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Models;
using MyAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Data.Repositories
{
    public class UserRepository
    {   //injeçao de dependencia
        private readonly MySQLContext _context;
        
        //construtor
        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> ListUsers()
        {
            List<ApplicationUser> list = await _context.User.ToListAsync();

            return list;
        }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            ApplicationUser user = await _context.User.Where(p => p.UserName.Equals(username)).FirstOrDefaultAsync();

            return user;
        }

        public async Task<ApplicationUser> GetUser(string userId)
        {
            ApplicationUser user = await _context.User.FindAsync(userId);

            return user;
        }

        public async Task<ApplicationUser> CreateUser(ApplicationUser user)
        {
            var newUser = await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            newUser.State = EntityState.Detached;

            return newUser.Entity;
        }

        public async Task<int> UpdateUser(ApplicationUser user)
        {
            _context.Entry(user).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _context.User.FindAsync(userId);
            _context.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<List<Post>> ListPostsByApplicationUserId(string applicationUserId)
        {
            List<Post> list = await _context.Post.Where(p => p.ApplicationUserId.Equals(applicationUserId)).OrderBy(p => p.Data).Include(p => p.ApplicationUser).ToListAsync();

            return list;
        }
    }
}
