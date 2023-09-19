using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int idMunicipio)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.ColoniaGetByIdMunicipio(idMunicipio).ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var registro in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = registro.IdColonia;
                            colonia.Nombre = registro.Nombre;

                            resultado.Objects.Add(colonia);
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
