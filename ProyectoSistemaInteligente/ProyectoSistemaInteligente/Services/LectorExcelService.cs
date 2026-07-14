using OfficeOpenXml;

namespace ProyectoSistemaInteligente.Services
{
    /// <summary>
    /// lee archivos Excel
    /// </summary>
    public class LectorExcelService
    {
        
        // Lee un archivo Excel y devuelve todas las filas.
        public List<List<string>> Leer(string ruta)
        {
            List<List<string>> datos = new();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using ExcelPackage excel = new ExcelPackage(new FileInfo(ruta));

            ExcelWorksheet hoja = excel.Workbook.Worksheets[0];

            int filas = hoja.Dimension.Rows;
            int columnas = hoja.Dimension.Columns;

            for (int fila = 1; fila <= filas; fila++)
            {
                List<string> datosFila = new();

                for (int columna = 1; columna <= columnas; columna++)
                {
                    string valor = hoja.Cells[fila, columna].Text;

                    datosFila.Add(valor);
                }

                datos.Add(datosFila);
            }

            return datos;
        }
    }
}