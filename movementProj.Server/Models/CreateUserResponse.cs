namespace movementProj.Server.Models
{
    public class CreateUserResponse : UserRequest
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
