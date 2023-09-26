using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/producto")]
    public class ProductoController : ApiController
    {

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Producto producto)
        {
            ML.Result result = BL.Producto.Add(producto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idProducto}")]
        [HttpPut]
        public IHttpActionResult Update(int idProducto, [FromBody] ML.Producto producto)
        {

            producto.IdProducto = idProducto;
            ML.Result result = BL.Producto.Update(producto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idProducto}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idProducto)
        {
            ML.Result result = BL.Producto.Delete(idProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var result = BL.Producto.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("{idProducto}")]
        [HttpGet]
        public IHttpActionResult GetById(int idProducto)
        {
            var result = BL.Producto.GetById(idProducto);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
