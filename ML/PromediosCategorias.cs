using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PromediosCategorias
    {
        public int IdPromediosCategorias { get; set; } = 0;
        public decimal Promedio { get; set; } = 0;
        public string Area { get; set; } = String.Empty;

        public BasesDeDatos BasesDeDatos { get; set; } = new BasesDeDatos();
        public Categoria Categoria { get; set; } = new Categoria();
        public Encuesta Encuesta { get; set; } = new Encuesta();
    }
}
