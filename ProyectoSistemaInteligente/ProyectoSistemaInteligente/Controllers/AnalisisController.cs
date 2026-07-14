using Microsoft.AspNetCore.Mvc;
using ProyectoSistemaInteligente.Helpers;
using ProyectoSistemaInteligente.Models;

namespace ProyectoSistemaInteligente.Controllers
{
    public class AnalisisController : Controller
    {
        public IActionResult Results()
        {
            DatosArchivo datos = SessionHelper.ObtenerObjeto<DatosArchivo>(
                    HttpContext.Session, "Analisis");

            if (datos == null)
            {
                return RedirectToAction("Index", "Archivo");
            }

            return View(datos);
        }
    }
}