using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Domain.Models
{
    public class User
    {
        public User(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
