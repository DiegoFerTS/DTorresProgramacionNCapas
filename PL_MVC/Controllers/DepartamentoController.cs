using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PL_MVC.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            // Metodo getall normal
            //ML.Result resultadoDepartamento = BL.Departamento.GetAll(departamento);

            // Metodo getall con servicio
            //ServiceReferenceDepartamento.DepartamentoCRUDClient departamentoWCF = new ServiceReferenceDepartamento.DepartamentoCRUDClient();
            //var resultadoDepartamento = departamentoWCF.GetAll(departamento);

            /*
            ML.Result resultadoArea = BL.Area.GetAll("");


            if (resultadoDepartamento.Correct)
            {
                departamento.Departamentos = resultadoDepartamento.Objects;
                departamento.Area.Areas = resultadoArea.Objects;
                return View(departamento);
            }
            else
            {
                return View();
            }*/

            departamento.Departamentos = new List<object>();
            ML.Result resultadoArea = BL.Area.GetAll("");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLApi"].ToString());

                var responseTask = client.GetAsync("departamento/GetAll/" + null + "/" + null);
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    departamento.Area.Areas = resultadoArea.Objects;

                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultDepartamento in readTask.Result.Objects)
                    {
                        ML.Departamento departamentoRespuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultDepartamento.ToString());
                        departamento.Departamentos.Add(departamentoRespuesta);
                    }
                }
            }
            return View(departamento);
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

            // Metodo getall normal
            // ML.Result resultadoDepartamento = BL.Departamento.GetAll(departamentoConsultado);

            // Metodo getall con servicio
            //ServiceReferenceDepartamento.DepartamentoCRUDClient departamentoWCF = new ServiceReferenceDepartamento.DepartamentoCRUDClient();
            //var resultadoDepartamento = departamentoWCF.GetAll(departamentoConsultado);

            /*
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
            */

            ML.Result resultadoArea = BL.Area.GetAll("");


            ML.Departamento departamentoResult = new ML.Departamento();
            departamentoResult.Departamentos = new List<object>();
            departamentoResult.Area = new ML.Area();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLApi"].ToString());

                var responseTask = client.GetAsync("departamento/GetAll/" + departamentoConsultado.Area.IdArea + "/" + departamentoConsultado.Area.Nombre);
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultDepartamento in readTask.Result.Objects)
                    {
                        ML.Departamento departamentoRespuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultDepartamento.ToString());
                        departamentoResult.Departamentos.Add(departamentoRespuesta);
                    }
                    departamentoResult.Area.Areas = resultadoArea.Objects;

                }
            }
            return View(departamentoResult);

        }

        [HttpGet]
        public ActionResult Form(int? idDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            ML.Result resultadoAreas = BL.Area.GetAll("");

            if (idDepartamento != null)
            {
                // Metodo getbyid normal
                //ML.Result resultado = BL.Departamento.GetById(idDepartamento.Value);

                // Metodo getbyid con servicio
                //ServiceReferenceDepartamento.DepartamentoCRUDClient departamentoWCF = new ServiceReferenceDepartamento.DepartamentoCRUDClient();
                //var resultado = departamentoWCF.GetById(idDepartamento.Value);

                /*
                if (resultado.Correct)
                {
                    departamento = (ML.Departamento)resultado.Object;
                    departamento.Area.Areas = resultadoAreas.Objects;
                }
                */

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLApi"].ToString());

                    var responseTask = client.GetAsync("departamento/" + idDepartamento);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {

                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();


                        departamento = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(readTask.Result.Object.ToString());

                        departamento.Area.Areas = resultadoAreas.Objects;

                    }
                }
            }
            else
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
                /* Metodo add Normal
                ML.Result resultado = BL.Departamento.Add(departamento);
                */

                //Metodo add con servicio
                //ServiceReferenceDepartamento.DepartamentoCRUDClient departamentoWCF = new ServiceReferenceDepartamento.DepartamentoCRUDClient();
                //var resultado = departamentoWCF.Add(departamento);


                /*
                if (resultado.Correct)
                {
                    ViewBag.Mensaje = "Se ha registrado el departamento: " + departamento.Nombre + " correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                }*/

                using (var client = new HttpClient())  // ADD
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Departamento>("departamento", departamento);
                    postTask.Wait();

                    var result = postTask.Result;

                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha registrado el departamento: " + departamento.Nombre + " correctamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                    }
                }

            }
            else
            {
                /* Metodo update normal
                ML.Result resultado = BL.Departamento.Update(departamento);
                */

                // Metodo update con servicio
                //ServiceReferenceDepartamento.DepartamentoCRUDClient departamentoWCF = new ServiceReferenceDepartamento.DepartamentoCRUDClient();
                //var resultado = departamentoWCF.Update(departamento);

                /*
                if (resultado.Correct)
                {
                    ViewBag.Mensaje = "Se ha actualizado el departamento: " + departamento.Nombre + " correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                }*/

                using (var client = new HttpClient())  // Update
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());


                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<ML.Departamento>("departamento/" + departamento.IdDepartamento, departamento);
                    putTask.Wait();
                                        
                    var result = putTask.Result;

                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el departamento: " + departamento.Nombre + " correctamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                    }
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int idDepartamento)
        {
            // Metodo delete normal
            //ML.Result resultado = BL.Departamento.Delete(idDepartamento);

            // Metodo delete con servicio
            //ServiceReferenceDepartamento.DepartamentoCRUDClient departamentoWCF = new ServiceReferenceDepartamento.DepartamentoCRUDClient();
            //var resultado = departamentoWCF.Delete(idDepartamento);

            /*
            if (resultado.Correct)
            {
                ViewBag.Mensaje = "Se ha eliminado el departamento con el id: " + idDepartamento + " correctamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
            }

            return PartialView("Modal");
            */

            using (var client = new HttpClient())  // Delete
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());


                //HTTP delete
                var putTask = client.DeleteAsync("departamento/" + idDepartamento);
                putTask.Wait();

                var result = putTask.Result;

                var readTask = result.Content.ReadAsAsync<ML.Result>();
                readTask.Wait();
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado el departamento con el id: " + idDepartamento + " correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                }
            }

            return PartialView("Modal");


        }
    }
}