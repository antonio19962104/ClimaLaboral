//Finalsta
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Net.Http;
using ML;
using System.Diagnostics;

namespace BL
{
    public class Encuesta
    {
        static int consulta = 0;
        public string cadenaConexionFinal = "";        
        public static ML.Result ConsultarAvance(ML.EmpleadoRespuesta EmpleadoRespuestas)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConsultarAvanceE(EmpleadoRespuestas.Empleado.IdEmpleado).ToList();

                    foreach (var obj in query)
                    {
                        result.Avance = obj.Value;//El numero que retorna es la View que debo mostrar
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result getEncuestas()
        {
            ML.Result result = new ML.Result();
            result.ListadoDeEncuestas = new List<ML.Encuesta>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var idEstatusActivo = 1;
                    var query = context.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEstatus ={0}", idEstatusActivo);
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuestas = new ML.Encuesta();
                            encuestas.IdEncuesta = obj.IdEncuesta;

                            //Consulta si ya la envíe
                            var enviada = context.EstatusEmail.SqlQuery("SELECT * FROM ESTATUSEMAIL WHERE IDENCUESTA = {0}", encuestas.IdEncuesta).ToList();
                            if (enviada.Count > 0)
                            { encuestas.Enviada = 1; }
                            else
                            { encuestas.Enviada = 0; }


                            encuestas.UID = obj.UID;
                            encuestas.Nombre = obj.Nombre;
                            encuestas.TipoEncuesta = obj.TipoEncuesta.NombreTipoDeEncuesta;
                            encuestas.TipoEstatus = new ML.TipoEstatus();
                            encuestas.TipoEstatus.IdEstatus = Convert.ToInt32(obj.IdEstatus);
                            encuestas.TipoEstatus.Descripcion = obj.TipoEstatus.Descripcion;
                            encuestas.IdTipoEncuesta = obj.TipoEncuesta.IdTipoEncuesta;
                            result.ListadoDeEncuestas.Add(encuestas);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;
        }
        public static Result getEncuestaById(int idEncuesta)
        {            
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {                        
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            var resulListEmpresa = BL.Company.GetAllCompany();
                            var resulListTipoDeEmpresa = BL.TipoEncuesta.getAllTipoEncuesta();
                            var listadoPlantillas = BL.Plantillas.getPlantillas(1);
                            var listadoBaseDeDatos = BL.BasesDeDatos.getBaseDeDatosAll();
                            var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonima();
                            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
                            //var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias();
                            var listadoTipoControl = BL.TipoControl.getTipoControl();
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.ListTipoEncuesta = resulListTipoDeEmpresa.ListadoTipoEncuesta;
                            encuesta.ListEmpresas = resulListEmpresa.Objects;
                            encuesta.ListPlantillas = listadoPlantillas.ListadoDePlantillasPredefinidas;
                            encuesta.ListDataBase = listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos;
                            encuesta.ListEnfoquePregunta = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
                            //encuesta.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
                            encuesta.ListTipoControl = listadoTipoControl.ListadoTipoControl;
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Agradecimiento = obj.Agradecimiento;
                            encuesta.Company = new ML.Company();
                            encuesta.Company.CompanyId = obj.IdEmpresa;
                            encuesta.Descripcion = obj.Descripcion;
                            encuesta.Estatus = obj.Estatus;
                            encuesta.FechaFin = obj.FechaFin;
                            encuesta.FechaInicio = obj.FechaInicio;
                            encuesta.ImagenAgradecimiento = obj.ImagenAgradecimiento;
                            encuesta.ImagenInstruccion = obj.ImagenInstruccion;
                            encuesta.Instruccion = obj.Instruccion;
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.Plantillas = new ML.Plantillas();
                            encuesta.Plantillas.IdPlantilla = Convert.ToInt32(obj.IdPlantilla);
                            encuesta.ProgramaCreacion = obj.ProgramaCreacion;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            encuesta.UsuarioCreacion = obj.UsuarioCreacion;
                            //Preguntas
                            //encuesta.ListarPreguntas = BL.Preguntas.getAllPreguntasByIdEncuestaEdit(obj.IdEncuesta);
                            //encuesta.NewCuestion = BL.Preguntas.getAllPreguntasByIdEncuesta(obj.IdEncuesta);
                            //Base de datos
                            encuesta.BasesDeDatos = new ML.BasesDeDatos();
                            encuesta.BasesDeDatos.IdBaseDeDatos = obj.IdBasesDeDatos;
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }
        public static Result getEncuestaByIdEditCL(int idEncuesta,string idusuarioAdmin)
        {
            ML.Result result = new ML.Result();
            try
            {
                
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            var resulListEmpresa = BL.Company.GetAllCompany();
                            var resulListTipoDeEmpresa = BL.TipoEncuesta.getAllTipoEncuesta();
                            var listadoPlantillas = BL.Plantillas.getPlantillas(1);
                            var listadoBaseDeDatos = BL.BasesDeDatos.getBaseDeDatosAll();
                            var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonima();
                            var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
                            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idusuarioAdmin);
                            var listadoTipoControl = BL.TipoControl.getTipoControl();
                            var listadoTipoOrden = BL.TipoOrden.getAllTipoOrden();
                            encuesta.ListTipoOrden = listadoTipoOrden.ListTipoOrden;
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.ListTipoEncuesta = resulListTipoDeEmpresa.ListadoTipoEncuesta;
                            encuesta.ListEmpresas = resulListEmpresa.Objects;
                            encuesta.ListPlantillas = listadoPlantillas.ListadoDePlantillasPredefinidas;
                            encuesta.ListDataBase = listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos;
                            encuesta.ListEnfoquePregunta = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
                            encuesta.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
                            encuesta.ListTipoControl = listadoTipoControl.ListadoTipoControl;
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Agradecimiento = obj.Agradecimiento;
                            encuesta.Company = new ML.Company();
                            encuesta.Company.CompanyId = obj.IdEmpresa;
                            encuesta.Descripcion = obj.Descripcion;
                            encuesta.DosColumnas = Convert.ToBoolean(obj.DosColumnas);
                            encuesta.Estatus = obj.Estatus;
                            encuesta.FechaFin = obj.FechaFin;
                            encuesta.FechaInicio = obj.FechaInicio;


                            encuesta.CodeHTML = obj.CodeHTML;
                            encuesta.ImagenAgradecimiento = obj.ImagenAgradecimiento;
                            encuesta.ImagenInstruccion = obj.ImagenInstruccion;
                            encuesta.Instruccion = obj.Instruccion;
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.TipoOrden = new ML.TipoOrden();
                            encuesta.TipoOrden.IdTipoOrden = (Int32)obj.IdTipoOrden;
                            encuesta.Plantillas = new ML.Plantillas();
                            encuesta.Plantillas.IdPlantilla = Convert.ToInt32(obj.IdPlantilla);
                            encuesta.ProgramaCreacion = obj.ProgramaCreacion;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            encuesta.UsuarioCreacion = obj.UsuarioCreacion;
                            //Preguntas
                            encuesta.NewCuestionEdit = BL.Preguntas.getAllPreguntasByIdEncuestaEdit(obj.IdEncuesta,idusuarioAdmin);
                            //encuesta.NewCuestion = BL.Preguntas.getAllPreguntasByIdEncuesta(obj.IdEncuesta);
                            //Base de datos
                            encuesta.BasesDeDatos = new ML.BasesDeDatos();
                            encuesta.BasesDeDatos.IdBaseDeDatos = obj.IdBasesDeDatos;
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }
        public static ML.Result getEncuestaByIdConfigura(int idEncuesta, string idUsuarioAdmin)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Estatus = obj.Estatus;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            //Preguntas
                            //encuesta.ListarPreguntas = BL.Preguntas.getAllPreguntasByIdEncuestaEdit(obj.IdEncuesta);
                            encuesta.NewCuestion = BL.Preguntas.getAllPreguntasByIdEncuesta(obj.IdEncuesta,idUsuarioAdmin);
                            //Base de datos
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }
        public static ML.Result getEncuestaByIdConfiguraForEdit(int idEncuesta, string idUsuarioAmin)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Estatus = obj.Estatus;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            //Preguntas
                            //encuesta.ListarPreguntas = BL.Preguntas.getAllPreguntasByIdEncuestaEdit(obj.IdEncuesta);
                            encuesta.NewCuestion = BL.Preguntas.getAllPreguntasByIdEncuesta(obj.IdEncuesta,idUsuarioAmin);
                            //Base de datos
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                    //Query configuraciones
                    //var queryConf = contex.ConfiguraRespuesta.SqlQuery("SELECT * FROM ConfiguraRespuesta WHERE IdEncuesta = {0}", idEncuesta).ToList();
                    //result.Objects = new List<object>();
                    //if (queryConf != null)
                    //{
                    //    foreach (var item in queryConf)
                    //    {
                    //        ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();
                    //        conf.IdRespuesta = Convert.ToInt32(item.IdRespuesta);

                    //        result.Objects.Add(conf);
                    //    }
                    //}
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }
        public static ML.Result Add(ML.Encuesta Encuesta, string fullUrl, int usuarioCreacion)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Alta Encuesta
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO Encuesta (DosColumnas,Nombre,FechaInicio,FechaFin,IdEstatus,IdEmpresa,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,CodeHTML,IdPlantilla,IdBasesDeDatos,Descripcion,Instruccion,ImagenInstruccion,IdTipoEncuesta,Agradecimiento,ImagenAgradecimiento) " +
                            " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})", Encuesta.DosColumnas, Encuesta.Nombre, Encuesta.FechaInicio, Encuesta.FechaFin, 1, Encuesta.IdEmpresa, DateTime.Now, usuarioCreacion, "Alta Encuesta", "", Encuesta.Plantillas.IdPlantilla, Encuesta.BasesDeDatos.IdBaseDeDatos, Encuesta.Descripcion,
                            Encuesta.Instruccion, Encuesta.ImagenInstruccion, Encuesta.MLTipoEncuesta.IdTipoEncuesta, Encuesta.Agradecimiento, Encuesta.ImagenAgradecimiento);
                        int idEncuesta = context.Encuesta.Max(q => q.IdEncuesta);
                        //int idEncuesta = 34; 

                        //Alta Estatus de encuesta  1.- No iniciada
                        //Encuesta.BasesDeDatos.IdBaseDeDatos
                        int IdBaseDeDatos = Convert.ToInt32(Encuesta.BasesDeDatos.IdBaseDeDatos);
                        BL.Usuario.AddEstatus(idEncuesta, IdBaseDeDatos, context);




