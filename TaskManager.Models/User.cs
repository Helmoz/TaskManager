namespace TaskManager.Models
{
    public class User : Identity
    {
        public string UId { get; set; }

        public string Name { get; set; }

        public string PhotoUrl { get; set; }

        public string Email { get; set; }
    }
}
