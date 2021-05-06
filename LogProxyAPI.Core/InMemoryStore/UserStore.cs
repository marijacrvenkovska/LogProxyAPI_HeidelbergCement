using LogProxyAPI.Domain.Models;
using LogProxyAPI_HeidelbergCement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Core.InMemoryStore
{
    public class UserStore
    {
        private static List<User> Users = new List<User>()
        {
            new User("marija","marija"),

        };

        public static IReadOnlyList<User> GetAll() => Users;
    }
}
