using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyAPI.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Post> Posts { get; set; }
        public List<Friend> FriendList { get; set; }
    }
}
