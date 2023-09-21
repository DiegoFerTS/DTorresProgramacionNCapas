using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SLWCF
{
    [DataContract]
    [KnownType(typeof(ML.Departamento))]
    [KnownType(typeof(ML.Area))]
    [KnownType(typeof(ML.Producto))]
    [KnownType(typeof(ML.Proveedor))]
    public class Result
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public bool Correct { get; set; }
        [DataMember]
        public object Object { get; set; }
        [DataMember]
        public List<object> Objects { get; set; }
        [DataMember]
        public Exception Ex { get; set; }
    }
}