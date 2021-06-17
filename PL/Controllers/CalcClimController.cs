using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class CalcClimController : Controller
    {
        // GET: CalcClim
        public ActionResult Index()
        {
            return View();
        }
        /*Reporte completo clima laboral*/
        //Reporte master
        public List<double> GetEsperadas(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter
                
                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetEsperadasByUnegocio(DataForFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetEsperadasByGenero(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por funcion
                    var result = BL.ReporteD4U.GetEsperadasByFuncion(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByCondicionTrabajo(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByGradoAcademico(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByRangoAntiguedad(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByRangoEdad(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByCompany(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByArea(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasByDepartamento(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    var result = BL.ReporteD4U.GetEsperadasBySubdepartamento(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
            }

            return resultados;
        }
        public List<double> GetContestadas(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetContestadasByUNegocio(DataForFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByGenero(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByFuncion(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByCondicionTrabajo(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByGradoAcademico(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByRangoAntiguedad(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByRangoEdad(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByCompany(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByArea(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaByDepartamento(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetContestadaBySubdepartamento(DataForFilter, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        public List<double> GetPorcentajeAfirmativasEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByUNegocioEnfoqueEmpresa(model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByGeneroEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByFuncionEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByCondicionTrabajoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByGradoAcademicoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByRangoAntiguedadEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByRangoEdadEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByCompanyEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByAreaEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasByDepartamentoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPorcentajeAfirmativasBySubdepartamentoEnfoqueEmpresa(DataForFilter, model.IdPregunta, model.UnidadNegocioFilter);
                    resultados.Add(result);
                }
            }
            return resultados;
        }

        public List<double> GetValueComodinEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByUNegocioEnfoqueEmpresa(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGeneroEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByFuncionEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCondicionTrabajoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGradoAcademicoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoAntiguedadEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoEdadEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCompanyEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByAreaEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByDepartamentoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinBySubdepartamentoEnfoqueEmpresa(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }

            return resultados;
        }
        public List<double> GetValueComodinEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByUNegocioEnfoqueArea(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGeneroEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Func=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByFuncionEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCondicionTrabajoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByGradoAcademicoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoAntiguedadEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByRangoEdadEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByCompanyEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByAreaEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinByDepartamentoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetComodinBySubdepartamentoEnfoqueArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
            }
            return resultados;
        }

        public List<double> GetPorcentajeParticipacionEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetParticipacionByUNegocio(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByGenero(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByFuncion(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByCondicionTrabajo(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByGradoAcademico(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByRangoAntiguedad(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByRangoEdad(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByCompany(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByArea(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionByDepartamento(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetParticipacionBySubDepartamento(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        /************************ENFOQUE EMPRESA************************/
        //GetPromediosCredibilidadEE
        public List<double> GetPromediosCreedibilidadEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPrmediosImparcialidadEE
        public List<double> GetPromediosImparcialidadEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosOrgulloEE
        public List<double> GetPromediosOrgulloEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOrgulloByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosRespetoEE
        public List<double> GetPromediosRespetoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioRespetoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosCompañerismoEE
        public List<double> GetPromediosCompañerismoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosCoachingEE
        public List<double> GetPromediosCoachingEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoachingByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPrmediosCambio
        public List<double> GetPromediosCambioEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCambioByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosBienestarEE
        public List<double> GetPromediosBienestarEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBienestarByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosAlineacionCulturalEE
        public List<double> GetPromediosAlinCulturalEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosGestaltEE
        public List<double> GetPromediosGestaltEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioGestaltByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioConfianzaEE
        public List<double> GetPromediosConfianzaEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioConfianzaByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        //Get Total de impulsoresclave para el logro de resultados

        public List<double> GetImpulsoresClaves(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();

            return resultados;
        }

        //GetPromedios 66 Reactivos
        public List<double> GetPromedios66ReactivosEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66BySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedio 86 Reactivos
        public List<double> GetPromedios86ReactivosEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedio PracticasCultureales
        public List<double> GetPromediosPracticasCulturealesEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioReclutandoYDandoLaBienvenida
        public List<double> GetPromediosReclutandoBienvenidaEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosInspirando
        public List<double> GetPromediosInspirandoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioInspirandoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioHablando
        public List<double> GetPromediosHablandoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioHablandoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioEscuchando
        public List<double> GetPromediosEscuchandoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosAgradeciendo
        public List<double> GetPromediosAgradeciendoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioDesarrollando
        public List<double> GetPromediosDesarrollandoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioCuidando
        public List<double> GetPromediosCuidandoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCuidandoByUNegocioEE(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioPercepcionLugar
        public List<double> GetPromediosPercepcionLugarEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioCooperando
        public List<double> GetPromediosCooperandoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCooperandoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioAlineacionEstrategica
        public List<double> GetPromediosAlineacionEstrategicaEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioProcesosOrganizacionales
        public List<double> GetPromediosProcesosOrganizacionalesEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioPlanes de Carrera
        public List<double> GetPromediosCarreraYPromocionPersEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioCapacitacionYDesarrollo
        public List<double> GetPromediosCapacitacionYDesarrolloEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GEtPromedio ENVIROMENT
        public List<double> GetPromediosEMPOWERMENTEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioEvalDesempeño
        public List<double> GetPromediosEvalDesempeñoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioIntegracion
        public List<double> GetPromediosIntegracionEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioIntegracionByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioNivelCoolaboracion
        public List<double> GetPromediosNivelCoolaboracionEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //getPromedioNivelCompromiso
        public List<double> GetPromediosNivelCompromisoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioFacoteSocial
        public List<double> GetPromediosFactorSocialEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        public List<double> GetPromediosFactorPsicoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //Factor fisico
        public List<double> GetPromediosFactorFisicoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //Getpromedio Bio
        public List<double> GetPromediosBioEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBioByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //Getpromedio Bio
        public List<double> GetPromediosPsicoEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPsicoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosSocialEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioSocialByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosComunicacionEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioComunicacionByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosEnpowerEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEnpowerByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosCoordinacionEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosVisionEstrateEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosNivelDesempeñoEstrateEnfoqueEmpresa(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByUNegocioEE(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGeneroEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByFuncionEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCondicionTrabajoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGradoAcademicoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoAntiguedadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoEdadEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCompanyEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByAreaEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoBySubDepartamentoEE(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }



        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        //**********************ENFOQUE AREA************************//
        public List<double> GetPromediosCreedibilidadEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCredibilidadBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPrmediosImparcialidadEA
        public List<double> GetPromediosImparcialidadEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioImparcialidadBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosOrgulloEA
        public List<double> GetPromediosOrgulloEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOrgulloByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOrgulloBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosRespetoEA
        public List<double> GetPromediosRespetoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioRespetoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioRespetoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosCompañerismoEA
        public List<double> GetPromediosCompañerismoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCompañerismoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosCoachingEA
        public List<double> GetPromediosCoachingEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoachingByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoachingBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPrmediosCambio
        public List<double> GetPromediosCambioEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCambioByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCambioBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosBienestarEA
        public List<double> GetPromediosBienestarEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBienestarByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBienestarBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosAlineacionCulturalEA
        public List<double> GetPromediosAlinCulturalEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlinCulturalBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosGestaltEA
        public List<double> GetPromediosGestaltEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioGestaltByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioGestaltBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioConfianzaEA
        public List<double> GetPromediosConfianzaEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioConfianzaByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioConfianzaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        //Get Total de impulsoresclave para el logro de resultados

        public List<double> GetImpulsoresClavesEA(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();

            return resultados;
        }

        //GetPromedios 66 Reactivos
        public List<double> GetPromedios66ReactivosEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66ByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioOneTo66BySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedio 86 Reactivos
        public List<double> GetPromedios86ReactivosEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedio86ReactivosBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedio PracticasCultureales
        public List<double> GetPromediosPracticasCulturealesEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPracticasCulturalesBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioReclutandoYDandoLaBienvenida
        public List<double> GetPromediosReclutandoBienvenidaEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioReclutandoBienvenidaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosInspirando
        public List<double> GetPromediosInspirandoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioInspirandoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioInspirandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioHablando
        public List<double> GetPromediosHablandoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioHablandoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioHablandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioEscuchando
        public List<double> GetPromediosEscuchandoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEscuchandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromediosAgradeciendo
        public List<double> GetPromediosAgradeciendoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAgradeciendoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioDesarrollando
        public List<double> GetPromediosDesarrollandoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioDesarrollandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioCuidando
        public List<double> GetPromediosCuidandoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCuidandoByUNegocioEA(DataForFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCuidandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioPercepcionLugar
        public List<double> GetPromediosPercepcionLugarEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPercepcionLugarBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioCooperando
        public List<double> GetPromediosCooperandoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCooperandoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCooperandoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioAlineacionEstrategica
        public List<double> GetPromediosAlineacionEstrategicaEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioAlineacionEstrategicaBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioProcesosOrganizacionales
        public List<double> GetPromediosProcesosOrganizacionalesEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioProcesosOrganBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioPlanes de Carrera
        public List<double> GetPromediosCarreraYPromocionPersEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCarreraYPromocionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioCapacitacionYDesarrollo
        public List<double> GetPromediosCapacitacionYDesarrolloEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCapacitacionDesarrolloBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GEtPromedio ENVIROMENT
        public List<double> GetPromediosEMPOWERMENTEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEMPOWERMENTBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioEvalDesempeño
        public List<double> GetPromediosEvalDesempeñoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEvalDesempeñoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioIntegracion
        public List<double> GetPromediosIntegracionEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioIntegracionByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioIntegracionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioNivelCoolaboracion
        public List<double> GetPromediosNivelCoolaboracionEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCoolaboracionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //getPromedioNivelCompromiso
        public List<double> GetPromediosNivelCompromisoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelCompromisoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //GetPromedioFacoteSocial
        public List<double> GetPromediosFactorSocialEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorSocialBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        public List<double> GetPromediosFactorPsicoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorPsicoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //Factor fisico
        public List<double> GetPromediosFactorFisicoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioFactorFisicoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //Getpromedio Bio
        public List<double> GetPromediosBioEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioBioByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioBioBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }
        //Getpromedio Bio
        public List<double> GetPromediosPsicoEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioPsicoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioPsicoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosSocialEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioSocialByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioSocialBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosComunicacionEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioComunicacionByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioComunicacionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosEnpowerEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioEnpowerByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioEnpowerBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosCoordinacionEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioCoordinacionBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosVisionEstrateEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioVisionEstrateBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

        public List<double> GetPromediosNivelDesempeñoEstrateEnfoqueArea(ML.ReporteD4U model)
        {
            List<double> resultados = new List<double>();
            foreach (string item in model.ListFiltros)
            {
                char[] arreglo = new char[50];
                arreglo = item.ToCharArray();
                string prefijo = Convert.ToString(arreglo[0].ToString() + arreglo[1].ToString() + arreglo[2].ToString() + arreglo[3].ToString() + arreglo[4].ToString() + arreglo[5].ToString());
                string DataForFilter = item.Remove(0, 6);

                //Set Data for filter

                if (prefijo == "UNeg=>")
                {
                    //Metodo por unidad de negocio
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByUNegocioEA(DataForFilter);

                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Gene=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGeneroEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

                if (prefijo == "Func=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByFuncionEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "CTra=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCondicionTrabajoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "GAca=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByGradoAcademicoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "RAnt=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoAntiguedadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "REda=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByRangoEdadEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Comp=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByCompanyEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Area=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByAreaEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "Dpto=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoByDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }
                if (prefijo == "SubD=>")
                {
                    //Metodo por genero
                    var result = BL.ReporteD4U.GetPromedioNivelDesempeñoBySubDepartamentoEA(DataForFilter, model.UnidadNegocioFilter);
                    if (Double.IsNaN(result) == true)
                    {
                        result = 0;
                    }
                    resultados.Add(result);
                }

            }
            return resultados;
        }

    }
}