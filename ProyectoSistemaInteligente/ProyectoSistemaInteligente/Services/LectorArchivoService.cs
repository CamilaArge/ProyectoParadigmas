using ProyectoSistemaInteligente.Models;
using ProyectoSistemaInteligente.Helpers;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// controla qu lector utilizar según el tipo de archivo
    /// </summary>
    public class LectorArchivoService
    {
        private readonly LectorCsvService lectorCsv;
        private readonly LectorExcelService lectorExcel;
        private readonly DetectorDatosService detector;
        private readonly AnalisisService analisis;

        public LectorArchivoService()
        {
            lectorCsv = new LectorCsvService();
            lectorExcel = new LectorExcelService();
            detector = new DetectorDatosService();
            analisis = new AnalisisService();
        }

        // lee el archivo según su extensión.
        public DatosArchivo Leer(string ruta)
        {
            List<List<string>> datos;

            string extension = Path.GetExtension(ruta).ToLower();

            if (extension == ".csv")
            {
                datos = lectorCsv.Leer(ruta);
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                datos = lectorExcel.Leer(ruta);
            }
            else
            {
                throw new Exception("Formato no permitido");
            }

            DatosArchivo resultado = new DatosArchivo
            {
                NombreArchivo = Path.GetFileName(ruta),
                Datos = datos,
                Columnas = detector.Detectar(datos)
            };

            //convierte una sola vez las columnas numéricas
            resultado.DatosNumericos = ConversorDatos.Convertir(resultado);

            // ejecuta todos los análisis
            analisis.Analizar(resultado);

            return resultado;
        }
    }
}