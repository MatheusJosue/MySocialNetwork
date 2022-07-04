using System;

namespace MyAPI.Domain.Models.DTOS
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }
        public string SenderUserId { get; set; }
        public string SenderUsername{ get; set; }
        public string ReceiverUserId { get; set; }
        public string ReceiverUsername { get; set; }
        public bool IsRead { get; set; }
    }
}
