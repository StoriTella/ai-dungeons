namespace smart_dungeons.Models
{
    public class User
    { 
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hashcode { get; set; }
        public string Salt { get; set; }

    }
}