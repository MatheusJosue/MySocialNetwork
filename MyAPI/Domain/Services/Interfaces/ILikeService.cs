using MyAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Interfaces
{
    public interface ILikeService
    {
        Task<Like> CreateLike(int postId);
        Task<bool> RemoveLike(int postId);
        Task<List<Like>> ListMyLikes();
    }
}
