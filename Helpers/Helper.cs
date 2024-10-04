using API_REST_ADMIN_NOTAS.Class;
using API_REST_ADMIN_NOTAS.Models;

namespace API_REST_ADMIN_NOTAS.Helpers
{
    public class Helper
    {
        public static Session CrearSesion(Usuario user)
        {
            return new Session()
            {
                IdUsuario = user.IdUsuario,
                Username = user.Username
            };
        }

        public static Error CrearError(string descripcion)
        {
            return new Error()
            {
                Descripcion = descripcion
            };
        }

        public static Usuario NormalizarUsuario(UserRequest user)
        {
            return new Usuario()
            {
                Username = user.Username,
                Pass = user.Pass
            };
        } 
    }
}
