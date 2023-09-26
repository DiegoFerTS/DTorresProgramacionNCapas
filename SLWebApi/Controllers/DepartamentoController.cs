using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace SLWebApi.Controllers
{
    [RoutePrefix("api/departamento")]
    public class DepartamentoController : ApiController
    {
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.Add(departamento);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idDepartamento}")]
        [HttpPut]
        public IHttpActionResult Update(int idDepartamento, [FromBody]ML.Departamento departamento)
        {

            departamento.IdDepartamento = idDepartamento;
            ML.Result result = BL.Departamento.Update(departamento);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idDepartamento}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idDepartamento)
        {
            ML.Result result = BL.Departamento.Delete(idDepartamento);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("GetAll/{idArea?}/{nombreArea?}")]
        [HttpGet]
        public IHttpActionResult GetAll(int? idArea = 0, string nombreArea = null)
        {

            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();
            departamento.Area.IdArea = idArea.Value;
            departamento.Area.Nombre = nombreArea;

            var result = BL.Departamento.GetAll(departamento);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("{idDepartamento}")]
        [HttpGet]
        public IHttpActionResult GetById(int idDepartamento)
        {
            var result = BL.Departamento.GetById(idDepartamento);
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
