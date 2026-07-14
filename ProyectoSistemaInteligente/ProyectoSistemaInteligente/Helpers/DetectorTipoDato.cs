namespace ProyectoSistemaInteligente.Helpers
{
    /// <summary>
    /// Detecta el tipo de dato de un valor.
    /// </summary>
    public class DetectorTipoDato
    {
        public string GetType(string value)
        {
            if (double.TryParse(value, out _))
                return "Número";

            if (DateTime.TryParse(value, out _))
                return "Fecha";

            if (bool.TryParse(value, out _))
                return "Booleano";

            return "Texto";
        }
    }
}