using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult GetAll()
        {
            ML.Result resultado = BL.Producto.GetAll();
            ML.Producto producto = new ML.Producto();

            if (resultado.Correct)
            {
                producto.Productos = resultado.Objects;
                return View(producto);
            } else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult Delete(int idProducto)
        {
            ML.Result resultado = BL.Producto.Delete(idProducto);

            if (resultado.Correct)
            {
                ViewBag.Mensaje = "El producto con el ID " + idProducto + " ha sido eliminado.";
            }
            else
            {
                ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(int? idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Proveedor = new ML.Proveedor();
            producto.Departamento = new ML.Departamento();
            producto.Departamento.Area = new ML.Area();

            ML.Result proveedorResult = BL.Proveedor.GetAll();
            ML.Result departamentoResult = BL.Departamento.GetAll(producto.Departamento);

            if(idProducto != null) // Update
            {
                ML.Result resultado = BL.Producto.GetById(idProducto.Value);

                if (resultado.Correct)
                {
                    producto = (ML.Producto)resultado.Object;

                    producto.Proveedor.Proveedores = proveedorResult.Objects;
                    producto.Departamento.Departamentos = departamentoResult.Objects;
                }
               

            } else  // Add
            {
                producto.Proveedor.Proveedores = proveedorResult.Objects;
                producto.Departamento.Departamentos = departamentoResult.Objects;
            }


            return View(producto);
        }


        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["ImagenProducto"];

                if (file.ContentLength > 0)
                {
                    producto.Imagen = ConvertirABase64(file);
                }

                if (producto.IdProducto == 0)  // Add
                {
                    ML.Result resultado = BL.Producto.Add(producto);

                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se ha registrado el producto " + producto.Nombre + " correctamente.";
                    } else {
                        ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                    }
                } else   // Update
                {
                    ML.Result resultado = BL.Producto.Update(producto);

                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el producto " + producto.Nombre + " correctamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                    }
                }

                return PartialView("Modal");

            } else if (!ModelState.IsValid)
            {
                producto.Proveedor = new ML.Proveedor();
                producto.Departamento = new ML.Departamento();
                ML.Departamento departamento = new ML.Departamento();

                ML.Result proveedorResult = BL.Proveedor.GetAll();
                ML.Result departamentoResult = BL.Departamento.GetAll(departamento);

                producto.Proveedor.Proveedores = proveedorResult.Objects;
                producto.Departamento.Departamentos = departamentoResult.Objects;
            }

            return View(producto);
        }


        public string ConvertirABase64(HttpPostedFileBase foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            byte[] data = reader.ReadBytes((int)foto.ContentLength);
            string imagen = Convert.ToBase64String(data);
            return imagen;
        }



        [HttpGet]
        public ActionResult GetByIdArea(int idArea, string nombreArea)
        {
            if (idArea != 0) 
            {
                ML.Producto producto = new ML.Producto();
                producto.Departamento = new ML.Departamento();
                producto.Departamento.Area = new ML.Area();
                producto.Proveedor = new ML.Proveedor();

                producto.Departamento.Area.Nombre = "Productos del area de " + nombreArea;

                ML.Result resultado = BL.Producto.GetByIdArea(idArea);

                if (resultado.Correct)
                {
                    producto.Productos = resultado.Objects;
                    return View(producto);
                }
                else
                {
                    return View();
                }
            } else
            {
                ML.Producto producto = new ML.Producto();
                producto.Departamento = new ML.Departamento();
                producto.Departamento.Area = new ML.Area();
                producto.Proveedor = new ML.Proveedor();

                producto.Departamento.Area.Nombre = "Productos de " + nombreArea;

                ML.Result resultado = BL.Producto.GetAll();

                if (resultado.Correct)
                {
                    producto.Productos = resultado.Objects;
                    return View(producto);
                }
                else
                {
                    return View();
                }
            }
            

        }
    }
}