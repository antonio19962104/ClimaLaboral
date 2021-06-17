using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{ 
    public class EstatusEmail
    {
        public int noIntentos { get; set; }
        public int IdEstatusEmail { get; set; }
        public string Mensaje { get; set; }//Mensaje HTML
        public string Destinatario { get; set; }//Destinatario
        public string MsgEnvio { get; set; }
        public ML.EstatusMail EstatusMail { get; set; }//1 => No enviado; 2 =>Enviado
        public ML.BasesDeDatos BaseDeDatos { get; set; }//Base de datos a la que se esta enviando reintentando cada mail
        public ML.Encuesta Encuesta { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public string DatePrimerIntento { get; set; }
        public string DateUltimoIntento { get; set; }
    }
}
