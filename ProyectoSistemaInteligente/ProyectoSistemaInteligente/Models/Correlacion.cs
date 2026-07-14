namespace ProyectoSistemaInteligente.Models
{
    /// <summary>
    /// guarda la correlacion entre dos columnas
    /// </summary>
    public class Correlacion
    {
        public string Columna1 { get; set; } = "";
        public string Columna2 { get; set; } = "";
        public double Coeficiente {  get; set; }
        public string Interpretacion { get; set; } = "";
    }
}
