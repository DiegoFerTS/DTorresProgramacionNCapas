using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.ProveedorGetAll().ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var proveedorResult in query)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = proveedorResult.IdProveedor;
                            proveedor.Nombre = proveedorResult.Nombre;
                           
                            resultado.Objects.Add(proveedor);
                        }

                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontraron datos";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
    }
}
