using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ML
{
    public partial class Persona
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("IdRH")]
        public long IdRh { get; set; }

        [JsonProperty("IdEmpresa")]
        public string IdEmpresa { get; set; }

        [JsonProperty("Empresa")]
        public string Empresa { get; set; }

        [JsonProperty("Puesto")]
        public string Puesto { get; set; }

        [JsonProperty("LugarDeTrabajo")]
        public string LugarDeTrabajo { get; set; }

        [JsonProperty("UnidadOrganizativa")]
        public string UnidadOrganizativa { get; set; }

        [JsonProperty("IdTipo")]
        public string IdTipo { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("ApellidoPaterno")]
        public string ApellidoPaterno { get; set; }

        [JsonProperty("ApellidoMaterno")]
        public string ApellidoMaterno { get; set; }

        [JsonProperty("RFC")]
        public string Rfc { get; set; }

        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("IdOrganizacion")]
        public string IdOrganizacion { get; set; }
        [JsonProperty("LugarTrabajo")]
        public string LugarTrabajo { get; set; }
    }

}
