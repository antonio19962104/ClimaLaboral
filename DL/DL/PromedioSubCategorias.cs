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
    
    public partial class PromedioSubCategorias
    {
        public int IdPromedioSubCategorias { get; set; }
        public string AreaAgencia { get; set; }
        public Nullable<int> IdBaseDeDatos { get; set; }
        public Nullable<int> IdEncuesta { get; set; }
        public Nullable<int> AnioAplicacion { get; set; }
        public string JsonData { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
    
        public virtual BasesDeDatos BasesDeDatos { get; set; }
        public virtual Encuesta Encuesta { get; set; }
    }
}