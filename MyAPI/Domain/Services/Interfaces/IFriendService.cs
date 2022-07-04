using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Interfaces
{
    public interface IFriendService
    {
        Task<bool> AddFriend(string username);
        Task<Friend> GetFriendByFriendName(string username);
        Task<Friend> AcceptFriendRequestById(string id);
        Task<Friend> RefuseFriendRequestById(string id);
        Task<List<FriendAcceptedDTO>> ListRequestsAccepted();
        Task<List<FriendAcceptedDTO>> ListRequestsPendents();
        Task<bool> RemoveFriend(string username);
        Task<List<Friend>> RequestsPendents();
        Task<List<Friend>> RequestsSends();
    }
}
