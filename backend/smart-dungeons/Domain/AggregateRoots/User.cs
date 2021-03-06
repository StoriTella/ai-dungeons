using System.ComponentModel.DataAnnotations;
using System;
using smart_dungeons.Domain.Shared;

namespace smart_dungeons.Domain.Users
{
    public class User: Entity<UserId>, IAggregateRoot
    { 
        public Guid UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
        public byte[] Hashcode { get; set; }
        public byte[] Salt { get; set; }

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
                    byte[] Hashcode,
                    byte[] Salt)
        {
            this.Id = new UserId(Guid.NewGuid());
            this.Username = Username;
            this.Email = Email;
            this.Hashcode = Hashcode;
            this.Salt = Salt;
        }

    }
}