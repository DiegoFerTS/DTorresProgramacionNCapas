using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SLWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductoService" in both code and config file together.
    [ServiceContract]
    public interface IProductoService
    {
        [OperationContract]
        SLWCF.Result Add(ML.Producto producto);

        [OperationContract]
        SLWCF.Result Update(ML.Producto Producto);

        [OperationContract]
        SLWCF.Result Delete(int idProducto);

        [OperationContract]
        SLWCF.Result GetAll();

        [OperationContract]
        SLWCF.Result GetById(int idProducto);

        [OperationContract]
        SLWCF.Result GetByIdArea(int idArea);

    }
}
