namespace ProyectoSistemaInteligente.Models
{
    /// <summary>
    /// Contiene la información resumida de un grupo generado por K-Means.
    /// </summary>
    public class ResumenCluster
    {
        public uint Grupo { get; set; }
        public int CantidadRegistros { get; set; }
        public List<string> Clientes { get; set; } = new();
        public string Interpretacion { get; set; } = "";
        public List<string> VariablesDiferenciadoras { get; set; } = new();
        public string Tendencia { get; set; } = "";
        public List<string> DetallesVariables { get; set; } = new();
    }
}
