using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProyectoSistemaInteligente.Helpers
{
    /// <summary>
    /// Permite guardar y recuperar objetos desde la sesión.
    /// </summary>
    public static class SessionHelper
    {
        public static void GuardarObjeto<T>(ISession session,
            string llave,
            T objeto)
        {
            string json = JsonConvert.SerializeObject(objeto);

            session.SetString(llave, json);
        }

        public static T ObtenerObjeto<T>(ISession session,
            string llave)
        {
            string json = session.GetString(llave);

            if (json == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}