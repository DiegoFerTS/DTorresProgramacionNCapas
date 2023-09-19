using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {

        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultado = BL.Usuario.GetAllEF(usuario);
            if (resultado.Correct)
            {
                usuario.Usuarios = resultado.Objects;
                return View(usuario);
            } else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuarioConsultado)
        {
            ML.Result resultado = BL.Usuario.GetAllEF(usuarioConsultado);

            ML.Usuario usuario = new ML.Usuario();
            if (resultado.Correct)
            {
                usuario.Usuarios = resultado.Objects;
                return View(usuario);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


            ML.Result resultadoRol = BL.Rol.GetAll();
            ML.Result resultadoPais = BL.Pais.GetAll();

            ViewBag.Estado = false;
            ViewBag.Municipio = false;
            ViewBag.Colonia = false;

            if (idUsuario != null)
            {
                ML.Result resultado = BL.Usuario.GetByIdEF(idUsuario.Value);
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultadoPais.Objects;

                if (resultado.Correct)
                {
                    usuario = (ML.Usuario)resultado.Object;
                    usuario.Rol.Roles = resultadoRol.Objects;

                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultadoPais.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais).Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;
                    usuario.Direccion.Colonia.Colonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;

                }
            } else
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultadoPais.Objects;
                usuario.Rol.Roles = resultadoRol.Objects;
            }

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            if (ModelState.IsValid)
            {

                ViewBag.Estado = false;
                ViewBag.Municipio = false;
                ViewBag.Colonia = false;
                HttpPostedFileBase file = Request.Files["ImagenUsuario"];
                if (file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirABase64(file);
                }

                if (usuario.IdUsuario == 0)     //ADD
                {

                    ML.Result resultado = BL.Usuario.AddEF(usuario);
                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se ha registrado al usuario " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno + " correctamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                    }
                }
                else    //Update
                {
                    ML.Result resultado = BL.Usuario.UpdateEF(usuario);
                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se ha actualizado al usuario " + usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno + " correctamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                    }
                }
                return PartialView("Modal");

            } else if(!ModelState.IsValid)
            {

                ML.Result resultadoRol = BL.Rol.GetAll();
                ML.Result resultadoPais = BL.Pais.GetAll();

                usuario.Rol.Roles = resultadoRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultadoPais.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais).Objects;
                usuario.Direccion.Colonia.Municipio.Municipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado).Objects;
                usuario.Direccion.Colonia.Colonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio).Objects;

                ViewBag.Estado = true;
                ViewBag.Municipio = true;
                ViewBag.Colonia = true;
            } 

            return View(usuario);

        }

        [HttpGet]
        public ActionResult Delete(int? idUsuario)
        {
            ML.Result resultado = BL.Usuario.DeleteEF(idUsuario);

            if (resultado.Correct)
            {
                ViewBag.Mensaje = "El usuario con el ID " + idUsuario + " ha sido eliminado.";
            }
            else
            {
                ViewBag.Mensaje = "Algo salio mal.";
            }
            return PartialView("Modal");

        }


        // METODOS PARA OBTENCION DE DATOS RELACIONADOS CON LA DIRECCION

        public JsonResult EstadoGetByIdPais(int idPais)
        {
            ML.Result resultado = BL.Estado.GetByIdPais(idPais);
            return Json(resultado.Objects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MunicipioGetByIdEstado(int idEstado)
        {
            ML.Result resultado = BL.Municipio.GetByIdEstado(idEstado);
            return Json(resultado.Objects, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColoniaGetByIdMunicipio(int idMunicipio)
        {
            ML.Result resultado = BL.Colonia.GetByIdMunicipio(idMunicipio);
            return Json(resultado.Objects, JsonRequestBehavior.AllowGet);
        }


        // Metodo para convertir una imagen a base64
        public string ConvertirABase64(HttpPostedFileBase foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            byte[] data = reader.ReadBytes((int)foto.ContentLength);
            string imagen = Convert.ToBase64String(data);
            return imagen;
        }

        // Metodo para cmabiar el status de un usuario
        public JsonResult ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result resultado = BL.Usuario.ChangeStatus(IdUsuario, Status);

            return Json(null);
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


    }



}