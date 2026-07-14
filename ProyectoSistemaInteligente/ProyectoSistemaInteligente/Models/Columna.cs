namespace ProyectoSistemaInteligente.Models
{

    /// <summary>
    /// Una columna del conjunto de datos
    /// </summary>
    public class Columna
    {
        public string Nombre { get; set; } = string.Empty;

        public string TipoDato { get; set; } = string.Empty;

        public int ValoresNulos { get; set; }

        public bool EsNumerica { get; set; }
    }
}

