using System.Collections.Generic;
using System.Linq;
namespace EdDSAJwtBearerSSOServer.Models
{
    public class Repository
    {
        private static List<User> Users = new List<User>
        {
            new User{ Id = 1, FirstName = "Daniel", LastName = "Sánchez", Email = "1@1.com", Passwors = "12345", Roles = new string[]{"Admin"} },
            new User{ Id = 1, FirstName = "Alberto", LastName = "Castro", Email = "2@2.com", Passwors = "12345", Roles = new string[]{"Accountant"} },
            new User{ Id = 1, FirstName = "Pedro", LastName = "Mendez", Email = "3@3.com", Passwors = "12345", Roles = new string[]{"Seller"} },
            new User{ Id = 1, FirstName = "Ricardo", LastName = "Perez", Email = "4@4.com", Passwors = "12345", Roles = new string[]{"Accountant", "Seller"} }
        };
        public static User GetUser(string email, string password)
        {
            return Users.FirstOrDefault(u => u.Email == email && u.Passwors == password);
        }
    }
}
