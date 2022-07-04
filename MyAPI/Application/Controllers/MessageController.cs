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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("send-message")]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageDTO message)
        {
            MessageDTO msg = await _messageService.SendMessage(message);
            return Ok(msg);
        }

        [HttpGet("list-my-send-messages")]
        public async Task<ActionResult> ListMySendMessages()
        {
            try
            {

                List<MessageDTO> list = await _messageService.ListMySendMessages();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-my-received-messages")]
        public async Task<ActionResult> ListMyReceivedMessages()
        {
            try
            {
                List<MessageDTO> list = await _messageService.ListMyReceivedMessages();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-my-chats")]
        public async Task<ActionResult> ListMyChats()
        {
            try
            {
                List<ChatDTO> list = await _messageService.ListMyChats();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("list-all-messages-between-current-user-and-user-id")]
        public async Task<ActionResult> ListAllMessagesBetweenCurrentUserAndUserId([FromQuery] string userId)
        {
            try
            {
                List<MessageDTO> list = await _messageService.ListAllMessagesBetweenCurrentUserAndUserId(userId);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("get-message")]
        public async Task<ActionResult> GetMessage([FromQuery] int messageId)
        {
            try
            {
                Message message = await _messageService.GetMessage(messageId);

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-message")]
        public async Task<ActionResult> DeleteMessage([FromQuery] int messageId)
        {
            try
            {
                return Ok(await _messageService.RemoveMessage(messageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("read-message")]
        public async Task<ActionResult> ReadMessage([FromQuery] int messageId)
        {
            try
            {
                return Ok(await _messageService.ReadMessage(messageId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
