using API_REST_ADMIN_NOTAS.Class;
using API_REST_ADMIN_NOTAS.Models;

namespace API_REST_ADMIN_NOTAS.Helpers
{
    public class Helper
    {
        #region Sesion
        public static Session CrearSesion(Usuario user)
        {
            return new Session()
            {
                IdUsuario = user.IdUsuario,
                Username = user.Username
            };
        }
        #endregion

        #region Error
        public static Error CrearError(string descripcion)
        {
            return new Error()
            {
                Descripcion = descripcion
            };
        }
        #endregion

        #region Usuario
        public static Usuario NormalizarUsuario(UserRequest user)
        {
            return new Usuario()
            {
                Username = user.Username,
                Pass = user.Pass
            };
        }
        #endregion

        #region Etiqueta
        public static Etiqueta NormalizarEtiqueta(EtiquetaRequest etiq)
        {
            return new Etiqueta()
            {
                IdEtiqueta = etiq.IdEtiqueta,
                IdUsuario = etiq.IdUsuario,
                Nombre = etiq.Nombre
            };
        }

        public static EtiquetaRequest CrearEtiquetaRequest(Etiqueta etiqueta)
        {
            return new EtiquetaRequest()
            {
                IdEtiqueta = etiqueta.IdEtiqueta,
                IdUsuario = etiqueta.IdUsuario,
                Nombre = etiqueta.Nombre
            };
        }
        #endregion

        #region Nota
        public static Nota NormalizarNota(NotaRequest nota)
        {
            return new Nota()
            {
                IdNota = nota.IdNota,
                IdUsuario = nota.IdUsuario,
                IdEtiqueta = nota.IdEtiqueta,
                Nombre = nota.Nombre,
                Contenido = nota.Contenido,
                Estado = nota.Estado
            };
        }

        public static NotaRequest CrearNotaRequest(Nota nota)
        {
            return new NotaRequest()
            {
                IdNota = nota.IdNota,
                IdUsuario = nota.IdUsuario,
                IdEtiqueta = nota.IdEtiqueta,
                Nombre = nota.Nombre,
                Contenido = nota.Contenido,
                Estado = nota.Estado
            };
        }
        #endregion

        #region Recordatorio
        public static Nota NormalizarRecordatorio(RecordatorioRequest recordatorio)
        {
            return new Nota()
            {
                IdNota = recordatorio.IdNota,
                IdUsuario = recordatorio.IdUsuario,
                IdEtiqueta = recordatorio.IdEtiqueta,
                Nombre = recordatorio.Nombre,
                Contenido = recordatorio.Contenido,
                Estado = recordatorio.Estado,
                FechaRecordatorio = recordatorio.FechaRecordatorio
            };
        }

        public static RecordatorioRequest CrearRecordatorioRequest(Nota recordatorio)
        {
            return new RecordatorioRequest()
            {
                IdNota = recordatorio.IdNota,
                IdUsuario = recordatorio.IdUsuario,
                IdEtiqueta = recordatorio.IdEtiqueta,
                Nombre = recordatorio.Nombre,
                Contenido = recordatorio.Contenido,
                Estado = recordatorio.Estado,
                FechaRecordatorio = recordatorio.FechaRecordatorio
            };
        }
        #endregion
    }
}
