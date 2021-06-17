using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
   public class DetallePlantilla
    {
        public int IdDetallePlantilla { get; set; }
        public int IdPlantillaDefinida { get; set; }
        public string Nombre { get; set; }
        public string CodeHTML { get; set; }
        public string  ImagenIco { get; set; }
        public string ThumbImage { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public ML.TipoEstatus IdEstatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
    }
}
