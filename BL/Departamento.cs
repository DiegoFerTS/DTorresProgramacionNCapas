using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {

        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.DepartamentoAdd(departamento.Nombre, departamento.Area.IdArea, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo realizar el registro";
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;

        }

        public static ML.Result Update(ML.Departamento departamento)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.DepartamentoUpdate(departamento.IdDepartamento, departamento.Nombre, departamento.Area.IdArea, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo actualizar el departamento: " + departamento.Nombre;
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

        public static ML.Result Delete(int idDepartamento)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ML.Departamento departamento = new ML.Departamento();

                    ObjectParameter filasAfectadas = new ObjectParameter("FilasAfectadas", typeof(int));
                    var query = context.DepartamentoDelete(idDepartamento, filasAfectadas);

                    if (query > 0)
                    {
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se pudo eliminar el departamento con id: " + idDepartamento;
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }

        public static ML.Result GetAll(ML.Departamento departamentoConsultado)
        {

            if (departamentoConsultado.Area.IdArea == null)
            {
                departamentoConsultado.Area.IdArea = 0;
            }

            if (departamentoConsultado.Area.Nombre == null)
            {
                departamentoConsultado.Area.Nombre = "";
            }

            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    var query = context.DepartamentoGetAll(departamentoConsultado.Area.IdArea, departamentoConsultado.Area.Nombre).ToList();

                    resultado.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = registro.IdDepartamento;
                            departamento.Nombre = registro.NombreDepartamento;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = registro.IdArea;
                            departamento.Area.Nombre = registro.NombreArea;

                            resultado.Objects.Add(departamento);
                        }
                        resultado.Correct = true;
                    } else
                    {
                        resultado.ErrorMessage = "No se encontraron datos.";
                        resultado.Correct = false;
                    }
                    
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }
                        
            return resultado;
        }


        public static ML.Result GetById(int idDepartamento)
        {
            ML.Result resultado = new ML.Result();

            try
            {
                using (DLEF.DTorresProgramacionNCapasEntities context = new DLEF.DTorresProgramacionNCapasEntities())
                {
                    ML.Departamento departamentoResult = new ML.Departamento();

                    var query = context.DepartamentoGetById(idDepartamento).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = query.IdDepartamento;
                        departamento.Nombre = query.NombreDepartamento;
                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = query.IdArea;
                        departamento.Area.Nombre = query.NombreArea;

                        resultado.Object = departamento;
                        resultado.Correct = true;
                    } else
                    {
                        resultado.Correct = false;
                        resultado.ErrorMessage = "No se encontro el departamento con el id: " + idDepartamento;
                    }
                }
            } catch (Exception ex)
            {
                resultado.Correct = false;
                resultado.ErrorMessage = ex.Message;
                resultado.Ex = ex;
            }

            return resultado;
        }


    }
}
