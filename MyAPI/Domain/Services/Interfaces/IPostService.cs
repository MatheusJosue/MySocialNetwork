using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> ListPosts();
        Task<List<PostDTO>> ListPostsByUser();
        Task<List<PostDTO>> ListMeusPosts();
        Task<Post> GetPost(int postId);
        Task<Post> CreatePost(Post post);
        Task<int> UpdatePost(Post post);
        Task<bool> DeletePost(int postId);
    }
}
