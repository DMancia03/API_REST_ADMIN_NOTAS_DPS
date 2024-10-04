using API_REST_ADMIN_NOTAS.Class;
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

        #region Get
        //Todas las etiquetas
        [HttpGet(Name = "GetEtiquetas")]
        public IActionResult Get()
        {
            List<EtiquetaRequest> etiquetas = (from e in _db.Etiquetas
                                               select new EtiquetaRequest
                                               {
                                                   IdEtiqueta = e.IdEtiqueta,
                                                   IdUsuario = e.IdUsuario,
                                                   Nombre = e.Nombre
                                               }).ToList();

            return Ok(etiquetas);
        }

        //Una etiqueta
        [HttpGet("{IdEtiqueta}", Name = "GetEtiqueta")]
        public IActionResult GetEtiqueta(int IdEtiqueta)
        {
            var etiqueta = _db.Etiquetas.Find(IdEtiqueta);

            if (etiqueta == null)
            {
                return BadRequest(Helper.CrearError("No existe la etiqueta"));
            }

            EtiquetaRequest etiquetaRequest = Helper.CrearEtiquetaRequest(etiqueta);
            return Ok(etiquetaRequest);
        }

        //Las etiquetas de un usuario
        [HttpGet("id_usuario/{IdUsuario}", Name = "GetEtiquetasUsuario")]
        public IActionResult GetEtiquetasUsuario(int IdUsuario)
        {
            List<EtiquetaRequest> etiquetas = (from e in _db.Etiquetas
                                               where e.IdUsuario == IdUsuario
                                               select new EtiquetaRequest
                                               {
                                                   IdEtiqueta = e.IdEtiqueta,
                                                   IdUsuario = e.IdUsuario,
                                                   Nombre = e.Nombre
                                               }).ToList();

            if (etiquetas.Count == 0)
            {
                return BadRequest(Helper.CrearError("No existen etiquetas para este usuario"));
            }

            return Ok(etiquetas);
        }
        #endregion

        #region Post
        [HttpPost(Name = "CrearEtiqueta")]
        public IActionResult CrearEtiqueta([FromBody] EtiquetaRequest etiquetaRequest)
        {
            Etiqueta etiqueta = Helper.NormalizarEtiqueta(etiquetaRequest);

            _db.Etiquetas.Add(etiqueta);
            _db.SaveChanges();

            etiquetaRequest = Helper.CrearEtiquetaRequest(etiqueta);

            return Ok(etiquetaRequest);
        }
        #endregion

        #region Delete
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

            EtiquetaRequest etiquetaRequest = Helper.CrearEtiquetaRequest(etiqueta);

            return Ok(etiquetaRequest);
        }
        #endregion
    }
}
