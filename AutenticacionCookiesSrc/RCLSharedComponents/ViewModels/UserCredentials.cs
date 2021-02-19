using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCLSharedComponents.ViewModels
{
    public class UserCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        // Url de retorno después de haber sido autenticado.
        public string ReturnUrl { get; set; }
        // Para indicar si se guardan los datos
        // del usuario en una cookie permanente.
        [DisplayName("Remember Me")]
        public bool IsPersistent { get; set; }
        // Para indicar si se muestran las opciones
        // para autenticación externa.
        public bool ShowExternalLogin { get; set; } = false;
    }
}
