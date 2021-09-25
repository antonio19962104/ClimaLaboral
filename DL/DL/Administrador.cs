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
    
    public partial class Administrador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administrador()
        {
            this.Administrador1 = new HashSet<Administrador>();
            this.BasesDeDatos = new HashSet<BasesDeDatos>();
            this.Company1 = new HashSet<Company>();
            this.Competencia = new HashSet<Competencia>();
            this.PerfilModulo = new HashSet<PerfilModulo>();
            this.AdministradorCompany = new HashSet<AdministradorCompany>();
        }
    
        public int IdAdministrador { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<int> IdPerfil { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> IdEstatus { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<int> IdAdministradorCreate { get; set; }
        public Nullable<int> AdminSA { get; set; }
    
        public virtual TipoEstatus TipoEstatus { get; set; }
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administrador> Administrador1 { get; set; }
        public virtual Administrador Administrador2 { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual PerfilD4U PerfilD4U { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasesDeDatos> BasesDeDatos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> Company1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Competencia> Competencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerfilModulo> PerfilModulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdministradorCompany> AdministradorCompany { get; set; }
    }
}
