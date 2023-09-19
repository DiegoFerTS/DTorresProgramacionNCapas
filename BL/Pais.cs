using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.PaisGetAll().ToList();

                    if (query != null)
                    {
                        resultado.Objects = new List<object>();

                        foreach (var registro in query)
                        {
                            ML.Pais pais = new ML.Pais();

                            pais.IdPais = registro.IdPais;
                            pais.Nombre = registro.Nombre;

                            resultado.Objects.Add(pais);
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
