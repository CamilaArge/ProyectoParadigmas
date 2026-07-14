namespace ProyectoSistemaInteligente.Models
{

    /// <summary>
    /// guarda los resultados estadisticos de una columna
    /// </summary>
    public class Estadisticas
    {
        public string NombreColumna { get; set; }
        public int Cantidad { get; set; }
        public double Promedio { get; set; }
        public double Mediana { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double DesviacionEstandar { get; set; }
    }
 }

