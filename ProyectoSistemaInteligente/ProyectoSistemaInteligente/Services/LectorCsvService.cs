using System.Text;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// lee archivos csv
    /// </summary>
    public class LectorCsvService
    {
        
        //lee un archivo csv y devuelve sus filas.
      
        public List<List<string>> Leer(string ruta)
        {
            List<List<string>> datos = new();

            // Lee todas las líneas del archivo.
            string[] lineas = File.ReadAllLines(ruta, Encoding.UTF8);

            foreach (string linea in lineas)
            {
                
                List<string> fila = linea
                    .Split(',')
                    .Select(x => x.Trim())
                    .ToList();

                datos.Add(fila);
            }

            return datos;
        }
    }
}