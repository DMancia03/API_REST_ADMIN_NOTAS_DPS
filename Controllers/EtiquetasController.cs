using API_REST_ADMIN_NOTAS.Data;
using API_REST_ADMIN_NOTAS.Helpers;
using API_REST_ADMIN_NOTAS.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_REST_ADMIN_NOTAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EtiquetasController : BasicController
    {
        public EtiquetasController(AdminNotasContext db)
        {
            _db = db;
        }

        [HttpGet(Name = "GetEtiquetas")]
        public IActionResult Get()
        {
            return Ok(_db.Etiquetas.ToList());
        }

        [HttpGet("{IdUsuario}", Name = "GetEtiqueta")]
        public IActionResult Get(int IdUsuario)
        {
            var etiquetas = _db.Etiquetas.Where(e => e.IdUsuario == IdUsuario).ToList();

            if (etiquetas.Count == 0)
            {
                return BadRequest(Helper.CrearError("No existen etiquetas para este usuario"));
            }

            return Ok(etiquetas);
        }

        [HttpPost(Name = "CrearEtiqueta")]
        public IActionResult CrearEtiqueta([FromBody] Etiqueta etiqueta)
        {
            _db.Etiquetas.Add(etiqueta);
            _db.SaveChanges();

            return Ok(etiqueta);
        }

        [HttpDelete("{IdEtiqueta}", Name = "BorrarEtiqueta")]
        public IActionResult BorrarEtiqueta(int IdEtiqueta)
        {
            var etiqueta = _db.Etiquetas.Find(IdEtiqueta);

            if (etiqueta == null)
            {
                return BadRequest(Helper.CrearError("No existe la etiqueta"));
            }

            _db.Etiquetas.Remove(etiqueta);
            _db.SaveChanges();

            return Ok(etiqueta);
        }
    }
}
