using Microsoft.AspNetCore.Mvc;
using ProyectoSistemaInteligente.Helpers;
using ProyectoSistemaInteligente.Services;

namespace ProyectoSistemaInteligente.Controllers
{

    /// <summary>
    /// Gontrola la carga de archivos
    /// </summary>
    public class ArchivoController : Controller
    {
        private readonly ArchivoService archivoService;

        public ArchivoController()
        {
            archivoService = new ArchivoService();
        }

        public IActionResult Index()
        {
            return View();
        }

        //recibe el archivo
        [HttpPost]
        public IActionResult Cargar(IFormFile archivo)
        {
            if (archivo == null)
            {
                ViewBag.Error = "Debe seleccionar un archivo";
                return View("Index");
            }
            var resultado = archivoService.GuardarArchivo(archivo);

            LectorArchivoService lector = new LectorArchivoService();
            var datos = lector.Leer(resultado.Ruta);
            LimpiezaDatosService limpieza = new LimpiezaDatosService();
            datos = limpieza.Limpiar(datos);
            datos.DatosNumericos = ConversorDatos.Convertir(datos);
            AnalisisService analisis = new AnalisisService();
            analisis.Analizar(datos);
            SessionHelper.GuardarObjeto(HttpContext.Session, "Analisis", datos);
            return View("ResultadoArchivo", datos);
        }
    }
}