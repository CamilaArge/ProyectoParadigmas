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

            //Falta:
            //EjecutarClustering(archivo);
            //GenerarResumen(archivo);
        }


        //calcula estadísticas descriptivas para las columnas con numeros
        private void CalcularEstadisticas(DatosArchivo archivo)
        {
            archivo.Estadisticas.Clear();

            for (int i = 0; i < archivo.Columnas.Count; i++)
            {
                if (!archivo.Columnas[i].EsNumerica)
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


        //calcula la correlacion de Pearson entre todas las colunmas de numeros.
        private void CalcularCorrelaciones(DatosArchivo archivo)
        {
            archivo.Correlaciones.Clear();


            for (int i = 0; i < archivo.Columnas.Count; i++)
            {
                if (!archivo.Columnas[i].EsNumerica)
                    continue;


                for (int j = i + 1; j < archivo.Columnas.Count; j++)
                {
                    if (!archivo.Columnas[j].EsNumerica)
                        continue;


                    double[] datos1 =
                        archivo.DatosNumericos[
                            archivo.Columnas[i].Nombre]
                        .ToArray();


                    double[] datos2 =
                        archivo.DatosNumericos[
                            archivo.Columnas[j].Nombre]
                        .ToArray();


                    double correlacion =
                        Correlation.Pearson(datos1, datos2);


                    archivo.Correlaciones.Add(new Correlacion
                    {
                        Columna1 = archivo.Columnas[i].Nombre,

                        Columna2 = archivo.Columnas[j].Nombre,

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
                if (!columna.EsNumerica)
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

    }
}