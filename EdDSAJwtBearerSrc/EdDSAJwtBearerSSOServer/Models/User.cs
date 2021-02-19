namespace EdDSAJwtBearerSSOServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Passwors { get; set; }
        public string[] Roles { get; set; }
    }
}
