using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AreaController : Controller
    {
        // GET: Areas
        public ActionResult GetAll()
        {

            ML.Area area = new ML.Area();
            ML.Result resultado = BL.Area.GetAll("");

            if (resultado.Correct)
            {
                area.Areas = resultado.Objects;
                return View(area);
            } else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Area area)
        {

            if (area.Nombre == "" || area.Nombre == null)
            {
                area.Nombre = "";
            }

            ML.Result resultado = BL.Area.GetAll(area.Nombre);

            if (resultado.Correct)
            {
                area.Areas = resultado.Objects;
                return View(area);
            }
            else
            {
                return View();
            }
        }
    }
}