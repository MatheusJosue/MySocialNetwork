using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Models;
using MyAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Data.Repositories
{
    public class LikeRepository
    {
        private readonly MySQLContext _context;

        public LikeRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Like> CreateLike(Like like)
        {
            var newlike = await _context.Like.AddAsync(like);

            await _context.SaveChangesAsync();

            newlike.State = EntityState.Detached;

            return newlike.Entity;
        }

        public async Task<Like> RemoveLike(Like like)
        {

            _context.Like.Remove(like);

            await _context.SaveChangesAsync();

            return like;
        }

        public async Task<List<Like>> ListMyLikes(string userId)
        {
            return await _context.Like.Where(p => p.ApplicationUserId.Equals(userId)).ToListAsync();
        }

        //metodo para checagem
        public async Task<Like> FindLikeByPostIdAndApplicationUserId(int postId, string userId)
        {
            Like fndlike = await _context.Like.FirstOrDefaultAsync(p => p.postId == postId && p.ApplicationUserId == userId);

            return fndlike;
        }
    }
}
