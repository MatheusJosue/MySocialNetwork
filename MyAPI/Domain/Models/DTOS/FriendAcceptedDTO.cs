namespace MyAPI.Domain.Models.DTOS
{
    public class FriendAcceptedDTO
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string FriendId { get; set; }
        public string FriendUsername { get; set; }
    }
}
