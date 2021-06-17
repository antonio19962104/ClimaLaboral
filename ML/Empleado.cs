using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int IdEmpleadoRH { get; set; }
        public string MensajeEmpleado { get; set; }
        public string dateNacim { get; set; }
        public string dateAntig { get; set; }
        public BasesDeDatos BaseDeDatos { get; set; }
        public ML.EstatusEncuesta EstatusEncuesta { get; set; }
        public int IdEmpleado { get; set; }
        public int NumeroEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public ML.Perfil Perfil { get; set; }
        public ML.Departamento Departamento { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public ML.ClavesAcceso ClavesAcceso { get; set; }
        public List<ML.Empleado> EmpleadoResultSearch { get; set; }
        public ML.Subdepartamento Subdepartamento { get; set; }

        public string Puesto { get; set; }
        public DateTime FechaNaciemiento { get; set; }
        public DateTime FechaAntiguedad { get; set; }
        public string Sexo { get; set; }
        public string CondicionTrabajo { get; set; }
        public string GradoAcademico { get; set; }
        public string EmpresaContratante { get; set; }
        public int IdResponsableRH { get; set; }
        public string NombreResponsableRH { get; set; }
        public int IdJefe { get; set; }
        public string NombreJefe { get; set; }
        public string PuestoJefe { get; set; }
        public int IdRespinsableEstructura { get; set; }
        public string NombreResponsableEstrucutra { get; set; }
        public string RangoAntiguedad { get; set; }
        public string RangoEdad { get; set; }
        public string EstatusEmpleado { get; set; }
        public string UnidadNegocio { get; set; }
        public string DivisonMarca { get; set; }
        public string TipoFuncion { get; set; }
        public string AreaAgencia { get; set; }
        public string Depto { get; set; }
        public string Subdepto { get; set; }

    }
}
