namespace MyAPI.Domain.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int postId { get; set; }

        public Post post { get; set; }

        //atributo de relacionamento / foreign key
        public string ApplicationUserId { get; set; }

        //referencia do applicationuserid, o entity framework faz o relacionamento automaticamente
        public ApplicationUser ApplicationUser { get; set; }
    }
}
