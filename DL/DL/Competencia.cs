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
    
    public partial class Competencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Competencia()
        {
            this.Preguntas = new HashSet<Preguntas>();
            this.PuntuacionCompetencia = new HashSet<PuntuacionCompetencia>();
        }
    
        public int IdCompetencia { get; set; }
        public string Nombre { get; set; }
        public Nullable<int> Valoracion { get; set; }
        public Nullable<int> IdEstatus { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public Nullable<int> IdAdminCreate { get; set; }
    
        public virtual TipoEstatus TipoEstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Preguntas> Preguntas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PuntuacionCompetencia> PuntuacionCompetencia { get; set; }
        public virtual Administrador Administrador { get; set; }
    }
}