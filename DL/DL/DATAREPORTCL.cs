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
    
    public partial class DATAREPORTCL
    {
        public int IDDATAREPORTCL { get; set; }
        public string DATAR { get; set; }
        public string PROGRESS { get; set; }
        public Nullable<int> IDREPORTCL { get; set; }
    
        public virtual REPORTCL REPORTCL { get; set; }
    }
}