using System;

namespace MyAPI.Domain.Models.DTOS
{
    public class SsoDTO
    {
        public string access_token { get; set; }
        public DateTime Expiration { get; set; }
        public ApplicationUser me { get; set; }

        //construtor
        public SsoDTO(string access_token, DateTime expiration)
        {
            this.access_token = access_token;
            Expiration = expiration;
        }

    }
}
