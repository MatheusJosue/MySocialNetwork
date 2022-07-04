using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Domain.Models;
using System.Threading.Tasks;
using System;
using MyAPI.Domain.Services.Interfaces;
using MyAPI.Domain.Models.DTOS;
using System.Collections.Generic;

namespace MyAPI.Application.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        [HttpPost("add-friend")]
        public async Task<ActionResult> AddFriend([FromBody] AddFriendDTO addFriend)
        {
            try
            {
                bool ret = await _friendService.AddFriend(addFriend.Username);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-friend-by-name")]
        public async Task<ActionResult> GetFriendByFriendName([FromQuery] string username)
        {
            try
            {
                Friend ret = await _friendService.GetFriendByFriendName(username);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove-friend")]
        public async Task<ActionResult> RemoveFriend([FromBody] string username)
        {
            try
            {
                bool ret = await _friendService.RemoveFriend(username);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("requests-pendents")]
        public async Task<ActionResult> RequestsPendents()
        {
            try
            {
                List<Friend> ret = await _friendService.RequestsPendents();

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-requests-Accepted")]
        public async Task<ActionResult> ListRequestsAccepted()
        {
            try
            {
                List<FriendAcceptedDTO> ret = await _friendService.ListRequestsAccepted();

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-requests-pendents")]
        public async Task<ActionResult> ListRequestsPendents()
        {
            try
            {
                List<FriendAcceptedDTO> ret = await _friendService.ListRequestsPendents();

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("requests-sends")]
        public async Task<ActionResult> RequestsSends()
        {
            try
            {
                List<Friend> ret = await _friendService.RequestsSends();

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("accept-friend-request-by-id")]
        public async Task<ActionResult> AcceptFriendRequestById([FromBody] string id)
        {
            try
            {
                Friend ret = await _friendService.AcceptFriendRequestById(id);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("refuse-friend-request-by-id")]
        public async Task<ActionResult> RefuseFriendRequestById([FromBody] string id)
        {
            try
            {
                Friend ret = await _friendService.RefuseFriendRequestById(id);

                return Ok(ret);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
