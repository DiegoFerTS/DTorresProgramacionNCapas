using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductoService.svc or ProductoService.svc.cs at the Solution Explorer and start debugging.
    public class ProductoService : IProductoService
    {
        public SLWCF.Result Add(ML.Producto producto)
        {

            ML.Result resultado = BL.Producto.Add(producto);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result Update(ML.Producto producto)
        {

            ML.Result resultado = BL.Producto.Update(producto);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result Delete(int idProducto)
        {

            ML.Result resultado = BL.Producto.Delete(idProducto);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result GetAll()
        {

            ML.Result resultado = BL.Producto.GetAll();

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result GetById(int idProducto)
        {

            ML.Result resultado = BL.Producto.GetById(idProducto);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result GetByIdArea(int idArea)
        {

            ML.Result resultado = BL.Producto.GetByIdArea(idArea);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }
    }
}
