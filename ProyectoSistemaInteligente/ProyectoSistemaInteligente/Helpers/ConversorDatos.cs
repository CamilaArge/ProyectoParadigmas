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

                List<double> valores = new();

                foreach (var fila in archivo.Datos.Skip(1))
                {
                    var valor = fila[i];

                    if (string.IsNullOrWhiteSpace(valor))
                        continue;

                    if (!EsValorVacio(valor) && double.TryParse(valor, out double num))
                    {
                        valores.Add(num);
                    }
                }

                if (valores.Count > 0)
                {
                    resultado.Add(archivo.Columnas[i].Nombre, valores);
                }
            }
            return resultado;
        }

        private static bool EsValorVacio(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return true;

            string valorNormalizado = valor.Trim().ToUpper();

            return valorNormalizado == "NA" ||
                   valorNormalizado == "N/A" ||
                   valorNormalizado == "NULL" ||
                   valorNormalizado == "NONE" ||
                   valorNormalizado == "-";
        }
    }
}