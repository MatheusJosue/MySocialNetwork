using System;
using System.Text.Json.Serialization;

namespace MyAPI.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public int Likes { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUsername { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
        
    }
}
