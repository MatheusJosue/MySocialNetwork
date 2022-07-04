using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using MyAPI.Domain.Services.Interfaces;
using MyAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly PostRepository _postRepository;
        private readonly IAuthService _authService;
        private readonly LikeRepository _likeRepository;
        private readonly IMessageService _messageService;

        public PostService(PostRepository postRepository, IAuthService authService, LikeRepository likeRepository, IMessageService messageService)
        {
            _postRepository = postRepository;
            _authService = authService;
            _likeRepository = likeRepository;
            _messageService = messageService;
        }

        public async Task<List<Post>> ListPosts()
        {
            List<Post> posts = await _postRepository.ListPosts();

            return posts;
        }

        public async Task<List<PostDTO>> ListPostsByUser()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            List<Post> posts = await _postRepository.ListPosts();

            List<PostDTO> list = new List<PostDTO>();

            foreach(Post post in posts)
            {
                PostDTO postDTO = new PostDTO();

                Like fndLike = await _likeRepository.FindLikeByPostIdAndApplicationUserId(post.Id, currentUser.Id);
                if (fndLike != null) 
                {
                    postDTO.AlreadyLiked = true;
                }
                else
                {
                    postDTO.AlreadyLiked = false;
                }

                postDTO.id = post.Id;
                postDTO.titulo = post.Titulo;
                postDTO.descricao = post.Descricao;
                postDTO.data = post.Data;
                postDTO.likes = post.Likes;
                postDTO.ApplicationUserId = post.ApplicationUserId;
                postDTO.ApplicationUsername = post.ApplicationUsername = await _messageService.GetUsername(post.ApplicationUserId);

                list.Add(postDTO);
            }

            return list;
        }

        public async Task<List<PostDTO>> ListMeusPosts()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Post> list = await _postRepository.ListPostsByApplicationUserId(currentUser.Id);

            List<PostDTO> listDto = new List<PostDTO>();

            foreach (Post post in list)
            {
                PostDTO postDTO = new PostDTO();

                postDTO.id = post.Id;
                postDTO.titulo = post.Titulo;
                postDTO.descricao = post.Descricao;
                postDTO.data = post.Data;
                postDTO.likes = post.Likes;
                postDTO.ApplicationUserId = post.ApplicationUserId;
                postDTO.ApplicationUsername = post.ApplicationUsername = await _messageService.GetUsername(post.ApplicationUserId);

                listDto.Add(postDTO);
            }

            return listDto;
        }

        public async Task<Post> GetPost(int postId)
        {
            Post post = await _postRepository.GetPost(postId);
            if (post == null)
                throw new ArgumentException("Post não existe!");

            return post;
        }

        public async Task<Post> CreatePost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Post novoPost = new Post();

            novoPost.ApplicationUserId = currentUser.Id;
            novoPost.ApplicationUser = currentUser;
            novoPost.Data = DateTime.Now;
            novoPost.Titulo = post.Titulo;
            novoPost.Descricao = post.Descricao;

            novoPost = await _postRepository.CreatePost(novoPost);

            return novoPost;
        }

        public async Task<int> UpdatePost(Post post)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            var fndpost = await _postRepository.GetPost(post.Id);

            if (currentUser == null)
                throw new ArgumentException("Você não está logado");

            if (fndpost == null)
                throw new ArgumentException("Post não existe");

            if (!fndpost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão.");

            fndpost.Titulo = post.Titulo;
            fndpost.Descricao = post.Descricao;

            return await _postRepository.UpdatePost(fndpost);
        }

        public async Task<bool> DeletePost(int postId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            var fndpost = await _postRepository.GetPost(postId);

            if (currentUser == null)
                throw new ArgumentException("Você não está logado");

            if (fndpost == null)
                throw new ArgumentException("Post não existe");

            if (!fndpost.ApplicationUserId.Equals(currentUser.Id))
                throw new ArgumentException("Sem permissão.");

            await _postRepository.DeletePost(postId);
            return true;
        }
    }
}
