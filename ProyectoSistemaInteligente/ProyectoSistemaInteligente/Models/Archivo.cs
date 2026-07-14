
using System;

namespace ProyectoSistemaInteligente.Models
{
    /// <summary>
    /// Representa un archivo cargado por el usuario
    /// Tiene la información básica necesaria para validar y procesar el archivo
    /// </summary>
    public class Archivo
    {
        
        ///Nombre original del archivo
        public string Nombre { get; set; } = string.Empty;

        //extension del archivo
        public string Extension { get; set; } = string.Empty;

        //ruta temporal para almacenar el archivo
        public string Ruta { get; set; } = string.Empty;

        //tamaño del archivo en bytes
        public long Tamano { get; set; }

        //fecha y hora en el que se cargó el archivo
        public DateTime FechaCarga { get; set; }

    }
}
