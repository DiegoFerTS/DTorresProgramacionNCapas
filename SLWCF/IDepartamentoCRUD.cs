using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDepartamentoCRUD" in both code and config file together.
    [ServiceContract]
    public interface IDepartamentoCRUD
    {
        [OperationContract]
        SLWCF.Result Add(ML.Departamento departamento);

        [OperationContract]
        SLWCF.Result Update(ML.Departamento departamento);

        [OperationContract]
        SLWCF.Result Delete(int odDepartamento);

        [OperationContract]
        SLWCF.Result GetAll(ML.Departamento departamento);

        [OperationContract]
        SLWCF.Result GetById(int idDepartamento);
    }
}
