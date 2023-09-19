using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaMasivaTxt()
        {
            HttpPostedFileBase fileTxt = Request.Files["ArchivoTxt"];

            StreamReader streamReader = new StreamReader(fileTxt.InputStream);

            string line = streamReader.ReadLine(); //SALTAR HEDEARS

            while ((line = streamReader.ReadLine()) != null)
            {
                string[] row = line.Split('|');
                ML.Usuario usuario = new ML.Usuario();
                usuario.UserName = row[0];
                usuario.Nombre = row[1];
                usuario.ApellidoPaterno = row[2];
                usuario.ApellidoMaterno = row[3];
                usuario.Email = row[4];
                usuario.Password = row[5];
                usuario.Sexo = row[6];
                usuario.Telefono = row[7];
                usuario.Celular = row[8];
                usuario.FechaNacimiento = DateTime.Parse(row[9]);
                usuario.CURP = row[10];
                usuario.Rol = new ML.Rol();
                usuario.Rol.IdRol = int.Parse(row[11]);
                usuario.Status = (1 == int.Parse(row[12]));
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Calle = row[13];
                usuario.Direccion.NumeroInterior = row[14];
                usuario.Direccion.NumeroExterior = row[15];
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.IdColonia = int.Parse(row[16]);

                BL.Usuario.AddEF(usuario);

            }

            ViewBag.Mensaje = "Proceso de carga masiva terminado.";

            return PartialView("Modal");
        }

        [HttpPost]
        public ActionResult CargaMasiva(ML.Result result)
        {
            HttpPostedFileBase file = Request.Files["ArchivoExcelXlsx"];

            if (Session["pathExcel"] == null)
            {
                if (file != null)
                {

                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extesionValida = ConfigurationManager.AppSettings["TipoExcel"];

                    if (extensionArchivo == extesionValida)
                    {
                        string rutaproyecto = Server.MapPath(@"~\CargaMasiva\");
                        string filePath = rutaproyecto + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {

                            file.SaveAs(filePath);


                            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filePath;
                            ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    Session["pathExcel"] = filePath;
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                    }
                }
                else
                {
                    ViewBag.Message = "No selecciono ningun archivo, Seleccione uno correctamente";
                }
                return View(result);
            }
            else
            {
                string filepath = Session["pathExcel"].ToString();

                if (filepath != null)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filepath;
                    ML.Result resultUsuarios = BL.Usuario.LeerExcel(connectionString);

                    if (resultUsuarios.Correct)
                    {
                        ML.Result resultadoErrores = new ML.Result();
                        resultadoErrores.Objects = new List<object>();

                        foreach (ML.Usuario usuario in resultUsuarios.Objects)
                        {
                            ML.Result result1 = BL.Usuario.AddEF(usuario);
                            if (!result1.Correct)
                            {
                                string error = "Ha ocurrido un error al agregar al usuario con UserName: " + usuario.UserName + "\n" + "El error es: " +
                                                result1.ErrorMessage + "\n\n";
                                resultadoErrores.Objects.Add(error);
                            }

                            Session["pathExcel"] = null;

                            if (resultadoErrores.Objects.Count > 0)
                            {
                                string pathTxt = Server.MapPath(@"\Files\logErrores" + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");
                                result.Correct = false;

                                using (StreamWriter writer = new StreamWriter(pathTxt))
                                {
                                    foreach (string lineaError in resultadoErrores.Objects)
                                    {
                                        writer.WriteLine(lineaError);
                                    }
                                }
                            } else
                            {
                                result.Correct = true;
                            }

                        }
                    }

                }
         


            }
            return View(result);
        }



    }
}