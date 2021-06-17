using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Models
{
    public class ReporteClimaDinamico
    {
        #region props
        public JsonResult objProcesosOrg { get; set; } = new JsonResult();
        public int AnioActual { get; set; } = 0;
        public int AnioHistorico { get; set; } = 0;
        public bool hasHistorico { get; set; } = false;
        public object historicoEE { get; set; } = new object();
        //public object historicoEA { get; set; } = new object();
        public JsonResult objEsperadas { get; set; } = new JsonResult();
        public List<int> Esperadas { get; set; } = new List<int>();
        public JsonResult objParticipacion { get; set; } = new JsonResult();
        public List<double> participacion { get; set; } = new List<double>();
        public JsonResult objCalificacionGlobal { get; set; } = new JsonResult();
        public JsonResult objConfianza { get; set; } = new JsonResult();
        public JsonResult objNivelCompromiso { get; set; } = new JsonResult();
        public JsonResult objNivelColaboracion { get; set; } = new JsonResult();
        public JsonResult objCredibilidad { get; set; } = new JsonResult();
        public JsonResult objImparcialidad { get; set; } = new JsonResult();
        public JsonResult objOrgullo { get; set; } = new JsonResult();
        public JsonResult objRespeto { get; set; } = new JsonResult();
        public JsonResult objCompanierismo { get; set; } = new JsonResult();
        public JsonResult objCoaching { get; set; } = new JsonResult();
        public JsonResult objHabgerenciales { get; set; } = new JsonResult();
        public JsonResult objAlineacionEstrategica { get; set; } = new JsonResult();
        public JsonResult objPracticasCulturales { get; set; } = new JsonResult();
        public JsonResult objCambio { get; set; } = new JsonResult();
        public JsonResult objMejoresEE { get; set; } = new JsonResult();
        //public JsonResult objMejoresEA { get; set; } = new JsonResult();
        public JsonResult objMayorCrecimientoEE { get; set; } = new JsonResult();
        //public JsonResult objMayorCrecimientoEA { get; set; } = new JsonResult();
        public JsonResult objPeoresEE { get; set; } = new JsonResult();
        //public JsonResult objPeoresEA { get; set; } = new JsonResult();
        public JsonResult objBienestarEE { get; set; } = new JsonResult();
        //public JsonResult objBienestarEA { get; set; } = new JsonResult();
        public JsonResult objPermanencia { get; set; } = new JsonResult();
        public JsonResult objAbandono { get; set; } = new JsonResult();
        public JsonResult objComparativoPermanencia { get; set; } = new JsonResult();
        public JsonResult objComparativoAbandono { get; set; } = new JsonResult();
        public JsonResult objComparativoEntidadesResultadoGeneralEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoEntidadesResultadoGeneralEA { get; set; } = new JsonResult();
        public JsonResult objComparativoResultadoGeneralPorNivelesEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoResultadoGeneralPorNivelesEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorAntiguedadEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoPorAntiguedadEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorGeneroEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoPorGeneroEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorGradoAcademicoEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoPorGradoAcademicoEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorCondicionTrabajoEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoPorCondicionTrabajoEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorFuncionEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoPorFuncionEA { get; set; } = new JsonResult();
        public JsonResult objComparativoPorRangoEdadEE { get; set; } = new JsonResult();
        //public JsonResult objComparativoPorRangoEdadEA { get; set; } = new JsonResult();
        public JsonResult objNube1 { get; set; } = new JsonResult();
        public JsonResult objNube2 { get; set; } = new JsonResult();
        public JsonResult objNube3 { get; set; } = new JsonResult();
        public JsonResult objNube4 { get; set; } = new JsonResult();
        #endregion props
    }
}