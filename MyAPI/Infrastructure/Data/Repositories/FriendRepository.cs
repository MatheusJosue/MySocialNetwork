using Microsoft.EntityFrameworkCore;
using MyAPI.Domain.Models;
using MyAPI.Infrastructure.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Data.Repositories
{
    public class FriendRepository
    {
        private readonly MySQLContext _context;

        public FriendRepository(MySQLContext context)
        {
            _context = context;
        }

        public async Task<Friend> AddFriend(Friend friend)
        {
            var newfriend = await _context.Friend.AddAsync(friend);

            await _context.SaveChangesAsync();

            newfriend.State = EntityState.Detached;

            return newfriend.Entity;
        }

        public async Task<Friend> RemoveFriend(Friend friend)
        {

            _context.Friend.Remove(friend);

            await _context.SaveChangesAsync();

            return friend;
        }

        public async Task<List<Friend>> ListAcceptsFriendsByApplicationUserId(string userId)
        {
            return await _context.Friend.Where(p => p.ApplicationUserId.Equals(userId) && p.Status == 1).ToListAsync();
        }

        public async Task<List<Friend>> ListPendentsFriendsByApplicationUserId(string userId)
        {
            return await _context.Friend.Where(p => p.ApplicationUserId.Equals(userId) && p.Status == 0).ToListAsync();
        }

        public async Task<Friend> GetUserByUsername(string currentUserId, string username)
        {
            Friend user = await _context.Friend.Where(p => p.ApplicationUserId.Equals(currentUserId) && p.FriendUsername.Equals(username)).FirstOrDefaultAsync();

            return user;
        }

        public async Task<Friend> GetFriendById(string Id)
        {
            Friend user = await _context.Friend.FindAsync(Id);

            return user;
        }

        public async Task<List<Friend>> RequestsPendents(string friendId)
        {
            List<Friend> request = await _context.Friend.Where(p => p.FriendId.Equals(friendId) && p.Status == 0).ToListAsync();

            return request;
        }

        public async Task<List<Friend>> RequestsSends(string ApplicationUserId)
        {
            List<Friend> request = await _context.Friend.Where(p => p.ApplicationUserId.Equals(ApplicationUserId) && p.Status == 0).ToListAsync();

            return request;
        }

        public async Task<List<Friend>> ListRequestsAccepted(string friendId)
        {
            List<Friend> request = await _context.Friend.Where(p => p.FriendId.Equals(friendId) && p.Status == 1 || p.ApplicationUserId.Equals(friendId) && p.Status == 1).ToListAsync();

            return request;
        }

        public async Task<List<Friend>> ListRequestsPendents(string friendId)
        {
            List<Friend> request = await _context.Friend.Where(p => p.FriendId.Equals(friendId) && p.Status == 0 || p.ApplicationUserId.Equals(friendId) && p.Status == 0).ToListAsync();

            return request;
        }

        public async Task<int> UpdateFriend(Friend friend)
        {
            _context.Entry(friend).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }
    }
}
