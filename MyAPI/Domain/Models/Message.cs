using System;

namespace MyAPI.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }

        //atributo de relacionamento / foreign key
        public ApplicationUser SenderUserId { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser ReceiverUserId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsRead { get; set; }
    }
}