                        //Encuesta Area
                        var obtieneAreasPorIdEmpresa = BL.Area.AreaGetByCompanyId(Int32.Parse(Encuesta.IdEmpresa.ToString()));
                        if (obtieneAreasPorIdEmpresa.Objects.Count >= 0)
                        {
                            foreach (ML.Area obj in obtieneAreasPorIdEmpresa.Objects)
                            {
                                var queryEncuestaArea = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaArea (IdArea,IdEncuesta,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion) " +
                                    " VALUES({0},{1},{2},{3},{4})", obj.IdArea, idEncuesta, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                            }
                        }
                        //Pregunta
                        Console.Write(Encuesta.SeccionarEncuesta);
                        int IdPreguntaSubSeccion = 0; ;
                        foreach (ML.Preguntas obj in Encuesta.NewCuestion)
                        {
                            obj.Competencia = new ML.Competencia();
                            // obj.Competencia.IdCompetencia = null;
                            //Insert Preguntas
                            if (Encuesta.SeccionarEncuesta == false)
                            {
                                if (obj.TipoControl.IdTipoControl != 12)
                                {
                                    var queryPreguntas = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                    "(idEncuesta,Pregunta,Valoracion,TipoControl,IdCompetencia,IdEstatus,FechaHoraCreacion, " +
                                    " UsuarioCreacion,ProgramaCreacion,Enfoque, IdTipoControl, RespuestaCondicion, PreguntasCondicion, Obligatoria, Seccion) " +
                                    " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}, {14})", idEncuesta, obj.Pregunta,
                                    obj.Valoracion, "", obj.Competencia.IdCompetencia, 1, DateTime.Now,
                                    Encuesta.UsuarioCreacion, "Alta Encuesta", obj.Enfoque, obj.TipoControl.IdTipoControl,
                                    obj.RespuestaCondicion, obj.PreguntasCondicion, obj.Obligatoria, 1);

                                    int idPregunta = context.Preguntas.Max(q => q.IdPregunta);
                                    if (obj.TipoControl.IdTipoControl == 13)
                                    {
                                        IdPreguntaSubSeccion = idPregunta;
                                    }
                                    var actualiza = context.Database.ExecuteSqlCommand("UPDATE PREGUNTAS SET SUBSECCION = {0} WHERE IDPREGUNTA ={1}", IdPreguntaSubSeccion, idPregunta);
                                    //Tabla Encuesta Pregunta
                                    var queryEncuestaPregunta = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                        "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                        "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPregunta, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                    //Respuestas
                                    switch (obj.TipoControl.IdTipoControl)
                                    {
                                        case 1:
                                            var queryRespuesta1 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Respuesta Corta", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 2:
                                            var queryRespuesta2 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Respuesta Larga", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 3:
                                            foreach (ML.Respuestas objR in obj.NewAnswer)
                                            {
                                                var queryRespuesta3 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            }
                                            break;
                                        case 4:
                                            foreach (ML.Respuestas objR in obj.NewAnswer)
                                            {
                                                var queryRespuesta4 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            }
                                            break;
                                        case 5:
                                            foreach (ML.Respuestas objR in obj.NewAnswer)
                                            {
                                                var queryRespuesta4 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            }
                                            break;

                                        case 6:
                                            var queryRespuesta6 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Sentimiento", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;

                                        case 7:
                                            var queryRespuesta7 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Rango", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 8:
                                            var queryRespuesta8 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Acuerdo", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 9:
                                            var queryRespuesta9 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Frecuencia", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 10:
                                            var queryRespuesta10 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Importacia", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 11:
                                            var queryRespuesta11 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Probabilidad", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;

                                    }
                                    idPregunta = 0;
                                }
                                else
                                {
                                    // se inserta primero la pregunta
                                    var queryPreguntas = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                           "(idEncuesta,Pregunta,Valoracion,TipoControl,IdCompetencia,IdEstatus,FechaHoraCreacion, " +
                                           " UsuarioCreacion,ProgramaCreacion,Enfoque, IdTipoControl, RespuestaCondicion, PreguntasCondicion, Obligatoria, Seccion) " +
                                           " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}, {14})", idEncuesta, obj.Pregunta,
                                       obj.Valoracion, "", obj.Competencia.IdCompetencia, 1, DateTime.Now,
                                       Encuesta.UsuarioCreacion, "Alta Encuesta", obj.Enfoque, obj.TipoControl.IdTipoControl,
                                       obj.RespuestaCondicion, obj.PreguntasCondicion, obj.Obligatoria, 1);
                                    //Obtiene maximo de pregunta
                                    int idPreguntaLDoble = context.Preguntas.Max(q => q.IdPregunta);
                                    //Tabla Encuesta Pregunta
                                    var queryEncuestaPreguntaLDoble = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                        "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                        "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPreguntaLDoble, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");

                                    for (int i=2; i < obj.NewAnswer.Count; i++)
                                    {
                                        //Inserta pregunta likert doble
                                        var queryPreguntasLikert = context.Database.ExecuteSqlCommand("INSERT INTO PreguntasLikert " +
                                            "(idPregunta,idEncuesta,Pregunta,IdEstatus,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5},{6})", idPreguntaLDoble, idEncuesta, obj.NewAnswer[i].Respuesta,
                                        1, DateTime.Now,Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        //Obtiene maximo de pregunta Likert
                                        int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);                                        
                                        //Inserta Respuesta por index 0 e index 1                                      
                                        var queryRespuestaColA = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta,IdPreguntaLikertD, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5},{6})", obj.NewAnswer[0].Respuesta, idPreguntaLDoble, idPreguntasLikert, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        var queryRespuestaColB = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta,IdPreguntaLikertD,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5},{6})", obj.NewAnswer[1].Respuesta, idPreguntaLDoble, idPreguntasLikert, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");


                                    }



                                }


                               
                            }
                            else// Con seccion
                            {
                                if (obj.TipoControl.IdTipoControl != 12)
                                {
                                    var queryPreguntas = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                    "(idEncuesta,Pregunta,Valoracion,TipoControl,IdCompetencia,IdEstatus,FechaHoraCreacion, " +
                                    " UsuarioCreacion,ProgramaCreacion,Enfoque, IdTipoControl, RespuestaCondicion, PreguntasCondicion, Obligatoria) " +
                                    " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", idEncuesta, obj.Pregunta,
                                    obj.Valoracion, "", obj.Competencia.IdCompetencia, 1, DateTime.Now,
                                    Encuesta.UsuarioCreacion, "Alta Encuesta", obj.Enfoque, obj.TipoControl.IdTipoControl,
                                    obj.RespuestaCondicion, obj.PreguntasCondicion, obj.Obligatoria);

                                    int idPregunta = context.Preguntas.Max(q => q.IdPregunta);
                                    if (obj.TipoControl.IdTipoControl == 13)
                                    {
                                        IdPreguntaSubSeccion = idPregunta;
                                    }
                                    var actualiza = context.Database.ExecuteSqlCommand("UPDATE PREGUNTAS SET SUBSECCION = {0} WHERE IDPREGUNTA ={1}", IdPreguntaSubSeccion, idPregunta);

                                    //Tabla Encuesta Pregunta
                                    var queryEncuestaPregunta = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                        "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                        "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPregunta, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                    //Respuestas
                                    switch (obj.TipoControl.IdTipoControl)
                                    {
                                        case 1:
                                            var queryRespuesta1 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Respuesta Corta", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 2:
                                            var queryRespuesta2 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Respuesta Larga", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 3:
                                            foreach (ML.Respuestas objR in obj.NewAnswer)
                                            {
                                                var queryRespuesta3 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            }
                                            break;
                                        case 4:
                                            foreach (ML.Respuestas objR in obj.NewAnswer)
                                            {
                                                var queryRespuesta4 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            }
                                            break;
                                        case 5:
                                            foreach (ML.Respuestas objR in obj.NewAnswer)
                                            {
                                                var queryRespuesta4 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            }
                                            break;

                                        case 6:
                                            var queryRespuesta6 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Sentimiento", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;

                                        case 7:
                                            var queryRespuesta7 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Rango", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 8:
                                            var queryRespuesta8 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Acuerdo", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 9:
                                            var queryRespuesta9 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Frecuencia", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 10:
                                            var queryRespuesta10 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Importacia", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;
                                        case 11:
                                            var queryRespuesta11 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                                "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                                " VALUES({0},{1},{2},{3},{4},{5})", "Likert Probabilidad", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                            break;

                                    }
                                    idPregunta = 0;
                                }
                                else
                                {                                   
                                    var queryPreguntas = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                   "(idEncuesta,Pregunta,Valoracion,TipoControl,IdCompetencia,IdEstatus,FechaHoraCreacion, " +
                                   " UsuarioCreacion,ProgramaCreacion,Enfoque, IdTipoControl, RespuestaCondicion, PreguntasCondicion, Obligatoria) " +
                                   " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", idEncuesta, obj.Pregunta,
                                   obj.Valoracion, "", obj.Competencia.IdCompetencia, 1, DateTime.Now,
                                   Encuesta.UsuarioCreacion, "Alta Encuesta", obj.Enfoque, obj.TipoControl.IdTipoControl,
                                   obj.RespuestaCondicion, obj.PreguntasCondicion, obj.Obligatoria);
                                    //Obtiene maximo de pregunta
                                    int idPreguntaLDoble = context.Preguntas.Max(q => q.IdPregunta);
                                    //Tabla Encuesta Pregunta
                                    var queryEncuestaPreguntaLDoble = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                        "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                        "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPreguntaLDoble, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");

                                    for (int i = 2; i < obj.NewAnswer.Count; i++)
                                    {
                                        //Inserta pregunta likert doble
                                        var queryPreguntasLikert = context.Database.ExecuteSqlCommand("INSERT INTO PreguntasLikert " +
                                            "(idPregunta,idEncuesta,Pregunta,IdEstatus,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5},{6})", idPreguntaLDoble, idEncuesta, obj.NewAnswer[i].Respuesta,
                                        1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        //Obtiene maximo de pregunta Likert
                                        int idPreguntasLikert = context.PreguntasLikert.Max(q => q.idPreguntasLikert);
                                        //Inserta Respuesta por index 0 e index 1                                      
                                        var queryRespuestaColA = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta,IdPreguntaLikertD, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5},{6})", obj.NewAnswer[0].Respuesta, idPreguntaLDoble, idPreguntasLikert, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        var queryRespuestaColB = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta,IdPreguntaLikertD,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5},{6})", obj.NewAnswer[1].Respuesta, idPreguntaLDoble, idPreguntasLikert, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");


                                    }



                                }



                            }

                            //End insert preguntas


                        }
                        result.Correct = true;
                        result.idEncuestaAlta = idEncuesta;
                        context.SaveChanges();
                        transaction.Commit();
                        idEncuesta = 0;
                    }
                    catch (Exception aE)
                    {
                        result.Correct = false;
                        result.ErrorMessage = aE.Message;
                        transaction.Rollback();

                    }
                }
            }
            return result;
        }

        public static object ValidarObjeto(ML.Encuesta respuestas)
        {
            throw new NotImplementedException();
        }

        public static ML.Result AddRespuestas(ML.Encuesta EncuestaRespuesta)
        {
            int ExceptionCode = 0;
            var path1 = @"\\10.5.2.101\RHDiagnostics\log\Log" + EncuestaRespuesta.IdEncuesta + "_" + EncuestaRespuesta.UsuarioCreacion + ".txt";
            var fullPath1 = Path.GetFullPath(path1);
            if (!File.Exists(fullPath1))
            {
                string createText = "Log" + Environment.NewLine;
                File.WriteAllText(fullPath1, createText);
            }
            string appendText1 = "Envio de encuesta" + " " + DateTime.Now + Environment.NewLine;
            File.AppendAllText(fullPath1, appendText1);
            consulta = 0;
            bool validaDuplicado = false;
            if (EncuestaRespuesta.MLTipoEncuesta.IdTipoEncuesta == 1)
            {
                validaDuplicado = false;
                EncuestaRespuesta.UsuarioCreacion = "20728";
                //INSERT INTO USUARIORESPUESTAS => CON UN ID DE USUARIO DIFERENTE
                var newIdEmpleadoForAnonima = BL.Encuesta.GetnewIdUsuarioForEncuestaAnonima(EncuestaRespuesta.IdEncuesta);
                EncuestaRespuesta.UsuarioCreacion = Convert.ToString(newIdEmpleadoForAnonima);
            }
            else
            {
                // validaDuplicado = BL.Respuestas.ValidaDuplicadoRespuesta(Convert.ToInt32(EncuestaRespuesta.UsuarioCreacion), EncuestaRespuesta.IdEncuesta);
            }

            reintento:

            ML.Result result = new ML.Result();
            result.IdusuarioForAnonima = Convert.ToInt32(EncuestaRespuesta.UsuarioCreacion);
            if (!validaDuplicado)
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //using (var transaction = context.Database.BeginTransaction())
                    //{
                        try
                        {
                            var invalidaSiguienteSubsecciones = BL.ConfiguraRespuesta.coinciden(EncuestaRespuesta);
                            var idEncuesta = EncuestaRespuesta.IdEncuesta;
                            var usuarioContesta = EncuestaRespuesta.UsuarioCreacion;
                            var progCreate = "RespuestaUsuario";
                            var idPregunta = 0;
                            var idTipoDeControl = 0;
                            var idRespuesta = 0;
                            for (var r = 0; r < EncuestaRespuesta.ListarPreguntas.Count; r++)
                            {
                                if (consulta == 1)
                                {
                                    break;
                                }
                                idPregunta = EncuestaRespuesta.ListarPreguntas[r].IdPregunta;
                                var getIdrespuestaConfig = BL.ConfiguraRespuesta.getAllAnswersConfigByIdEncuestIdPregunta(EncuestaRespuesta.IdEncuesta, idPregunta);
                                idTipoDeControl = (int)EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl;
                                if (idTipoDeControl != 13)
                                {
                                    if (idTipoDeControl != 5)
                                    {
                                        if (EncuestaRespuesta.ListarPreguntas[r].Respuestas.Count > 1)
                                        {//Mas de una respuesta
                                            int btermina = 0;
                                            for (var y = 0; y < EncuestaRespuesta.ListarPreguntas[r].Respuestas.Count; y++)
                                            {
                                                idRespuesta = EncuestaRespuesta.ListarPreguntas[r].Respuestas[y].IdRespuesta;

                                                if (EncuestaRespuesta.ListarPreguntas[r].Respuestas[y].Selected == true || idTipoDeControl == 12)
                                                {
                                                    if (getIdrespuestaConfig.Count > 0 && idTipoDeControl == 4 || idTipoDeControl == 3)
                                                    {
                                                        List<ML.ConfiguraRespuesta> respuesta = BL.ConfiguraRespuesta.getAllAnswersConfigByIdEncuestIdPregunta(EncuestaRespuesta.IdEncuesta, EncuestaRespuesta.ListarPreguntas[r].IdPregunta, idRespuesta);
                                                        for (var e = 0; e < respuesta.Count; e++)
                                                        {
                                                            for (var t = 0; t < EncuestaRespuesta.ListarPreguntas.Count; t++)
                                                            {
                                                                if (respuesta[e].IdPreguntaOpen == EncuestaRespuesta.ListarPreguntas[t].IdPregunta)
                                                                {
                                                                    EncuestaRespuesta.ListarPreguntas[t].PreguntasCondicion = "si";
                                                                    EncuestaRespuesta.ListarPreguntas[t].isDisplay = true;

                                                                }

                                                            }
                                                        }

                                                    }


                                                    if (EncuestaRespuesta.ListarPreguntas[r].PreguntasCondicion != "no")
                                                    {
                                                        string nombrePregunta = "";
                                                        if (idTipoDeControl != 12)
                                                        {
                                                            nombrePregunta = BL.Respuestas.RegresaNombreRespuesta(idRespuesta);
                                                        }
                                                        else
                                                        {
                                                            nombrePregunta = EncuestaRespuesta.ListarPreguntas[r].Respuestas[y].Respuesta;
                                                        }
                                                    //var tieneRespuesta = BL.Encuesta.tieneRespuesta(idPregunta);
                                                    //if (tieneRespuesta == false)
                                                    //{
                                                        if (idPregunta == 0 || idRespuesta == 0)
                                                        {
                                                            int IdUsr = Convert.ToInt32(usuarioContesta);
                                                            BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                            var res = new ML.Result();
                                                            res.Correct = false;
                                                            res.ErrorMessage = "reload";
                                                            return res;
                                                        }
                                                            var queryUpdate = context.Database.ExecuteSqlCommand
                                                            ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado'  WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2} AND IDRESPUESTA = {6}",// 
                                                            idEncuesta, usuarioContesta, idPregunta, nombrePregunta, DateTime.Now, usuarioContesta, idRespuesta);//idRespuesta
                                                        
                                                    }
                                                }
                                                else
                                                {//Aqui se validan las preguntas que se deben guardar

                                                    if (getIdrespuestaConfig.Count > 0 && idTipoDeControl == 4 || idTipoDeControl == 3)
                                                    {

                                                        List<ML.ConfiguraRespuesta> respuesta = BL.ConfiguraRespuesta.getAllAnswersConfigByIdEncuestIdPregunta(EncuestaRespuesta.IdEncuesta, EncuestaRespuesta.ListarPreguntas[r].IdPregunta, idRespuesta);
                                                        for (var e = 0; e < respuesta.Count; e++)
                                                        {
                                                            for (var t = 0; t < EncuestaRespuesta.ListarPreguntas.Count; t++)
                                                            {
                                                                if (respuesta[e].IdPreguntaOpen == EncuestaRespuesta.ListarPreguntas[t].IdPregunta)
                                                                {
                                                                    if (EncuestaRespuesta.ListarPreguntas[t].PreguntasCondicion != "si" || EncuestaRespuesta.ListarPreguntas[t].PreguntasCondicion == null)
                                                                    {
                                                                        EncuestaRespuesta.ListarPreguntas[t].PreguntasCondicion = "no";
                                                                        EncuestaRespuesta.ListarPreguntas[t].isDisplay = true;
                                                                    }

                                                                }

                                                            }
                                                        }
                                                    }
                                                }
                                                btermina++;
                                            }
                                        }
                                        else
                                        {//Solo una respuesta
                                            idRespuesta = EncuestaRespuesta.ListarPreguntas[r].Respuestas[0].IdRespuesta;
                                            if (EncuestaRespuesta.ListarPreguntas[r].Respuestas[0].Respuesta != null)
                                            {
                                                if (EncuestaRespuesta.ListarPreguntas[r].PreguntasCondicion != "no")
                                                {
                                                    string nombrePregunta = EncuestaRespuesta.ListarPreguntas[r].Respuestas[0].Respuesta;//BL.Respuestas.RegresaNombreRespuesta(idRespuesta);
                                                    if (EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 1 || EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 2)
                                                    {
                                                        if (idPregunta == 0)
                                                        {
                                                            int IdUsr = Convert.ToInt32(usuarioContesta);
                                                            BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                        var res = new ML.Result();
                                                        res.Correct = false;
                                                        res.ErrorMessage = "reload";
                                                        return res;
                                                    }
                                                        var queryUpdate1 = context.Database.ExecuteSqlCommand
                                                            ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2}",
                                                            idEncuesta, usuarioContesta, idPregunta, nombrePregunta,DateTime.Now, usuarioContesta);

                                                        
                                                    }
                                                    else if (EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 3 || EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 4)
                                                    {
                                                    //if (Encuesta.tieneRespuesta(idPregunta) == false)
                                                    //{
                                                        if (EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 3)
                                                        {
                                                            if (idPregunta == 0 || idRespuesta == 0)
                                                            {
                                                                int IdUsr = Convert.ToInt32(usuarioContesta);
                                                                BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                                var res = new ML.Result();
                                                                res.Correct = false;
                                                                res.ErrorMessage = "reload";
                                                                return res;
                                                            }
                                                            var queryUpdate = context.Database.ExecuteSqlCommand
                                                                ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2} AND IDRESPUESTA = {6}",
                                                                idEncuesta, usuarioContesta, idPregunta, nombrePregunta, DateTime.Now, usuarioContesta, idRespuesta);//idRespuesta
                                                        }
                                                        else
                                                        {
                                                            if (idPregunta == 0)
                                                            {
                                                                int IdUsr = Convert.ToInt32(usuarioContesta);
                                                                BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                                var res = new ML.Result();
                                                                res.Correct = false;
                                                                res.ErrorMessage = "reload";
                                                                return res;
                                                            }
                                                            var queryUpdate = context.Database.ExecuteSqlCommand
                                                                ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2}",// AND IDRESPUESTA = {3}
                                                                idEncuesta, usuarioContesta, idPregunta, nombrePregunta, DateTime.Now, usuarioContesta);//idRespuesta
                                                        }
                                                    }
                                                    else if (EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 6 || EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 7)
                                                    {
                                                            if (idPregunta == 0)
                                                            {
                                                                int IdUsr = Convert.ToInt32(usuarioContesta);
                                                                BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                                var res = new ML.Result();
                                                                res.Correct = false;
                                                                res.ErrorMessage = "reload";
                                                                return res;
                                                            }
                                                            var queryUpdate1 = context.Database.ExecuteSqlCommand
                                                                ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2}",
                                                                idEncuesta, usuarioContesta, idPregunta, nombrePregunta,DateTime.Now, usuarioContesta);
                                                    }
                                                    else if (EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl > 7 && EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl < 12)
                                                    {
                                                        if (idPregunta == 0)
                                                        {
                                                            int IdUsr = Convert.ToInt32(usuarioContesta);
                                                            BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                            var res = new ML.Result();
                                                            res.Correct = false;
                                                            res.ErrorMessage = "reload";
                                                            return res;
                                                        }
                                                        var queryUpdate1 = context.Database.ExecuteSqlCommand
                                                            ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2}",
                                                            idEncuesta, usuarioContesta, idPregunta, nombrePregunta, DateTime.Now, usuarioContesta);
                                                    }
                                                    else if (EncuestaRespuesta.ListarPreguntas[r].TipoControl.IdTipoControl == 12)
                                                    {
                                                        if (idPregunta == 0 || idRespuesta == 0)
                                                        {
                                                            int IdUsr = Convert.ToInt32(usuarioContesta);
                                                            BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                            var res = new ML.Result();
                                                            res.Correct = false;
                                                            res.ErrorMessage = "reload";
                                                            return res;
                                                        }
                                                        var queryUpdate = context.Database.ExecuteSqlCommand
                                                            ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {4}, FechaHoraModificacion = {5}, UsuarioModificacion = {6}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2} AND IDRESPUESTA = {3}",
                                                            idEncuesta, usuarioContesta, idPregunta, idRespuesta, nombrePregunta, DateTime.Now, usuarioContesta);
                                                    }
                                                    
                                                    //fill Bandera termina
                                                    consulta = Encuesta.getConfiguracionTermina(idRespuesta);
                                                }


                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (EncuestaRespuesta.ListarPreguntas[r].ListadoRespuestas.Count > 0)
                                        {
                                            for (var n = 0; n < EncuestaRespuesta.ListarPreguntas[r].ListadoRespuestas.Count; n++)
                                            {
                                                if (EncuestaRespuesta.ListarPreguntas[r].PreguntasCondicion != "no")
                                                {
                                                    idRespuesta = Convert.ToInt32(EncuestaRespuesta.ListarPreguntas[r].ListadoRespuestas[n]);
                                                    string nombrePregunta = BL.Respuestas.RegresaNombreRespuesta(idRespuesta);

                                                    if (idPregunta == 0 || idRespuesta == 0)
                                                    {
                                                        int IdUsr = Convert.ToInt32(usuarioContesta);
                                                        BL.Encuesta.writeLogIdResCeroAddResp(idEncuesta, IdUsr, idPregunta);
                                                        var res = new ML.Result();
                                                        res.Correct = false;
                                                        res.ErrorMessage = "reload";
                                                        return res; 
                                                    }
                                                    var queryUpdate = context.Database.ExecuteSqlCommand
                                                        ("UPDATE USUARIORESPUESTAS SET IDESTATUS = 1, RespuestaUsuario = {3}, FechaHoraModificacion = {4}, UsuarioModificacion = {5}, ProgramaModificacion = 'Autoguardado' WHERE IDENCUESTA = {0} AND IDUSUARIO = {1} AND IDPREGUNTA = {2} AND IDRESPUESTA = {6}",// 
                                                        idEncuesta, usuarioContesta, idPregunta, nombrePregunta, DateTime.Now, usuarioContesta, idRespuesta);
                                                    consulta = Encuesta.getConfiguracionTermina(idRespuesta);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (invalidaSiguienteSubsecciones == true)
                            {
                                var preg = BL.ConfiguraRespuesta.getPregsToDisable(EncuestaRespuesta.IdEncuesta);
                                foreach (var item in preg)
                                {
                                    var query = context.Database.ExecuteSqlCommand
                                    ("UPDATE USUARIORESPUESTAS SET IDESTATUS = NULL, usuariomodificacion = 'Descartada', FechaHoraModificacion = {3}, UsuarioModificacion = {4}, ProgramaModificacion = 'Autoguardado' WHERE IDPREGUNTA = {0} AND IDENCUESTA = {1} AND IDUSUARIO = {2}", item.IdPregunta, idEncuesta, usuarioContesta, DateTime.Now, usuarioContesta);
                                }
                            }


                            result.Correct = true;
                            context.SaveChanges();
                            var pathS = @"\\10.5.2.101\RHDiagnostics\log\Log" + EncuestaRespuesta.IdEncuesta + "_" + EncuestaRespuesta.UsuarioCreacion + ".txt";
                            var fullPath1S = Path.GetFullPath(pathS);
                            if (!File.Exists(fullPath1S))
                            {
                                string createText = "Log" + Environment.NewLine;
                                File.WriteAllText(fullPath1S, createText);
                            }
                            string appendText1S = "Envio de encuesta correcto" + " " + DateTime.Now + Environment.NewLine;
                            File.AppendAllText(fullPath1S, appendText1S);
                    }
                        catch (SqlException aE)
                        {
                            result.Correct = false;
                            result.ErrorMessage = aE.Message;
                            var path2 = @"\\10.5.2.101\RHDiagnostics\log\Log" + EncuestaRespuesta.IdEncuesta + "_" + EncuestaRespuesta.UsuarioCreacion + ".txt";
                            var fullPath2 = Path.GetFullPath(path2);
                            if (!File.Exists(fullPath2))
                            {
                                string createText = "Log" + Environment.NewLine;
                                File.WriteAllText(fullPath2, createText);
                            }
                            string appendText2 = "Envio de encuesta: Error Metodo: AddRespuestas: " + aE.Message +  " " + "  Error Code " + aE.ErrorCode + " "  + DateTime.Now + Environment.NewLine;
                            File.AppendAllText(fullPath2, appendText2);

                            if (aE.Message.Contains("fue elegida como sujeto del interbloqueo. Ejecute de nuevo la transacción"))
                            {
                                Console.WriteLine("Reintento");
                                ExceptionCode = 1205;
                            }
                    }
                }
            }
            else
            {
                result.Correct = false;
                result.ErrorMessage = "Respondio";
            }
            if (result.Correct == false && ExceptionCode == 1205)
            {
                var path3 = @"\\10.5.2.101\RHDiagnostics\log\Log" + EncuestaRespuesta.IdEncuesta + "_" + EncuestaRespuesta.UsuarioCreacion + ".txt";
                var fullPath3 = Path.GetFullPath(path3);
                if (!File.Exists(fullPath3))
                {
                    string createText = "Log" + Environment.NewLine;
                    File.WriteAllText(fullPath3, createText);
                }
                string appendText3 = "Se reintenta el grabado final" + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath3, appendText3);
                goto reintento;
            }
            return result;
        }
        public static bool tieneRespuesta(int IdPregunta)
        {
            bool tieneRespuesta = false;//
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.UsuarioRespuestas.SqlQuery("select * from usuariorespuestas where idpregunta = {0}", IdPregunta).ToList();
                    if (query.Count > 0)
                    {
                        foreach (var item in query)
                        {
                            if (item.RespuestaUsuario != null)
                            {
                                tieneRespuesta = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return tieneRespuesta;
        }
        public static ML.Result AddBasico(ML.Encuesta Encuesta, int currentUsercreacion)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {                        //Alta Encuesta
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO Encuesta (DosColumnas,Nombre,FechaInicio,FechaFin,IdEstatus,IdEmpresa,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,CodeHTML,IdPlantilla,IdBasesDeDatos,Descripcion,Instruccion,ImagenInstruccion,IdTipoEncuesta,Agradecimiento,ImagenAgradecimiento) " +
                            " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})", Encuesta.DosColumnas, Encuesta.Nombre, Encuesta.FechaInicio, Encuesta.FechaFin, 1, Encuesta.IdEmpresa, DateTime.Now, currentUsercreacion, "Alta Encuesta", "", Encuesta.Plantillas.IdPlantilla, Encuesta.BasesDeDatos.IdBaseDeDatos, Encuesta.Descripcion,
                            Encuesta.Instruccion, Encuesta.ImagenInstruccion, Encuesta.MLTipoEncuesta.IdTipoEncuesta, Encuesta.Agradecimiento, Encuesta.ImagenAgradecimiento);
                        int idEncuesta = context.Encuesta.Max(q => q.IdEncuesta);


                        ////Alta reporte Diagnostic4U
                        //var EXISTReporte = context.EncuestaReporte.SqlQuery("SELECT * FROM EncuestaReporte WHERE IDENCUESTA = {0}", idEncuesta).ToList();
                        //if (EXISTReporte.Count > 0)
                        //{
                        //    //No inserta para evitar os
                        //}
                        //else
                        //{
                        //    var insertReporte = context.Database.ExecuteSqlCommand("INSERT INTO REPORTE (NOMBRE, DESCRIPCION, LOCATION) VALUES ({0}, {1}, {2})", "Reporte de la encuesta " + Encuesta.Nombre, "Reporte de resultados y estatus de la encuesta", "/ReporteD4U/Reporte?IdEncuesta=" + idEncuesta);// Encuesta / ViewReporte ? IdEncuesta = 19
                        //    int idreporte = context.Reporte.Max(p => p.IdReporte);
                        //    var insertEncuestaReporte = context.Database.ExecuteSqlCommand("INSERT INTO ENCUESTAREPORTE (IDENCUESTA, IDREPORTE) VALUES ({0}, {1})", idEncuesta, idreporte);
                        //    //context.SaveChanges();
                        //}

                        int IdBaseDeDatos = Convert.ToInt32(Encuesta.BasesDeDatos.IdBaseDeDatos);
                        BL.Usuario.AddEstatus(idEncuesta, IdBaseDeDatos, context);


                        //Encuesta Area
                        var obtieneAreasPorIdEmpresa = BL.Area.AreaGetByCompanyId(Int32.Parse(Encuesta.IdEmpresa.ToString()));
                        if (obtieneAreasPorIdEmpresa.Objects.Count >= 0)
                        {
                            foreach (ML.Area obj in obtieneAreasPorIdEmpresa.Objects)
                            {
                                var queryEncuestaArea = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaArea (IdArea,IdEncuesta,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion) " +
                                    " VALUES({0},{1},{2},{3},{4})", obj.IdArea, idEncuesta, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                            }
                        }
                        result.Correct = true;
                        result.idEncuestaAlta = idEncuesta;
                        context.SaveChanges();
                        transaction.Commit();
                        idEncuesta = 0;
                    }
                    catch (Exception aE)
                    {
                        result.Correct = false;
                        result.ErrorMessage = aE.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }
        public static ML.Result Delete(int idEncuesta)
        {
            ML.Result result = new ML.Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        DL.Encuesta deleteEncuesta = context.Encuesta.FirstOrDefault(x => x.IdEncuesta == idEncuesta);
                        deleteEncuesta.IdEstatus = 3;
                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;

                    }
                    catch (Exception aE)
                    {
                        transaction.Rollback();
                        result.Correct = false;
                        result.ErrorMessage = aE.Message;
                    }
                }
            }
            return result;
        }

        public static ML.Result getPreviewEncuesta(int idEncuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " inner join Plantillas on Plantillas.IdPlantilla = Encuesta.IdPlantilla " +
                        " where Encuesta.idEncuesta = {0} and Encuesta.IdEstatus = {1} and Plantillas.IdEstatus = {2}", idEncuesta, idEstatus, idEstatus).ToList();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.DosColumnas = Convert.ToBoolean(obj.DosColumnas);
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.UID = obj.UID;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Agradecimiento = obj.Agradecimiento;
                            encuesta.Company = new ML.Company();
                            encuesta.Company.CompanyId = obj.IdEmpresa;
                            encuesta.Descripcion = obj.Descripcion;
                            encuesta.Estatus = obj.Estatus;
                            encuesta.FechaFin = obj.FechaFin;
                            encuesta.FechaInicio = obj.FechaInicio;
                            encuesta.ImagenAgradecimiento = obj.ImagenAgradecimiento;
                            encuesta.ImagenInstruccion = obj.ImagenInstruccion;
                            encuesta.Instruccion = obj.Instruccion;
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.Plantillas = new ML.Plantillas();
                            ML.Result obtieneTodaLaPlantilla = BL.Plantillas.getPlantillaById(Convert.ToInt32(obj.IdPlantilla));
                            encuesta.Plantillas.HeaderPlantilla = new ML.HeaderPlantilla();
                            encuesta.Plantillas.HeaderPlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.CodeHTML;
                            encuesta.Plantillas.HeaderPlantilla.IdHeaderPlantilla = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.IdHeaderPlantilla;
                            //alta de colores
                            encuesta.Plantillas.HeaderPlantilla.color1 = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.color1;
                            encuesta.Plantillas.HeaderPlantilla.color2 = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.color2;
                            encuesta.Plantillas.DetallePlantilla = new ML.DetallePlantilla();
                            encuesta.Plantillas.DetallePlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.CodeHTML;
                            encuesta.Plantillas.DetallePlantilla.ImagenIco = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.ImagenIco;
                            encuesta.Plantillas.DetallePlantilla.ThumbImage = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.ThumbImage;
                            //alta de colores
                            encuesta.Plantillas.DetallePlantilla.Color1 = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.Color1;
                            encuesta.Plantillas.DetallePlantilla.Color2 = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.Color2;
                            encuesta.Plantillas.DetallePlantilla.IdDetallePlantilla = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.IdDetallePlantilla;
                            encuesta.Plantillas.DetallePlantilla.IdPlantillaDefinida = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.IdPlantillaDefinida;
                            encuesta.Plantillas.BodyPlantilla = new ML.BodyPlantilla();
                            encuesta.Plantillas.BodyPlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.CodeHTML;
                            //alta de colores
                            encuesta.Plantillas.BodyPlantilla.Color1 = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.Color1;
                            encuesta.Plantillas.BodyPlantilla.Color2 = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.Color2;
                            encuesta.Plantillas.BodyPlantilla.ImagenFondo = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.ImagenFondo;
                            encuesta.Plantillas.FooterPlantilla = new ML.FooterPlantilla();
                            encuesta.Plantillas.FooterPlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.FooterPlantilla.CodeHTML;
                            //alta de colores
                            encuesta.Plantillas.FooterPlantilla.Color1 = obtieneTodaLaPlantilla.EditaPlantillas.FooterPlantilla.Color1;
                            encuesta.Plantillas.FooterPlantilla.Color2 = obtieneTodaLaPlantilla.EditaPlantillas.FooterPlantilla.Color2;
                            encuesta.Plantillas.IdPlantilla = Convert.ToInt32(obj.IdPlantilla);
                            encuesta.ProgramaCreacion = obj.ProgramaCreacion;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            encuesta.UsuarioCreacion = obj.UsuarioCreacion;
                            //var configuraciones = BL.ConfiguraRespuesta.getAllByIdEncuesta(encuesta.IdEncuesta);
                            //encuesta.ConfiguraRespuesta = configuraciones.Objects;
                            encuesta.ListarPreguntas = BL.Preguntas.getPreguntasByIdEncuesta(obj.IdEncuesta);
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                            result.EncuestaActiva = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        return result;
                    }
                    return result;

                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result Edit(ML.Encuesta Encuesta, string usrLog)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Cambios de BD
                        var IdBDOriginal = (from a in context.Encuesta where a.IdEncuesta == Encuesta.IdEncuesta select a.IdBasesDeDatos).FirstOrDefault();
                        if (Encuesta.BasesDeDatos.IdBaseDeDatos != IdBDOriginal)
                        {
                            //Remove userStatus for lastDB
                            var getUsrRemove = context.Usuario.SqlQuery("SELECT * FROM USUARIO WHERE IDBASEDEDATOS = {0}", IdBDOriginal);
                            if (getUsrRemove.Count() > 0)
                            {
                                foreach (var item in getUsrRemove)
                                {
                                    var remove = context.Database.ExecuteSqlCommand("DELETE FROM UsuarioEstatusEncuesta WHERE IdUsuario = {0} AND IdEncuesta = {1}", item.IdUsuario, Encuesta.IdEncuesta);
                                    context.SaveChanges();
                                }
                            }
                            //Add userStatus for new DB
                            var getNewUsers = context.Usuario.SqlQuery("SELECT * FROM USUARIO WHERE IDBASEDEDATOS = {0}", Encuesta.BasesDeDatos.IdBaseDeDatos).ToList();
                            if (getNewUsers.Count() > 0)
                            {
                                foreach (var item in getNewUsers)
                                {
                                    var insert = context.Database.ExecuteSqlCommand
                                    ("INSERT INTO UsuarioEstatusEncuesta (IdUsuario, IdEncuesta, IdEstatusEncuestaD4U, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                                    item.IdUsuario, Encuesta.IdEncuesta, 1, DateTime.Now, usrLog, "Diagnostic4U");
                                    context.SaveChanges();
                                }
                            }
                        }


                        var UsuarioModificacion = Encuesta.UsuarioModificacion == null ? "Usuario Editor" : Encuesta.UsuarioModificacion.ToString();
                        var ProgramaModificacion = "Actualiza Encuesta";
                        //Edita Encuesta
                        DL.Encuesta updateEncuesta = context.Encuesta.FirstOrDefault(x => x.IdEncuesta == Encuesta.IdEncuesta);
                        int idEncuesta = Convert.ToInt32(updateEncuesta.IdEncuesta);
                        int idEstatus = Convert.ToInt32(updateEncuesta.IdEstatus);
                        int idPlantilla = Convert.ToInt32(updateEncuesta.IdPlantilla);
                        int idBaseDeDatos = Convert.ToInt32(updateEncuesta.IdBasesDeDatos);
                        int idTipoEncuesta = Convert.ToInt32(updateEncuesta.IdTipoEncuesta);
                        int idEmpresa = Convert.ToInt32(updateEncuesta.IdEmpresa);
                        updateEncuesta.Nombre = Encuesta.Nombre.ToString();
                        updateEncuesta.FechaInicio = Encuesta.FechaInicio;
                        updateEncuesta.FechaFin = Encuesta.FechaFin;
                        updateEncuesta.Estatus = Encuesta.Estatus;
                        updateEncuesta.DosColumnas = Encuesta.DosColumnas;
                        updateEncuesta.IdEstatus = 1;
                        updateEncuesta.FechaHoraModificacion = DateTime.Now;
                        updateEncuesta.UsuarioModificacion = UsuarioModificacion;
                        updateEncuesta.ProgramaModificacion = ProgramaModificacion;
                        updateEncuesta.CodeHTML = "";
                        updateEncuesta.IdPlantilla = Encuesta.Plantillas.IdPlantilla;
                        updateEncuesta.IdBasesDeDatos = Encuesta.BasesDeDatos.IdBaseDeDatos;
                        updateEncuesta.Descripcion = Encuesta.Descripcion;
                        updateEncuesta.Instruccion = Encuesta.Instruccion;
                        updateEncuesta.ImagenInstruccion = Encuesta.ImagenInstruccion;
                        updateEncuesta.IdTipoEncuesta = Encuesta.MLTipoEncuesta.IdTipoEncuesta;
                        updateEncuesta.IdEmpresa = Encuesta.IdEmpresa;
                        updateEncuesta.Agradecimiento = Encuesta.Agradecimiento;
                        updateEncuesta.ImagenAgradecimiento = Encuesta.ImagenAgradecimiento;
                        context.SaveChanges();

                        //Edita Pregunta
                        if (Encuesta.NewCuestionEdit != null)
                        {
                            foreach (ML.Preguntas obj in Encuesta.NewCuestionEdit)
                            {
                                DL.Preguntas updatePregunta = context.Preguntas.FirstOrDefault(x => x.IdPregunta == obj.IdPregunta);
                                updatePregunta.Pregunta = obj.Pregunta;
                                updatePregunta.IdEstatus = 1;
                                updatePregunta.FechaHoraModificacion = DateTime.Now;
                                updatePregunta.UsuarioModificacion = UsuarioModificacion;
                                updatePregunta.ProgramaModificacion = ProgramaModificacion;
                                updatePregunta.IdTipoControl = obj.TipoControl.IdTipoControl;
                                updatePregunta.Obligatoria = obj.Obligatoria;
                                context.SaveChanges();
                                //Respuesta Update
                                if (obj.NewAnswerEdit != null)
                                {
                                    foreach (ML.Respuestas objR in obj.NewAnswerEdit)
                                    {
                                        DL.Respuestas updateRespuesta = context.Respuestas.FirstOrDefault(x => x.IdRespuesta == objR.IdRespuesta);

                                    }

                                }
                            }

                        }
                        if (Encuesta.NewCuestion != null)
                        {
                            foreach (ML.Preguntas obj in Encuesta.NewCuestion)
                            {
                                obj.Competencia = new ML.Competencia();
                                // obj.Competencia.IdCompetencia = null;
                                var queryPreguntas = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                "(idEncuesta,Pregunta,Valoracion,TipoControl,IdCompetencia,IdEstatus,FechaHoraCreacion, " +
                                " UsuarioCreacion,ProgramaCreacion,Enfoque, IdTipoControl, RespuestaCondicion, PreguntasCondicion, Obligatoria) " +
                                " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13})", idEncuesta, obj.Pregunta,
                                obj.Valoracion, "", obj.Competencia.IdCompetencia, 1, DateTime.Now,
                                Encuesta.UsuarioCreacion, "Alta Encuesta", obj.Enfoque, obj.TipoControl.IdTipoControl,
                                obj.RespuestaCondicion, obj.PreguntasCondicion, obj.Obligatoria);
                                int idPregunta = context.Preguntas.Max(q => q.IdPregunta);
                                //Tabla Encuesta Pregunta
                                var queryEncuestaPregunta = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                    "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                    "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPregunta, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                //Respuestas
                                switch (obj.TipoControl.IdTipoControl)
                                {
                                    case 1:
                                        var queryRespuesta1 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Respuesta Corta", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                    case 2:
                                        var queryRespuesta2 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Respuesta Larga", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                    case 3:
                                        foreach (ML.Respuestas objR in obj.NewAnswer)
                                        {
                                            var queryRespuesta3 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        }
                                        break;
                                    case 4:
                                        foreach (ML.Respuestas objR in obj.NewAnswer)
                                        {
                                            var queryRespuesta4 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        }
                                        break;
                                    case 5:
                                        foreach (ML.Respuestas objR in obj.NewAnswer)
                                        {
                                            var queryRespuesta4 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta,IdEstatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", objR.Respuesta, idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        }
                                        break;

                                    case 6:
                                        var queryRespuesta6 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Sentimiento", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;

                                    case 7:
                                        var queryRespuesta7 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Rango", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                    case 8:
                                        var queryRespuesta8 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Likert Acuerdo", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                    case 9:
                                        var queryRespuesta9 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Likert Frecuencia", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                    case 10:
                                        var queryRespuesta10 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Likert Importacia", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                    case 11:
                                        var queryRespuesta11 = context.Database.ExecuteSqlCommand("INSERT INTO Respuestas " +
                                            "(Respuesta, IdPregunta, IdEstatus,FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                            " VALUES({0},{1},{2},{3},{4},{5})", "Likert Probabilidad", idPregunta, 1, DateTime.Now, Encuesta.UsuarioCreacion, "Alta Encuesta");
                                        break;
                                }
                                idPregunta = 0;
                            }


                        }
                        else
                        { }
                        result.Correct = true;
                        context.SaveChanges();
                        transaction.Commit();
                        idEncuesta = 0;
                    }
                    catch (Exception aE)
                    {
                        result.Correct = false;
                        result.ErrorMessage = aE.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }

        public static ML.Result e(string UID)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.UID = {0} and Encuesta.IdEstatus = {1}", UID, idEstatus).ToList();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.DosColumnas = Convert.ToBoolean(obj.DosColumnas);
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.UID = obj.UID;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Agradecimiento = obj.Agradecimiento;
                            encuesta.Company = new ML.Company();
                            encuesta.Company.CompanyId = obj.IdEmpresa;
                            encuesta.Descripcion = obj.Descripcion;
                            encuesta.Estatus = obj.Estatus;
                            encuesta.FechaFin = obj.FechaFin;
                            encuesta.FechaInicio = obj.FechaInicio;
                            encuesta.ImagenAgradecimiento = obj.ImagenAgradecimiento;
                            encuesta.ImagenInstruccion = obj.ImagenInstruccion;
                            encuesta.Instruccion = obj.Instruccion;
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.Plantillas = new ML.Plantillas();
                            ML.Result obtieneTodaLaPlantilla = BL.Plantillas.getPlantillaById(Convert.ToInt32(obj.IdPlantilla));
                            encuesta.Plantillas.HeaderPlantilla = new ML.HeaderPlantilla();
                            encuesta.Plantillas.HeaderPlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.CodeHTML;
                            encuesta.Plantillas.HeaderPlantilla.IdHeaderPlantilla = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.IdHeaderPlantilla;
                            //alta de colores
                            encuesta.Plantillas.HeaderPlantilla.color1 = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.color1;
                            encuesta.Plantillas.HeaderPlantilla.color2 = obtieneTodaLaPlantilla.EditaPlantillas.HeaderPlantilla.color2;
                            encuesta.Plantillas.DetallePlantilla = new ML.DetallePlantilla();
                            encuesta.Plantillas.DetallePlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.CodeHTML;
                            encuesta.Plantillas.DetallePlantilla.ImagenIco = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.ImagenIco;
                            encuesta.Plantillas.DetallePlantilla.ThumbImage = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.ThumbImage;
                            //alta de colores
                            encuesta.Plantillas.DetallePlantilla.Color1 = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.Color1;
                            encuesta.Plantillas.DetallePlantilla.Color2 = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.Color2;
                            encuesta.Plantillas.DetallePlantilla.IdDetallePlantilla = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.IdDetallePlantilla;
                            encuesta.Plantillas.DetallePlantilla.IdPlantillaDefinida = obtieneTodaLaPlantilla.EditaPlantillas.DetallePlantilla.IdPlantillaDefinida;
                            encuesta.Plantillas.BodyPlantilla = new ML.BodyPlantilla();
                            encuesta.Plantillas.BodyPlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.CodeHTML;
                            //alta de colores
                            encuesta.Plantillas.BodyPlantilla.Color1 = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.Color1;
                            encuesta.Plantillas.BodyPlantilla.Color2 = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.Color2;
                            encuesta.Plantillas.BodyPlantilla.ImagenFondo = obtieneTodaLaPlantilla.EditaPlantillas.BodyPlantilla.ImagenFondo;
                            encuesta.Plantillas.FooterPlantilla = new ML.FooterPlantilla();
                            encuesta.Plantillas.FooterPlantilla.CodeHTML = obtieneTodaLaPlantilla.EditaPlantillas.FooterPlantilla.CodeHTML;
                            //alta de colores
                            encuesta.Plantillas.FooterPlantilla.Color1= obtieneTodaLaPlantilla.EditaPlantillas.FooterPlantilla.Color1;
                            encuesta.Plantillas.FooterPlantilla.Color2 = obtieneTodaLaPlantilla.EditaPlantillas.FooterPlantilla.Color2;
                            encuesta.Plantillas.IdPlantilla = Convert.ToInt32(obj.IdPlantilla);
                            encuesta.ProgramaCreacion = obj.ProgramaCreacion;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            encuesta.UsuarioCreacion = obj.UsuarioCreacion;
                            //var configuraciones = BL.ConfiguraRespuesta.getAllByIdEncuesta(encuesta.IdEncuesta);
                            //encuesta.ConfiguraRespuesta = configuraciones.Objects;
                            encuesta.ListarPreguntas = BL.Preguntas.getPreguntasByIdEncuesta(obj.IdEncuesta);
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                            result.EncuestaActiva = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        return result;
                    }
                    return result;

                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;
        }

        public static ML.Result GetData(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            //string NombreEncuesta = "";
            //DateTime FechaInicio = DateTime.Now;
            //DateTime FechaFin = DateTime.Now;
            int BaseDeDatos = 0;
            //string URL = "";
            //int TipoEncuesta = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var getIdBD = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA INNER JOIN BasesDeDatos ON Encuesta.IdBasesDeDatos = BasesDeDatos.IdBasesDeDatos WHERE IDENCUESTA = {0}", IdEncuesta).ToList();
                    if (getIdBD != null)
                    {
                        foreach (var item in getIdBD)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.BasesDeDatos = new ML.BasesDeDatos();
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();

                            encuesta.Nombre = item.Nombre;
                            encuesta.FechaInicio = Convert.ToDateTime(item.FechaInicio);
                            encuesta.FechaFin = Convert.ToDateTime(item.FechaFin);
                            encuesta.BasesDeDatos.IdBaseDeDatos = Convert.ToInt32(item.IdBasesDeDatos);
                            encuesta.BasesDeDatos.Nombre = item.BasesDeDatos.Nombre;
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = Convert.ToInt32(item.IdTipoEncuesta);
                            encuesta.UID = item.UID;

                            result.Object = encuesta;
                            BaseDeDatos = Convert.ToInt32(item.IdBasesDeDatos);
                        }
                    }

                    var getData = context.Usuario.SqlQuery("SELECT * FROM USUARIO LEFT JOIN BASESDEDATOS ON Usuario.IdBaseDeDatos = BASESDEDATOS.IDBASESDEDATOS WHERE Usuario.IdBaseDeDatos = {0} AND Usuario.IDESTATUS != 6", BaseDeDatos).ToList();
                    if (getData != null)
                    {
                        foreach (var item in getData)
                        {
                            ML.Usuario user = new ML.Usuario();
                            user.BaseDeDatos = new ML.BasesDeDatos();

                            //DataUsuario
                            user.Nombre = item.Nombre;
                            user.ApellidoPaterno = item.ApellidoPaterno;
                            user.ApellidoMaterno = item.ApellidoMaterno;
                            user.Email = item.Email;
                            user.ClaveAcceso = item.ClaveAcceso;

                            result.Objects.Add(user);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Autenticar(ML.Usuario usuarios)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    int Idusuario = 0;
                    string sinespacio = usuarios.ClaveAcceso.Trim();

                    var getDataUser = context.Usuario.SqlQuery("SELECT * FROM USUARIO WHERE CLAVEACCESO = {0} COLLATE Latin1_General_CS_AS", sinespacio);
                    if (getDataUser != null)
                    {
                        foreach (var item in getDataUser)
                        {
                            ML.Usuario user = new ML.Usuario();

                            user.IdUsuario = item.IdUsuario;
                            Idusuario = user.IdUsuario;
                            result.Object = user;
                        }
                    }


                    int IdEncuesta = 0;
                    var query = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA INNER JOIN BasesDeDatos ON Encuesta.IdBasesDeDatos = BasesDeDatos.IdBasesDeDatos INNER JOIN Usuario ON BasesDeDatos.IdBasesDeDatos = Usuario.IdBaseDeDatos where Usuario.ClaveAcceso =  {0}  COLLATE Latin1_General_CS_AS AND Encuesta.IdEncuesta= {1}", usuarios.ClaveAcceso, usuarios.IdEncuesta).ToList();

                    //if (query.Count == 0)
                    //{
                    //    result.Correct = false;
                    //}

                    if (query.Count != 0)
                    {
                        foreach (var item in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.BasesDeDatos = new ML.BasesDeDatos();
                            encuesta.BasesDeDatos.Encuesta = new ML.Encuesta();
                            encuesta.usuario = new ML.Usuario();

                            encuesta.Nombre = item.Nombre;
                            encuesta.IdEncuesta = item.IdEncuesta;
                            encuesta.UID = item.UID;
                            IdEncuesta = encuesta.IdEncuesta;

                            result.ObjectAux = encuesta;
                        }
                    }
                    var validaDuplicado = context.UsuarioEstatusEncuesta.SqlQuery("SELECT * FROM UsuarioEstatusEncuesta WHERE IDENCUESTA = {0} AND IDUSUARIO = {1}", IdEncuesta, Idusuario);
                    foreach (var item in validaDuplicado)
                    {
                        if (item.IdEstatusEncuestaD4U == 3)
                        {
                            result.IsContestada = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetTipoEncuesta(int IdEncuesta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA WHERE IDENCUESTA = {0}", IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            result.Object = item.IdTipoEncuesta;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result ValidaFechaEncuesta(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Encuesta.SqlQuery("SELECT * FROM Encuesta where IdEncuesta={0} and FechaFin >= CONVERT(char(10), GetDate(),126)", IdEncuesta).ToList();
                    if (query != null)
                    {
                        if (query.Count > 0)
                        {
                            result.Correct = true;
                            result.Exist = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Exist = false;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString() +"_"+ aE.StackTrace.ToString();
            }
            return result;
        }
        public static ML.Result GetAgradecimientoEncuesta(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            try {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Encuesta.SqlQuery("SELECT * FROM Encuesta where IdEncuesta={0} and IdEstatus =1",IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            result.EditaEncuesta = new ML.Encuesta();
                            if (String.IsNullOrEmpty(item.Agradecimiento))
                            {
                                result.EditaEncuesta.Agradecimiento = "";
                            }
                            else
                            {
                                result.EditaEncuesta.Agradecimiento = item.Agradecimiento;
                            }
                            
                        }
                    }

                }                
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString() + "_" + aE.StackTrace.ToString();
            }
            return result;
        }

        public static System.Data.DataSet GetRespuestasDinamycReport(int IdENcuesta, string url, int IdTipoEncuesta)
        {
            ML.Result result = new ML.Result();
            System.Data.DataSet ds = new System.Data.DataSet();
            List<string> ListIdPregunta = new List<string>();
            List<int> IdsPregunta = new List<int>();
            string getValueRespuestas = "";
            string Respuestas = "";
            DataSet DatosFinales = new DataSet();
            DatosFinales.Tables.Add();


            //Tipo Encuesta 2 y 3
            if (IdTipoEncuesta == 1)//Anonima
            {
                string SqlQuery =
                    "SELECT " +
                    " EstatusEncuestaD4U.Descripcion, ";
                try
                {
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        var getIdPreguntas = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = {0} ORDER BY IDPREGUNTA ASC", IdENcuesta).ToList();
                        if (getIdPreguntas != null)
                        {
                            int cont = 1;
                            foreach (var item in getIdPreguntas)
                            {
                                int idPregunta = item.IdPregunta;
                                IdsPregunta.Add(item.IdPregunta);
                                getValueRespuestas += "max(case when (Preguntas.IdPregunta = " + idPregunta + " AND TipoControl.IdTipoControl != 3 AND TipoControl.IdTipoControl != 12) then RespuestaUsuario when Preguntas.IdPregunta = " + idPregunta + " AND TipoControl.IdTipoControl = 3 then 'MULTRES' when Preguntas.IdPregunta = " + idPregunta + " AND TipoControl.IdTipoControl = 12 THEN 'LIKERTDOBLE' end) EMP_RES,";
                                cont++;
                            }
                        }
                        string aux = getValueRespuestas.Remove(getValueRespuestas.Length - 1);
                        SqlQuery = SqlQuery + aux;
                        string finalQuery =
                            " FROM Usuario " +
                            "left JOIN UsuarioRespuestas ON UsuarioRespuestas.IdUsuario = Usuario.IdUsuario " +
                            "left JOIN Perfil ON Usuario.IdPerfil = Perfil.IdPerfil INNER JOIN TipoEstatus ON Usuario.IdEstatus = TipoEstatus.IdEstatus " +
                            "left join Preguntas on UsuarioRespuestas.IdPregunta = Preguntas.IdPregunta " +
                            "left join TipoControl on Preguntas.IdTipoControl = TipoControl.IdTipoControl " +
                            "left join UsuarioEstatusEncuesta on UsuarioRespuestas.IdUsuario = UsuarioEstatusEncuesta.IdUsuario " +
                            "left join EstatusEncuestaD4U on UsuarioEstatusEncuesta.IdEstatusEncuestaD4U = EstatusEncuestaD4U.IdEstatusEncuestaD4U " +
                            "WHERE USUARIO.IDESTATUS != 6 AND UsuarioRespuestas.IdEstatus = 1 and UsuarioRespuestas.IdEncuesta = " + IdENcuesta +
                            "GROUP BY Usuario.IdUsuario, EstatusEncuestaD4U.Descripcion ORDER BY Usuario.IdUsuario ASC"; 
                        SqlQuery = SqlQuery + finalQuery;
                    }
                    string cadenaConexion = "";
                    string urlFinal = url.Substring(0, 22);
                    if (urlFinal == "http://demo.climalabor" || urlFinal == "http://localhost:11124")
                    {
                        cadenaConexion = "Data Source=192.192.192.97;Initial Catalog=RH_Des;User ID=sa;Password=Pa$$w0rd01;";
                    }
                    else
                    {
                        cadenaConexion = "Data Source=10.5.2.108;Initial Catalog=RH_Diagnostics;User ID=RHDiagnostics;Password=Pasword01;";
                    }
                    //Agrego al nuevo objeto tal como viene
                    //DatosFinales.Tables[0].Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                    //ConfigurationManager.ConnectionStrings["ConnectionStringName"].ToString()
                    //using (SqlConnection connection = new SqlConnection(cadenaConexion))
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                    {
                        try
                        {
                            connection.Open();
                            SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(SqlQuery, connection);
                            dat_1.Fill(ds, "dat_1");
                            int Numcolumnas = ds.Tables[0].Columns.Count;
                            for (int col = 0; col < Numcolumnas; col++)
                            {
                                string NombreCol = "Columna " + col;
                                DatosFinales.Tables[0].Columns.Add(NombreCol);
                            }

                            object[] datos = { };
                            object[] fromData = { };
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                fromData = ds.Tables[0].Rows[i].ItemArray;
                                for (int j = 0; j < ds.Tables[0].Rows[0].ItemArray.Count(); j++)
                                {
                                    if (ds.Tables[0].Rows[i].ItemArray[j].ToString() == "MULTRES")
                                    {
                                        using (DL.RH_DesEntities context_ = new DL.RH_DesEntities())
                                        {
                                            int index = BL.Reporte.getIndexListIdPregunta(j);
                                            var query = context_.UsuarioRespuestas.SqlQuery("SELECT * FROM UsuarioRespuestas WHERE IdEncuesta = {0} AND IdUsuario = {1} and idpregunta = {2}", IdENcuesta, ds.Tables[0].Rows[i].ItemArray[0], IdsPregunta[index]).ToList();
                                            if (query != null)
                                            {
                                                Respuestas = "";
                                                foreach (var item in query)
                                                {
                                                    Respuestas += item.RespuestaUsuario + " ";
                                                }
                                                datos = fromData;
                                                datos[j] = Respuestas;
                                            }
                                        }
                                    }
                                    else if (ds.Tables[0].Rows[i].ItemArray[j].ToString() == "LIKERTDOBLE")
                                    {
                                        using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                                        {
                                            Respuestas = "";
                                            string PreguntaLikert = "";
                                            int index = BL.Reporte.getIndexListIdPregunta(j);
                                            var getRespuestasLikert = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join Respuestas on UsuarioRespuestas.IdRespuesta = Respuestas.IdRespuesta inner join PreguntasLikert on Respuestas.IdPreguntaLikertD = PreguntasLikert.idPreguntasLikert WHERE usuariorespuestas.IDPREGUNTA = {0} AND UsuarioRespuestas.IdUsuario = {1} order by Respuestas.IdPreguntaLikertD", IdsPregunta[index], ds.Tables[0].Rows[i].ItemArray[0]).ToList();

                                            if (getRespuestasLikert != null)
                                            {
                                                foreach (var item in getRespuestasLikert)
                                                {
                                                    ML.UsuarioRespuestas userResp = new ML.UsuarioRespuestas();
                                                    userResp.Respuestas = new ML.Respuestas();
                                                    userResp.RespuestaUsuario = item.RespuestaUsuario;
                                                    userResp.Respuestas.Respuesta = item.Respuestas.Respuesta;//ColA & ColB
                                                                                                              //getIdDe pregunta Likert
                                                    userResp.Respuestas.PreguntasLikert = new ML.PreguntasLikert();
                                                    userResp.Respuestas.PreguntasLikert.IdPreguntaLikert = Convert.ToInt32(item.Respuestas.IdPreguntaLikertD);
                                                    var getPreguntaLikert = context.PreguntasLikert.SqlQuery("SELECT * FROM PreguntasLikert WHERE idPreguntasLikert = {0}", userResp.Respuestas.PreguntasLikert.IdPreguntaLikert).ToList();
                                                    foreach (var obj in getPreguntaLikert)
                                                    {
                                                        PreguntaLikert = obj.Pregunta;
                                                    }

                                                    Respuestas += " >" + PreguntaLikert + " (" + userResp.Respuestas.Respuesta + ") " + userResp.RespuestaUsuario;
                                                }
                                                datos = fromData;
                                                datos[j] = Respuestas;
                                            }
                                        }
                                    }
                                }
                                DatosFinales.Tables[0].Rows.Add(fromData);
                            }
                            connection.Close();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                return DatosFinales;
            }
            else
            {
                string SqlQuery =
                    "SELECT Usuario.IdUsuario ID_USUARIO," +
                    "Usuario.Nombre NOMBRE, Usuario.ApellidoPaterno A_PATERNO, Usuario.ApellidoMaterno A_MATERNO, " +
                    "Usuario.Puesto PUESTO ,Usuario.FechaNacimiento F_NACIM, Usuario.FechaAntiguedad F_ANTIG, " +
                    "Usuario.Sexo SEXO, Usuario.Email EMAIL, " +
                    "Usuario.TipoFuncion TIPO_FUNCION, Usuario.CondicionTrabajo CONDIC_TRAB, " +
                    "Usuario.GradoAcademico GRADO_ACADEM, Usuario.UnidadNegocio UNIDAD_NEGOCIO, Usuario.DivisionMarca DIVIS_MARCA, " +
                    "Usuario.AreaAgencia AREA_AGENCIA, Usuario.Departamento DEPARTAMENTO, " +
                    "Usuario.Subdepartamento SUBDEPARTAMENTO," +
                    "Usuario.EmpresaContratante EMP_CONTRATANTE, " +
                    "Usuario.IdResponsableRH ID_RESPONS_RH, " +
                    "Usuario.NombreResponsableRH NOM_RESPONS_RH, Usuario.IdJefe ID_JEFE, " +
                    "Usuario.NombreJefe NOMBRE_JEFE, PuestoJefe PUESTO_JEFE,  " +
                    "Usuario.IdResponsableEstructura ID_RESPONS_EST, " +
                    "Usuario.NombreResponsableEstructura NOM_RESP_ESTRUC , " +
                    "Usuario.RangoAntiguedad RANGO_ANTIG , Usuario.RangoEdad RANGO_EDAD," +
                    "TipoEstatus.Descripcion ESTATUS," +
                    "Usuario.CampoNumerico_1 CAMPONUMERICO_1, usuario.CampoNumerico_2 CAMPONUMERICO_2, usuario.CampoNumerico_3 CAMPONUMERICO_3," +
                    "Usuario.CampoDeTexto_1 CAMPOTEXTO_1, Usuario.CampoDeTexto_2 CAMPOTEXTO_2, Usuario.CampoDeTexto_3 CAMPOTEXTO_3," +
                    "Usuario.ClaveAcceso CVE_ACCESO, " +
                    " EstatusEncuestaD4U.Descripcion, ";
                try
                {
                    using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                    {
                        var getIdPreguntas = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = {0} ORDER BY IDPREGUNTA ASC", IdENcuesta).ToList();
                        if (getIdPreguntas != null)
                        {
                            int cont = 1;
                            foreach (var item in getIdPreguntas)
                            {
                                int idPregunta = item.IdPregunta;
                                IdsPregunta.Add(item.IdPregunta);
                                getValueRespuestas += "max(case when (Preguntas.IdPregunta = " + idPregunta + " AND TipoControl.IdTipoControl != 3 AND TipoControl.IdTipoControl != 12 AND UsuarioRespuestas.IdEstatus = 1) then RespuestaUsuario when Preguntas.IdPregunta = " + idPregunta + " AND TipoControl.IdTipoControl = 3 then 'MULTRES' when Preguntas.IdPregunta = " + idPregunta + " AND TipoControl.IdTipoControl = 12 THEN 'LIKERTDOBLE' end) EMP_RES,";
                                cont++;
                            }
                        }
                        string aux = getValueRespuestas.Remove(getValueRespuestas.Length - 1);
                        SqlQuery = SqlQuery + aux;
                        string finalQuery =
                            " FROM Usuario " +
                            "left JOIN UsuarioRespuestas ON UsuarioRespuestas.IdUsuario = Usuario.IdUsuario " +
                            "left JOIN Perfil ON Usuario.IdPerfil = Perfil.IdPerfil " +
                            "INNER JOIN TipoEstatus ON Usuario.IdEstatus = TipoEstatus.IdEstatus " +
                            "INNER JOIN BasesDeDatos ON Usuario.IdBaseDeDatos = BasesDeDatos.IdBasesDeDatos " +
                            "INNER JOIN Encuesta ON BasesDeDatos.IdBasesDeDatos = Encuesta.IdBasesDeDatos " +
                            "left join Preguntas on UsuarioRespuestas.IdPregunta = Preguntas.IdPregunta " +
                            "left join TipoControl on Preguntas.IdTipoControl = TipoControl.IdTipoControl " +
                            "LEFT JOIN UsuarioEstatusEncuesta ON Usuario.IdUsuario = UsuarioEstatusEncuesta.IdUsuario and UsuarioEstatusEncuesta.IdEncuesta = Encuesta.IdEncuesta " +
                            "LEFT JOIN EstatusEncuestaD4U ON UsuarioEstatusEncuesta.IdEstatusEncuestaD4U = EstatusEncuestaD4U.IdEstatusEncuestaD4U " +
                            "WHERE Encuesta.IdEncuesta = " + IdENcuesta + " AND USUARIO.IDESTATUS != 6 " +
                            "GROUP BY Usuario.IdUsuario, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, " +
                            "Usuario.Nombre, Usuario.Puesto ,Usuario.FechaNacimiento, " +
                            "Usuario.FechaAntiguedad, Usuario.Sexo, Usuario.Email, " +
                            "Usuario.TipoFuncion, Usuario.CondicionTrabajo, Usuario.GradoAcademico, " +
                            "Usuario.EmpresaContratante, " +
                            "Usuario.IdResponsableRH, Usuario.NombreResponsableRH, Usuario.IdJefe, " +
                            "Usuario.NombreJefe, PuestoJefe, Usuario.IdResponsableEstructura, " +
                            "Usuario.NombreResponsableEstructura, Usuario.ClaveAcceso, " +
                            "Usuario.RangoAntiguedad, Usuario.RangoEdad, " +
                            "Usuario.UnidadNegocio, Usuario.DivisionMarca, Usuario.AreaAgencia, Usuario.Departamento, Usuario.Subdepartamento, " +
                            "TipoEstatus.Descripcion, UsuarioRespuestas.IdUsuario, " +
                            "Usuario.CampoNumerico_1, Usuario.CampoNumerico_2, Usuario.CampoNumerico_3, " +
                            "Usuario.CampoDeTexto_1, Usuario.CampoDeTexto_2, Usuario.CampoDeTexto_3, EstatusEncuestaD4U.Descripcion " +
                            "ORDER BY Usuario.IdUsuario ASC";
                        SqlQuery = SqlQuery + finalQuery;
                    }
                    string cadenaConexion = "";
                    string urlFinal = url.Substring(0, 22);
                    if (urlFinal == "http://demo.climalabor" || urlFinal == "http://localhost:11124")
                    {
                        cadenaConexion = "Data Source=192.192.192.97;Initial Catalog=RH_Des;User ID=sa;Password=Pa$$w0rd01;";
                    }
                    else
                    {
                        cadenaConexion = "Data Source=10.5.2.108;Initial Catalog=RH_Diagnostics;User ID=RHDiagnostics;Password=Pasword01;";
                    }
                    //Agrego al nuevo objeto tal como viene
                    //DatosFinales.Tables[0].Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                    {
                        try
                        {
                            connection.Open();
                            SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(SqlQuery, connection);
                            dat_1.Fill(ds, "dat_1");
                            int Numcolumnas = ds.Tables[0].Columns.Count;
                            for (int col = 0; col < Numcolumnas; col++)
                            {
                                string NombreCol = "Columna " + col;
                                DatosFinales.Tables[0].Columns.Add(NombreCol);
                            }

                            object[] datos = { };
                            object[] fromData = { };
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                fromData = ds.Tables[0].Rows[i].ItemArray;
                                for (int j = 0; j < ds.Tables[0].Rows[0].ItemArray.Count(); j++)
                                {
                                    if (ds.Tables[0].Rows[i].ItemArray[j].ToString() == "MULTRES")
                                    {
                                        using (DL.RH_DesEntities context_ = new DL.RH_DesEntities())
                                        {
                                            int index = BL.Reporte.getIndexListIdPregunta(j);
                                            var query = context_.UsuarioRespuestas.SqlQuery("SELECT * FROM UsuarioRespuestas WHERE IdEncuesta = {0} AND IdUsuario = {1} and idpregunta = {2} and idestatus = 1", IdENcuesta, ds.Tables[0].Rows[i].ItemArray[0], IdsPregunta[index]).ToList();
                                            if (query != null)
                                            {
                                                Respuestas = "";
                                                foreach (var item in query)
                                                {
                                                    Respuestas += item.RespuestaUsuario + " ";
                                                }
                                                datos = fromData;
                                                datos[j] = Respuestas;
                                            }
                                        }
                                    }
                                    else if (ds.Tables[0].Rows[i].ItemArray[j].ToString() == "LIKERTDOBLE")
                                    {
                                        using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                                        {
                                            Respuestas = "";
                                            string PreguntaLikert = "";
                                            int index = BL.Reporte.getIndexListIdPregunta(j);
                                            var getRespuestasLikert = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS inner join Respuestas on UsuarioRespuestas.IdRespuesta = Respuestas.IdRespuesta inner join PreguntasLikert on Respuestas.IdPreguntaLikertD = PreguntasLikert.idPreguntasLikert WHERE usuariorespuestas.IDPREGUNTA = {0} AND UsuarioRespuestas.IdUsuario = {1} AND UsuarioRespuestas.IdEstatus = 1 order by Respuestas.IdPreguntaLikertD", IdsPregunta[index], ds.Tables[0].Rows[i].ItemArray[0]).ToList();

                                            if (getRespuestasLikert != null)
                                            {
                                                foreach (var item in getRespuestasLikert)
                                                {
                                                    ML.UsuarioRespuestas userResp = new ML.UsuarioRespuestas();
                                                    userResp.Respuestas = new ML.Respuestas();
                                                    userResp.RespuestaUsuario = item.RespuestaUsuario;
                                                    userResp.Respuestas.Respuesta = item.Respuestas.Respuesta;//ColA & ColB
                                                                                                              //getIdDe pregunta Likert
                                                    userResp.Respuestas.PreguntasLikert = new ML.PreguntasLikert();
                                                    userResp.Respuestas.PreguntasLikert.IdPreguntaLikert = Convert.ToInt32(item.Respuestas.IdPreguntaLikertD);
                                                    var getPreguntaLikert = context.PreguntasLikert.SqlQuery("SELECT * FROM PreguntasLikert WHERE idPreguntasLikert = {0}", userResp.Respuestas.PreguntasLikert.IdPreguntaLikert).ToList();
                                                    foreach (var obj in getPreguntaLikert)
                                                    {
                                                        PreguntaLikert = obj.Pregunta;
                                                    }

                                                    Respuestas += " >" + PreguntaLikert + " (" + userResp.Respuestas.Respuesta + ") " + userResp.RespuestaUsuario;
                                                }
                                                datos = fromData;
                                                datos[j] = Respuestas;
                                            }
                                        }
                                    }
                                }
                                DatosFinales.Tables[0].Rows.Add(fromData);
                            }
                            connection.Close();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                return DatosFinales;
            }
        }

        public static ML.Result GetReporteEncuestaIngreso()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EncuestaIngreso.SqlQuery("SELECT * FROM ENCUESTAINGRESO").ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.EncuestaIngreso encuesta = new ML.EncuestaIngreso();
                            ML.Persona persona = new ML.Persona();
                            encuesta.ID = item.ID;
                            encuesta.RFC = item.RFC;
                            encuesta.Correo = item.Correo;
                            encuesta.Empresa = item.Empresa;
                            encuesta.Area = item.Area;
                            encuesta.Departamento = item.Departamento;
                            encuesta.Puesto = item.Puesto;
                            encuesta.Contacto = item.Contacto;
                            encuesta.FechaContacto = Convert.ToDateTime(item.FechaContacto);

                            encuesta.Temperatura = item.Temperatura;
                            encuesta.Tos = item.Tos;
                            encuesta.Malestar = item.Malestar;
                            encuesta.MalestarGastrico = item.MalestarGastrico;
                            encuesta.DifRespirar = item.DifRespirar;
                            encuesta.Fecha = Convert.ToDateTime(item.Fecha);
                            //new
                            encuesta.NTemperatura = item.dTemperatura;
                            encuesta.DolorGarganta = item.DolorGarganta;
                            encuesta.EscurrimientoNasal = item.EscurrimientoNasal;
                            encuesta.DolorCuerpo = item.DolorCuerpo;
                            encuesta.Conjuntivitis = item.Conjuntivitis;
                            encuesta.Enfermedades = item.Enfermedades;
                            encuesta.AltGustoOlfato = item.AltGustOlfato;

                            double temperatura = Convert.ToDouble(encuesta.NTemperatura);
                            if (encuesta.Contacto == "SI" || encuesta.Temperatura == "SI" || 
                                encuesta.Tos == "SI" || encuesta.Malestar == "SI" ||
                                encuesta.DifRespirar == "SI" ||
                                temperatura > 37.5 || encuesta.DolorGarganta == "SI" || encuesta.EscurrimientoNasal == "SI" ||
                                encuesta.DolorCuerpo == "SI" || encuesta.Conjuntivitis == "SI" ||
                                encuesta.AltGustoOlfato == "SI")
                            {
                                encuesta.resultAcceso = "Servicio Médico";
                            }else
                            {
                                encuesta.resultAcceso = "Acceso Permitido";
                            }

                            //Consumir web service
                            if (encuesta.RFC.Length == 10)
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.5.2.21:8882/apiEmpleadoRH/api/empleados/empleadovigente/" + encuesta.RFC.ToUpper().Trim());
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "GET";

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                                {
                                    var resultWS = streamReader.ReadToEnd();
                                    if (!string.Equals(resultWS, "\"No se encontró información\""))
                                    {
                                        var Resultados = JsonConvert.DeserializeObject<List<ML.Persona>>(resultWS);
                                        if (Resultados.Count > 0)
                                        {
                                            persona.IdRh = Resultados[0].IdRh;
                                            persona.Nombre = string.IsNullOrEmpty(Resultados[0].Nombre) ? "GAM" : Resultados[0].Nombre;
                                            persona.ApellidoPaterno = string.IsNullOrEmpty(Resultados[0].ApellidoPaterno) ? "GAM" : Resultados[0].ApellidoPaterno;
                                            persona.ApellidoMaterno = string.IsNullOrEmpty(Resultados[0].ApellidoMaterno) ? "GAM" : Resultados[0].ApellidoMaterno;
                                            persona.LugarTrabajo = string.IsNullOrEmpty(Resultados[0].LugarDeTrabajo) ? "GAM" : Resultados[0].LugarDeTrabajo;
                                        }
                                        else
                                        {
                                            persona.IdRh = 12;
                                            persona.Nombre = "";
                                            persona.ApellidoPaterno = "";
                                            persona.ApellidoMaterno = "";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                persona.IdRh = 0;
                                persona.Nombre = "";
                                persona.ApellidoPaterno = "";
                                persona.ApellidoMaterno = "";
                            }
                            //end cosnumir
                            encuesta.IdRH = persona.IdRh;
                            encuesta.Nombre = persona.Nombre;
                            encuesta.ApellidoPaterno = persona.ApellidoPaterno;
                            encuesta.ApellidoMaterno = persona.ApellidoMaterno;
                            encuesta.LugarTrabajo = persona.LugarTrabajo;


                            result.Objects.Add(encuesta);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetReporteEncuestaIngresoByFilter(ML.EncuestaIngreso enc)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //case 1://Fecha
                    //case 2://Servicio medico
                    //case 3://tempe
                    //case 4://empres > 37.5
                    string NameFilter = "";
                    switch (enc.Filtro)
                    {
                        case 1: NameFilter = "Fecha"; break;
                        case 3: NameFilter = "dTemperatura"; break;
                        case 4: NameFilter = "Empresa"; break;
                    }
                    var query = new List<DL.EncuestaIngreso>();
                    if (enc.Filtro == 3)
                    {
                        if (enc.ValueFilter == "> 37.5")
                        {
                            enc.ValueFilter = ">= 37.5";
                        }
                        string SQLQUery = "SELECT * FROM ENCUESTAINGRESO WHERE DTEMPERATURA " + enc.ValueFilter;
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                        {
                            try
                            {
                                connection.Open();
                                SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(SQLQUery, connection);
                                dat_1.Fill(ds, "dat_1");
                                connection.Close();
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        //Itera ds
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            //< td > @Model.DataSet.Tables[0].Rows[i].ItemArray[j] </ td >
                            ML.EncuestaIngreso encuesta = new ML.EncuestaIngreso();
                            ML.Persona persona = new ML.Persona();
                            encuesta.ID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                            encuesta.RFC = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                            encuesta.Correo = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                            encuesta.Empresa = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                            encuesta.Area = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                            encuesta.Departamento = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                            encuesta.Puesto = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                            encuesta.Contacto = ds.Tables[0].Rows[i].ItemArray[7].ToString();


                            encuesta.stringFechaContacto = (ds.Tables[0].Rows[i].ItemArray[8].ToString());




                            encuesta.Temperatura = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                            encuesta.Tos = ds.Tables[0].Rows[i].ItemArray[10].ToString();
                            encuesta.Malestar = ds.Tables[0].Rows[i].ItemArray[11].ToString();
                            encuesta.MalestarGastrico = ds.Tables[0].Rows[i].ItemArray[12].ToString();
                            encuesta.DifRespirar = ds.Tables[0].Rows[i].ItemArray[13].ToString();
                            encuesta.stringFecha = (ds.Tables[0].Rows[i].ItemArray[14].ToString());

                            //new
                            encuesta.NTemperatura = ds.Tables[0].Rows[i].ItemArray[15].ToString();
                            if (encuesta.NTemperatura == "")
                            {
                                encuesta.NTemperatura = "0";
                            }

                            try
                            {
                                encuesta.DolorGarganta = ds.Tables[0].Rows[i].ItemArray[16].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.DolorGarganta = "";
                            }
                            try
                            {
                                encuesta.EscurrimientoNasal = ds.Tables[0].Rows[i].ItemArray[17].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.EscurrimientoNasal = "";
                            }
                            try
                            {
                                encuesta.DolorCuerpo = ds.Tables[0].Rows[i].ItemArray[18].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.DolorCuerpo = "";
                            }
                            try
                            {
                                encuesta.Conjuntivitis = ds.Tables[0].Rows[i].ItemArray[19].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.Conjuntivitis = "";
                            }
                            try
                            {
                                encuesta.Enfermedades = ds.Tables[0].Rows[i].ItemArray[20].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.Enfermedades = "";
                            }
                            try
                            {
                                encuesta.AltGustoOlfato = ds.Tables[0].Rows[i].ItemArray[21].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.AltGustoOlfato = "";
                            }





                            double temperatura = Convert.ToDouble(encuesta.NTemperatura);
                            if (encuesta.Contacto == "SI" || encuesta.Temperatura == "SI" ||
                                encuesta.Tos == "SI" || encuesta.Malestar == "SI" ||
                                encuesta.DifRespirar == "SI" ||
                                temperatura > 37.5 || encuesta.DolorGarganta == "SI" || encuesta.EscurrimientoNasal == "SI" ||
                                encuesta.DolorCuerpo == "SI" || encuesta.Conjuntivitis == "SI" ||
                                encuesta.AltGustoOlfato == "SI")
                            {
                                encuesta.resultAcceso = "Servicio Médico";
                            }
                            else
                            {
                                encuesta.resultAcceso = "Acceso Permitido";
                            }

                            //Consumir web service
                            if (encuesta.RFC.Length == 10)
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.5.2.21:8882/apiEmpleadoRH/api/empleados/empleadovigente/" + encuesta.RFC.ToUpper().Trim());
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "GET";
                                
                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                                {
                                    var resultWS = streamReader.ReadToEnd();
                                    if (!string.Equals(resultWS, "\"No se encontró información\""))
                                    {
                                        var Resultados = JsonConvert.DeserializeObject<List<ML.Persona>>(resultWS);
                                        if (Resultados.Count > 0)
                                        {
                                            persona.IdRh = Resultados[0].IdRh;
                                            persona.Nombre = string.IsNullOrEmpty(Resultados[0].Nombre) ? "GAM" : Resultados[0].Nombre;
                                            persona.ApellidoPaterno = string.IsNullOrEmpty(Resultados[0].ApellidoPaterno) ? "GAM" : Resultados[0].ApellidoPaterno;
                                            persona.ApellidoMaterno = string.IsNullOrEmpty(Resultados[0].ApellidoMaterno) ? "GAM" : Resultados[0].ApellidoMaterno;
                                            persona.LugarTrabajo = string.IsNullOrEmpty(Resultados[0].LugarDeTrabajo) ? "GAM" : Resultados[0].LugarDeTrabajo;
                                        }
                                        else
                                        {
                                            persona.IdRh = 12;
                                            persona.Nombre = "";
                                            persona.ApellidoPaterno = "";
                                            persona.ApellidoMaterno = "";
                                        }
                                    }
                                    else
                                    {
                                        persona.IdRh = 0;
                                        persona.Nombre = "";
                                        persona.ApellidoPaterno = "";
                                        persona.ApellidoMaterno = "";
                                    }
                                }
                            }
                            else
                            {
                                persona.IdRh = 0;
                                persona.Nombre = "";
                                persona.ApellidoPaterno = "";
                                persona.ApellidoMaterno = "";
                            }
                            //end cosnumir
                            encuesta.IdRH = persona.IdRh;
                            encuesta.Nombre = persona.Nombre;
                            encuesta.ApellidoPaterno = persona.ApellidoPaterno;
                            encuesta.ApellidoMaterno = persona.ApellidoMaterno;
                            encuesta.LugarTrabajo = persona.LugarTrabajo;


                            result.Objects.Add(encuesta);
                            result.Correct = true;




                        }
                    }
                    if (enc.Filtro == 1)
                    {
                        string stringQuery = "SELECT * FROM ENCUESTAINGRESO WHERE CONVERT(VARCHAR(25), " + NameFilter + ", 126) LIKE '" + enc.ValueFilter + "%'";
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                        {
                            try
                            {
                                connection.Open();
                                SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(stringQuery, connection);
                                dat_1.Fill(ds, "dat_1");
                                connection.Close();
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        //Itera ds
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            
                                //< td > @Model.DataSet.Tables[0].Rows[i].ItemArray[j] </ td >
                                ML.EncuestaIngreso encuesta = new ML.EncuestaIngreso();
                                ML.Persona persona = new ML.Persona();
                                encuesta.ID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                                encuesta.RFC = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                                encuesta.Correo = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                                encuesta.Empresa = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                                encuesta.Area = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                                encuesta.Departamento = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                                encuesta.Puesto = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                                encuesta.Contacto = ds.Tables[0].Rows[i].ItemArray[7].ToString();

                                
                                encuesta.stringFechaContacto = (ds.Tables[0].Rows[i].ItemArray[8].ToString());
                               
                               
                                

                                encuesta.Temperatura = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                                encuesta.Tos = ds.Tables[0].Rows[i].ItemArray[10].ToString();
                                encuesta.Malestar = ds.Tables[0].Rows[i].ItemArray[11].ToString();
                                encuesta.MalestarGastrico = ds.Tables[0].Rows[i].ItemArray[12].ToString();
                                encuesta.DifRespirar = ds.Tables[0].Rows[i].ItemArray[13].ToString();
                                encuesta.stringFecha = (ds.Tables[0].Rows[i].ItemArray[14].ToString());

                                //new
                                encuesta.NTemperatura = ds.Tables[0].Rows[i].ItemArray[15].ToString();
                                if (encuesta.NTemperatura == "")
                                {
                                    encuesta.NTemperatura = "0";
                                }

                                try
                                {
                                    encuesta.DolorGarganta = ds.Tables[0].Rows[i].ItemArray[16].ToString();
                                }
                                catch (Exception ex)
                                {
                                    encuesta.DolorGarganta = "";
                                }
                                try
                                {
                                    encuesta.EscurrimientoNasal = ds.Tables[0].Rows[i].ItemArray[17].ToString();
                                }
                                catch (Exception ex)
                                {
                                    encuesta.EscurrimientoNasal = "";
                                }
                                try
                                {
                                    encuesta.DolorCuerpo = ds.Tables[0].Rows[i].ItemArray[18].ToString();
                                }
                                catch (Exception ex)
                                {
                                    encuesta.DolorCuerpo = "";
                                }
                                try
                                {
                                    encuesta.Conjuntivitis = ds.Tables[0].Rows[i].ItemArray[19].ToString();
                                }
                                catch (Exception ex)
                                {
                                    encuesta.Conjuntivitis = "";
                                }
                                try
                                {
                                    encuesta.Enfermedades = ds.Tables[0].Rows[i].ItemArray[20].ToString();
                                }
                                catch (Exception ex)
                                {
                                    encuesta.Enfermedades = "";
                                }
                                try
                                {
                                    encuesta.AltGustoOlfato = ds.Tables[0].Rows[i].ItemArray[21].ToString();
                                }
                                catch (Exception ex)
                                {
                                    encuesta.AltGustoOlfato = "";
                                }
                                
                                
                                
                                

                                double temperatura = Convert.ToDouble(encuesta.NTemperatura);
                                if (encuesta.Contacto == "SI" || encuesta.Temperatura == "SI" ||
                                    encuesta.Tos == "SI" || encuesta.Malestar == "SI" ||
                                    encuesta.DifRespirar == "SI" ||
                                    temperatura > 37.5 || encuesta.DolorGarganta == "SI" || encuesta.EscurrimientoNasal == "SI" ||
                                    encuesta.DolorCuerpo == "SI" || encuesta.Conjuntivitis == "SI" ||
                                    encuesta.AltGustoOlfato == "SI")
                                {
                                    encuesta.resultAcceso = "Servicio Médico";
                                }
                                else
                                {
                                    encuesta.resultAcceso = "Acceso Permitido";
                                }

                                //Consumir web service
                                if (encuesta.RFC.Length == 10)
                                {
                                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.5.2.21:8882/apiEmpleadoRH/api/empleados/empleadovigente/" + encuesta.RFC.ToUpper().Trim());
                                    httpWebRequest.ContentType = "application/json";
                                    httpWebRequest.Method = "GET";

                                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                                    {
                                        var resultWS = streamReader.ReadToEnd();
                                        if (!string.Equals(resultWS, "\"No se encontró información\""))
                                        {
                                            var Resultados = JsonConvert.DeserializeObject<List<ML.Persona>>(resultWS);
                                            if (Resultados.Count > 0)
                                            {
                                                persona.IdRh = Resultados[0].IdRh;
                                                persona.Nombre = string.IsNullOrEmpty(Resultados[0].Nombre) ? "GAM" : Resultados[0].Nombre;
                                                persona.ApellidoPaterno = string.IsNullOrEmpty(Resultados[0].ApellidoPaterno) ? "GAM" : Resultados[0].ApellidoPaterno;
                                                persona.ApellidoMaterno = string.IsNullOrEmpty(Resultados[0].ApellidoMaterno) ? "GAM" : Resultados[0].ApellidoMaterno;
                                                persona.LugarTrabajo = string.IsNullOrEmpty(Resultados[0].LugarDeTrabajo) ? "GAM" : Resultados[0].LugarDeTrabajo;
                                            }
                                            else
                                            {
                                                persona.IdRh = 12;
                                                persona.Nombre = "";
                                                persona.ApellidoPaterno = "";
                                                persona.ApellidoMaterno = "";
                                            }
                                        }
                                    else
                                    {
                                        persona.IdRh = 0;
                                        persona.Nombre = "";
                                        persona.ApellidoPaterno = "";
                                        persona.ApellidoMaterno = "";
                                    }
                                    }
                                }
                                else
                                {
                                    persona.IdRh = 0;
                                    persona.Nombre = "";
                                    persona.ApellidoPaterno = "";
                                    persona.ApellidoMaterno = "";
                                }
                                //end cosnumir
                                encuesta.IdRH = persona.IdRh;
                                encuesta.Nombre = persona.Nombre;
                                encuesta.ApellidoPaterno = persona.ApellidoPaterno;
                                encuesta.ApellidoMaterno = persona.ApellidoMaterno;
                                encuesta.LugarTrabajo = persona.LugarTrabajo;


                                result.Objects.Add(encuesta);
                                result.Correct = true;
                            

                        

                    }
                    }
                    if (enc.Filtro == 0)
                    {
                        enc.ValueFilter = DateTime.Now.ToString("yyyy-MM-dd");
                        string stringQuery = "SELECT * FROM ENCUESTAINGRESO WHERE CONVERT(VARCHAR(25), Fecha, 126) LIKE '" + enc.ValueFilter + "%'";
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                        {
                            try
                            {
                                connection.Open();
                                SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(stringQuery, connection);
                                dat_1.Fill(ds, "dat_1");
                                connection.Close();
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        //Itera ds
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            //< td > @Model.DataSet.Tables[0].Rows[i].ItemArray[j] </ td >
                            ML.EncuestaIngreso encuesta = new ML.EncuestaIngreso();
                            ML.Persona persona = new ML.Persona();
                            encuesta.ID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                            encuesta.RFC = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                            encuesta.Correo = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                            encuesta.Empresa = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                            encuesta.Area = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                            encuesta.Departamento = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                            encuesta.Puesto = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                            encuesta.Contacto = ds.Tables[0].Rows[i].ItemArray[7].ToString();


                            encuesta.stringFechaContacto = (ds.Tables[0].Rows[i].ItemArray[8].ToString());




                            encuesta.Temperatura = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                            encuesta.Tos = ds.Tables[0].Rows[i].ItemArray[10].ToString();
                            encuesta.Malestar = ds.Tables[0].Rows[i].ItemArray[11].ToString();
                            encuesta.MalestarGastrico = ds.Tables[0].Rows[i].ItemArray[12].ToString();
                            encuesta.DifRespirar = ds.Tables[0].Rows[i].ItemArray[13].ToString();
                            encuesta.stringFecha = (ds.Tables[0].Rows[i].ItemArray[14].ToString());

                            //new
                            encuesta.NTemperatura = ds.Tables[0].Rows[i].ItemArray[15].ToString();
                            if (encuesta.NTemperatura == "")
                            {
                                encuesta.NTemperatura = "0";
                            }

                            try
                            {
                                encuesta.DolorGarganta = ds.Tables[0].Rows[i].ItemArray[16].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.DolorGarganta = "";
                            }
                            try
                            {
                                encuesta.EscurrimientoNasal = ds.Tables[0].Rows[i].ItemArray[17].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.EscurrimientoNasal = "";
                            }
                            try
                            {
                                encuesta.DolorCuerpo = ds.Tables[0].Rows[i].ItemArray[18].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.DolorCuerpo = "";
                            }
                            try
                            {
                                encuesta.Conjuntivitis = ds.Tables[0].Rows[i].ItemArray[19].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.Conjuntivitis = "";
                            }
                            try
                            {
                                encuesta.Enfermedades = ds.Tables[0].Rows[i].ItemArray[20].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.Enfermedades = "";
                            }
                            try
                            {
                                encuesta.AltGustoOlfato = ds.Tables[0].Rows[i].ItemArray[21].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.AltGustoOlfato = "";
                            }





                            double temperatura = Convert.ToDouble(encuesta.NTemperatura);
                            if (encuesta.Contacto == "SI" || encuesta.Temperatura == "SI" ||
                                encuesta.Tos == "SI" || encuesta.Malestar == "SI" ||
                                encuesta.DifRespirar == "SI" ||
                                temperatura > 37.5 || encuesta.DolorGarganta == "SI" || encuesta.EscurrimientoNasal == "SI" ||
                                encuesta.DolorCuerpo == "SI" || encuesta.Conjuntivitis == "SI" ||
                                encuesta.AltGustoOlfato == "SI")
                            {
                                encuesta.resultAcceso = "Servicio Médico";
                            }
                            else
                            {
                                encuesta.resultAcceso = "Acceso Permitido";
                            }

                            //Consumir web service
                            if (encuesta.RFC.Length == 10)
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.5.2.21:8882/apiEmpleadoRH/api/empleados/empleadovigente/" + encuesta.RFC.ToUpper().Trim());
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "GET";

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                                {
                                    var resultWS = streamReader.ReadToEnd();
                                    if (!string.Equals(resultWS, "\"No se encontró información\""))
                                    {
                                        var Resultados = JsonConvert.DeserializeObject<List<ML.Persona>>(resultWS);
                                        if (Resultados.Count > 0)
                                        {
                                            persona.IdRh = Resultados[0].IdRh;
                                            persona.Nombre = string.IsNullOrEmpty(Resultados[0].Nombre) ? "GAM" : Resultados[0].Nombre;
                                            persona.ApellidoPaterno = string.IsNullOrEmpty(Resultados[0].ApellidoPaterno) ? "GAM" : Resultados[0].ApellidoPaterno;
                                            persona.ApellidoMaterno = string.IsNullOrEmpty(Resultados[0].ApellidoMaterno) ? "GAM" : Resultados[0].ApellidoMaterno;
                                            persona.LugarTrabajo = string.IsNullOrEmpty(Resultados[0].LugarDeTrabajo) ? "GAM" : Resultados[0].LugarDeTrabajo;
                                        }
                                        else
                                        {
                                            persona.IdRh = 12;
                                            persona.Nombre = "";
                                            persona.ApellidoPaterno = "";
                                            persona.ApellidoMaterno = "";
                                        }
                                    }
                                    else
                                    {
                                        persona.IdRh = 0;
                                        persona.Nombre = "";
                                        persona.ApellidoPaterno = "";
                                        persona.ApellidoMaterno = "";
                                    }
                                }
                            }
                            else
                            {
                                persona.IdRh = 0;
                                persona.Nombre = "";
                                persona.ApellidoPaterno = "";
                                persona.ApellidoMaterno = "";
                            }
                            //end cosnumir
                            encuesta.IdRH = persona.IdRh;
                            encuesta.Nombre = persona.Nombre;
                            encuesta.ApellidoPaterno = persona.ApellidoPaterno;
                            encuesta.ApellidoMaterno = persona.ApellidoMaterno;
                            encuesta.LugarTrabajo = persona.LugarTrabajo;


                            result.Objects.Add(encuesta);
                            result.Correct = true;




                        }
                    }
                    else
                    {
                        string QUERY =  "SELECT * FROM ENCUESTAINGRESO WHERE " + NameFilter + " = '" + enc.ValueFilter + "'";
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                        {
                            try
                            {
                                connection.Open();
                                SqlDataAdapter dat_1 = new System.Data.SqlClient.SqlDataAdapter(QUERY, connection);
                                dat_1.Fill(ds, "dat_1");
                                connection.Close();
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        //Itera ds
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            //< td > @Model.DataSet.Tables[0].Rows[i].ItemArray[j] </ td >
                            ML.EncuestaIngreso encuesta = new ML.EncuestaIngreso();
                            ML.Persona persona = new ML.Persona();
                            encuesta.ID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                            encuesta.RFC = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                            encuesta.Correo = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                            encuesta.Empresa = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                            encuesta.Area = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                            encuesta.Departamento = ds.Tables[0].Rows[i].ItemArray[5].ToString();
                            encuesta.Puesto = ds.Tables[0].Rows[i].ItemArray[6].ToString();
                            encuesta.Contacto = ds.Tables[0].Rows[i].ItemArray[7].ToString();


                            encuesta.stringFechaContacto = (ds.Tables[0].Rows[i].ItemArray[8].ToString());




                            encuesta.Temperatura = ds.Tables[0].Rows[i].ItemArray[9].ToString();
                            encuesta.Tos = ds.Tables[0].Rows[i].ItemArray[10].ToString();
                            encuesta.Malestar = ds.Tables[0].Rows[i].ItemArray[11].ToString();
                            encuesta.MalestarGastrico = ds.Tables[0].Rows[i].ItemArray[12].ToString();
                            encuesta.DifRespirar = ds.Tables[0].Rows[i].ItemArray[13].ToString();
                            encuesta.stringFecha = (ds.Tables[0].Rows[i].ItemArray[14].ToString());

                            //new
                            encuesta.NTemperatura = ds.Tables[0].Rows[i].ItemArray[15].ToString();
                            if (encuesta.NTemperatura == "")
                            {
                                encuesta.NTemperatura = "0";
                            }

                            try
                            {
                                encuesta.DolorGarganta = ds.Tables[0].Rows[i].ItemArray[16].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.DolorGarganta = "";
                            }
                            try
                            {
                                encuesta.EscurrimientoNasal = ds.Tables[0].Rows[i].ItemArray[17].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.EscurrimientoNasal = "";
                            }
                            try
                            {
                                encuesta.DolorCuerpo = ds.Tables[0].Rows[i].ItemArray[18].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.DolorCuerpo = "";
                            }
                            try
                            {
                                encuesta.Conjuntivitis = ds.Tables[0].Rows[i].ItemArray[19].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.Conjuntivitis = "";
                            }
                            try
                            {
                                encuesta.Enfermedades = ds.Tables[0].Rows[i].ItemArray[20].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.Enfermedades = "";
                            }
                            try
                            {
                                encuesta.AltGustoOlfato = ds.Tables[0].Rows[i].ItemArray[21].ToString();
                            }
                            catch (Exception ex)
                            {
                                encuesta.AltGustoOlfato = "";
                            }





                            double temperatura = Convert.ToDouble(encuesta.NTemperatura);
                            if (encuesta.Contacto == "SI" || encuesta.Temperatura == "SI" ||
                                encuesta.Tos == "SI" || encuesta.Malestar == "SI" ||
                                encuesta.DifRespirar == "SI" ||
                                temperatura > 37.5 || encuesta.DolorGarganta == "SI" || encuesta.EscurrimientoNasal == "SI" ||
                                encuesta.DolorCuerpo == "SI" || encuesta.Conjuntivitis == "SI" ||
                                encuesta.AltGustoOlfato == "SI")
                            {
                                encuesta.resultAcceso = "Servicio Médico";
                            }
                            else
                            {
                                encuesta.resultAcceso = "Acceso Permitido";
                            }

                            //Consumir web service
                            if (encuesta.RFC.Length == 10)
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://10.5.2.21:8882/apiEmpleadoRH/api/empleados/empleadovigente/" + encuesta.RFC.ToUpper().Trim());
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = "GET";

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                                {
                                    var resultWS = streamReader.ReadToEnd();
                                    if (!string.Equals(resultWS, "\"No se encontró información\""))
                                    {
                                        var Resultados = JsonConvert.DeserializeObject<List<ML.Persona>>(resultWS);
                                        if (Resultados.Count > 0)
                                        {
                                            persona.IdRh = Resultados[0].IdRh;
                                            persona.Nombre = string.IsNullOrEmpty(Resultados[0].Nombre) ? "GAM" : Resultados[0].Nombre;
                                            persona.ApellidoPaterno = string.IsNullOrEmpty(Resultados[0].ApellidoPaterno) ? "GAM" : Resultados[0].ApellidoPaterno;
                                            persona.ApellidoMaterno = string.IsNullOrEmpty(Resultados[0].ApellidoMaterno) ? "GAM" : Resultados[0].ApellidoMaterno;
                                            persona.LugarTrabajo = string.IsNullOrEmpty(Resultados[0].LugarDeTrabajo) ? "GAM" : Resultados[0].LugarDeTrabajo;
                                        }
                                        else
                                        {
                                            persona.IdRh = 12;
                                            persona.Nombre = "";
                                            persona.ApellidoPaterno = "";
                                            persona.ApellidoMaterno = "";
                                        }
                                    }
                                    else
                                    {
                                        persona.IdRh = 0;
                                        persona.Nombre = "";
                                        persona.ApellidoPaterno = "";
                                        persona.ApellidoMaterno = "";
                                    }
                                }
                            }
                            else
                            {
                                persona.IdRh = 0;
                                persona.Nombre = "";
                                persona.ApellidoPaterno = "";
                                persona.ApellidoMaterno = "";
                            }
                            //end cosnumir
                            encuesta.IdRH = persona.IdRh;
                            encuesta.Nombre = persona.Nombre;
                            encuesta.ApellidoPaterno = persona.ApellidoPaterno;
                            encuesta.ApellidoMaterno = persona.ApellidoMaterno;
                            encuesta.LugarTrabajo = persona.LugarTrabajo;


                            result.Objects.Add(encuesta);
                            result.Correct = true;




                        }
                    }

                    

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                BL.NLogGeneratorFile.logError(ex, new StackTrace());
            }
            return result;
        }
        //Secciones
        public static ML.Result ConfiguraSecciones(List<int> listaIdPregunta, List<int> listaSecciones, List<string> listaEncabezados)
        {
            //listaIdPregunta
            //listaSecciones
            ML.Result result = new ML.Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < listaIdPregunta.Count; i++)
                        {
                            //GetTitleSeccion
                            var title = GetSeccionForTitle(listaSecciones[i], listaEncabezados);

                            

                            var query = context.Database.ExecuteSqlCommand("UPDATE Preguntas SET Seccion = {0}, EncabezadoSeccion = {2} WHERE IdPregunta = {1}", listaSecciones[i], listaIdPregunta[i], title);

                            

                            context.SaveChanges();
                            result.Correct = true;
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }
        public static string GetSeccionForTitle(int seccion, List<string> listEncabezados)
        {
            var titulo = "";
            for (int i = 1; i < 10; i++)
            {
                if (seccion == i)
                {
                    string prefijo = "TS" + i;
                    listEncabezados = listEncabezados.Where(item => item.Contains(prefijo)).ToList();
                    titulo = listEncabezados[0];
                    break;
                }
            }


            titulo = titulo.Remove(0, 4);
            return titulo;
        }
        //SaveCnfon
        public static ML.Result SaveConfigClima(ML.ConfigClimaLab conf, string CURRENT_USER)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Si existe acualizare nuevas fechas
                        //Si no existe la inserto
                        var exists = context.ConfigClimaLab.SqlQuery("SELECT * FROM CONFIGCLIMALAB WHERE IDENCUESTA = {0} AND IDBASEDEDATOS = {1}", conf.Encuesta.IdEncuesta, conf.BaseDeDatos.IdBaseDeDatos).ToList();

                        if (exists.Count() > 0)//Si existe
                        {
                            var query = context.Database.ExecuteSqlCommand("UPDATE ConfigClimaLab SET FECHAINICIO = {0}, FECHAFIN = {1}, periodoAplicacion = {4} WHERE IDENCUESTA = {2} AND IDBASEDEDATOS = {3}",
                                conf.FechaInicio, conf.FechaFin, conf.Encuesta.IdEncuesta, conf.BaseDeDatos.IdBaseDeDatos, conf.PeriodoAplicacion);
                            context.SaveChanges();
                        }
                        else
                        {
                            //No existe
                            var query = context.Database.ExecuteSqlCommand
                                ("INSERT INTO ConfigClimaLab(IDENCUESTA, IDBASEDEDATOS, FECHAINICIO, FECHAFIN, FECHAHORACREACION, USUARIOCREACION, PROGRAMACREACION, periodoAplicacion) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                                conf.Encuesta.IdEncuesta, conf.BaseDeDatos.IdBaseDeDatos, conf.FechaInicio, conf.FechaFin, DateTime.Now, CURRENT_USER, "Diagnostic4U", conf.PeriodoAplicacion);
                            context.SaveChanges();
                        }
                        
                        
                        transaction.Commit();

                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }

        //EditEncuestaClimaLab
        public static ML.Result GetAllPeriodosConfig()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfigClimaLab.SqlQuery
                    ("SELECT * FROM ConfigClimaLab INNER JOIN Encuesta ON ConfigClimaLab.IdEncuesta = Encuesta.IdEncuesta INNER JOIN BasesDeDatos ON ConfigClimaLab.IdBaseDeDatos = BasesDeDatos.IdBasesDeDatos WHERE BasesDeDatos.IdEstatus = 1").ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfigClimaLab config = new ML.ConfigClimaLab();
                            config.BaseDeDatos = new ML.BasesDeDatos();
                            config.Encuesta = new ML.Encuesta();

                            config.IdConfigurtacion = item.IdConfiguracion;
                            config.BaseDeDatos.IdBaseDeDatos = item.BasesDeDatos.IdBasesDeDatos;
                            config.BaseDeDatos.Nombre = item.BasesDeDatos.Nombre;
                            config.Encuesta.IdEncuesta = item.Encuesta.IdEncuesta;
                            config.Encuesta.Nombre = item.Encuesta.Nombre;
                            config.InicioEncuesta = Convert.ToString(item.FechaInicio);
                            config.FinEncuesta = Convert.ToString(item.FechaFin);

                            result.Objects.Add(config);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result UpdatePeriodosClimaLab(ML.ConfigClimaLab conf)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var query = context.Database.ExecuteSqlCommand("UPDATE ConfigClimaLab SET FechaInicio = {0}, FechaFin = {1} WHERE IdConfiguracion = {2}",
                            conf.FechaInicio, conf.FechaFin, conf.IdConfigurtacion);

                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }
        public static ML.Result GetFechasByBD(ML.ConfigClimaLab conf)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //BaseDeDatos: { IdBaseDeDatos: IdBaseDeDatos
                    var query = context.ConfigClimaLab.SqlQuery("SELECT * FROM ConfigClimaLab WHERE IDBASEDEDATOS = {0}", conf.IdDatabase).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfigClimaLab config = new ML.ConfigClimaLab();

                            config.InicioEncuesta = Convert.ToString(item.FechaInicio);
                            config.FinEncuesta = Convert.ToString(item.FechaFin);

                            result.Objects.Add(config);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateEncuestaCL(ML.Encuesta encuesta, string currentUser)
        {
            ML.Result result = new ML.Result();

            encuesta.Instruccion = encuesta.Instrucciones1 + "*" + encuesta.Instrucciones2;

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //IdEncuesta	UID	Nombre	DosColumnas	FechaInicio	FechaFin	Descripcion	Instruccion	ImagenInstruccion	IdTipoEncuesta	IdEmpresa	Agradecimiento	ImagenAgradecimiento

                        var query = context.Database.ExecuteSqlCommand
                        ("UPDATE ENCUESTA SET Nombre = {0}, Descripcion = {1}, Instruccion = {2}, ImagenInstruccion = {3}, Agradecimiento = {4}, ImagenAgradecimiento = {5}, FechaHoraModificacion = {6}, UsuarioModificacion = {7}, ProgramaModificacion = {8} WHERE IDENCUESTA = 1",
                        encuesta.Nombre, encuesta.Descripcion, encuesta.Instruccion, encuesta.ImagenInstruccion, encuesta.Agradecimiento, encuesta.ImagenAgradecimiento, DateTime.Now, currentUser, "Diagnostic4U");


                        context.SaveChanges();
                        transaction.Commit();
                        result.Correct = true;
                    }
                    catch (Exception ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }

        public static ML.Result getClimaLaboralByIdEdit(int idEncuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.Agradecimiento = obj.Agradecimiento;
                            encuesta.Descripcion = obj.Descripcion;
                            encuesta.ImagenAgradecimiento = obj.ImagenAgradecimiento;
                            encuesta.ImagenInstruccion = obj.ImagenInstruccion;
                            encuesta.Instruccion = obj.Instruccion;
                            encuesta.Nombre = obj.Nombre;
                            
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }

        public static ML.Encuesta GetDataFromEncuesta(string IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            ML.Encuesta encuesta = new ML.Encuesta();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA WHERE IDENCUESTA = {0}", IdEncuesta).ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            encuesta.IdEncuesta = item.IdEncuesta;
                            encuesta.Nombre = item.Nombre;
                            encuesta.FechaInicio = item.FechaInicio;
                            encuesta.FechaFin = item.FechaFin;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return encuesta;
        }


        //Encuestas que no esten dos veces en portes
        public static ML.Result GetEncuestasForAddReporte(List<object> permisosEstrucura, string companyOfAdminloged)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    ML.Encuesta enc = new ML.Encuesta();

                    //Get CompanyIdForPermisos
                    List<string> CompanyIds = new List<string>();
                    string bodyQuery = "SELECT * FROM ENCUESTA INNER JOIN Administrador ON Encuesta.UsuarioCreacion = Administrador.IdAdministrador inner join Company on Administrador.CompanyId = Company.CompanyId WHERE (Encuesta.IdEstatus = 1 and IdEncuesta NOT IN (select IdEncuesta from EncuestaReporte) and company.COMPANYID = 0)  ";
                    string WHEREsql = "";
                    foreach (ML.AdministradorCompany item in permisosEstrucura)
                    {
                        string CompanyId = Convert.ToString(item.Company.CompanyId);
                        string CompanyIdFinal = " or (Encuesta.IdEstatus = 1 and IdEncuesta NOT IN (select IdEncuesta from EncuestaReporte) and company.COMPANYID = "+CompanyId+") ";//" OR company.COMPANYID = " + CompanyId + "";
                        WHEREsql += CompanyIdFinal;
                    }
                    Console.WriteLine(WHEREsql);

                    string finalQuery = bodyQuery + WHEREsql;

                    finalQuery = finalQuery + "  or (Encuesta.IdEstatus = 1 and IdEncuesta NOT IN (select IdEncuesta from EncuestaReporte) and company.COMPANYID = " + companyOfAdminloged + ")";

                    //var query = context.Encuesta.SqlQuery(finalQuery).ToList();
                    var query = context.Encuesta.SqlQuery(finalQuery);

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Encuesta Encuesta = new ML.Encuesta();

                            Encuesta.IdEncuesta = item.IdEncuesta;
                            Encuesta.Nombre = item.Nombre;
                            Encuesta.TipoEstatus = new ML.TipoEstatus();
                            Encuesta.TipoEstatus.IdEstatus = item.IdEstatus;

                            if (Encuesta.TipoEstatus.IdEstatus == 1)
                            {
                                result.Objects.Add(Encuesta);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static int GetnewIdUsuarioForEncuestaAnonima(int IdEncuesta)
        {
            List<int> Idsusuario = new List<int>();
            int NewIdusuario = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //var query = context.UsuarioRespuestas.Max(m => m.IdUsuario);
                    var query = context.Usuario.Min(M => M.IdUsuario);//Obtengo al primer usuario de todos
                    NewIdusuario = Convert.ToInt32(query);

                    var getusuariosfronIdEncuesta = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS WHERE IDENCUESTA = {0}", IdEncuesta).ToList();
                    if (getusuariosfronIdEncuesta.Count != 0)
                    {
                        foreach (var item in getusuariosfronIdEncuesta)
                        {
                            if (item.IdUsuario == NewIdusuario)
                            {
                                NewIdusuario = NewIdusuario + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NewIdusuario = 0;
            }
            return NewIdusuario;
        }

        public static ML.Result AddPreguntaTermina(ML.ConfiguraRespuesta conf)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO ConfiguraRespuesta (IDENCUESTA, IDPREGUNTA, IDRESPUESTA, TERMINAENCUESTA) VALUES ({0}, {1}, {2}, {3})", conf.IdEncuesta, conf.IdPregunta, conf.IdRespuesta, 1);
                    context.SaveChanges();

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result PreguntaHideNextSection(ML.ConfiguraRespuesta conf)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO ConfiguraRespuesta (IDENCUESTA, IDPREGUNTA, IDRESPUESTA, TERMINAENCUESTA) VALUES ({0}, {1}, {2}, {3})", conf.IdEncuesta, conf.IdPregunta, conf.IdRespuesta, 2);
                    context.SaveChanges();

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result PreguntaHideNextSubSeccion(ML.ConfiguraRespuesta conf)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("INSERT INTO ConfiguraRespuesta (IDENCUESTA, IDPREGUNTA, IDRESPUESTA, TERMINAENCUESTA) VALUES ({0}, {1}, {2}, {3})", conf.IdEncuesta, conf.IdPregunta, conf.IdRespuesta, 3);
                    context.SaveChanges();

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result DeleteTermina(ML.ConfiguraRespuesta conf)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("DELETE FROM ConfiguraRespuesta WHERE IDRESPUESTA = {0}", conf.IdRespuesta);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result DeletePreguntaOpen(ML.ConfiguraRespuesta conf)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("DELETE FROM ConfiguraRespuesta WHERE IDRESPUESTA = {0} and idpreguntaOpen = {1}", conf.IdRespuesta, conf.IdPreguntaOpen);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetRespuestaTermina(ML.Encuesta enc)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0}", enc.IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();
                            conf.IdEncuesta = (int)item.IdEncuesta;
                            conf.IdPregunta = (int)item.IdPregunta;
                            conf.IdRespuesta = (int)item.IdRespuesta;
                            conf.TerminaEncuesta = (int)item.TerminaEncuesta;

                            if (conf.TerminaEncuesta == 1)
                            {
                                result.Objects.Add(conf);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetRespuestaHideNextSection(ML.Encuesta enc)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("SELECT * FROM CONFIGURARESPUESTA WHERE IDENCUESTA = {0}", enc.IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();
                            conf.IdEncuesta = (int)item.IdEncuesta;
                            conf.IdPregunta = (int)item.IdPregunta;
                            conf.IdRespuesta = (int)item.IdRespuesta;
                            conf.TerminaEncuesta = (int)item.TerminaEncuesta;

                            if (conf.TerminaEncuesta == 2)
                            {
                                result.Objects.Add(conf);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetRespuestaHideNextSubSection(ML.Encuesta enc)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("SELECT * FROM CONFIGURARESPUESTA left join Preguntas on ConfiguraRespuesta.IdPregunta = Preguntas.IdPregunta  WHERE CONFIGURARESPUESTA.IDENCUESTA = {0} ", enc.IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();
                            conf.IdEncuesta = (int)item.IdEncuesta;
                            conf.IdPregunta = (int)item.IdPregunta;
                            conf.IdRespuesta = (int)item.IdRespuesta;
                            conf.TerminaEncuesta = item.TerminaEncuesta != null ? (int)item.TerminaEncuesta : 0;
                            conf.Preguntas = new ML.Preguntas();
                            conf.Preguntas.SubSeccion = (int) item.Preguntas.SubSeccion;

                            if (conf.TerminaEncuesta == 3)
                            {
                                result.Objects.Add(conf);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result getPeriodoByEncuesta(ML.Encuesta enc)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA WHERE IDENCUESTA = {0}", enc.IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.cadenaInicio = Convert.ToString(item.FechaInicio);
                            encuesta.cadenaFin = Convert.ToString(item.FechaFin);

                            result.Objects.Add(encuesta);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result GetMailsFaltantes()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EstatusEmail.SqlQuery("SELECT * FROM EstatusEmail WHERE IdEstatusMail = 1 or IdEstatusMail = 3 and noIntentos < 3").ToList();
                    BL.NLogGeneratorFile.logInfoEmailSender("Se encontraron: " + query.Count + " emails con envio fallido", 0, 0);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                            estatusEmail.IdEstatusEmail = item.IdEstatusEmail;
                            estatusEmail.Mensaje = item.Mensaje;
                            estatusEmail.Destinatario = item.Destinatario.Trim();
                            estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                            estatusEmail.BaseDeDatos.IdBaseDeDatos = item.IdBaseDeDatos;
                            estatusEmail.Encuesta = new ML.Encuesta();
                            estatusEmail.Encuesta.IdEncuesta = (int)item.IdEncuesta;
                            estatusEmail.EstatusMail = new ML.EstatusMail();
                            estatusEmail.EstatusMail.IdEstatusMail = Convert.ToInt32(item.IdEstatusMail);
                            estatusEmail.noIntentos = (int)item.noIntentos;
                            BL.NLogGeneratorFile.logInfoEmailSender("Email fallido: " + estatusEmail.Destinatario + " Intentos: " + estatusEmail.noIntentos, item.IdEncuesta, item.IdBaseDeDatos);
                            bool valid = isValidEmail(estatusEmail.Destinatario);
                            if (!String.IsNullOrEmpty(estatusEmail.Destinatario) && valid) // meter al cron solo emails validos
                                result.Objects.Add(estatusEmail);
                            else
                                BL.NLogGeneratorFile.logInfoEmailSender("El email con idEstatusEmail " + estatusEmail.IdEstatusEmail + " contiene un error, por favor verificalo", 0, 0);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static void sendEmail()
        {
            var result = GetMailsFaltantes();
            foreach (ML.EstatusEmail item in result.Objects)
            {
                var body = item.Mensaje;
                var message = new MailMessage();
                message.To.Add(new MailAddress(item.Destinatario));
                message.Subject = "Notificación Diagnostic4U";
                message.Body = string.Format(body, "DIAGNOSTIC4U", "", "");
                message.IsBodyHtml = true;
                
                using (var smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Send(message);
                        Encuesta.UpdateFlagEmailToSuccess(item, item.IdEstatusEmail);
                        BL.NLogGeneratorFile.logInfoEmailSender("Email enviado correctamente", item.Encuesta.IdEncuesta, item.BaseDeDatos.IdBaseDeDatos);
                    }
                    catch (SmtpException ex)
                    {
                        Console.Write(ex.Message);
                        Encuesta.UpdateFlagEmailToError(item, ex, item.IdEstatusEmail);
                        BL.NLogGeneratorFile.logInfoEmailSender(ex, item, 0);
                    }
                    finally
                    {
                        smtp.Dispose();
                    }
                }
            }
        }
        public static int AddToEstatusEmail(ML.EstatusEmail estatusEmail)
        {
            ML.Result result = new ML.Result();
            DL.EstatusEmail DLestatusEmail = new DL.EstatusEmail();
            DLestatusEmail.Mensaje = estatusEmail.Mensaje;
            DLestatusEmail.Destinatario = estatusEmail.Destinatario;
            DLestatusEmail.IdBaseDeDatos = estatusEmail.BaseDeDatos.IdBaseDeDatos;
            DLestatusEmail.FechaHoraCreacion = DateTime.Now;
            DLestatusEmail.UsuarioCreacion = "Reenvío de Mails";
            DLestatusEmail.ProgramaCreacion = "CRON Reenvío";
            DLestatusEmail.IdEstatusMail = 3;
            DLestatusEmail.MsgEnvio = "Email en espera a ser enviado";
            DLestatusEmail.IdEncuesta = estatusEmail.Encuesta.IdEncuesta;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EstatusEmail.Add(DLestatusEmail);
                    context.SaveChanges();
                    result.Correct = true;
                    var lastInsert = context.EstatusEmail.Max(m => m.IdEstatusEmail);
                    result.UltimoAdminInsertado = lastInsert;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result.UltimoAdminInsertado;
        }
        public static void AddFlagEmailToSuccess(ML.EstatusEmail estatusEmail)
        {
            ML.Result result = new ML.Result();
            DL.EstatusEmail DLestatusEmail = new DL.EstatusEmail();
            DLestatusEmail.Mensaje = estatusEmail.Mensaje;
            DLestatusEmail.Destinatario = estatusEmail.Destinatario;
            DLestatusEmail.IdBaseDeDatos = estatusEmail.BaseDeDatos.IdBaseDeDatos;
            DLestatusEmail.FechaHoraCreacion = DateTime.Now;
            DLestatusEmail.UsuarioCreacion = "Reenvío de Mails";
            DLestatusEmail.ProgramaCreacion = "CRON Reenvío";
            DLestatusEmail.IdEstatusMail = 2;
            DLestatusEmail.MsgEnvio = "Email enviado exitosamente";
            DLestatusEmail.IdEncuesta = estatusEmail.Encuesta.IdEncuesta;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EstatusEmail.Add(DLestatusEmail);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }
        public static void AddFlagEmailToError(ML.EstatusEmail estatusEmail, SmtpException excepcion)
        {
            ML.Result result = new ML.Result();
            string MensajeExcepcion = "Ocurrió un error al intentar enviar el email. " + excepcion.Message;
            DL.EstatusEmail DLestatusEmail = new DL.EstatusEmail();
            DLestatusEmail.Mensaje = estatusEmail.Mensaje;
            DLestatusEmail.Destinatario = estatusEmail.Destinatario;
            DLestatusEmail.IdBaseDeDatos = estatusEmail.BaseDeDatos.IdBaseDeDatos;
            DLestatusEmail.FechaHoraCreacion = DateTime.Now;
            DLestatusEmail.UsuarioCreacion = "Reenvío de Mails";
            DLestatusEmail.ProgramaCreacion = "CRON Reenvío";
            DLestatusEmail.IdEstatusMail = 1;
            DLestatusEmail.MsgEnvio = MensajeExcepcion;
            DLestatusEmail.IdEncuesta = estatusEmail.Encuesta.IdEncuesta;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.EstatusEmail.Add(DLestatusEmail);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }
        public static void UpdateFlagEmailToSuccess(ML.EstatusEmail estatusEmail, int IdEstatusEmail)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE EstatusEmail SET IDESTATUSMAIL = 2, MsgEnvio = 'Email enviado exitosamente', FechaHoraModificacion = {0}, UsuarioModificacion = 'Reenvío de Mails', ProgramaModificacion = 'CRON Reenvío' WHERE IDESTATUSEMAIL = {1}", DateTime.Now, IdEstatusEmail);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }
        public static void UpdateFlagEmailToSuccess(ML.EstatusEmail estatusEmail, int IdEncuesta, int idbase)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE EstatusEmail SET IDESTATUSMAIL = 2, MsgEnvio = 'Email enviado exitosamente', FechaHoraModificacion = {0}, UsuarioModificacion = 'Reenvío de Mails', ProgramaModificacion = 'CRON Reenvío' WHERE idencuesta = {1} and idbasededatos = {2}", DateTime.Now, IdEncuesta, idbase);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }
        public static void UpdateFlagEmailToError(ML.EstatusEmail estatusEmail, SmtpException excepcion, int IdEstatusEmail)
        {
            ML.Result result = new ML.Result();
            string MensajeExcepcion = "Ocurrió un error al intentar enviar el email. " + excepcion.Message;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE EstatusEmail SET IDESTATUSMAIL = 1, MsgEnvio = {0}, FechaHoraModificacion = {1}, UsuarioModificacion = 'Reenvío de Mails', ProgramaModificacion = 'CRON Reenvío' WHERE IDESTATUSEMAIL = {2}", MensajeExcepcion, DateTime.Now, IdEstatusEmail);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }
        public static void UpdateFlagEmailToError(ML.EstatusEmail estatusEmail, SmtpException excepcion, int IdEncuesta, int idBase)
        {
            ML.Result result = new ML.Result();
            string MensajeExcepcion = "Ocurrió un error al intentar enviar el email. " + excepcion.Message;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE EstatusEmail SET IDESTATUSMAIL = 1, MsgEnvio = {0}, FechaHoraModificacion = {1}, UsuarioModificacion = 'Reenvío de Mails', ProgramaModificacion = 'CRON Reenvío' WHERE idencuesta = {2} and idBaseDedatos = {3}", MensajeExcepcion, DateTime.Now, IdEncuesta, idBase);

                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
        }
        public static void CronReenvioEmail()
        {
            sendEmail();
        }
        public static ML.Result GetEstatusEnvioByIdBaseDeDatos(ML.Encuesta encuesta) 
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.EstatusEmail.SqlQuery("SELECT * FROM EstatusEmail INNER JOIN ENCUESTA ON ESTATUSEMAIL.IDENCUESTA = ENCUESTA.IDENCUESTA INNER JOIN BASESDEDATOS ON ESTATUSEMAIL.IDBASEDEDATOS = BASESDEDATOS.IDBASESDEDATOS WHERE ESTATUSEMAIL.IDENCUESTA = {0}", encuesta.IdEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.EstatusEmail estatusEmail = new ML.EstatusEmail();
                            estatusEmail.IdEstatusEmail = item.IdEstatusEmail;
                            estatusEmail.Mensaje = item.Mensaje;
                            estatusEmail.Destinatario = item.Destinatario;
                            estatusEmail.MsgEnvio = item.MsgEnvio;
                            estatusEmail.EstatusMail = new ML.EstatusMail();
                            estatusEmail.EstatusMail.IdEstatusMail = Convert.ToInt32(item.IdEstatusMail);
                            estatusEmail.BaseDeDatos = new ML.BasesDeDatos();
                            estatusEmail.BaseDeDatos.IdBaseDeDatos = item.IdBaseDeDatos;
                            estatusEmail.BaseDeDatos.Nombre = item.BasesDeDatos.Nombre;
                            estatusEmail.Encuesta = new ML.Encuesta();
                            estatusEmail.Encuesta.IdEncuesta = item.Encuesta.IdEncuesta;
                            estatusEmail.Encuesta.Nombre = item.Encuesta.Nombre;
                            estatusEmail.DatePrimerIntento = Convert.ToString(item.FechaHoraCreacion);
                            estatusEmail.DateUltimoIntento = Convert.ToString(item.FechaHoraModificacion);

                            result.Object = estatusEmail.Encuesta.Nombre;
                            result.ObjectAux = estatusEmail.Encuesta.IdEncuesta;
                            result.Objects.Add(estatusEmail);
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetSeccionTitle(int IdEncuesta)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int seccionAnterior = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDENCUESTA = {0}", IdEncuesta).ToList();
                    if (query != null)
                    {
                        int flag = 1;
                        foreach (var item in query)
                        {
                            ML.Preguntas preguntas = new ML.Preguntas();
                            preguntas.IdPregunta = item.IdPregunta;
                            preguntas.Encabezado = item.EncabezadoSeccion;
                            preguntas.Seccion = (int) item.Seccion;

                            
                            if (preguntas.Seccion == seccionAnterior && flag > 1)
                            {
                                //No inserta en lista
                            }
                            else
                            {
                                result.Objects.Add(preguntas);
                            }

                            result.Correct = true;
                            flag++;
                            seccionAnterior = preguntas.Seccion;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static string GetTitleBySeccion(int idpregunta)
        {
            string encabezado = "";
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Preguntas.SqlQuery("select * from preguntas where idpregunta = {0}", idpregunta);
                    foreach (var item in query)
                    {
                        encabezado = item.EncabezadoSeccion;
                    }
                }
            }
            catch (Exception ex)
            {
                encabezado = "";
            }
            return encabezado;
        }

        public static int getConfiguracionTermina(int idrespuesta)
        {
            ML.Result result = new ML.Result();
            var tipoTerminaEncuesta = 0;
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ConfiguraRespuesta.SqlQuery("select * from configurarespuesta where idrespuesta = {0}", idrespuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.ConfiguraRespuesta conf = new ML.ConfiguraRespuesta();
                            tipoTerminaEncuesta = (int) item.TerminaEncuesta;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return tipoTerminaEncuesta;
        }


        public static ML.Result AddNewUser(int lastUserId, int IdBaseDeDatos, string usrCreate, DL.RH_DesEntities context, System.Data.Entity.DbContextTransaction transact)
        {
            //SELECT * FROM Encuesta WHERE IdBasesDeDatos = IdBaseDeDatos
            ML.Result result = new ML.Result();
            try
            {
                
                var query = context.Encuesta.SqlQuery("SELECT * FROM Encuesta WHERE IdBasesDeDatos = {0}", IdBaseDeDatos);
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        var Insert = context.Database.ExecuteSqlCommand("INSERT INTO UsuarioEstatusEncuesta (IdUsuario, IdEncuesta, IdEstatusEncuestaD4U, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) values ({0}, {1}, {2}, {3}, {4}, {5})",
                            lastUserId, item.IdEncuesta, 1, DateTime.Now, usrCreate, "Diagnostic4U");
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result getPregH(string IdEncuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var totalPreg = context.Preguntas.SqlQuery("select * from Preguntas where idEncuesta = {0} and Obligatoria = 1 and IdTipoControl < 12", IdEncuesta).ToList();
                    var totalPregLikert = context.PreguntasLikert.SqlQuery("select * from PreguntasLikert where idEncuesta = {0}", IdEncuesta).ToList();

                    var pregOcultas = context.ConfiguraRespuesta.SqlQuery("select * from ConfiguraRespuesta inner join Preguntas on ConfiguraRespuesta.IdPreguntaOpen = Preguntas.IdPregunta where ConfiguraRespuesta.IdEncuesta = {0} and IdTipoControl != 13 and Preguntas.Obligatoria = 1", IdEncuesta).ToList();
                    int conteo = 0;
                    var GetConfig3 = context.Preguntas.SqlQuery("select * from Preguntas inner join Respuestas on Preguntas.IdPregunta = Respuestas.IdPregunta where Respuestas.IdRespuesta > (SELECT MAX(IdRespuesta) AS Confi FROM ConfiguraRespuesta where IdEncuesta = {0}  and TerminaEncuesta = 3) and Preguntas.idEncuesta = {0} and Obligatoria = 1", IdEncuesta).ToList();
                    if (GetConfig3.Count() > 0)
                    {
                        var list = GetConfig3.Select(o => o.Pregunta).Distinct();
                        conteo = list.Count();
                    }

                    var LikertOCultos = context.ConfiguraRespuesta.SqlQuery("select * from ConfiguraRespuesta inner join PreguntasLikert on ConfiguraRespuesta.IdPreguntaOpen = PreguntasLikert.IdPregunta where ConfiguraRespuesta.IdEncuesta ={0}", IdEncuesta).ToList();

                    var totalFinal = totalPreg.Count() + (totalPregLikert.Count() * 2);
                    var ttalLikertOcultos = (LikertOCultos.Count() * 2);
                    result.Object = totalFinal - pregOcultas.Count() - conteo - ttalLikertOcultos;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        //AutoSave 
        public static ML.Result existeRespuesta(int IdEncuesta, int Idusuario)
        {
            var path1 = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
            var fullPath1 = Path.GetFullPath(path1);
            if (!File.Exists(fullPath1))
            {
                string createText = "Log" + Environment.NewLine;
                File.WriteAllText(fullPath1, createText);
            }
            //string appendText1 = "Ingreso usuario" + " " + DateTime.Now + Environment.NewLine;
            //File.AppendAllText(fullPath1, appendText1);
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //context.Database.CommandTimeout = 30;
                    var query = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS WHERE IDENCUESTA = {0} AND IDUSUARIO = {1}", IdEncuesta, Idusuario);
                    if (query.Count() > 0)
                    {
                        result.Exist = true;
                    }
                    else
                    {
                        result.Exist = false;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path = @"\\10.5.2.101\RHDiagnostics\log\log" + IdEncuesta + "_" + Idusuario + ".txt";
                var fullpath = Path.GetFullPath(path);
                if (!File.Exists(fullpath))
                {
                    string createtext = "log" + Environment.NewLine;
                    File.WriteAllText(fullpath, createtext);
                }
                string appendtext = ex.Message + " metodo: existerespuesta. " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullpath, appendtext);
            }
            return result;
        }
        
        public static ML.Result AddRespuestasVacio(int IdEncuesta, int Idusuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    //using (var transaction = context.Database.BeginTransaction())
                    //{
                    //context.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
                    //var transaction = context.Database.BeginTransaction(IsolationLevel.Snapshot);
                    try
                        {
                            //Insercion control 1 al 11s
                            var query = context.Database.ExecuteSqlCommand
                            ("INSERT INTO UsuarioRespuestas(IdEncuesta, Idusuario, FechaHoraCreacion, ProgramaCreacion, UsuarioCreacion, IdPregunta) select {0}, {1}, {2}, {3}, {4}, IdPregunta from Preguntas where (IdTipoControl between 1 and 2 and idEncuesta = {0}) or (IdTipoControl between 4 and 11 and idEncuesta = {0})", IdEncuesta, Idusuario, DateTime.Now, "Autoguardado", Idusuario);
                            //context.SaveChanges();
                            //insercion LikertDoble
                            var getPregLikertDoble = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDTIPOCONTROL = 12 AND IDENCUESTA = {0}", IdEncuesta).ToList();
                            foreach (var item in getPregLikertDoble)
                            {
                                //var getRespuestas = context.Respuestas.SqlQuery("select * from Respuestas where IdPregunta = {0}", item.IdPregunta);
                            IList<DL.Respuestas> list = context.Respuestas.SqlQuery("select * from Respuestas where IdPregunta = {0}", item.IdPregunta).ToList();
                            foreach (var elem in list)
                                {
                                    var insert = context.Database.ExecuteSqlCommand
                                    ("INSERT INTO UsuarioRespuestas(IdEncuesta, Idusuario, FechaHoraCreacion, ProgramaCreacion, UsuarioCreacion, IdRespuesta, IdPregunta) select {0}, {1}, {2}, {3}, {4}, {5}, IdPregunta from Preguntas WHERE idEncuesta = {0} and IdTipoControl != 13 and IdPregunta = {6}", IdEncuesta, Idusuario, DateTime.Now, "Autoguardado", Idusuario, elem.IdRespuesta, item.IdPregunta);
                                    //context.SaveChanges();
                                }
                            }
                            //Insercion checkbox
                            var getPregCheckBox = context.Preguntas.SqlQuery("SELECT * FROM PREGUNTAS WHERE IDTIPOCONTROL = 3 AND IDENCUESTA = {0}", IdEncuesta).ToList();
                            foreach (var item in getPregCheckBox)
                            {
                                var getRespuestas = context.Respuestas.SqlQuery("select * from Respuestas where IdPregunta = {0}", item.IdPregunta).ToList();
                                foreach (var elem in getRespuestas)
                                {
                                    var insert = context.Database.ExecuteSqlCommand
                                    ("INSERT INTO UsuarioRespuestas(IdEncuesta, Idusuario, FechaHoraCreacion, ProgramaCreacion, UsuarioCreacion, IdRespuesta, IdPregunta) select {0}, {1}, {2}, {3}, {4}, {5}, IdPregunta from Preguntas WHERE idEncuesta = {0} and IdTipoControl != 13 and IdPregunta = {6}", IdEncuesta, Idusuario, DateTime.Now, "Autoguardado", Idusuario, elem.IdRespuesta, item.IdPregunta);
                                    //context.SaveChanges();
                                }
                            }
                            context.SaveChanges();
                            //transaction.Commit();
                            result.Correct = true;
                            var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                            var fullPath = Path.GetFullPath(path);
                            if (!File.Exists(fullPath))
                            {
                                string createText = "Log" + Environment.NewLine;
                                File.WriteAllText(fullPath, createText);
                            }
                            string appendText = "Agregado de respuestas vacias exitoso. Metodo: AddRespuestasVacio(). " + DateTime.Now + Environment.NewLine;
                            File.AppendAllText(fullPath, appendText);

                    }
                        catch (Exception ex)
                        {
                        result.Correct = false;
                        result.ErrorMessage = ex.Message;
                        var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                        var fullPath = Path.GetFullPath(path);
                        if (!File.Exists(fullPath))
                        {
                            string createText = "Log" + Environment.NewLine;
                            File.WriteAllText(fullPath, createText);
                        }
                        string appendText = ex.Message + " Metodo: AddRespuestasVacio 1er ex. Fallo agregado de respuestas en vacio" + DateTime.Now + Environment.NewLine;
                        File.AppendAllText(fullPath, appendText);
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path = @"\\10.5.2.101\RHDiagnostics\log\log" + IdEncuesta + "_" + Idusuario + ".txt";
                var fullpath = Path.GetFullPath(path);
                if (!File.Exists(fullpath))
                {
                    string createtext = "log" + Environment.NewLine;
                    File.WriteAllText(fullpath, createtext);
                }
                string appendtext = ex.Message + " metodo: addrespuestasvacio 2a ex. " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullpath, appendtext);
            }
            return result;
        }
        public static ML.Result UpdateRespuestaT1(ML.Preguntas preg, int IdEncuesta, int Idusuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand
                    ("UPDATE UsuarioRespuestas SET IdRespuesta = null, RespuestaUsuario = {1}, FechaHoraModificacion = {5}, ProgramaModificacion = 'Autoguardado', UsuarioModificacion = {6} WHERE IdEncuesta = {2} AND IdUsuario = {3} AND IdPregunta = {4}", preg.MLRespuestas.IdRespuesta, preg.MLRespuestas.Respuesta, IdEncuesta, Idusuario, preg.IdPregunta, DateTime.Now, Idusuario);
                    context.SaveChanges();
                    result.Correct = true;
                    var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                    var fullPath = Path.GetFullPath(path);
                    if (!File.Exists(fullPath))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath, createText);
                    }
                    string appendText = "Autoguardado exitoso. Metodo: UpdateRespuestaT1. IdPregunta: " + preg.IdPregunta + ". Respuesta: " + preg.MLRespuestas.Respuesta + ". " + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath, appendText);
                    
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + Idusuario + ".txt";
                var fullPath = Path.GetFullPath(path);
                if (!File.Exists(fullPath))
                {
                    string createText = "Log" + Environment.NewLine;
                    File.WriteAllText(fullPath, createText);
                }
                string appendText = ex.Message + " Metodo: UpdateRespuestaT1. Parametros: IdPreg: " + preg.IdPregunta + ". " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath, appendText);
            }
            return result;
        }
        public static ML.Result UpdateRespuestaT2(ML.Preguntas preg, int IdEncuesta, int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var Respuestausuario = BL.Respuestas.RegresaNombreRespuesta(preg.MLRespuestas.IdRespuesta);
                    var query = context.Database.ExecuteSqlCommand
                    ("UPDATE UsuarioRespuestas SET IdRespuesta = {0}, RespuestaUsuario = {1}, FechaHoraModificacion = {5}, UsuarioModificacion = {6}, ProgramaModificacion = 'Autoguardado' WHERE IdEncuesta = {2} AND IdUsuario = {3} AND IdPregunta = {4}", preg.MLRespuestas.IdRespuesta, Respuestausuario, IdEncuesta, IdUsuario, preg.IdPregunta, DateTime.Now, IdUsuario);
                    context.SaveChanges();
                    result.Correct = true;
                    var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                    var fullPath = Path.GetFullPath(path);
                    if (!File.Exists(fullPath))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath, createText);
                    }
                    string appendText = "Autoguardado exitoso. Metodo: UpdateRespuestaT2. IdPregunta: " + preg.IdPregunta + ". Respuesta: " + Respuestausuario + ". " + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath, appendText);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                var fullPath = Path.GetFullPath(path);
                if (!File.Exists(fullPath))
                {
                    string createText = "Log" + Environment.NewLine;
                    File.WriteAllText(fullPath, createText);
                }
                string appendText = ex.Message + " Metodo: UpdateRespuestaT2. Parametros: IdPreg: " + preg.IdPregunta + " IdRespuesta: " + preg.MLRespuestas.IdRespuesta + ". " + DateTime.Now + Environment.NewLine;
                //string appendText = ex.Message + " Metodo: UpdateRespuestaT2. " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath, appendText);
            }
            return result;
        }
        public static ML.Result UpdateRespuestaTLikertD(ML.Preguntas preg, int IdEncuesta, int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Database.ExecuteSqlCommand("UPDATE UsuarioRespuestas SET RespuestaUsuario = {0}, FechaHoraModificacion = {1}, UsuarioModificacion = {2}, ProgramaModificacion = 'Autoguardado' WHERE IdEncuesta = {3} AND IdUsuario = {4} AND IdPregunta = {5} AND IDRESPUESTA = {6}",
                     preg.MLRespuestas.Respuesta, DateTime.Now, IdUsuario, IdEncuesta, IdUsuario, preg.IdPregunta, preg.MLRespuestas.IdRespuesta);
                    context.SaveChanges();
                    result.Correct = true;
                    var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                    var fullPath = Path.GetFullPath(path);
                    if (!File.Exists(fullPath))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath, createText);
                    }
                    string appendText = "Autoguardado exitoso. Metodo: UpdateRespuestasTLikertDoble. IdPregunta: " + preg.IdPregunta + ". Respuesta: " + preg.MLRespuestas.Respuesta + ". " + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath, appendText);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                var fullPath = Path.GetFullPath(path);
                if (!File.Exists(fullPath))
                {
                    string createText = "Log" + Environment.NewLine;
                    File.WriteAllText(fullPath, createText);
                }
                string appendText = ex.Message + " Metodo: UpdateRespuestaTLikertD. Parametros: IdPregunta: " + preg.IdPregunta + ". " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath, appendText);
            }
            return result;
        }
        public static ML.Result UpdateRespuestaTCheck(ML.Preguntas preg, int  IdEncuesta, int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var Respuestausuario = BL.Respuestas.RegresaNombreRespuesta(preg.MLRespuestas.IdRespuesta);
                    //preg.MLRespuestas.IdRespuesta
                    //preg.MLRespuestas.Respuesta
                    var query = context.Database.ExecuteSqlCommand
                    ("UPDATE UsuarioRespuestas SET IdRespuesta = {0}, RespuestaUsuario = {1}, FechaHoraModificacion = {5}, UsuarioModificacion = {6}, ProgramaModificacion = 'Autoguardado', Selected = {8} WHERE IdEncuesta = {2} AND IdUsuario = {3} AND IdPregunta = {4} and IdRespuesta = {7}", preg.MLRespuestas.IdRespuesta, Respuestausuario, IdEncuesta, IdUsuario, preg.IdPregunta, DateTime.Now, IdUsuario, preg.MLRespuestas.IdRespuesta, preg.MLRespuestas.Selected);
                    context.SaveChanges();
                    result.Correct = true;
                    var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                    var fullPath = Path.GetFullPath(path);
                    if (!File.Exists(fullPath))
                    {
                        string createText = "Log" + Environment.NewLine;
                        File.WriteAllText(fullPath, createText);
                    }
                    string appendText = "Autoguardado exitoso. Metodo: UpdateRespuestaTCheck. IdPregunta: " + preg.IdPregunta + ". Respuesta: " + Respuestausuario + ". " + DateTime.Now + Environment.NewLine;
                    File.AppendAllText(fullPath, appendText);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
                var fullPath = Path.GetFullPath(path);
                if (!File.Exists(fullPath))
                {
                    string createText = "Log" + Environment.NewLine;
                    File.WriteAllText(fullPath, createText);
                }
                string appendText = ex.Message + " Metodo: UpdateRespuestaTCheck. Parametros: IdPregunta: " + preg.IdPregunta + " IdRespuesta: " + preg.MLRespuestas.IdRespuesta + ". " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath, appendText);
            }
            return result;
        }
        public static ML.Result GetRespuestas(int IdEncuesta, int Idusuario)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.UsuarioRespuestas.SqlQuery("SELECT * FROM USUARIORESPUESTAS WHERE IDENCUESTA = {0} AND IDUSUARIO = {1}", IdEncuesta, Idusuario);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.UsuarioRespuestas userRes = new ML.UsuarioRespuestas();
                            userRes.Preguntas = new ML.Preguntas();
                            userRes.Preguntas.IdPregunta = item.Preguntas.IdPregunta;
                            userRes.Preguntas.TipoControl = new ML.TipoControl();
                            userRes.Preguntas.TipoControl.IdTipoControl = item.Preguntas.IdTipoControl;
                            userRes.Respuestas = new ML.Respuestas();
                            userRes.Respuestas.IdRespuesta =  item.Respuestas == null ? 0: item.Respuestas.IdRespuesta;
                            if (userRes.Preguntas.TipoControl.IdTipoControl == 7)
                            {
                                userRes.RespuestaUsuario = item.RespuestaUsuario == null ? "" : item.RespuestaUsuario;
                            }
                            else
                            {
                                userRes.RespuestaUsuario = item.RespuestaUsuario == null ? "0" : item.RespuestaUsuario;
                            }
                           
                            //userRes.Respuestas.Selected = item.Respuestas == null ? false : item.Respuestas.Selected == null ? false: (bool)item.Respuestas.Selected ;
                            userRes.Respuestas.Selected = item.Selected == null ? false : (bool) item.Selected;
                            result.Objects.Add(userRes);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static void writeLogIdResCero(int IdEncuesta, int IdUsuario, ML.Preguntas preg)
        {
            var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
            var fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath))
            {
                string createText = "Log" + Environment.NewLine;
                File.WriteAllText(fullPath, createText);
            }
            string appendText = "El Id de respuesta se recibió en cero. Metodo: UpdatePreguntaConIdRespuesta. Parametros: IdPreg: " + preg.IdPregunta + " " + DateTime.Now + Environment.NewLine;
            File.AppendAllText(fullPath, appendText);
        }

        public static void writeLogIdResCero(int IdEncuesta, int IdUsuario, ML.Preguntas preg, string msg)
        {
            var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
            var fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath))
            {
                string createText = "Log" + Environment.NewLine;
                File.WriteAllText(fullPath, createText);
            }
            string appendText = msg + ". Metodo: Autoguardado. Parametros: IdPreg: " + preg.IdPregunta + " " + DateTime.Now + Environment.NewLine;
            File.AppendAllText(fullPath, appendText);
        }

        public static void writeLogIdResCeroAddResp(int IdEncuesta, int IdUsuario, int IdPregunta)
        {
            var path = @"\\10.5.2.101\RHDiagnostics\log\Log" + IdEncuesta + "_" + IdUsuario + ".txt";
            var fullPath = Path.GetFullPath(path);
            if (!File.Exists(fullPath))
            {
                string createText = "Log" + Environment.NewLine;
                File.WriteAllText(fullPath, createText);
            }
            string appendText = "El Id de respuesta se recibió en cero. Metodo: UpdatePreguntaConIdRespuesta. Parametros: IdPreg: " + IdPregunta + " " + DateTime.Now + Environment.NewLine;
            File.AppendAllText(fullPath, appendText);
        }

        //Transaccion con lectura
        public static ML.Result InsertTransaction()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    using (var transaction = context.Database.BeginTransaction(IsolationLevel.ReadCommitted))
                    {
                        //context.Database
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result getEncuestaByUID(string UID)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Encuesta.SqlQuery("SELECT * FROM ENCUESTA WHERE UID = {0}", UID).Select(m => m.IdEncuesta).FirstOrDefault();
                    result.Object = "/Encuesta/Login/?e=" + query;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Object = "/Encuesta/Login/?e=0";
            }
            return result;
        }

        public static ML.Result Demo()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    ML.Encuesta encuesta = new ML.Encuesta();
                    encuesta.Nombre = "Ejemplo";
                    encuesta.BasesDeDatos = new ML.BasesDeDatos();
                    encuesta.BasesDeDatos.IdBaseDeDatos = 1;


                    var data = BL.MappingConfigurations.MappingEncuesta(encuesta);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
        //public override string ToString()
        //{
        //    return "";
        //}

        public static ML.Result AddCL(ML.Encuesta Encuesta, int usuarioCreacion)
        {
            ML.Result result = new Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Alta Encuesta
                        var query = context.Database.ExecuteSqlCommand("INSERT INTO Encuesta (DosColumnas,Nombre,FechaInicio,FechaFin,IdEstatus,IdEmpresa,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,CodeHTML,IdBasesDeDatos,Descripcion,Instruccion,ImagenInstruccion,IdTipoEncuesta,Agradecimiento,ImagenAgradecimiento,IdTipoOrden) " +
                            " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})", 1, Encuesta.Nombre, Encuesta.FechaInicio, Encuesta.FechaFin, 1, Encuesta.IdEmpresa, DateTime.Now, usuarioCreacion, "Alta Encuesta", Encuesta.Instruccion,  Encuesta.BasesDeDatos.IdBaseDeDatos, Encuesta.Descripcion,
                            Encuesta.Agradecimiento, Encuesta.ImagenInstruccion, 4, "", Encuesta.ImagenAgradecimiento,Encuesta.TipoOrden.IdTipoOrden);
                        int idEncuesta = context.Encuesta.Max(q => q.IdEncuesta);
                        //Alta Periodo Encuesta --- ConfigClimaLab ---
                        DL.ConfigClimaLab savePeriodo = new DL.ConfigClimaLab();
                        savePeriodo.IdEncuesta = idEncuesta;
                        savePeriodo.IdBaseDeDatos = Encuesta.BasesDeDatos.IdBaseDeDatos;
                        savePeriodo.FechaInicio = (DateTime)Encuesta.FechaInicio;
                        savePeriodo.FechaFin = (DateTime)Encuesta.FechaFin;
                        savePeriodo.PeriodoAplicacion = Convert.ToInt32(Encuesta.Instrucciones1);
                        savePeriodo.FechaHoraCreacion = DateTime.Now;
                        savePeriodo.UsuarioCreacion = usuarioCreacion.ToString();
                        savePeriodo.ProgramaCreacion = "Alta Encuesta";
                        var queryP = context.ConfigClimaLab.Add(savePeriodo);
                        //Alta Estatus de encuesta  1.- No iniciada
                        int IdBaseDeDatos = Convert.ToInt32(Encuesta.BasesDeDatos.IdBaseDeDatos);
                        BL.Usuario.AddEstatus(idEncuesta, IdBaseDeDatos, context);
                        //Encuesta Area
                        var obtieneAreasPorIdEmpresa = BL.Area.AreaGetByCompanyId(Int32.Parse(Encuesta.IdEmpresa.ToString()));
                        if (obtieneAreasPorIdEmpresa.Objects.Count >= 0)
                        {
                            foreach (ML.Area obj in obtieneAreasPorIdEmpresa.Objects)
                            {
                                var queryEncuestaArea = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaArea (IdArea,IdEncuesta,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion) " +
                                    " VALUES({0},{1},{2},{3},{4})", obj.IdArea, idEncuesta, DateTime.Now, usuarioCreacion, "Alta Encuesta");
                            }
                        }
                        //Preguntas Insercion doble por el tipo de Enfoque  Empresa/Area
                        //int IdPreguntaSubseccion = 0;***** No se usa
                        foreach (ML.Preguntas obj in Encuesta.NewCuestion)
                        {
                            //Se inserta el tipo de Control segun su competencia
                            var idTipoControl = 0;
                            switch (obj.Competencia.IdCompetencia)
                            {
                                case 1:case 2:case 3:case 4:case 5:case 6:case 7:case 8:case 9:case 10:case 11:case 12:
                                    idTipoControl = 12;
                                    break;                              
                                case 13: case 14: case 15:
                                    idTipoControl = 5;
                                    break;
                                case 16:
                                    idTipoControl = 2;
                                    break;
                                case 17:
                                    idTipoControl = 4;
                                    break;
                                default:
                                    idTipoControl  = 12;
                                    break;
                            }
                            // Se inserta la pregunta Enfoque Empresa
                            var queryPreguntas = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                "(idEncuesta,Pregunta,Valoracion,IdCompetencia,IdEstatus,Enfoque,IdEnfoque,Seccion,IdPreguntaPadre,IdTipoControl,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion)" +
                                    " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", idEncuesta, obj.Pregunta,
                                    obj.Valoracion, obj.Competencia.IdCompetencia, obj.Obligatoria == false ? 1:2, "Enfoque Empresa", obj.IdEnfoque,obj.Seccion,obj.IdPreguntaPadre,idTipoControl, DateTime.Now,
                                    usuarioCreacion, "Alta Encuesta");
                            //Obtiene maximo de pregunta EE
                            int idPreguntaEE= context.Preguntas.Max(q => q.IdPregunta);
                            //Tabla Encuesta Pregunta
                            var queryEncuestaPreguntaEE = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPreguntaEE, DateTime.Now, usuarioCreacion, "Alta Encuesta");
                            //Se omite el insertado de respuestas ya que son fijas
                            //-------------------------------------------------------------//  
                            // Se valida el Id de Competencia 
                            // Si es mayor a 12, ya no se duplican y guarda el enfoque en Null

                            if (obj.Competencia.IdCompetencia <= 12)
                            {
                                //al id padre EE se le suma 86 para obtener el IdPadre de EA
                                //Se cambia a id padre original por asi convenir al reporte -- Jose 05/06/2021
                                int idpadreEA = obj.IdPreguntaPadre;//obj.IdPreguntaPadre + 86;
                                // Se inserta la pregunta Enfoque Area
                                var queryPreguntasEA = context.Database.ExecuteSqlCommand("INSERT INTO Preguntas " +
                                    "(idEncuesta,Pregunta,Valoracion,IdCompetencia,IdEstatus,Enfoque,IdEnfoque,Seccion,IdPreguntaPadre,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,IdTipoControl)" +
                                        " VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", idEncuesta, obj.Pregunta,
                                        obj.Valoracion, obj.Competencia.IdCompetencia, obj.Obligatoria == false ? 1 : 2, "Enfoque Area", 2, obj.Seccion, idpadreEA, DateTime.Now,
                                        usuarioCreacion, "Alta Encuesta", idTipoControl);
                                //Obtiene maximo de pregunta EA
                                int idPreguntaEA = context.Preguntas.Max(q => q.IdPregunta);
                                //Tabla Encuesta Pregunta
                                var queryEncuestaPreguntaEA = context.Database.ExecuteSqlCommand("INSERT INTO EncuestaPregunta " +
                                    "(IdEncuesta, IdPregunta, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) " +
                                    "VALUES({0},{1},{2},{3},{4})", idEncuesta, idPreguntaEA, DateTime.Now, usuarioCreacion, "Alta Encuesta");
                                //Se omite el insertado de respuestas ya que son fijas
                                //-------------------------------------------------------------//       
                            }
                            //SI tiene Categorias la pregunta se insertan
                            if (obj.NewCat != null)
                            {
                                foreach (ML.Categoria cat in obj.NewCat)
                                {
                                    var queryAddCat = context.Database.ExecuteSqlCommand("INSERT INTO PreguntaCategorias (IdPregunta,IdEncuesta,IdCategoria,Valoracion,IdEstatus,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})",
                                        idPreguntaEE,idEncuesta,cat.IdCategoria,cat.Valoracion,1,DateTime.Now,usuarioCreacion.ToString(),"Alta de Categorias");
                                    
                                }
                            }
                        }
                        result.Correct = true;
                        result.idEncuestaAlta = idEncuesta;
                        context.SaveChanges();
                        transaction.Commit();
                        idEncuesta = 0;
                    }
                    catch (Exception aE)
                    {
                        BL.NLogGeneratorFile.logError(aE, new StackTrace());
                        result.ex = aE;
                        result.ErrorMessage = aE.Message.ToString();
                        result.Correct = false;
                        transaction.Rollback();
                    }                   
                    return result;
                }
            }

        }
        public static ML.Result getEncuestaByIdOrden(int idEncuesta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Estatus = obj.Estatus;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            //Preguntas
                            //encuesta.ListarPreguntas = BL.Preguntas.getAllPreguntasByIdEncuestaEdit(obj.IdEncuesta);
                            encuesta.NewCuestion = BL.Preguntas.getAllPreguntasByIdEncuestaOrden(obj.IdEncuesta);
                            //Base de datos
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }

        public static Result ConfiguraOrden(List<ML.Preguntas> listaPreguntas)
        {
            Result result = new Result();
            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        for (int i = 0; i < listaPreguntas.Count; i++)
                        {
                            var idOrden = i + 1;
                            var idPadreCalculado = 0;
                            var query1 = context.Database.ExecuteSqlCommand("UPDATE Preguntas SET IdOrden = {0} WHERE idEncuesta={1} and IdPreguntaPadre={2}", idOrden, listaPreguntas[i].IdEncuesta, listaPreguntas[i].IdPreguntaPadre);
                            if (listaPreguntas[i].IdCompetencia <= 12)
                            {
                                idPadreCalculado = listaPreguntas[i].IdPreguntaPadre + 86;

                                var query2 = context.Database.ExecuteSqlCommand("UPDATE Preguntas SET IdOrden = {0} WHERE idEncuesta={1} and IdPreguntaPadre={2}", idOrden, listaPreguntas[i].IdEncuesta, idPadreCalculado);
                            }


                            context.SaveChanges();
                            result.Correct = true;
                        }

                        transaction.Commit();
                    }
                    catch (Exception aE)
                    {
                        result.Correct = false;
                        result.ErrorMessage = aE.Message.ToString();
                        transaction.Rollback();                        
                    }
                }

            }
            return result;
        }

        public static int getEncuestaTipoById(int idEncuesta)
        {
            int idtipoEncuesta = 0;
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var query = contex.Encuesta.SqlQuery("SELECT * FROM Encuesta where IdEncuesta = {0}",idEncuesta).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            idtipoEncuesta = (Int32)item.IdTipoEncuesta;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                return idtipoEncuesta;                
            }
            return idtipoEncuesta;
        }
        public static Result getEncuestaByIdEditClimaL(int idEncuesta ,string idUsuarioAdmin)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var idEstatus = 1;
                    var query = contex.Encuesta.SqlQuery("SELECT *  FROM Encuesta " +
                        " INNER JOIN TipoEstatus on TipoEstatus.IdEstatus = Encuesta.IdEstatus " +
                        " INNER JOIN TipoEncuesta on TipoEncuesta.IdTipoEncuesta = Encuesta.IdTipoEncuesta " +
                        " where Encuesta.IdEncuesta = {0} and Encuesta.IdEstatus = {1}", idEncuesta, idEstatus).ToList();
                    result.EditaEncuesta = new ML.Encuesta();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Encuesta encuesta = new ML.Encuesta();
                            var resulListEmpresa = BL.Company.GetAllCompany();
                            //var resulListTipoDeEmpresa = BL.TipoEncuesta.getAllTipoEncuesta();
                            //var listadoPlantillas = BL.Plantillas.getPlantillas(1);
                            //var listadoBaseDeDatos = BL.BasesDeDatos.getBaseDeDatosAll();
                            //var listadoBaseDeDatosAnonima = BL.BasesDeDatos.getBaseDeDatosAnonima();
                            //var listadoEnfoquePregunta = BL.Preguntas.getEnfoquePregunta();
                            var listadoCompetenciaPreguntas = BL.Competencia.getCompetencias(idUsuarioAdmin);
                           // var listadoTipoControl = BL.TipoControl.getTipoControl();
                            var listadoTipoOrden = BL.TipoOrden.getAllTipoOrden();
                            encuesta.ListTipoOrden = listadoTipoOrden.ListTipoOrden;
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            //Periodos
                            var exists = contex.ConfigClimaLab.SqlQuery("SELECT * FROM CONFIGCLIMALAB WHERE IDENCUESTA = {0} AND IDBASEDEDATOS = {1}", obj.IdEncuesta, obj.IdBasesDeDatos).ToList();
                            encuesta.Instrucciones1 = exists.Count == 0 ? DateTime.Now.Year.ToString() : exists[0].PeriodoAplicacion.ToString();
                            //encuesta.ListTipoEncuesta = resulListTipoDeEmpresa.ListadoTipoEncuesta;
                            encuesta.ListEmpresas = resulListEmpresa.Objects;
                            //encuesta.ListPlantillas = listadoPlantillas.ListadoDePlantillasPredefinidas;
                            encuesta.ListDataBase = BasesDeDatos.GetBDClima().Objetos;//listadoBaseDeDatosAnonima.ListadoDeBaseDeDatos;
                            //encuesta.ListEnfoquePregunta = listadoEnfoquePregunta.ListadoEnfoquesPregunta;
                            encuesta.ListCompetencias = listadoCompetenciaPreguntas.ListadoCompetenciasPregunta;
                            //encuesta.ListTipoControl = listadoTipoControl.ListadoTipoControl;
                            encuesta.IdEncuesta = obj.IdEncuesta;
                            encuesta.IdEmpresa = obj.IdEmpresa;
                            encuesta.Agradecimiento = obj.Agradecimiento;
                            encuesta.Company = new ML.Company();
                            encuesta.Company.CompanyId = obj.IdEmpresa;
                            encuesta.Descripcion = obj.Descripcion;
                            encuesta.DosColumnas = Convert.ToBoolean(obj.DosColumnas);
                            encuesta.Estatus = obj.Estatus;
                            encuesta.FechaFin = obj.FechaFin;
                            encuesta.FechaInicio = obj.FechaInicio;
                            encuesta.CodeHTML = obj.CodeHTML;
                            encuesta.ImagenAgradecimiento = obj.ImagenAgradecimiento;
                            encuesta.ImagenInstruccion = obj.ImagenInstruccion;
                            encuesta.Instruccion = obj.Instruccion;
                            encuesta.MLTipoEncuesta = new ML.TipoEncuesta();
                            encuesta.MLTipoEncuesta.IdTipoEncuesta = obj.IdTipoEncuesta;
                            encuesta.Nombre = obj.Nombre;
                            encuesta.TipoOrden = new ML.TipoOrden();
                            encuesta.TipoOrden.IdTipoOrden = (Int32)obj.IdTipoOrden;
                            encuesta.Plantillas = new ML.Plantillas();
                            encuesta.Plantillas.IdPlantilla = Convert.ToInt32(obj.IdPlantilla);
                            encuesta.ProgramaCreacion = obj.ProgramaCreacion;
                            encuesta.TipoEstatus = new ML.TipoEstatus();
                            encuesta.TipoEstatus.IdEstatus = obj.IdEstatus;
                            encuesta.UsuarioCreacion = obj.UsuarioCreacion;
                            //Preguntas
                            encuesta.NewCuestionEdit = BL.Preguntas.getAllPreguntasByIdEncuestaEdit(obj.IdEncuesta,idUsuarioAdmin);
                            //encuesta.NewCuestion = BL.Preguntas.getAllPreguntasByIdEncuesta(obj.IdEncuesta);
                            //Base de datos
                            encuesta.BasesDeDatos = new ML.BasesDeDatos();
                            encuesta.BasesDeDatos.IdBaseDeDatos = obj.IdBasesDeDatos;
                            result.EditaEncuesta = encuesta;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                result.ex = aE;
                result.ErrorMessage = aE.Message.ToString();
                result.Correct = false;
            }
            return result;

        }
        public static Result EditCL(ML.Encuesta Encuesta, string usrLog)
        {
            ML.Result result = new ML.Result();

            using (DL.RH_DesEntities context = new DL.RH_DesEntities())
            {
                context.Database.Log = Console.Write;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //Cambios de BD
                        var IdBDOriginal = (from a in context.Encuesta where a.IdEncuesta == Encuesta.IdEncuesta select (int)a.IdBasesDeDatos).FirstOrDefault();
                        if (Encuesta.BasesDeDatos.IdBaseDeDatos != IdBDOriginal)
                        {
                            //Remove userStatus for lastDB
                            var getUsrRemove = context.Empleado.SqlQuery("SELECT * FROM EMPLEADO WHERE IDBASEDEDATOS = {0}", IdBDOriginal);
                            if (getUsrRemove.Count() > 0)
                            {
                                foreach (var item in getUsrRemove)
                                {
                                    var remove = context.Database.ExecuteSqlCommand("DELETE FROM EstatusEncuesta WHERE IdEmpleado = {0} AND IdEncuesta = {1}", item.IdEmpleado, Encuesta.IdEncuesta);
                                    context.SaveChanges();
                                }
                            }
                            //Add userStatus for new DB
                            var getNewUsers = context.Empleado.SqlQuery("SELECT * FROM EMPLEADO WHERE IDBASEDEDATOS = {0}", Encuesta.BasesDeDatos.IdBaseDeDatos).ToList();
                            if (getNewUsers.Count() > 0)
                            {
                                foreach (var item in getNewUsers)
                                {
                                    var insert = context.Database.ExecuteSqlCommand
                                    ("INSERT INTO EstatusEncuesta (IdEmpleado, IdEncuesta, Estatus, FechaHoraCreacion, UsuarioCreacion, ProgramaCreacion) VALUES ({0}, {1}, {2}, {3}, {4}, {5})",
                                    item.IdEmpleado, Encuesta.IdEncuesta, "No comenzada", DateTime.Now, usrLog, "Edita Clima Laboral");
                                    context.SaveChanges();
                                }
                            }
                        }


                        var UsuarioModificacion = Encuesta.UsuarioModificacion == null ? "Usuario Editor" : Encuesta.UsuarioModificacion.ToString();
                        var ProgramaModificacion = "Actualiza Encuesta";
                        //Edita Encuesta
                        DL.Encuesta updateEncuesta = context.Encuesta.FirstOrDefault(x => x.IdEncuesta == Encuesta.IdEncuesta);
                        int idEncuesta = Convert.ToInt32(updateEncuesta.IdEncuesta);
                        int idEstatus = Convert.ToInt32(updateEncuesta.IdEstatus);
                        int idPlantilla = Convert.ToInt32(updateEncuesta.IdPlantilla);
                        int idBaseDeDatos = Convert.ToInt32(updateEncuesta.IdBasesDeDatos);
                        int idTipoEncuesta = Convert.ToInt32(updateEncuesta.IdTipoEncuesta);
                        int idEmpresa = Convert.ToInt32(updateEncuesta.IdEmpresa);
                        updateEncuesta.Nombre = Encuesta.Nombre.ToString();
                        updateEncuesta.FechaInicio = Encuesta.FechaInicio;
                        updateEncuesta.FechaFin = Encuesta.FechaFin;
                        updateEncuesta.Estatus = Encuesta.Estatus;
                        updateEncuesta.DosColumnas = Encuesta.DosColumnas;
                        updateEncuesta.IdEstatus = 1;
                        updateEncuesta.FechaHoraModificacion = DateTime.Now;
                        updateEncuesta.UsuarioModificacion = UsuarioModificacion;
                        updateEncuesta.ProgramaModificacion = ProgramaModificacion;
                        updateEncuesta.CodeHTML = Encuesta.CodeHTML;                        
                        updateEncuesta.IdBasesDeDatos = Encuesta.BasesDeDatos.IdBaseDeDatos;
                        updateEncuesta.Descripcion = Encuesta.Descripcion;
                        updateEncuesta.Instruccion = Encuesta.Instruccion;                        
                        updateEncuesta.ImagenInstruccion = Encuesta.ImagenInstruccion;                        
                        updateEncuesta.IdEmpresa = Encuesta.IdEmpresa;
                        updateEncuesta.Agradecimiento = Encuesta.Agradecimiento;
                        updateEncuesta.ImagenAgradecimiento = Encuesta.ImagenAgradecimiento;
                        context.SaveChanges();

                        //Edita Periodos  -- [ConfigClimaLab] --
                        var idPeriodoOriginal = (from a in context.ConfigClimaLab where  a.IdEncuesta == Encuesta.IdEncuesta select (int)a.IdBaseDeDatos).FirstOrDefault();
                        if (Encuesta.BasesDeDatos.IdBaseDeDatos != idPeriodoOriginal)
                        {
                            //Remove Config Clima
                            var removePeriodo = context.Database.ExecuteSqlCommand("DELETE FROM ConfigClimaLab WHERE IdEncuesta = {0}",Encuesta.IdEncuesta);
                            context.SaveChanges();
                            //Add Config Clima
                            var getNewPeriodo = context.ConfigClimaLab.SqlQuery("INSERT INTO ConfigClimaLab (IdEncuesta,IdBaseDeDatos,FechaInicio,FechaFin,FechaHoraCreacion,UsuarioCreacion,ProgramaCreacion,PeriodoAplicacion) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})",
                                Encuesta.IdEncuesta,Encuesta.BasesDeDatos.IdBaseDeDatos,Encuesta.FechaInicio,Encuesta.FechaFin,DateTime.Now, UsuarioModificacion, "Edita Clima Laboral", Encuesta.Instrucciones1);
                            context.SaveChanges();
                        }
                        else
                        { 
                            DL.ConfigClimaLab updatePeriodo = context.ConfigClimaLab.FirstOrDefault(x => x.IdEncuesta == Encuesta.IdEncuesta && x.IdBaseDeDatos == idBaseDeDatos);
                            updatePeriodo.FechaInicio = Encuesta.FechaInicio;
                            updatePeriodo.FechaFin = Encuesta.FechaFin;
                            updatePeriodo.FechaHoraModificacion = DateTime.Now;
                            updatePeriodo.UsuarioModificacion = UsuarioModificacion;
                            updatePeriodo.ProgramaModificacion = "Edita Clima Laboral";
                            updatePeriodo.PeriodoAplicacion = Convert.ToInt32(Encuesta.Instrucciones1);
                            context.SaveChanges();
                        }

                        //Edita Pregunta                       
                        if (Encuesta.NewCuestion != null)
                        {
                            foreach (var item in Encuesta.NewCuestion)
                            {
                                DL.Preguntas updatePregunta = context.Preguntas.FirstOrDefault(x => x.IdPregunta == item.IdPregunta);
                                updatePregunta.Pregunta = item.Pregunta;
                                updatePregunta.IdEstatus = 1;
                                updatePregunta.FechaHoraModificacion = DateTime.Now;
                                updatePregunta.UsuarioModificacion = UsuarioModificacion;
                                updatePregunta.ProgramaModificacion = ProgramaModificacion;
                                updatePregunta.Valoracion = item.Valoracion;
                                updatePregunta.Obligatoria = item.Obligatoria;
                                context.SaveChanges();
                                //Respuesta Update
                                if (item.Competencia.IdCompetencia <= 12)
                                {
                                    int idpadreEA = item.IdPreguntaPadre + 86;
                                    DL.Preguntas updatePreguntaEA = context.Preguntas.FirstOrDefault(x => x.idEncuesta == Encuesta.IdEncuesta && x.IdPreguntaPadre == idpadreEA);
                                    updatePreguntaEA.Pregunta = item.Pregunta;
                                    updatePreguntaEA.IdEstatus = 1;
                                    updatePreguntaEA.FechaHoraModificacion = DateTime.Now;
                                    updatePreguntaEA.UsuarioModificacion = UsuarioModificacion;
                                    updatePreguntaEA.ProgramaModificacion = ProgramaModificacion;
                                    updatePreguntaEA.Obligatoria = item.Obligatoria;
                                    context.SaveChanges();
                                }

                         
                            }
                        }                       
                        result.Correct = true;
                        context.SaveChanges();
                        transaction.Commit();                        
                    }
                    catch (Exception aE)
                    {
                        result.Correct = false;
                        result.ErrorMessage = aE.Message;
                        transaction.Rollback();
                    }
                }
            }
            return result;
        }
        public static List<ML.Preguntas> getPreguntasDefault()
        {
            try
            {
                var list = new List<ML.Preguntas>();
                using (DL.RH_DesEntities context =new DL.RH_DesEntities())
                {
                    var data = context.Preguntas.Where(o => o.idEncuesta == 1 && o.IdEnfoque == 1).ToList();
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            ML.Preguntas preg = new ML.Preguntas()
                            {
                                IdPregunta = item.IdPregunta,
                                Pregunta = item.Pregunta
                            };
                            list.Add(preg);
                        }
                        return list;
                    }
                    return list;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<ML.Preguntas>();
            }
        }
    }
}
