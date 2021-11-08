using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    /// <summary>
    /// Capa de negocios del monitor de HangFire
    /// </summary>
    public class HangFireMonitor
    {
        public class ModelJob
        {
            public int Id { get; set; }
            public int StateId { get; set; }
            public string StateName { get; set; }
            public string InvocationData { get; set; }
            public string Arguments { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime ExpireAt { get; set; }
        }
        public static ML.Result GetJobs()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var Jobs = context.Job.ToList();
                    foreach (var item in Jobs)
                    {
                        var ListModel = new List<ML.Historico>();
                        string args = string.Empty;
                        if (item.InvocationData.Contains("PL.Controllers.BackGroundJobController"))
                        {
                            ListModel = JsonConvert.DeserializeObject<List<ML.Historico>>(item.Arguments);
                            var model = ListModel.FirstOrDefault();
                            args = string.Format("{ opc: {0}, EntidadName: {1}, nivelDetalle: {2}, IdBaseDeDatos: {3}, currentUrl: {4}, currentUsr: {5}, Anio: {6}, TipoEntidad: {7} }", model.opc + model.EntidadName + model.nivelDetalle + model.IdBaseDeDatos + model.currentURL + model.CurrentUsr + model.Anio + model.IdTipoEntidad);
                        }
                        else
                        {
                            args = item.Arguments;
                        }
                        ModelJob modelJob = new ModelJob()
                        {
                            Arguments = args,
                            CreatedAt = item.CreatedAt,
                            ExpireAt = (DateTime)item.ExpireAt,
                            Id = (int)item.Id,
                            InvocationData = item.InvocationData,
                            StateId = (int)item.StateId,
                            StateName = item.StateName,
                        };
                        result.Objects.Add(modelJob);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            return result;
        }
    }
}
