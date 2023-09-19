using Microsoft.AspNetCore.Mvc;

namespace CVM.Controllers
{
    public class UsuarioGetAll : Controller
    {
        public IActionResult Index()
        {
            DLEF.DTorresProgramacionNCapasEntities usuarios = new DLEF.DTorresProgramacionNCapasEntities();
            return View();
        }
    }
}
