namespace MyAPI.Domain.Models
{
    public class Friend
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string FriendId { get; set; }
        public string FriendUsername { get; set; }
        public int Status { get; set; }
    }
}
