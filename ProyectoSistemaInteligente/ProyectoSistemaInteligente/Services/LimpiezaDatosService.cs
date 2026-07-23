using ProyectoSistemaInteligente.Models;
using System.Globalization;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// Se utiliza para limpiar los datos de entrada.
    /// </summary>
    public class LimpiezaDatosService
    {
        /*
        public DatosArchivo Limpiar(DatosArchivo archivo)
        {
            // Eliminar filas vacías
            archivo.Datos = archivo.Datos
                .Where(fila => fila.Any(c => !string.IsNullOrWhiteSpace(c)))
                .ToList();

            // Leer los datos, sin el encabezado
            for (int i = 1; i < archivo.Datos.Count; i++)
            {
                for (int j = 0; j < archivo.Datos[i].Count; j++)
                {
                    var valor = archivo.Datos[i][j];

                    valor = valor?.Trim();

                    if (string.IsNullOrWhiteSpace(valor) ||
                        valor == "NA" ||
                        valor == "N/A" ||
                        valor == "null")
                    {
                        archivo.Datos[i][j] = null;
                    }
                    else
                    {
                        archivo.Datos[i][j] = valor;
                    }
                }
            }

            // Contar valores nulos por columna
            for (int j = 0; j < archivo.Columnas.Count; j++)
            {
                int nulos = 0;

                for (int i = 1; i < archivo.Datos.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(archivo.Datos[i][j]))
                        nulos++;
                }

                archivo.Columnas[j].ValoresNulos = nulos;
            }

            return archivo;
        }*/
        public DatosArchivo Limpiar(DatosArchivo datos)
        {
            // 👇 SOLO quitar filas completamente vacías
            datos.Datos = datos.Datos
                .Where(fila => fila.Any(valor => !string.IsNullOrWhiteSpace(valor)))
                .ToList();

            return datos;
        }
    }
}
