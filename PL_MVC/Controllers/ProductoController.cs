using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult GetAll()
        {
            // Metodo getall normal
            //ML.Result resultado = BL.Producto.GetAll();


            // Metodo getall con servicio
            //ServiceReferenceProducto.ProductoServiceClient productoWCF = new ServiceReferenceProducto.ProductoServiceClient();

            /*
            var resultado = productoWCF.GetAll();

            ML.Producto producto = new ML.Producto();

            if (resultado.Correct)
            {
                producto.Productos = resultado.Objects;
                return View(producto);
            } else
            {
                return View();
            }
            */

            ML.Producto producto = new ML.Producto();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLApi"].ToString());

                var responseTask = client.GetAsync("producto");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    producto.Productos = new List<object>();

                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultProducto in readTask.Result.Objects)
                    {
                        ML.Producto productoRespuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultProducto.ToString());
                        producto.Productos.Add(productoRespuesta);
                    }

                    return View(producto);
                } else
                {
                    return View();
                }
            }
            

        }

        [HttpGet]
        public ActionResult Delete(int idProducto)
        {

            // Metodo delete normal
            //ML.Result resultado = BL.Producto.Delete(idProducto);

            // Metodo delete con servicio
            //ServiceReferenceProducto.ProductoServiceClient productoWCF = new ServiceReferenceProducto.ProductoServiceClient();
            //var resultado = productoWCF.Delete(idProducto);

            /*
            if (resultado.Correct)
            {
                ViewBag.Mensaje = "El producto con el ID " + idProducto + " ha sido eliminado.";
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
                var putTask = client.DeleteAsync("producto/" + idProducto);
                putTask.Wait();

                var result = putTask.Result;

                var readTask = result.Content.ReadAsAsync<ML.Result>();
                readTask.Wait();
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Se ha eliminado el producto con el id: " + idProducto + " correctamente.";
                }
                else
                {
                    ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                }
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
                // Metodo getbyid normal
                //ML.Result resultado = BL.Producto.GetById(idProducto.Value);

                // Metodo getbyid con servicio
                //ServiceReferenceProducto.ProductoServiceClient productoWCF = new ServiceReferenceProducto.ProductoServiceClient();
                //var resultado = productoWCF.GetById(idProducto.Value);

                /*
                if (resultado.Correct)
                {
                    producto = (ML.Producto)resultado.Object;

                    producto.Proveedor.Proveedores = proveedorResult.Objects;
                    producto.Departamento.Departamentos = departamentoResult.Objects;
                }
                */

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLApi"].ToString());

                    var responseTask = client.GetAsync("producto/" + idProducto);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {

                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        


                        producto = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(readTask.Result.Object.ToString());


                        producto.Proveedor.Proveedores = proveedorResult.Objects;
                        producto.Departamento.Departamentos = departamentoResult.Objects;

                    }
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

                    // Metodo add normal
                    //ML.Result resultado = BL.Producto.Add(producto);

                    // Metodo add con servicio
                    //ServiceReferenceProducto.ProductoServiceClient productoWCF = new ServiceReferenceProducto.ProductoServiceClient();
                    //var resultado = productoWCF.Add(producto);

                    /*
                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se ha registrado el producto " + producto.Nombre + " correctamente.";
                    } else {
                        ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                    }
                    */

                    using (var client = new HttpClient())  // ADD
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());

                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<ML.Producto>("producto", producto);
                        postTask.Wait();

                        var result = postTask.Result;

                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se ha registrado el producto: " + producto.Nombre + " correctamente.";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                        }
                    }
                } else   // Update
                {

                    // Metodo update normal
                    //ML.Result resultado = BL.Producto.Update(producto);

                    // Metodo update con servicio
                    //ServiceReferenceProducto.ProductoServiceClient productoWCF = new ServiceReferenceProducto.ProductoServiceClient();
                    //var resultado = productoWCF.Update(producto);

                    /*
                    if (resultado.Correct)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el producto " + producto.Nombre + " correctamente.";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Error: " + resultado.ErrorMessage;
                    }
                    */

                    using (var client = new HttpClient())  // Update
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["URLapi"].ToString());


                        //HTTP POST
                        var putTask = client.PutAsJsonAsync<ML.Producto>("producto/" + producto.IdProducto, producto);
                        putTask.Wait();

                        var result = putTask.Result;

                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Se ha actualizado el producto: " + producto.Nombre + " correctamente.";
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error: " + readTask.Result.ErrorMessage;
                        }
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

                // Metodo getbyidarea normal
                //ML.Result resultado = BL.Producto.GetByIdArea(idArea);

                // Metodo getbyidarea con servicio
                ServiceReferenceProducto.ProductoServiceClient productoWCF = new ServiceReferenceProducto.ProductoServiceClient();

                var resultado = productoWCF.GetByIdArea(idArea);

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