using MyAPI.Domain.Models;
using MyAPI.Domain.Services.Interfaces;
using MyAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services
{
    public class LikeService : ILikeService
    {
        private readonly LikeRepository _likeRepository;
        private readonly IPostService _postService;
        private readonly IAuthService _authService;
        private readonly PostRepository _postRepository;

        public LikeService(LikeRepository likeRepository, IAuthService authService, IPostService postService, PostRepository postRepository)
        {
            _authService = authService;
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _postService = postService;
        }

        public async Task<Like> CreateLike(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            //checagem para ver se o post ja foi curtido pelo usuario              //parametros para consulta
            Like like = await _likeRepository.FindLikeByPostIdAndApplicationUserId(postId, currentUser.Id);
            if (like != null)
                throw new ArgumentException("Post já foi curtido");

            Post post = await _postService.GetPost(postId);

            if (post == null)
                throw new ArgumentException("Post não existe");

            if (post.ApplicationUserId == currentUser.Id)
                throw new ArgumentException("Você não pode curtir seu proprio post");

            Post varpost = await _postService.GetPost(postId);
            varpost.Likes += 1;

            Like newLike = new Like();

            newLike.ApplicationUserId = currentUser.Id;
            newLike.postId = post.Id;

            await _likeRepository.CreateLike(newLike);
            await _postRepository.UpdatePost(post);

            return newLike;
        }

        public async Task<bool> RemoveLike(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Like like = await _likeRepository.FindLikeByPostIdAndApplicationUserId(postId, currentUser.Id);

            if (like == null)
                throw new ArgumentException("Post não foi curtido, impossivel remover");


            Post post = await _postService.GetPost(postId);
            post.Likes -= 1;


            await _likeRepository.RemoveLike(like);
            return true;
        }

        public async Task<List<Like>> ListMyLikes()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Like> likeslist = await _likeRepository.ListMyLikes(currentUser.Id);

            return likeslist;
        }
    }
}
