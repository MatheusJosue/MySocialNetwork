using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Models;
using MyAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Data.Repositories
{
    public class PostRepository
    {   //injeção de dependencia
        private readonly MySQLContext _context;

        //construtor
        public PostRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> list = await _context.Post.ToListAsync();

            return list;
        }

        public async Task<List<Post>> ListPostsByApplicationUserId(string applicationUserId)
        {
            List<Post> list = await _context.Post.Where(p => p.ApplicationUserId.Equals(applicationUserId)).OrderBy(p => p.Data).Include(p => p.ApplicationUser).ToListAsync();

            return list;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _context.Post.FindAsync(postId);

            return post;
        }

        public async Task<Post> CreatePost(Post post)
        {
            var newPost = await _context.Post.AddAsync(post);

            await _context.SaveChangesAsync();

            newPost.State = EntityState.Detached;

            return newPost.Entity;
        }

        public async Task<int> UpdatePost(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePost(int postId)
        {
            Post post = await _context.Post.FindAsync(postId);
            _context.Remove(post);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
