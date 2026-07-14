namespace ProyectoSistemaInteligente.Helpers
{
    /// <summary>
    /// tiene las operaciones matematicas comunes
    /// </summary>
    public class Calculadora
    {
        //este calcula el promedio de una listade numeros
        public double Average(List<double> values)
        {
            if (values.Count == 0)
                return 0;
            return values.Average();
        }
        //este obtiene el valor mínimo
        public double Min(List<double> values)
        {
            return values.Min();
        }
        //este el valor maximo
        public double Max(List<double> values)
        {
            return values.Max();
        }
    }
}
