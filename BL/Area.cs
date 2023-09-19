using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAll(string nombreArea)
        {

            if (nombreArea == "" || nombreArea == null)
            {
                nombreArea = "";
            }

            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.AreaGetAll(nombreArea).ToList();

                    resultado.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            ML.Area area = new ML.Area();
                           
                            area.IdArea = registro.IdArea;
                            area.Nombre = registro.Nombre;

                            resultado.Objects.Add(area);
                        }
                        resultado.Correct = true;
                    }
                    else
                    {
                        resultado.ErrorMessage = "No se encontraron datos.";
                        resultado.Correct = false;
                    }

                }
            }
            catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }
    }
}
