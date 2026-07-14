using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// Detecta los tipos de datos.
    /// </summary>
    public class DetectorDatosService
    {

        //analisa las columnas
        public List<Columna> Detectar(List<List<string>> datos)
        {
            List<Columna> columnas = new();

            if (datos.Count == 0)
            {
                return columnas;
            }

            // La primera fila tiene los nombres.
            List<string> nombres = datos[0];

            for (int i = 0; i < nombres.Count; i++)
            {
                List<string> valores = datos
                    .Skip(1)
                    .Select(fila => fila[i])
                    .ToList();

                string tipo = ObtenerTipo(valores);

                columnas.Add(new Columna
                {
                    Nombre = nombres[i],
                    TipoDato = tipo,
                    ValoresNulos = valores
                        .Count(x => string.IsNullOrWhiteSpace(x)),
                        EsNumerica = tipo == "Número"
                });

            }

            return columnas;
        }



        
        // determina el tipo de una lista de valores.
        private string ObtenerTipo(List<string> valores)
        {
            int numeros = 0;

            int booleanos = 0;

            int fechas = 0;

            foreach (string valor in valores)
            {

                if (double.TryParse(valor, out _))
                {
                    numeros++;
                }

                if (bool.TryParse(valor, out _))
                {
                    booleanos++;
                }

                if (DateTime.TryParse(valor, out _))
                {
                    fechas++;
                }

            }

            if (numeros == valores.Count)
            {
                return "Número";
            }

            if (booleanos == valores.Count)
            {
                return "Booleano";
            }

            if (fechas == valores.Count)
            {
                return "Fecha";
            }

            return "Texto";
        }
    }
}