using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PerfilModulo
    {
        public string CURRENT_USER { get; set; }
        public int IdPerfilModulo { get; set; }
        public ML.PerfilD4U PerfilD4U { get; set; }
        public ML.Modulo Modulo { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string ProgramaCreacion { get; set; }
        public DateTime FechaHoraModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ProgramaModificacion { get; set; }
        public DateTime FechaHoraEliminacion { get; set; }
        public string UsuarioEliminacion { get; set; }
        public string ProgramaEliminacion { get; set; }
        public ML.Administrador Administrador { get; set; }
        public ML.Empleado Empleado { get; set; }
        public ML.PerfilModuloAccion PerfilModuloAccion { get; set; }
        //public ML.Administrador Administrador { get; set; }
        public List<string> Acciones { get; set; }

    }
}
