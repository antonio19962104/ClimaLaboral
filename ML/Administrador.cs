using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ML
{
    public class Administrador
    {
        public int AdminSA { get; set; }
        public string Code { get; set; }
        public int ID_EMPLEADO_FOR_UPDATE_PERMISOS { get; set; }
        public List<object> Objects { get; set; }
        public string CURRENT_USER { get; set; }
        public int IdAdministrador { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "El Username es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El Password es requerido")]
        [StringLength(12, ErrorMessage = "El password debe contener un minimo de 8 caracteres", MinimumLength = 8)]
        public string Password { get; set; }
        public ML.Empleado Empleado { get; set; }
        public int IdentificadorEstatus { get; set; }
        public ML.PerfilD4U PerfilD4U { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<object> ListPerfilD4U { get; set; }
        public List<string> Acciones { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public List<object> listUNegocio { get; set; }

        //Data for empleado
        
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "El Email es requerido")]
        public string Correo { get; set; }
        public string UNegocio { get; set; }
        public List<string> listaCompaniesForPermission { get; set; }
        public ML.Company Company { get; set; }
        public List<int> listId { get; set; }


        public enum Data
        {
            verdadero = 1,
            falso = 0
        }
        public Data Selected { get; set; }
    }
}
