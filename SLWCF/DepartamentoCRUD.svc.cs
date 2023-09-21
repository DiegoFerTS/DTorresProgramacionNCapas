using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DepartamentoCRUD" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DepartamentoCRUD.svc or DepartamentoCRUD.svc.cs at the Solution Explorer and start debugging.
    public class DepartamentoCRUD : IDepartamentoCRUD
    {

        public SLWCF.Result Add(ML.Departamento departamento)
        {
           
            ML.Result resultado = BL.Departamento.Add(departamento);

            return new SLWCF.Result {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result Update(ML.Departamento departamento)
        {

            ML.Result resultado = BL.Departamento.Update(departamento);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result Delete(int idDepartamento)
        {

            ML.Result resultado = BL.Departamento.Delete(idDepartamento);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result GetAll(ML.Departamento departamento)
        {

            ML.Result resultado = BL.Departamento.GetAll(departamento);

            return new SLWCF.Result
            {
                ErrorMessage = resultado.ErrorMessage,
                Correct = resultado.Correct,
                Object = resultado.Object,
                Objects = resultado.Objects,
                Ex = resultado.Ex
            };
        }

        public SLWCF.Result GetById(int idDepartamento)
        {

            ML.Result resultado = BL.Departamento.GetById(idDepartamento);

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
