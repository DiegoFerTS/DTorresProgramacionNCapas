using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            ML.Result resultadoDepartamento = BL.Departamento.GetAll(departamento);
            ML.Result resultadoArea = BL.Area.GetAll("");


            if (resultadoDepartamento.Correct)
            {
                departamento.Departamentos = resultadoDepartamento.Objects;
                departamento.Area.Areas = resultadoArea.Objects;
                return View(departamento);
            } else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Departamento departamentoConsultado)
        {
            if (departamentoConsultado.Area.IdArea == null)
            {
                departamentoConsultado.Area.IdArea = 0;
            }

            if (departamentoConsultado.Area.Nombre == null)
            {
                departamentoConsultado.Area.Nombre = "";
            }
                    
            ML.Result resultadoDepartamento = BL.Departamento.GetAll(departamentoConsultado);
            ML.Result resultadoArea = BL.Area.GetAll("");

            if (resultadoDepartamento.Correct)
            {
                ML.Departamento departamento = new ML.Departamento();
                departamento.Area = new ML.Area();
                departamento.Departamentos = resultadoDepartamento.Objects;
                departamento.Area.Areas = resultadoArea.Objects;
                return View(departamento);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? idDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            ML.Result resultadoAreas = BL.Area.GetAll("");

            if (idDepartamento != null)
            {
                ML.Result resultado = BL.Departamento.GetById(idDepartamento.Value);

                if (resultado.Correct)
                {
                    departamento = (ML.Departamento)resultado.Object;
                    departamento.Area.Areas = resultadoAreas.Objects;
                }
            } else
            {
                departamento.Area.Areas = resultadoAreas.Objects;
            }

            return View(departamento);
        }

        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            if (departamento.IdDepartamento == 0)
            {
                ML.Result resultado = BL.Departamento.Add(departamento);

                if (resultado.Correct)
                {
                    ViewBag.Mensaje = "Se ha registrado el departamento: " + departamento.Nombre + " correctamente.";
                } else
                {
                    ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                }
            } else
            {
                ML.Result resultado = BL.Departamento.Update(departamento);

                if (resultado.Correct)
                {
                    ViewBag.Mensaje = "Se ha actualizado el departamento: " + departamento.Nombre + " correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idDepartamento)
        {
            ML.Result resultado = BL.Departamento.Delete(idDepartamento);

            if (resultado.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado el departamento con el id: " + idDepartamento + " correctamente.";
            } else
            {
                ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
            }

            return PartialView("Modal");
        }

    }
}