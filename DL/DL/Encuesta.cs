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
    
    public partial class Encuesta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Encuesta()
        {
            this.EncuestaArea = new HashSet<EncuestaArea>();
            this.EncuestaPregunta = new HashSet<EncuestaPregunta>();
            this.EncuestaReporte = new HashSet<EncuestaReporte>();
            this.EncuestaUnidadNegocio = new HashSet<EncuestaUnidadNegocio>();
            this.EstatusEncuesta = new HashSet<EstatusEncuesta>();
            this.Preguntas = new HashSet<Preguntas>();
            this.UsuarioRespuestas = new HashSet<UsuarioRespuestas>();
            this.ConfiguraRespuesta = new HashSet<ConfiguraRespuesta>();
            this.ConfigClimaLab = new HashSet<ConfigClimaLab>();
            this.UsuarioEstatusEncuesta = new HashSet<UsuarioEstatusEncuesta>();
            this.EstatusEmail = new HashSet<EstatusEmail>();
            this.EmpleadoRespuestas = new HashSet<EmpleadoRespuestas>();
            this.ValoracionSubcategoriaPorCategoria = new HashSet<ValoracionSubcategoriaPorCategoria>();
            this.ValoracionPreguntaPorSubcategoria = new HashSet<ValoracionPreguntaPorSubcategoria>();
        }
    
        public int IdEncuesta { get; set; }
        public string Nombre { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public Nullable<bool> Estatus { get; set; }
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
        public string CodeHTML { get; set; }
        public Nullable<int> IdPlantilla { get; set; }
        public Nullable<int> IdBasesDeDatos { get; set; }
        public string Descripcion { get; set; }
        public string Instruccion { get; set; }
        public string ImagenInstruccion { get; set; }
        public Nullable<int> IdTipoEncuesta { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public string Agradecimiento { get; set; }
        public string ImagenAgradecimiento { get; set; }
        public string UID { get; set; }
        public Nullable<bool> DosColumnas { get; set; }
        public Nullable<int> IdTipoOrden { get; set; }
    
        public virtual BasesDeDatos BasesDeDatos { get; set; }
        public virtual Plantillas Plantillas { get; set; }
        public virtual TipoEncuesta TipoEncuesta { get; set; }
        public virtual TipoEstatus TipoEstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EncuestaArea> EncuestaArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EncuestaPregunta> EncuestaPregunta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EncuestaReporte> EncuestaReporte { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EncuestaUnidadNegocio> EncuestaUnidadNegocio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstatusEncuesta> EstatusEncuesta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Preguntas> Preguntas { get; set; }
        public virtual Encuesta Encuesta1 { get; set; }
        public virtual Encuesta Encuesta2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioRespuestas> UsuarioRespuestas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfiguraRespuesta> ConfiguraRespuesta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfigClimaLab> ConfigClimaLab { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioEstatusEncuesta> UsuarioEstatusEncuesta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstatusEmail> EstatusEmail { get; set; }
        public virtual TipoOrden TipoOrden { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpleadoRespuestas> EmpleadoRespuestas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValoracionSubcategoriaPorCategoria> ValoracionSubcategoriaPorCategoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ValoracionPreguntaPorSubcategoria> ValoracionPreguntaPorSubcategoria { get; set; }
    }
}
