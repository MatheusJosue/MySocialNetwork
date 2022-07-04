using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using MyAPI.Domain.Services.Interfaces;
using MyAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Implementations
{
    public class FriendService: IFriendService
    {
        private readonly FriendRepository _friendRepository;
        private readonly IAuthService _authService;
        private readonly UserRepository _userRepository;

        public FriendService(FriendRepository FriendRepository, IAuthService authService, UserRepository userRepository)
        {
            _friendRepository = FriendRepository;
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<bool> AddFriend(string username)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();
            ApplicationUser findUser = await _userRepository.GetUserByUsername(username);
            if(findUser == null)
                throw new ArgumentException("Usuario não existe");

            if (findUser.UserName == currentUser.UserName)
                throw new ArgumentException("Você nao pode adicionar essa pessoa");

            Friend friend = await _friendRepository.GetUserByUsername(currentUser.Id, username);

            if (friend != null)
                throw new ArgumentException("Você ja enviou um pedido de amizade para essa pessoa");

            Friend newfriend = new Friend();

            newfriend.Id = Guid.NewGuid().ToString();
            newfriend.FriendId = findUser.Id;
            newfriend.ApplicationUserId = currentUser.Id;
            newfriend.Status = 0;
            newfriend.FriendUsername = findUser.UserName;
            await _friendRepository.AddFriend(newfriend);

            return true;
        }

        public async Task<Friend> GetFriendByFriendName(string username)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Friend friend = await _friendRepository.GetUserByUsername(currentUser.Id, username);
            if (friend == null)
                throw new ArgumentException("Usuario não existe");

            return friend;
        }

        public async Task<bool> RemoveFriend(string username)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Friend findfriend = await _friendRepository.GetUserByUsername(currentUser.Id, username);

            if (findfriend == null)
                throw new ArgumentException("Usuario não existe");

            await _friendRepository.RemoveFriend(findfriend);

            return true;
        }

        public async Task<List<Friend>> RequestsPendents()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Friend> request = await _friendRepository.RequestsPendents(currentUser.Id);

            return request;
        }

        public async Task<List<FriendAcceptedDTO>> ListRequestsAccepted()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<FriendAcceptedDTO> listFriendsAccepted = new List<FriendAcceptedDTO>();

            List<Friend> request = await _friendRepository.ListRequestsAccepted(currentUser.Id);
            foreach (Friend friend in request)
            {
                FriendAcceptedDTO friendDTO = new FriendAcceptedDTO();

                if (friend.FriendId == currentUser.Id)
                {
                    friendDTO.Id = friend.Id;
                    friendDTO.FriendId = friend.ApplicationUserId;
                    ApplicationUser friendUser = await _userRepository.GetUser(friend.ApplicationUserId);
                    friendDTO.FriendUsername = friendUser.UserName;
                    friendDTO.ApplicationUserId = friend.FriendId;
                }
                else
                {
                    friendDTO.Id = friend.Id;
                    friendDTO.FriendId = friend.FriendId;
                    friendDTO.FriendUsername = friend.FriendUsername;
                    friendDTO.ApplicationUserId = friend.ApplicationUserId;
                }      

                if (friendDTO.FriendId != currentUser.Id)
                {
                    listFriendsAccepted.Add(friendDTO);
                }    
            }

            return listFriendsAccepted;
        }

        public async Task<List<FriendAcceptedDTO>> ListRequestsPendents()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<FriendAcceptedDTO> listFriendsAccepted = new List<FriendAcceptedDTO>();

            List<Friend> request = await _friendRepository.ListRequestsPendents(currentUser.Id);
            foreach (Friend friend in request)
            {
                FriendAcceptedDTO friendDTO = new FriendAcceptedDTO();

                if (friend.FriendId == currentUser.Id)
                {
                    friendDTO.Id = friend.Id;
                    friendDTO.FriendId = friend.ApplicationUserId;
                    ApplicationUser friendUser = await _userRepository.GetUser(friend.ApplicationUserId);
                    friendDTO.FriendUsername = friendUser.UserName;
                    friendDTO.ApplicationUserId = friend.FriendId;

                    if (friendDTO.FriendId != currentUser.Id)
                    {
                        listFriendsAccepted.Add(friendDTO);
                    }
                }
            }

            return listFriendsAccepted;
        }

        public async Task<List<Friend>> RequestsSends()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Friend> request = await _friendRepository.RequestsSends(currentUser.Id);

            return request;
        }

        public async Task<Friend> AcceptFriendRequestById(string id)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Friend findFriend = await _friendRepository.GetFriendById(id);
            if (findFriend == null)
                throw new ArgumentException("Pedido não existe");

            if (findFriend.FriendId != currentUser.Id)
                throw new ArgumentException("Você não recebeu este pedido");

            findFriend.Status = 1;

                await _friendRepository.UpdateFriend(findFriend);

            return findFriend;
        }

        public async Task<Friend> RefuseFriendRequestById(string id)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Friend findFriend = await _friendRepository.GetFriendById(id);
            if (findFriend == null)
                throw new ArgumentException("Pedido não existe");

            if (findFriend.FriendId != currentUser.Id)
                throw new ArgumentException("Você não recebeu este pedido");


            await _friendRepository.RemoveFriend(findFriend);


            return findFriend;
        }
    }
}
