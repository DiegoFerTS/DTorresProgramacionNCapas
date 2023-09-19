using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        [Required]
        public string Calle { get; set; }
        public string NumeroInterior { get; set; }
        [Required]
        public string NumeroExterior { get; set; }
        public int IdColonia { get; set; }
        public int IdUsuario { get; set; }
        public ML.Colonia Colonia { get; set; }
    }
}
