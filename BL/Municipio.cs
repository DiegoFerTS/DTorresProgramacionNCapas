using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int idEstado)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.MunicipioGetByIdEstado(idEstado).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var registro in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();

                            municipio.IdMunicipio = registro.IdMunicipio;
                            municipio.Nombre = registro.Nombre;

                            resultado.Objects.Add(municipio);
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
