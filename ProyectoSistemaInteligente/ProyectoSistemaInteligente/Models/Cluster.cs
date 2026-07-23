namespace ProyectoSistemaInteligente.Models
{
    // <summary>
    /// Grupo de datos agrupados mediante el algoritmo K-Means.
    /// <summary>  
    public class Cluster
    {
        public int Fila { get; set; }
        public uint Grupo { get; set; }
        public string Nombre { get; set; } = "";
        public float[] Features { get; set; } = Array.Empty<float>();
    }
}
