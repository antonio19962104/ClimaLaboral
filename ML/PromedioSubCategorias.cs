using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PromedioSubCategorias
    {
        public int IdPromedioSubCategorias { get; set; }
        public string AreaAgencia { get; set; }
        public int IdBaseDeDatos { get; set; }
        public int IdEncuesta { get; set; }
        public int AnioAplicacion { get; set; }
        public string JsonData { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        //Auxiliares
        public int IdPregunta { get; set; }
    }
}
