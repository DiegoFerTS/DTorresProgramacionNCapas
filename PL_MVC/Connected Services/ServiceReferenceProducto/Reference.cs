﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PL_MVC.ServiceReferenceProducto {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceProducto.IProductoService")]
    public interface IProductoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/Add", ReplyAction="http://tempuri.org/IProductoService/AddResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SLWCF.Result))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Proveedor))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Area))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Producto))]
        SLWCF.Result Add(ML.Producto producto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/Add", ReplyAction="http://tempuri.org/IProductoService/AddResponse")]
        System.Threading.Tasks.Task<SLWCF.Result> AddAsync(ML.Producto producto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/Update", ReplyAction="http://tempuri.org/IProductoService/UpdateResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SLWCF.Result))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Proveedor))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Area))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Producto))]
        SLWCF.Result Update(ML.Producto Producto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/Update", ReplyAction="http://tempuri.org/IProductoService/UpdateResponse")]
        System.Threading.Tasks.Task<SLWCF.Result> UpdateAsync(ML.Producto Producto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/Delete", ReplyAction="http://tempuri.org/IProductoService/DeleteResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Producto))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Area))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Proveedor))]
        SLWCF.Result Delete(int idProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/Delete", ReplyAction="http://tempuri.org/IProductoService/DeleteResponse")]
        System.Threading.Tasks.Task<SLWCF.Result> DeleteAsync(int idProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/GetAll", ReplyAction="http://tempuri.org/IProductoService/GetAllResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Producto))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Area))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Proveedor))]
        SLWCF.Result GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/GetAll", ReplyAction="http://tempuri.org/IProductoService/GetAllResponse")]
        System.Threading.Tasks.Task<SLWCF.Result> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/GetById", ReplyAction="http://tempuri.org/IProductoService/GetByIdResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Producto))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Area))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Proveedor))]
        SLWCF.Result GetById(int idProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/GetById", ReplyAction="http://tempuri.org/IProductoService/GetByIdResponse")]
        System.Threading.Tasks.Task<SLWCF.Result> GetByIdAsync(int idProducto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/GetByIdArea", ReplyAction="http://tempuri.org/IProductoService/GetByIdAreaResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Producto))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Departamento))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Area))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ML.Proveedor))]
        SLWCF.Result GetByIdArea(int idArea);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IProductoService/GetByIdArea", ReplyAction="http://tempuri.org/IProductoService/GetByIdAreaResponse")]
        System.Threading.Tasks.Task<SLWCF.Result> GetByIdAreaAsync(int idArea);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IProductoServiceChannel : PL_MVC.ServiceReferenceProducto.IProductoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductoServiceClient : System.ServiceModel.ClientBase<PL_MVC.ServiceReferenceProducto.IProductoService>, PL_MVC.ServiceReferenceProducto.IProductoService {
        
        public ProductoServiceClient() {
        }
        
        public ProductoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SLWCF.Result Add(ML.Producto producto) {
            return base.Channel.Add(producto);
        }
        
        public System.Threading.Tasks.Task<SLWCF.Result> AddAsync(ML.Producto producto) {
            return base.Channel.AddAsync(producto);
        }
        
        public SLWCF.Result Update(ML.Producto Producto) {
            return base.Channel.Update(Producto);
        }
        
        public System.Threading.Tasks.Task<SLWCF.Result> UpdateAsync(ML.Producto Producto) {
            return base.Channel.UpdateAsync(Producto);
        }
        
        public SLWCF.Result Delete(int idProducto) {
            return base.Channel.Delete(idProducto);
        }
        
        public System.Threading.Tasks.Task<SLWCF.Result> DeleteAsync(int idProducto) {
            return base.Channel.DeleteAsync(idProducto);
        }
        
        public SLWCF.Result GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<SLWCF.Result> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public SLWCF.Result GetById(int idProducto) {
            return base.Channel.GetById(idProducto);
        }
        
        public System.Threading.Tasks.Task<SLWCF.Result> GetByIdAsync(int idProducto) {
            return base.Channel.GetByIdAsync(idProducto);
        }
        
        public SLWCF.Result GetByIdArea(int idArea) {
            return base.Channel.GetByIdArea(idArea);
        }
        
        public System.Threading.Tasks.Task<SLWCF.Result> GetByIdAreaAsync(int idArea) {
            return base.Channel.GetByIdAreaAsync(idArea);
        }
    }
}
