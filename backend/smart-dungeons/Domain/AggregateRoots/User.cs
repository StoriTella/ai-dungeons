using System;
using smart_dungeons.Domain.Shared;

namespace smart_dungeons.Domain.Users
{
    public class User: Entity<UserId>, IAggregateRoot
    { 
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hashcode { get; set; }
        public string Salt { get; set; }

        private User()
        {
        }

        public User(string Username,
                    string Email)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Username = Username;
            this.Email = Email;
        }

        public User(string Username,
                    string Email,
                    string Hashcode,
                    string Salt)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Username = Username;
            this.Email = Email;
            this.Hashcode = Hashcode;
            this.Salt = Salt;
        }

    }
}