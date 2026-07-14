namespace ProyectoSistemaInteligente.Models
{
    /// <summary>
    /// guarda la informacion leida de un archivo
    /// </summary>
    public class DatosArchivo
    {
        public string NombreArchivo { get; set; }

        //cada lista representa una fila
        public List<List<string>> Datos { get; set; }
        public List<Columna> Columnas { get; set; }
        public List<Estadisticas> Estadisticas { get; set; }
        public List<Correlacion> Correlaciones { get; set; }
        public Dictionary<string, List<double>> DatosNumericos { get; set; }
        public List<ValorAtipico> ValoresAtipicos { get; set; }

        public DatosArchivo() { 
        Datos = new();
        Columnas = new();
        Estadisticas = new();
        Correlaciones = new();
        DatosNumericos = new();
        ValoresAtipicos = new();

        }
    }
}
