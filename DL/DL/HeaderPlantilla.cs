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
    
    public partial class HeaderPlantilla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HeaderPlantilla()
        {
            this.Plantillas = new HashSet<Plantillas>();
        }
    
        public int IdHeaderPlantilla { get; set; }
        public string Nombre { get; set; }
        public string CodeHTML { get; set; }
        public string ImagenFondo { get; set; }
        public string LogoIco { get; set; }
        public Nullable<int> LogoAlinea { get; set; }
        public Nullable<int> ImagenFondoAlinea { get; set; }
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
        public Nullable<int> IdPlantillaDefinida { get; set; }
        public string color1 { get; set; }
        public string color2 { get; set; }
    
        public virtual Aling Aling { get; set; }
        public virtual Aling Aling1 { get; set; }
        public virtual TipoEstatus TipoEstatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Plantillas> Plantillas { get; set; }
    }
}
