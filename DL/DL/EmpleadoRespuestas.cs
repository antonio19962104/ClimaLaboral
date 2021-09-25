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
    
    public partial class EmpleadoRespuestas
    {
        public int IdEmpleadoRespuestas { get; set; }
        public Nullable<int> IdPregunta { get; set; }
        public string RespuestaEmpleado { get; set; }
        public Nullable<int> IdRespuesta { get; set; }
        public Nullable<int> IdEmpleado { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public Nullable<int> Anio { get; set; }
        public Nullable<int> IdEncuesta { get; set; }
        public Nullable<int> IdEnfoque { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        public virtual Encuesta Encuesta { get; set; }
        public virtual Preguntas Preguntas { get; set; }
        public virtual Respuestas Respuestas { get; set; }
    }
}
