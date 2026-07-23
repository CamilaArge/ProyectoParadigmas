using MathNet.Numerics.Statistics;
using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// hace los análisis sobre los datos cargados
    /// </summary>
    public class AnalisisService
    {

        //Ejecuta todos los análisis disponibles.
        public void Analizar(DatosArchivo archivo)
        {
            CalcularEstadisticas(archivo);

            CalcularCorrelaciones(archivo);

            CalcularValoresAtipicos(archivo);

            CalcularClustering(archivo);
        }


        //calcula estadísticas descriptivas para las columnas con numeros
        private void CalcularEstadisticas(DatosArchivo archivo)
        {
            archivo.Estadisticas.Clear();

            for (int i = 0; i < archivo.Columnas.Count; i++)
            {
                string nombre = archivo.Columnas[i].Nombre;

                if (!archivo.DatosNumericos.ContainsKey(nombre))
                    continue;

                List<double> valores =
                    archivo.DatosNumericos[archivo.Columnas[i].Nombre];

                Estadisticas estadistica = new Estadisticas
                {
                    NombreColumna = archivo.Columnas[i].Nombre,

                    Cantidad = valores.Count,

                    Promedio = valores.Mean(),

                    Mediana = valores.Median(),

                    Minimo = valores.Minimum(),

                    Maximo = valores.Maximum(),

                    DesviacionEstandar = valores.StandardDeviation()
                };


                archivo.Estadisticas.Add(estadistica);
            }

        }

        // calcula la correlacion de Pearson entre todas las columnas numericas
        private void CalcularCorrelaciones(DatosArchivo archivo)
        {
            archivo.Correlaciones.Clear();

            for (int i = 0; i < archivo.Columnas.Count; i++)
            {
                string columna1 = archivo.Columnas[i].Nombre;

                if (!archivo.DatosNumericos.ContainsKey(columna1))
                    continue;


                for (int j = i + 1; j < archivo.Columnas.Count; j++)
                {
                    string columna2 = archivo.Columnas[j].Nombre;

                    if (!archivo.DatosNumericos.ContainsKey(columna2))
                        continue;


                    List<double> valores1 = new();
                    List<double> valores2 = new();


                    // recorrer las filas originales
                    for (int fila = 1; fila < archivo.Datos.Count; fila++)
                    {
                        string valor1 = archivo.Datos[fila][i];
                        string valor2 = archivo.Datos[fila][j];


                        // solamente tomar filas donde ambos valores existan
                        if (string.IsNullOrWhiteSpace(valor1) ||
                            string.IsNullOrWhiteSpace(valor2))
                        {
                            continue;
                        }


                        if (double.TryParse(valor1, out double numero1) &&
                            double.TryParse(valor2, out double numero2))
                        {
                            valores1.Add(numero1);
                            valores2.Add(numero2);
                        }
                    }


                    // Pearson necesita mínimo 2 pares de datos
                    if (valores1.Count < 2)
                        continue;


                    double correlacion = Correlation.Pearson(
                        valores1.ToArray(),
                        valores2.ToArray()
                    );


                    archivo.Correlaciones.Add(new Correlacion
                    {
                        Columna1 = columna1,

                        Columna2 = columna2,

                        Coeficiente = correlacion,

                        Interpretacion = ObtenerInterpretacion(correlacion)
                    });
                }
            }
        }

        //detecta valores atípicos utilizando el método IQR
        private void CalcularValoresAtipicos(DatosArchivo archivo)
        {
            archivo.ValoresAtipicos.Clear();


            foreach (var columna in archivo.Columnas)
            {
                string nombre = columna.Nombre;

                if (!archivo.DatosNumericos.ContainsKey(nombre))
                    continue;

                List<double> valores =
                    archivo.DatosNumericos[columna.Nombre];


                if (valores.Count < 4)
                    continue;


                List<double> ordenados =
                    valores.OrderBy(x => x).ToList();


                double q1 =
                    CalcularCuartil(ordenados, 0.25);


                double q3 =
                    CalcularCuartil(ordenados, 0.75);


                double iqr = q3 - q1;


                double limiteInferior =
                    q1 - (1.5 * iqr);


                double limiteSuperior =
                    q3 + (1.5 * iqr);


                for (int i = 0; i < valores.Count; i++)
                {
                    if (valores[i] < limiteInferior ||
                        valores[i] > limiteSuperior)
                    {

                        archivo.ValoresAtipicos.Add(new ValorAtipico
                        {
                            Columna = columna.Nombre,

                            Fila = i + 2,

                            Valor = valores[i],

                            Metodo = "IQR"
                        });

                    }
                }
            }
        }


        //calcula el cuartil indicado de una lista ordenada
        private double CalcularCuartil(List<double> valores, double posicion)
        {
            double indice =
                (valores.Count - 1) * posicion;


            int inferior =
                (int)Math.Floor(indice);


            int superior =
                (int)Math.Ceiling(indice);


            if (inferior == superior)
            {
                return valores[inferior];
            }


            return valores[inferior] +
                   ((indice - inferior) *
                   (valores[superior] - valores[inferior]));
        }


        //devuelve una interpretacion del coeficiente de Pearson
        private string ObtenerInterpretacion(double valor)
        {
            valor = Math.Abs(valor);


            if (valor >= 0.90)
                return "Muy fuerte";


            if (valor >= 0.70)
                return "Fuerte";


            if (valor >= 0.50)
                return "Moderada";


            if (valor >= 0.30)
                return "Débil";


            return "Muy débil";
        }

        // Ejecuta K-Means y genera un resumen automático de los grupos.
        private void CalcularClustering(DatosArchivo archivo)
        {

            archivo.Clusters.Clear();

            archivo.ResumenClusters.Clear();

            ClusteringService clustering = new();

            archivo.Clusters = clustering.Ejecutar(archivo, 3);

            ResumenClusterService resumen = new();

            archivo.ResumenClusters = resumen.Generar(archivo.Clusters,archivo);
        }
    }
}