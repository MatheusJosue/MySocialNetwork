using MyAPI.Domain.Models;
using MyAPI.Domain.Models.DTOS;
using MyAPI.Domain.Services.Interfaces;
using MyAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Domain.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _messageRepository;
        private readonly IAuthService _authService;
        private readonly IFriendService _friendService;


        //construtor
        public MessageService(MessageRepository messageRepository, IAuthService authService, IFriendService friendService)
        {
            _messageRepository = messageRepository;
            _authService = authService;
            _friendService = friendService;
        }


        public async Task<List<ChatDTO>> ListMyChats()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<ChatDTO> chats = new List<ChatDTO>();

            List<FriendAcceptedDTO> friends = await _friendService.ListRequestsAccepted();

            foreach (FriendAcceptedDTO friend in friends)
            {
                ChatDTO chatDTO = new()
                {
                    messagesDTO = await ListAllMessagesBetweenCurrentUserAndUserId(friend.FriendId),
                    MemberMe = currentUser.UserName,
                    OtherMember = friend.FriendUsername,
                    OtherMemberId = friend.FriendId
                };
                chats.Add(chatDTO);
            }
            return chats;
        }
        

        public async Task<List<MessageDTO>> ListMySendMessages()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Message> listMessage = await _messageRepository.ListMySendMessages(currentUser.Id);

            List<MessageDTO> newlist = new List<MessageDTO>();

            foreach (Message message in listMessage)
            {
                MessageDTO sendMessageDTO = new MessageDTO();

                sendMessageDTO.Id = message.Id;
                sendMessageDTO.Titulo = message.Titulo;
                sendMessageDTO.Texto = message.Texto;
                sendMessageDTO.Data = DateTime.Now;
                sendMessageDTO.SenderUserId = message.SenderId;
                sendMessageDTO.SenderUsername = await GetUsername(message.SenderId);
                sendMessageDTO.ReceiverUserId = message.ReceiverId;
                sendMessageDTO.ReceiverUsername = await GetUsername(message.ReceiverId);

                newlist.Add(sendMessageDTO);
            }

            return newlist;
        }

        public async Task<List<MessageDTO>> ListMyReceivedMessages()
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Message> listMessages = await _messageRepository.ListMyReceivedMessages(currentUser.Id);

            List<MessageDTO> newlist = new List<MessageDTO>();

            foreach (Message message in listMessages)
            {
            MessageDTO receiverMessageDTO = new MessageDTO();

                receiverMessageDTO.Id = message.Id;
                receiverMessageDTO.Titulo = message.Titulo;
                receiverMessageDTO.Texto = message.Texto;
                receiverMessageDTO.Data = DateTime.Now;
                receiverMessageDTO.SenderUserId = message.SenderId;
                receiverMessageDTO.SenderUsername = await GetUsername(message.SenderId);
                receiverMessageDTO.ReceiverUserId = message.ReceiverId;
                receiverMessageDTO.ReceiverUsername = await GetUsername(message.ReceiverId);
                receiverMessageDTO.IsRead = message.IsRead;

                newlist.Add(receiverMessageDTO);
            }

            return newlist;
        }

        public async Task<string> GetUsername(string userId)
        {
            ApplicationUser user = await _authService.GetUserById(userId);
            if (user != null)
                return user.UserName;

            return null;
        }

        public async Task<List<MessageDTO>> ListAllMessagesBetweenCurrentUserAndUserId(string userId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            List<Message> listMessages = await _messageRepository.ListAllMessagesBetweenCurrentUserAndUserId(currentUser.Id ,userId);

            List<MessageDTO> newlist = new List<MessageDTO>();
            foreach (Message message in listMessages)
            {
                MessageDTO allMessageDTO = new MessageDTO();

                allMessageDTO.Id = message.Id;
                allMessageDTO.Titulo = message.Titulo;
                allMessageDTO.Texto = message.Texto;
                allMessageDTO.Data = DateTime.Now;
                allMessageDTO.SenderUserId = message.SenderId;
                allMessageDTO.SenderUsername = await GetUsername(message.SenderId);
                allMessageDTO.ReceiverUserId = message.ReceiverId;
                allMessageDTO.ReceiverUsername = await GetUsername(message.ReceiverId);
                allMessageDTO.IsRead = message.IsRead;

                newlist.Add(allMessageDTO);
            }
            return newlist;
        }

        public async Task<bool> ReadMessage(int messageId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Message readMessage = await _messageRepository.GetMessage(messageId);

            if (readMessage == null)
                throw new ArgumentException("Mensagem não existe");

            if (readMessage.SenderId == currentUser.Id)
                throw new ArgumentException("Você não pode retornar como lida sua propria mensagem");

            if (readMessage.IsRead == true)
                throw new ArgumentException("mensagem já está lida");

            readMessage.IsRead = true;

            //List<MessageDTO> newlist = new List<MessageDTO>();

            await _messageRepository.ReadMessage(readMessage); 
            

            return true;
        }

        public async Task<MessageDTO> SendMessage(SendMessageDTO messageDTO)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            ApplicationUser receiver = await _authService.GetUserById(messageDTO.ReceiverId);

            if (receiver == null)
                throw new ArgumentException("usuario não existe");

            if (currentUser.Id == receiver.Id)
                throw new ArgumentException("Você não pode enviar mensagem para você mesmo");

            Message newMessage = new Message();

            newMessage.SenderId = currentUser.Id;
            newMessage.ReceiverId = receiver.Id;
            newMessage.Titulo = messageDTO.titulo;
            newMessage.Texto = messageDTO.texto;
            newMessage.Data = DateTime.Now;

            newMessage = await _messageRepository.SendMessage(newMessage);

            MessageDTO message = new MessageDTO();

            message.Id = newMessage.Id;
            message.SenderUserId = newMessage.SenderId;
            message.SenderUsername = currentUser.UserName;
            message.ReceiverUserId = newMessage.ReceiverId;
            message.ReceiverUsername = receiver.UserName;
            message.Titulo = newMessage.Titulo;
            message.Texto = newMessage.Texto;
            message.Data = newMessage.Data;

            return message;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            Message findmessage = await _messageRepository.GetMessage(messageId);

            if (findmessage == null)
                throw new ArgumentException("Mensagem não existe");

            return findmessage;
        }

        public async Task<bool> RemoveMessage(int messageId)
        {
            ApplicationUser currentUser = await _authService.GetCurrentUser();

            Message findmessage = await _messageRepository.GetMessage(messageId);

            if (findmessage == null)
                throw new ArgumentException("Mensagem não existe");

            if (findmessage.IsRead == true && currentUser != findmessage.ReceiverUserId)
                throw new ArgumentException("Você não pode apagar uma mensagem que ja foi lida");

            await _messageRepository.RemoveMessage(messageId);

            return true;
        }
    }
}
