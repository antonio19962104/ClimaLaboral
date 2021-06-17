using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Plantillas
    {
        public int? IdPlantilla { get; set; }
        public ML.DetallePlantilla DetallePlantilla { get; set; }
        public ML.HeaderPlantilla HeaderPlantilla { get; set; }
        public ML.FooterPlantilla FooterPlantilla { get; set; }  
        public ML.BodyPlantilla BodyPlantilla { get; set;}  
        public TipoPlantilla TipoPlantilla { get; set; }      
        public string Nombre { get; set; }
        public ML.TipoEstatus TipoEstatus { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public List<ML.Aling> aling { get; set; }
        public List<ML.TipoEstatus> EstatusList { get; set;}
        public List<ML.HeaderPlantilla> HeaderList { get; set; }
        public List<ML.DetallePlantilla> DetalleList { get; set; }
        public List<ML.FooterPlantilla> FooterList { get; set; }
        public List<ML.TipoPlantilla> TipoPlantillaList { get; set;}
    }
}
