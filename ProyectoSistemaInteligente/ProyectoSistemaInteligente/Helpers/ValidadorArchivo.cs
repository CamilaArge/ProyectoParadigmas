using Microsoft.AspNetCore.Http;
using System.IO;

//hace basicamente esto: seleccioa archivo -> FileValidator lo valida-> si es valido continua
//si no es valido muestra el mensaje
namespace ProyectoSistemaInteligente.Helpers
{
    /// <summary>
    /// valida los archivos cargados por el usuario
    /// </summary>
    public class ValidadorArchivo
    {
        //extensiones
        private readonly string[] allowedExtensions =
        {
            ".csv",
            ".xlsx",
            ".xls"
        };

        //el tamaño permitido
        private const long MaxSize = 20 * 1024 * 1024;

        //verifica si el archivo cumple con las reglas del sistema
        public bool IsValid(IFormFile file, out string message)
        {
            message = "";

            if (file == null || file.Length == 0)
                    {
                message = "Debe seleccionar un archivo";
                return false;
            }
            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension)) {
                message = "Solo se permiten archivos CSV o Excel";
                return false;
            }
            if (file.Length > MaxSize) {
                message = "El archivo supera el tamaño permitido";
                return false;
            }
            return true;
        }
    }
}
