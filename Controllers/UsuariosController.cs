using API_REST_ADMIN_NOTAS.Class;
using API_REST_ADMIN_NOTAS.Data;
using API_REST_ADMIN_NOTAS.Helpers;
using API_REST_ADMIN_NOTAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_REST_ADMIN_NOTAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : BasicController
    {
        public UsuariosController(AdminNotasContext db)
        {
            _db = db;
        }

        #region SignUp
        [HttpPost("signup", Name = "SignUpUsuario")]
        public IActionResult Register([FromBody] UserRequest user)
        {
            //Validar si username no viene vacio
            if(user.Username.IsNullOrEmpty())
            {
                return BadRequest(Helper.CrearError("Nombre de usuario no puede estar vacio"));
            }

            //Validar si password no viene vacio
            if (user.Pass.IsNullOrEmpty())
            {
                return BadRequest(Helper.CrearError("Contraseña no puede estar vacio"));
            }

            //Validar si existe el username en bd
            int username_count = _db.Usuarios.Where(u => u.Username == user.Username).Count();

            //Si existe se devuelve un error y se corta el flujo
            if (username_count > 0)
            {
                return BadRequest(Helper.CrearError("Nombre de usuario ya existe"));
            }

            //Crear usuario
            Usuario usuario = Helper.NormalizarUsuario(user);

            //Guardar usuario en bd
            _db.Usuarios.Add(usuario);
            _db.SaveChanges();

            //Devolver objeto de session personalizado
            return Ok(Helper.CrearSesion(usuario));
        }
        #endregion

        #region LogIn
        [HttpPost("login", Name = "LoginUsuario")]
        public IActionResult Login([FromBody] UserRequest user)
        {
            //Validar si username no viene vacio
            if (user.Username.IsNullOrEmpty())
            {
                return BadRequest(Helper.CrearError("Nombre de usuario no puede estar vacio"));
            }

            //Validar si password no viene vacio
            if (user.Pass.IsNullOrEmpty())
            {
                return BadRequest(Helper.CrearError("Contraseña no puede estar vacio"));
            }

            //Buscamos el usuario en bd
            var usuario = _db.Usuarios.Where(u => u.Username == user.Username && u.Pass == user.Pass).FirstOrDefault();

            //Si no existe retornamos un error
            if(usuario == null)
            {
                return BadRequest(Helper.CrearError("Credenciales incorrectas"));
            }

            //Devolver objeto de session personalizado
            return Ok(Helper.CrearSesion(usuario));
        }
        #endregion
    }
}
