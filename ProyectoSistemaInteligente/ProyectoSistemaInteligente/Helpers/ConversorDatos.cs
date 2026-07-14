using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Helpers
{
    /// <summary>
    /// convierte las columnas de numeros para facilitar el análisis
    /// </summary>
    public static class ConversorDatos
    {
        public static Dictionary<string, List<double>> Convertir(DatosArchivo archivo)
        {
            Dictionary<string, List<double>> resultado = new();

            for (int i = 0; i < archivo.Columnas.Count; i++)
            {
                if (!archivo.Columnas[i].EsNumerica)
                    continue;

                List<double> valores = archivo.Datos
                    .Skip(1)
                    .Select(f => Convert.ToDouble(f[i]))
                    .ToList();

                resultado.Add(archivo.Columnas[i].Nombre, valores);
            }

            return resultado;
        }
    }
}