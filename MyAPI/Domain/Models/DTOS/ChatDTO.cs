using System.Collections.Generic;

namespace MyAPI.Domain.Models.DTOS
{
    public class ChatDTO
    {
        public List<MessageDTO> messagesDTO { get; set; }
        public string MemberMe { get; set; }
        public string OtherMember { get; set; }
        public string OtherMemberId { get; set; }
    }
}
