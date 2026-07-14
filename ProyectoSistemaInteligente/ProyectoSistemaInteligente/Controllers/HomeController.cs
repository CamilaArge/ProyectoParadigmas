
using Microsoft.AspNetCore.Mvc;


namespace ProyectoSistemaInteligente.Controllers
{

    ///summary>
    ///Controlador principal de la aplicación.
    ///Gestiona las páginas principales del sistema.
    ///</summary>
    public class HomeController : Controller
    {
        ///Summary>
        ///Muestra la página de inicio.
        ///</summary>
        ///<returns>Vista principal.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
