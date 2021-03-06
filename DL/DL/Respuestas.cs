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
    
    public partial class Respuestas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Respuestas()
        {
            this.EmpleadoRespuestas = new HashSet<EmpleadoRespuestas>();
            this.UsuarioRespuestas = new HashSet<UsuarioRespuestas>();
            this.ConfiguraRespuesta = new HashSet<ConfiguraRespuesta>();
        }
    
        public int IdRespuesta { get; set; }
        public string Respuesta { get; set; }
        public Nullable<int> IdPregunta { get; set; }
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
        public Nullable<bool> Selected { get; set; }
        public Nullable<int> IdPreguntaLikertD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpleadoRespuestas> EmpleadoRespuestas { get; set; }
        public virtual Preguntas Preguntas { get; set; }
        public virtual TipoEstatus TipoEstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioRespuestas> UsuarioRespuestas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfiguraRespuesta> ConfiguraRespuesta { get; set; }
    }
}
