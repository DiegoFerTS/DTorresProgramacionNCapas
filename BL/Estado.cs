using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int idPais)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.EstadoGetByIdPais(idPais).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var registro in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            estado.IdEstado = registro.IdEstado;
                            estado.Nombre = registro.Nombre;

                            resultado.Objects.Add(estado);
                        }

                        resultado.Correct = true;

                    }
                    else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "Al parecer no se encontro ningun registro.";
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
