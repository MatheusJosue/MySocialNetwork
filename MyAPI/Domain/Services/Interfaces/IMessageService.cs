using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDTO>> ListMySendMessages();
        Task<bool> ReadMessage(int messageId);
        Task<List<MessageDTO>> ListMyReceivedMessages();
        Task<List<ChatDTO>> ListMyChats();
        Task<string> GetUsername(string userId);
        Task<List<MessageDTO>> ListAllMessagesBetweenCurrentUserAndUserId(string userId);
        Task<MessageDTO> SendMessage(SendMessageDTO message);
        Task<Message> GetMessage(int messageId);
        Task<bool> RemoveMessage(int messageId);
    }
}
