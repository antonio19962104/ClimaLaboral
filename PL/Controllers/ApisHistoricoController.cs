using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ApisHistoricoController : Controller
    {
        /*[HttpPost]
        public JsonResult existeHistorico(ML.Historico aHistorico)
        {
            return Json(BL.HistoricoClimaLaboral.existeHistorico(aHistorico));
        }*/
        [HttpPost]
        public JsonResult existeHistorico_2(ML.Historico aHistorico)
        {
            Session["AnioActual_AnioHistorico"] = (aHistorico.Anio + 1) + "_" + aHistorico.Anio;
            return Json(BL.HistoricoClimaLaboral.existeHistorico_2(aHistorico));
        }
        /// <summary>
        /// Funcion equivalente a print_r de php
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void print_r<T>(List<T> data)
        {
            var tipo = typeof(T).AssemblyQualifiedName;
            var objectType = Type.GetType(tipo);

            //dynamic instantiatedObject = Activator.CreateInstance(objectType) as ITestClass;
            var instantiatedObject = Activator.CreateInstance(objectType);
            foreach (var item in data)
            {
               
            }
            Console.WriteLine(tipo);
        }
        public interface ITestClass
        {
            int Id { get; set; }
            string Name { get; set; }
            string print_r();
        }
        /*[HttpPost]
        public JsonResult getHistorico(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.getHistorico(aFiltros));
        }*/
        [HttpPost]
        public JsonResult getHistorico_2EE(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.getHistorico_2EE(aFiltros));
        }
        [HttpPost]
        public JsonResult getHistorico_2EEBienestar(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.getHistorico_2EEBienestar(aFiltros));
        }
        [HttpPost]
        public JsonResult getHistorico_2EABienestar(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.getHistorico_2EABienestar(aFiltros));
        }
        [HttpPost]
        public JsonResult getHistorico_2EA(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.getHistorico_2EA(aFiltros));
        }
        /*[HttpPost]
        public JsonResult AddHistorico(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.AddHistorico(aFiltros, Session["AdminLog"]));
        }*/
        [HttpPost]
        public JsonResult getPromediosGenerales(List<ML.minHistorico> aFiltros)
        {
            if (aFiltros != null)
            {
                return Json(BL.HistoricoClimaLaboral.getPromedioGeneral(aFiltros));
            }
            else
            {
                return Json(new List<ML.Historico>());
            }
        }
        [HttpPost]
        public JsonResult getHistoricoBienestarEE(ML.Historico aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.getHistoricoBienestarEE(aFiltros));
        }

        #region Guardar Historico mientras se genera el reporte Actual
        [HttpPost]
        public JsonResult AddHistoricoFromReporte(ML.HistoricoClima aFiltros)
        {
            return Json(BL.HistoricoClimaLaboral.AddHistoricoFromReporte(aFiltros, Session["AdminLog"]));
        }
        #endregion Guardar Historico
    }
}