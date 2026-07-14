using Microsoft.AspNetCore.Http;
using ProyectoSistemaInteligente.Helpers;
using ProyectoSistemaInteligente.Models;


namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// Este gestiona las operaciones relacionadas con archivos cargados
    /// </summary>
    public class ArchivoService
    {
        private readonly ValidadorArchivo validador;

        public ArchivoService()
        {
            validador = new ValidadorArchivo();
        }

        //guarda un archivo en la carpeta temporal del sistema
        public Archivo GuardarArchivo(IFormFile archivo)
        {
            string carpeta = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads");

            //crea la carpeta si todavia xiste
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
            string nombre = Guid.NewGuid().ToString()
                + Path.GetExtension(archivo.FileName);
            string ruta = Path.Combine(carpeta, nombre);

            using (var stream = new FileStream(ruta, FileMode.Create))
            {
                archivo.CopyTo(stream);
            }

            return new Archivo
            {
                Nombre = archivo.FileName,
                Extension = Path.GetExtension(archivo.FileName),
                Ruta = ruta,
                Tamano = archivo.Length,
                FechaCarga = DateTime.Now
            };
        }
    }
}
