//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            this.Administrador = new HashSet<Administrador>();
            this.EmpleadoRespuestas = new HashSet<EmpleadoRespuestas>();
            this.EstatusEncuesta = new HashSet<EstatusEncuesta>();
            this.Usuario = new HashSet<Usuario>();
        }
    
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public string ClaveAcceso { get; set; }
        public Nullable<int> IdDepartamento { get; set; }
        public string Puesto { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public Nullable<System.DateTime> FechaAntiguedad { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> IdPerfil { get; set; }
        public string CondicionTrabajo { get; set; }
        public string GradoAcademico { get; set; }
        public string EmpresaContratante { get; set; }
        public Nullable<int> IdResponsableRH { get; set; }
        public string NombreResponsableRH { get; set; }
        public Nullable<int> IdJefe { get; set; }
        public string NombreJefe { get; set; }
        public string PuestoJefe { get; set; }
        public Nullable<int> IdResponsableEstructura { get; set; }
        public string NombreResponsableEstructura { get; set; }
        public string RangoAntiguedad { get; set; }
        public string RangoEdad { get; set; }
        public string EstatusEmpleado { get; set; }
        public Nullable<int> IdSubdepartamento { get; set; }
        public string TipoFuncion { get; set; }
        public string UnidadNegocio { get; set; }
        public string DivisionMarca { get; set; }
        public string AreaAgencia { get; set; }
        public string Depto { get; set; }
        public string Subdepartamento { get; set; }
        public Nullable<int> IdBaseDeDatos { get; set; }
        public Nullable<int> IdEmpleadoRH { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administrador> Administrador { get; set; }
        public virtual BasesDeDatos BasesDeDatos { get; set; }
        public virtual ClavesAcceso ClavesAcceso { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual SubDepartamento SubDepartamento1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpleadoRespuestas> EmpleadoRespuestas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstatusEncuesta> EstatusEncuesta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
