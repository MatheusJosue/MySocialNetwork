using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyAPI.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost("create-like")]
        public async Task<ActionResult> CreateLike([FromBody] int postId)
        {
            try
            {
                var like = await _likeService.CreateLike(postId);

                return Ok(like);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-like")]
        public async Task<ActionResult> RemoveLike([FromBody] int postId)
        {
            try
            {
                var unlike = await _likeService.RemoveLike(postId);

                return Ok(unlike);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-my-likes")]
        public async Task<ActionResult> ListMyLikes()
        {
            try
            {
                var list = await _likeService.ListMyLikes();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
