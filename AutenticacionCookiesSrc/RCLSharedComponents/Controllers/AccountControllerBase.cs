using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RCLSharedComponents.Models;
using RCLSharedComponents.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RCLSharedComponents.Controllers
{
    public class AccountControllerBase : Controller
    {
        public async Task<IActionResult> DoLocalLogin(UserCredentials credentials)
        {
            IActionResult Response = View("Login", credentials);
            // Verificar la existencia del usuario
            var User = Repository.GetUserByEmailAndPassword(credentials.Email, credentials.Password);
            if (User != null)
            {
                // El usuario existe, generar su lista de Claims
                var Claims = new List<Claim>
                     {
                     new Claim("sub", User.Id.ToString()),
                     new Claim(ClaimTypes.Name, $"{User.FirstName} {User.LastName}"),
                     new Claim("firstName", User.FirstName),
                     new Claim("lastName", User.LastName),
                     new Claim("email", User.Email)
                     };
                // Generar sus Claim de roles.
                if (User.Roles != null)
                {
                    foreach (var Role in User.Roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role, Role));
                    }
                }

                // Crear el ClaimsIdentity
                var ClaimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Establecer opción de persistencia de cookie
                var AuthenticationProperties =
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = credentials.IsPersistent
                };

                // Iniciar sesión con el ClaimPrincipal
                // Esto persiste el ClaimsPrincipal en una cookie.
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(ClaimsIdentity),
               AuthenticationProperties);

                if (credentials.ReturnUrl == null)
                    credentials.ReturnUrl = "/home";

                Response = Redirect(credentials.ReturnUrl);
            }
            return Response;
        }

        // Url de retorno después de haber sido autenticado
        // Nombre del esquema de autenticación a utilizar
        // Identificador del proveedor de autenticación
        public IActionResult DoExternalLogin(string returnUrl, string authenticationScheme, AuthenticationProvider provider)
        {
            // Invocar la acción Challenge del controlador de autenticación
            return new ChallengeResult(authenticationScheme,
             new AuthenticationProperties
             {
                 // Después de realizar la autenticación se
                 // invocará al método LoginCallback pasándole
                 // el Url de regreso y el identificador del proveedor.
                 RedirectUri = Url.Action(nameof(LoginCallback),
            new
            {
                ReturnUrl = returnUrl,
                AuthenticationProvider = provider
            })
             });
        }

        public virtual async Task<IActionResult> LoginCallback(string returnUrl, AuthenticationProvider authenticationProvider)
        {
            IActionResult Result = Redirect(returnUrl);
            // ¿El usuario se encuentra autenticado?
            if (User.Identity.IsAuthenticated)
            {
                // Está autenticado, intentar agregar sus Claims locales.
                // Esto implica verificar la existencia de un usuario local
                // asociado al usuario autenticado externamente.
                if (await Repository.TryAddLocalUserClaims(User,
                authenticationProvider))
                {
                    // Hay una cuenta de usuario local asociada,
                    // actualizar la cookie
                    await HttpContext.SignInAsync(User);
                    // Direccionar al usuario al Url que haya solicitado.
                    if (returnUrl == null) returnUrl = "/home";
                    Result = Redirect(returnUrl);
                }
                else
                {
                    // No hay un usuario asociado al usuario
                    // autenticado externamente.
                    // Aunque fue autenticado externamente,
                    // no fue autenticado por la aplicación.
                    // Forzar el cierre de sesión eliminando la cookie.
                    await HttpContext.SignOutAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme);
                    // Direccionar a la página de error.
                    Result = View("NotMappedUser", Enum.GetName(
                     typeof(AuthenticationProvider),
                     authenticationProvider));
                }
            }
            return await Task.FromResult(Result);
        }

        public async Task<IActionResult> LogOut(string redirectUrl = "/home")
        {
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect(redirectUrl);
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
