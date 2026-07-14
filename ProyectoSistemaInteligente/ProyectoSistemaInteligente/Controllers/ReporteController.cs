using Microsoft.AspNetCore.Mvc;

namespace ProyectoSistemaInteligente.Controllers
{
    /// <summary>
    /// Gestiona la generacion y visualizacion de reportes
    /// </summary>
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
