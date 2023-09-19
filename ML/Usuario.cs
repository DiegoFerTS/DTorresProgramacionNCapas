using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }
        [MinLength(0)]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }
        [Required]
        [StringLength(254)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(12)]
        [RegularExpression("^[0-9]{10,12}$")]
        public string Telefono { get; set; }
        [RegularExpression("^[0-9]{10,12}$")]
        public string Celular { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [RegularExpression("^[A-Z0-9]{18}$")]
        public string CURP { get; set; }
        public ML.Rol Rol { get; set; }
        public string Imagen { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Direccion Direccion { get; set; }
        public bool Status { get; set; }
    }
}
