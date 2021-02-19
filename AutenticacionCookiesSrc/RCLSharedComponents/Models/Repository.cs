using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RCLSharedComponents.Models
{
    public static class Repository
    {
        private static readonly List<User> Users = new List<User>
             {
             new User(1, "Maria", "Sanders", "msanders@northwind.com", "12345",
             new string[]{"Admin" }),
             new User(2, "Pedro", "Flores", "pflores@northwind.com", "12345",
             new string[]{"Accountant" }),
             new User(3, "Estela", "Castillo", "ecastillo@northwind.com", "12345",
             new string[]{"Seller" }),
             new User(4, "Gloria", "Ruiz", "gruiz@northwind.com", "12345",
             new string[]{"Seller", "Accountant"})
             };
        public static void SetExternalUsersData(List<User> users)
        {
            foreach (var User in users)
            {
                var CurrentUser = Users.First(u => u.Id == User.Id);
                CurrentUser.AuthenticationProvider =
                User.AuthenticationProvider;
                CurrentUser.ExternalUserId = User.ExternalUserId;
            }
        }
        public static User GetUserByEmailAndPassword(
            string email, string password)
        {
            return Users
            .FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        public static User GetUserByExternalUserId(AuthenticationProvider authenticationProvider,
                 string externalUserId)
        {
            return Users.FirstOrDefault(
            u => u.AuthenticationProvider
            == authenticationProvider &&
            u.ExternalUserId == externalUserId);
        }
        public static List<Claim> GetUserInfoAsClaims(
         AuthenticationProvider provider,
         string providerClaimName,
         string externalUserId)
        {
            // Obtener los datos del usuario local
            var User = GetUserByExternalUserId(provider, externalUserId);
            List<Claim> Claims = null;
            if (User != null)
            {
                Claims = new List<Claim>();
                // El Claim "sub" identificará al usuario
                Claims.Add(new Claim("sub", User.Id.ToString()));
                // Concatenamos el nombre del usuario manejado por
                // el proveedor externo con el nombre del usuario local.
                // Esto es para mostrar el nombre de usuario local junto
                // con el nombre del usuario registrado con el proveedor.
                Claims.Add(new Claim(ClaimTypes.Name,
                $"{User.FirstName} {User.LastName} ({providerClaimName})"));
                Claims.Add(new Claim("firstName", User.FirstName));
                Claims.Add(new Claim("lastName", User.LastName));
                // Agregamos los Claims de los roles de usuario
                if (User.Roles != null)
                {
                    foreach (var Role in User.Roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role, Role));
                    }
                }
            }
            return Claims;
        }
        public static async Task<bool> TryAddLocalUserClaims(
     // Claims del usuario autenticado externamente
     ClaimsPrincipal principal,
     AuthenticationProvider provider)
        {
            bool Result = false;
            // Obtener el ClaimIdentity
            var ClaimsIdentity =
            principal.Identity as ClaimsIdentity;
            // Eliminar el Claim Name ya que se creará un "nuevo" Claim Name.
            var ProviderClaimName =
            ClaimsIdentity
            .Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            ClaimsIdentity.RemoveClaim(ProviderClaimName);
            // Obtener el Id externo del usuario
            string ExternalUserId = ClaimsIdentity.Claims
            .Where(c => c.Type == ClaimTypes.NameIdentifier)
            .Select(c => c.Value).FirstOrDefault();
            // Obtener los Claims del usuario local
            List<Claim> LocalUserClaims =
            GetUserInfoAsClaims(
            provider,
           // Se concatenará con el FirstName y LastName local.
           ProviderClaimName.Value,
           ExternalUserId);
            if (LocalUserClaims != null)
            {
                // Se pudieron obtener los Claims.
                // Esto indica que el usuario externo está
                // mapeado a un usuario local.
                ClaimsIdentity.AddClaims(LocalUserClaims);
                Result = true;
            }
            return await Task.FromResult(Result);
        }

    }
}
