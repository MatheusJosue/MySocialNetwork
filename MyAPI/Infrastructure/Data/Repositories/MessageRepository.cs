    using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Models;
using MyAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Data.Repositories
{
    public class MessageRepository
    {   //injeção de dependencia
        private readonly MySQLContext _context;

        //construtor
        public MessageRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Message> SendMessage(Message mensagem)
        {
            var newMessage = await _context.Message.AddAsync(mensagem);

            await _context.SaveChangesAsync();

            newMessage.State = EntityState.Detached;

            return newMessage.Entity;
        }

        public async Task<List<Message>> ListMySendMessages(string userId)
        {
            List<Message> list = await _context.Message.Where(p => p.SenderId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.ReceiverUserId).ToListAsync();

            return list;
        }

        public async Task<List<Message>> ListMyReceivedMessages(string userId)
        {
            List<Message> list = await _context.Message.Where(p => p.ReceiverId.Equals(userId)).OrderBy(p => p.Data).Include(p => p.SenderUserId).ToListAsync();

            return list;
        }

        public async Task<List<Message>> ListAllMessagesBetweenCurrentUserAndUserId(string user1, string user2)
        {
            List<Message> list = await _context.Message.Where(p => (p.SenderId.Equals(user1) && p.ReceiverId.Equals(user2)) || (p.SenderId.Equals(user2) && p.ReceiverId.Equals(user1))).OrderBy(p => p.Data).ToListAsync();
            return list;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            Message message = await _context.Message.FindAsync(messageId);

            return message;
        }

        public async Task<bool> RemoveMessage(int messageId)
        {
            var item = await _context.Message.FindAsync(messageId);
            _context.Message.Remove(item);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReadMessage(Message messageId)
        {
            _context.Entry(messageId).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}