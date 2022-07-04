using System;

namespace MyAPI.Domain.Models.DTOS
{
    public class PostDTO
    {
        public int id{ get; set; }
        public string titulo{ get; set; }
        public string descricao{ get; set; }
        public DateTime data { get; set; }
        public int likes { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUsername { get; set; }

        public bool AlreadyLiked { get; set; }

    }
}
