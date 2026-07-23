using Microsoft.ML;
using Microsoft.ML.Data;
using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// Realiza agrupamientos automáticos utilizando K-Means.
    /// </summary>
    public class ClusteringService
    {

        public List<Cluster> Ejecutar(DatosArchivo archivo,int cantidadGrupos = 3)
        {

            MLContext mlContext = new();

            List<DatosCluster> datos = new();

            List<string> columnasNumericas = archivo.Columnas
                .Where(x => x.EsNumerica)
                .Select(x => x.Nombre)
                .ToList();

            if (columnasNumericas.Count == 0)
            {
                return new List<Cluster>();
            }

            List<float[]> filasFeatures = new();

            List<float[]> valoresColumnas = new();

            foreach (string columna in columnasNumericas)
            {
                valoresColumnas.Add(
                    archivo.DatosNumericos[columna]
                    .Select(x => (float)x)
                    .ToArray()
                );

            }

            int cantidadFilas = archivo.Datos.Count - 1;

            for (int fila = 0; fila < cantidadFilas; fila++)
            {

                List<float> features = new();

                for (int columna = 0;columna < columnasNumericas.Count;columna++)
                {

                    float valor = 0;

                    if (fila < valoresColumnas[columna].Length)
                    {
                        valor = valoresColumnas[columna][fila];
                    }
                    features.Add(valor);

                }
                filasFeatures.Add(features.ToArray());

            }

            foreach (var features in filasFeatures)
            {
                datos.Add(new DatosCluster
                {
                    Features = features
                });
            }

            if (datos.Count < cantidadGrupos)
            {
                cantidadGrupos = datos.Count;
            }

            var schema = SchemaDefinition.Create(typeof(DatosCluster));

            schema[nameof(DatosCluster.Features)].ColumnType = new VectorDataViewType(
                NumberDataViewType.Single,
                datos[0].Features.Length
            );

            IDataView dataView = mlContext.Data.LoadFromEnumerable(datos,schema);

            var pipeline =mlContext.Transforms.NormalizeMinMax("Features")

                .Append(
                    mlContext.Clustering.Trainers.KMeans(
                        featureColumnName: "Features",
                        numberOfClusters: cantidadGrupos
                    ));

            var modelo = pipeline.Fit(dataView);

            var predicciones = modelo.Transform(dataView);

            var resultados = mlContext.Data.CreateEnumerable<ResultadoCluster>
                (predicciones,reuseRowObject: false)
                .ToList();

            List<Cluster> clusters = new();

            for (int i = 0; i < resultados.Count; i++)
            {
                clusters.Add(new Cluster
                {
                    Fila = i + 2,

                    Grupo = resultados[i].PredictedClusterId,

                    Nombre = archivo.Datos[i + 1][0],

                    Features = datos[i].Features
                });

            }
            return clusters;

        }
    }

    public class DatosCluster
    {
        public float[] Features { get; set; } = Array.Empty<float>();
    }

    public class ResultadoCluster
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId { get; set; }

    }
}
