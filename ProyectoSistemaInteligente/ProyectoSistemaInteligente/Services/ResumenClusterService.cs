using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Services
{
    public class ResumenClusterService
    {

        public List<ResumenCluster> Generar(List<Cluster> clusters, DatosArchivo archivo)
        {
            List<ResumenCluster> resumen = new();

            foreach (var grupo in clusters.GroupBy(x => x.Grupo))
            {
                var registros = grupo.ToList();

                var resultado = AnalizarGrupo(registros, archivo);

                resumen.Add(new ResumenCluster
                {
                    Grupo = grupo.Key,
                    CantidadRegistros = registros.Count,
                    Clientes = registros.Select(x => x.Nombre).ToList(),
                    VariablesDiferenciadoras = resultado.variables,
                    Tendencia = resultado.tendencia,
                    Interpretacion = resultado.texto
                });
            }
            return resumen;
        }

        private (List<string> variables, string tendencia, string texto) AnalizarGrupo(List<Cluster> grupo,DatosArchivo archivo)
        {

            List<(string nombre, double diferencia)> diferencias = new();

            int superiores = 0;
            int inferiores = 0;

            foreach (var columna in archivo.Columnas)
            {
                if (!columna.EsNumerica)
                    continue;

                if (!archivo.DatosNumericos.ContainsKey(columna.Nombre))
                    continue;

                double promedioGeneral = archivo.DatosNumericos[columna.Nombre].Average();

                int indice = archivo.Columnas.FindIndex(x => x.Nombre == columna.Nombre);

                List<double> valoresGrupo = new();

                foreach (var registro in grupo)
                {
                    int fila = registro.Fila - 1;

                    if (fila < archivo.Datos.Count)
                    {

                        string valor = archivo.Datos[fila][indice];

                        if (double.TryParse(valor, out double numero))
                        {
                            valoresGrupo.Add(numero);
                        }
                    }
                }

                if (valoresGrupo.Any())
                {
                    double promedioGrupo = valoresGrupo.Average();

                    double porcentaje = ((promedioGrupo - promedioGeneral) / promedioGeneral) * 100;

                    if (porcentaje > 10)
                        superiores++;

                    if (porcentaje < -10)
                        inferiores++;

                    diferencias.Add(
                    (
                        $"{columna.Nombre} ({porcentaje:+0.0;-0.0}%)",
                        Math.Abs(porcentaje)
                    ));
                }
            }

            var variables = diferencias.OrderByDescending(x => x.diferencia).Take(3).Select(x => x.nombre).ToList();

            string tendencia;

            if (superiores > inferiores)
            {
                tendencia =
                "valores superiores al promedio general";
            }
            else if (inferiores > superiores)
            {
                tendencia =
                "valores inferiores al promedio general";
            }
            else
            {
                tendencia =
                "un comportamiento similar al promedio general";
            }

            string texto =
                $"Este grupo contiene {grupo.Count} registros y presenta {tendencia}.";

            return
            (
                variables,
                tendencia,
                texto
            );
        }
    }
}