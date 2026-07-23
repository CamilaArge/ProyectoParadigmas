using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// Detecta los tipos de datos.
    /// </summary>
    public class DetectorDatosService
    {

        //analiza las columnas
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
                        .Count(x => EsValorVacio(x)),
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

            int cantidadValidos = 0;


            foreach (string valor in valores)
            {
                if (EsValorVacio(valor))
                    continue;

                cantidadValidos++;

                if (double.TryParse(valor, out _))
                {
                    numeros++;
                }

                else if (bool.TryParse(valor, out _))
                {
                    booleanos++;
                }

                else if (DateTime.TryParse(valor, out _))
                {
                    fechas++;
                }
            }


            if (cantidadValidos == 0)
            {
                return "Desconocido";
            }


            if (numeros == cantidadValidos)
            {
                return "Número";
            }


            if (booleanos == cantidadValidos)
            {
                return "Booleano";
            }


            if (fechas == cantidadValidos)
            {
                return "Fecha";
            }


            return "Texto";
        }

        private bool EsValorVacio(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return true;


            string valorNormalizado = valor.Trim().ToUpper();


            string[] valoresVacios =
            {
                "NA",
                "N/A",
                "NULL",
                "NONE",
                "-"
            };

            return valoresVacios.Contains(valorNormalizado);
        }
    }
}