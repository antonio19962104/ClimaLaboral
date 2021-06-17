using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Subdepartamento
    {
        public int IdSubdepartamento { get; set; }
        public string Nombre { get; set; }
        public ML.Departamento Departamento { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public string CURRENT_USER { get; set; }
    }
}
