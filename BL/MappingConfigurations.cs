using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MappingConfigurations
    {
        public static NLog.Logger nlogClimaDinamico = NLog.LogManager.GetLogger(ML.LogTypes.LogClimaDinamico);
        public static DL.Encuesta MappingEncuesta(ML.Encuesta model_Encuesta)
        {
            var data = new DL.Encuesta();
            //data.IdEncuesta = model_Encuesta.IdEncuesta == null ? 0 : model_Encuesta.IdEncuesta;
            data.Nombre = model_Encuesta.Nombre == null ? "" : model_Encuesta.Nombre;
            data.FechaInicio = model_Encuesta.FechaInicio == null ? DateTime.Now : model_Encuesta.FechaInicio;
            data.FechaFin = model_Encuesta.FechaFin == null ? DateTime.Now : model_Encuesta.FechaFin;
            data.Agradecimiento = model_Encuesta.Agradecimiento == null ? "" : model_Encuesta.Agradecimiento;
            data.BasesDeDatos = new DL.BasesDeDatos();
            data.BasesDeDatos.IdBasesDeDatos = model_Encuesta.BasesDeDatos == null ? 0 : (int)model_Encuesta.BasesDeDatos.IdBaseDeDatos;
            data.Descripcion = model_Encuesta.Descripcion == null ? "" : model_Encuesta.Descripcion;
            data.TipoEstatus = new DL.TipoEstatus();
            data.TipoEstatus.IdEstatus = model_Encuesta.TipoEstatus == null ? 1 : (int)model_Encuesta.TipoEstatus.IdEstatus;
            data.ImagenAgradecimiento = model_Encuesta.ImagenAgradecimiento == null ? "" : model_Encuesta.ImagenAgradecimiento;
            data.ImagenInstruccion = model_Encuesta.ImagenInstruccion == null ? "" : model_Encuesta.ImagenInstruccion;
            data.Instruccion = model_Encuesta.Instruccion == null ? "" : model_Encuesta.Instruccion;
            data.Plantillas = new DL.Plantillas();
            data.Plantillas.IdPlantilla = model_Encuesta.Plantillas == null ? 0 : (int)model_Encuesta.Plantillas.IdPlantilla;
            data.FechaHoraCreacion = DateTime.Now;
            data.UsuarioCreacion = model_Encuesta.UsuarioCreacion == null ? "" : model_Encuesta.UsuarioCreacion;
            data.ProgramaCreacion = model_Encuesta.ProgramaCreacion == null ? "" : model_Encuesta.ProgramaCreacion;
            data.UID = model_Encuesta.UID == null ? "" : model_Encuesta.UID;
            data.TipoEncuesta = new DL.TipoEncuesta();
            data.TipoEncuesta.IdTipoEncuesta = model_Encuesta.MLTipoEncuesta == null ? 1 : (int)model_Encuesta.MLTipoEncuesta.IdTipoEncuesta;
            data.DosColumnas = model_Encuesta.DosColumnas == false ? false : model_Encuesta.DosColumnas;
            data.IdEmpresa = model_Encuesta.IdEmpresa == null ? 0 : model_Encuesta.IdEmpresa;
            return data;
        }
        public static List<DL.Competencia> MappingCompetencia(List<ML.Competencia> aListCompetencia, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            var list = new List<DL.Competencia>();
            try
            {
                foreach (var item in aListCompetencia)
                {
                    DL.Competencia compe = new DL.Competencia()
                    {
                        Nombre = item.Nombre,
                        IdEstatus = 1,
                        FechaHoraCreacion = DateTime.Now,
                        UsuarioCreacion = aUsuarioCreacion.ToString(),
                        ProgramaCreacion = "Alta de Competencia",
                        IdAdminCreate = Convert.ToInt32(aIdUsuarioCreacion),
                    };
                    list.Add(compe);
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<DL.Competencia>();
            }
        }
        public static List<DL.Categoria> MappingCategoria(List<ML.Categoria> aListCategoria, string aUsuarioCreacion, string aIdUsuarioCreacion)
        {
            var list = new List<DL.Categoria>();
            try
            {
                foreach (var item in aListCategoria)
                {
                    DL.Categoria cate = new DL.Categoria()
                    {
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion == null ? "" : item.Descripcion,
                        Estatus = 1,
                        IdPadre = item.IdPadre,
                        IdAdminCreate = Convert.ToInt32(aIdUsuarioCreacion),
                        FechaHoraCreacion = DateTime.Now,
                        UsuarioCreacion = aUsuarioCreacion,
                        ProgramaCreacion = "Alta de Categoria"
                    };
                    list.Add(cate);
                }
                return list;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<DL.Categoria>();
            }
        }
    }
}
