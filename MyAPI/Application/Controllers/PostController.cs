using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using MyAPI.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Application.Controllers
{
 
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("list-posts")]
        public async Task<ActionResult> ListPosts()
        {
            try
            {
                List<PostDTO> list = await _postService.ListPostsByUser();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-my-posts")]
        public async Task<ActionResult> ListMeusPosts()
        {
            try
            {
                List<PostDTO> list = await _postService.ListMeusPosts();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("get-post")]
        public async Task<ActionResult> GetPost([FromQuery] int postId)
        {
            try
            {
                Post post = await _postService.GetPost(postId);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-post")]
        public async Task<ActionResult> CreatePost([FromBody] Post post)
        {
            try
            {
                Post newpost = await _postService.CreatePost(post);
                return Ok(newpost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-post")]
        public async Task<ActionResult> UpdatePost([FromBody] Post post)
        {
            try
            {
                return Ok(await _postService.UpdatePost(post));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-post")]
        public async Task<ActionResult> DeletePost([FromQuery] int postId)
        {
            try
            {
                Post getPost = await _postService.GetPost(postId);
                await _postService.DeletePost(postId);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
