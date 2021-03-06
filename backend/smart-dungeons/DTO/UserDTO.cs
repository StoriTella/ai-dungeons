using System;
using smart_dungeons.Domain.Users;

namespace smart_dungeons.Domain.Users
{
    public class UserDTO
    {
        
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public UserDTO(Guid Id, string Username, string Email)
        {
            this.Id = Id;
            this.Username = Username;
            this.Email = Email;
        }
    }
}