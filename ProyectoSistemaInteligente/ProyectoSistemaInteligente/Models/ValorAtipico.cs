namespace ProyectoSistemaInteligente.Models
{
    /// <summary>
    ///representa un valor atípico encontrado
    /// </summary>
    public class ValorAtipico
    {
        public string Columna { get; set; } = "";
        public int Fila { get; set; }
        public double Valor { get; set; }
        public string Metodo { get; set; } = "IQR";
    }
}