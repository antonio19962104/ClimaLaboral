using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public string cadenaDateNacimiento { get; set; }
        public string cadenaDateAntiguedad { get; set; }
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ML.Empleado Empleado { get; set; }
        public ML.BasesDeDatos BaseDeDatos { get; set; }
        public ML.Perfil Perfil { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public string TipoUsuario { get; set; }

        //new
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Puesto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaAntiguedad { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string TipoFuncion { get; set; }
        public string CondicionTrabajo { get; set; }
        public string GradoAcademico { get; set; }
        public string UnidadNegocio { get; set; }
        public string DivisionMarca { get; set; }
        public string AreaAgencia { get; set; }
        public string Departamento { get; set; }
        public string Subdepartamento { get; set; }
        public string EmpresaContratante { get; set; }
        public int IdResponsableRH { get; set; }
        public string NombreResponsableRH { get; set; }
        public int IdJefe { get; set; }
        public string NombreJefe { get; set; }
        public string PuestoJefe { get; set; }
        public int IdRespinsableEstructura { get; set; }
        public string NombreResponsableEstructura { get; set; }
        public string ClaveAcceso { get; set; }
        public string RangoAntiguedad { get; set; }
        public string RangoEdad { get; set; }
        public ML.EstatusEncuesta EstatusEncuesta { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public int IdEstatus { get; set; }
        public int CampoNumerico_1 { get; set; }
        public int CampoNumerico_2 { get; set; }
        public int CampoNumerico_3 { get; set; }
        public string CampoDeTexto_1 { get; set; }
        public string CampoDeTexto_2 { get; set; }
        public string CampoDeTexto_3 { get; set; }
        public string dateNacim { get; set; }
        public string dateAntig { get; set; }
        public int IdEncuesta { get; set; }
    }
}
