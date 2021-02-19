using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLSharedComponents.Models
{
    public enum AuthenticationProvider
    {
        Local, Google, Facebook, Microsoft, Twitter
    }

    public class User
    {
        public User() { }

        public User(int id, string firstName, string lastName,
                     string email, string password, string[] roles) =>
                     (Id, FirstName, LastName, Email, Password, Roles) =
                     (id, firstName, lastName, email, password, roles);

        public User(int id, AuthenticationProvider authenticationProvider,
                     string externalUserId) =>
                 (Id, AuthenticationProvider, ExternalUserId) =
                 (id, authenticationProvider, externalUserId);

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AuthenticationProvider AuthenticationProvider { get; set; }
        public string ExternalUserId { get; set; }
        public string[] Roles { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
