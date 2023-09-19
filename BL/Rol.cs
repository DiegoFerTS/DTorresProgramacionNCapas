using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result resultado = new ML.Result();
            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.RolGetAll().ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();
                        foreach (var registro in query)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = registro.IdRol;
                            rol.Nombre = registro.Nombre;
                            resultado.Objects.Add(rol);
                        }

                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontro ningun rol.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.ErrorMessage = "\n" + ex.Message;
                resultado.Ex = ex;
            }
            return resultado;
        }
    }
}
