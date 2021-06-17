using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ML
{
    public class modelReporte
    {
        //public static SqlConnection sqlServerConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString());
        public int idTipoEntidad { get; set; } = 0;
        public int idEnfoque { get; set; }
        public int idEncuesta { get; set; }
        public static int entidadId { get; set; } = 0;
        public int idEntidad { get; set; }
        public int idCompetencia { get; set; }
        public int idCategoria { get; set; } = 0;
        public int idSubCategoria { get; set; } = 0;
        public string entidadNombre { get; set; } = String.Empty;
        public int anioActual { get; set; } = 0;
        public int idPregunta { get; set; }
        public static string tipoFiltroDemografico { get; set; } = String.Empty;
        public static string valorFintroDemografico { get; set; } = String.Empty;
        public static string validStatusEmpleado { get; set; } = "Activo";
        public static string validStatusEncuesta { get; set; } = "Terminada";
        public static string answervalid { get; set; } = "Verdad";
        public double result { get; set; } = 0;
        // filtros demograficos
        public string filtroDemografico { get; set; } = string.Empty;
        public string valorFiltroDemografico { get; set; } = string.Empty;
        public static string queryAfirmativasByReactivo = @"select Empleado.CondicionTrabajo, Empleado.GradoAcademico, Empleado.Puesto, Empleado.RangoAntiguedad, Empleado.RangoEdad, Empleado.Sexo,  Empleado.TipoFuncion from EmpleadoRespuestas 
                                                        inner join Empleado on EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        inner join EstatusEncuesta on EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                                                        inner join Preguntas on EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        where 
                                                        EmpleadoRespuestas.RespuestaEmpleado like '%e es verdad%' and
                                                        Empleado.EstatusEmpleado = 'Activo' and
                                                        EstatusEncuesta .Estatus = 'terminada' and
                                                        EmpleadoRespuestas.IdPregunta = {0} and
                                                        EmpleadoRespuestas.Anio = {1} and
                                                        EstatusEncuesta.Anio = {1} and
                                                        Empleado.{2} = '{3}' and
                                                        Preguntas.IdEnfoque = {4} and 
                                                        EmpleadoRespuestas.IdEncuesta = {5} and
                                                        EstatusEncuesta.IdEncuesta = {5} and
                                                        Preguntas.IdEncuesta = {5} and
                                                        Preguntas.IdEstatus = 1";

        public static string queryEncuestasTerminadas = @"select Empleado.CondicionTrabajo, Empleado.GradoAcademico, Empleado.Puesto, Empleado.RangoAntiguedad, Empleado.RangoEdad, Empleado.Sexo,  Empleado.TipoFuncion from EstatusEncuesta
                                                        inner join Empleado on EstatusEncuesta.IdEmpleado = Empleado.IdEmpleado
                                                        where 
                                                        EstatusEncuesta.Estatus = 'terminada' and
                                                        Empleado.EstatusEmpleado = 'Activo' and
                                                        EstatusEncuesta.Anio = {0} and
                                                        Empleado.{1} = '{2}' and
                                                        EstatusEncuesta.IdEncuesta = {3}";

        public static string queryEncuestasEsperadas = @"select Empleado.CondicionTrabajo, Empleado.GradoAcademico, Empleado.Puesto, Empleado.RangoAntiguedad, Empleado.RangoEdad, Empleado.Sexo,  Empleado.TipoFuncion from EstatusEncuesta
                                                        inner join Empleado on EstatusEncuesta.IdEmpleado = Empleado.IdEmpleado
                                                        where 
                                                        Empleado.EstatusEmpleado = 'Activo' and
                                                        EstatusEncuesta.Anio = {0} and
                                                        Empleado.{1} = '{2}' and
                                                        EstatusEncuesta.IdEncuesta = {3}";

        public static string queryPorcentajeAfirmativasByCompetencia = @"select Empleado.CondicionTrabajo, Empleado.GradoAcademico, Empleado.Puesto, Empleado.RangoAntiguedad, Empleado.RangoEdad, Empleado.Sexo,  Empleado.TipoFuncion from EmpleadoRespuestas 
                                                        inner join Empleado on EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        inner join Preguntas on EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        inner join EstatusEncuesta on EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                                                        where 
                                                        EmpleadoRespuestas.RespuestaEmpleado like '%e es verdad%' and
                                                        Empleado.EstatusEmpleado = 'Activo' and
                                                        EstatusEncuesta .Estatus = 'terminada' and
                                                        Preguntas.IdCompetencia = {0} and
                                                        EmpleadoRespuestas.Anio = {1} and
                                                        EstatusEncuesta.Anio = {1} and
                                                        Empleado.{2} = '{3}' and
                                                        Preguntas.IdEnfoque = {4} and
                                                        EmpleadoRespuestas.IdEncuesta = {5} and
                                                        EstatusEncuesta.IdEncuesta = {5} and
                                                        Preguntas.IdEncuesta = {5} and
                                                        Preguntas.IdEstatus = 1";

        /// <summary>
        /// idSubcategoria, anio, entidad, valorEntidad, idEnfoque, idencuesta
        /// requiere filtro demografico
        /// </summary>
        public static string queryPorcentajeAfirmativasBySubCategoria = @"select Empleado.CondicionTrabajo, Empleado.GradoAcademico, Empleado.Puesto, Empleado.RangoAntiguedad, Empleado.RangoEdad, Empleado.Sexo,  Empleado.TipoFuncion from EmpleadoRespuestas 
                                                        inner join Empleado on EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        inner join Preguntas on EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        inner join EstatusEncuesta on EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                                                        inner join ValoracionPreguntaPorSubcategoria on Preguntas.IdPregunta = ValoracionPreguntaPorSubcategoria.IdPregunta
                                                        where 
                                                        EmpleadoRespuestas.RespuestaEmpleado like '%e es verdad%' and
                                                        Empleado.EstatusEmpleado = 'Activo' and
                                                        EstatusEncuesta .Estatus = 'terminada' and
                                                        ValoracionPreguntaPorSubcategoria.IdSubcategoria = {0} and
                                                        EmpleadoRespuestas.Anio = {1} and
                                                        EstatusEncuesta.Anio = {1} and
                                                        Empleado.{2} = '{3}' and
                                                        Preguntas.IdEnfoque = {4} and
                                                        EmpleadoRespuestas.IdEncuesta = {5} and
                                                        EstatusEncuesta.IdEncuesta = {5} and
                                                        Preguntas.IdEncuesta = {5} and
                                                        Preguntas.IdEstatus = 1";

        /// <summary>
        /// anio, enfoque, encuesta, entidad, valor
        /// No necesita filtro por demografico ya que estos resultados son en base a la estructura GAFM
        /// </summary>
        public static string queryMejores = @"SELECT TOP 10 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                                                        FROM EmpleadoRespuestas
                                                        INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                                                        WHERE 
                                                        Empleado.EstatusEmpleado = 'Activo' AND 
                                                        EstatusEncuesta.Estatus = 'TERMINADA' AND 
                                                        EstatusEncuesta.Anio = {0} AND
                                                        EmpleadoRespuestas.Anio = {0} AND
                                                        Preguntas.IdEnfoque = {1} AND 
                                                        EmpleadoRespuestas.IdEncuesta = {2} AND
                                                        EstatusEncuesta.IdEncuesta = {2} AND
                                                        RespuestaEmpleado like '%e es verdad%' AND 
                                                        Empleado.{3} = '{4}' and 
                                                        Preguntas.IdEstatus = 1
                                                        GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                                                        ORDER BY 3 DESC";

        /// <summary>
        /// anio, enfoque, encuesta, entidad, valor
        /// No necesita filtro por demografico ya que estos resultados son en base a la estructura GAFM
        /// </summary>
        public static string queryPeores = @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                                                        FROM EmpleadoRespuestas
                                                        INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                                                        WHERE 
                                                        Empleado.EstatusEmpleado = 'Activo' AND 
                                                        EstatusEncuesta.Estatus = 'TERMINADA' AND 
                                                        EstatusEncuesta.Anio = {0} AND
                                                        EmpleadoRespuestas.Anio = {0} AND
                                                        Preguntas.IdEnfoque = {1} AND 
                                                        EmpleadoRespuestas.IdEncuesta = {2} AND
                                                        EstatusEncuesta.IdEncuesta = {2} AND
                                                        RespuestaEmpleado like '%e es verdad%' AND 
                                                        Empleado.{3} = '{4}' and
                                                        Preguntas.IdEstatus = 1
                                                        GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                                                        ORDER BY 3 ASC";

        /// <summary>
        /// anio, idencuesta, tipoentidad, valor, enfoque
        /// No necesita filtro por demografico ya que estos resultados son en base a la estructura GAFM
        /// </summary>
        public static string queryCrecimientoByReactivo = @"SELECT 
                                                        Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque, COUNT(200) AS CONTEO 
                                                        FROM EmpleadoRespuestas
                                                        INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
                                                        INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        WHERE 
                                                        EstatusEncuesta.Estatus = 'Terminada' AND 
                                                        Empleado.EstatusEmpleado = 'Activo' AND 
                                                        EstatusEncuesta.Anio = {0} AND
                                                        EmpleadoRespuestas.Anio = {0} AND
                                                        EmpleadoRespuestas.IdEncuesta = {1} AND
                                                        EstatusEncuesta.IdEncuesta = {1} AND
                                                        Empleado.{2} = '{3}' AND 
                                                        Preguntas.IdEnfoque = {4} AND 
                                                        EmpleadoRespuestas.RespuestaEmpleado like '%e es verdad%' and
                                                        Preguntas.IdEstatus = 1
                                                        GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque
                                                        order by Preguntas.IdPregunta";

        /// <summary>
        /// anio, idencuesta, filtro, valorFiltro, idenfoque
        /// se ajustó:
        /// Preguntas.IdPregunta BETWEEN 1 AND 66
        /// por:
        /// Preguntas.IdPreguntaPadre BETWEEN 1 AND 66
        /// </summary>
        public static string queryPromedio66Reactivos = @"SELECT 
                                                        Empleado.CondicionTrabajo, Empleado.GradoAcademico, Empleado.Puesto, Empleado.RangoAntiguedad, Empleado.RangoEdad, Empleado.Sexo,  Empleado.TipoFuncion 
                                                        FROM EmpleadoRespuestas 
                                                        INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado 
                                                        INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta 
                                                        INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                                                        WHERE
                                                        Preguntas.IdPreguntaPadre BETWEEN 1 AND 66 and 
                                                        EmpleadoRespuestas.RespuestaEmpleado like '%e es verdad%' and 
                                                        EstatusEncuesta.Estatus = 'TERMINADA' and 
                                                        Empleado.EstatusEmpleado = 'Activo' AND 
                                                        EstatusEncuesta.Anio = {0} AND
                                                        EmpleadoRespuestas.Anio = {0} AND
                                                        EmpleadoRespuestas.IdEncuesta = {1} AND
                                                        EstatusEncuesta.IdEncuesta = {1} AND
                                                        Empleado.{2} = '{3}' AND 
                                                        Preguntas.IdEnfoque = {4} and 
                                                        Preguntas.IdEstatus = 1";

        /// <summary>
        /// idpregunta, entidad, valorEntidad, anio, idencuesta
        /// deben iterar las preguntas que pertecenen a permanencia
        /// </summary>
        public static string queryPermanencia = @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia 
                                                        FROM EmpleadoRespuestas
                                                        INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                                                        INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                                                        INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                                                        WHERE 
                                                        Empleado.EstatusEmpleado = 'Activo' AND 
                                                        EstatusEncuesta.Estatus = 'TERMINADA' AND 
                                                        EmpleadoRespuestas.IdPregunta = {0} and 
                                                        Empleado.{1} = '{2}' and
                                                        EstatusEncuesta.Anio = {3} AND
                                                        EmpleadoRespuestas.Anio = {3} AND
                                                        EmpleadoRespuestas.IdEncuesta = {4} AND
                                                        EstatusEncuesta.IdEncuesta = {4} AND
                                                        Preguntas.IdEstatus = 1
                                                        GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                                                        ORDER BY 2 DESC";
    }
    public class modelMejoresPeores
    {
        public int IdPregunta { get; set; } = 0;
        public string Pregunta { get; set; } = string.Empty;
        public double Porcentaje { get; set; } = 0;
    }
    public class modelCrecimientoReactivo
    {
        public double DiferenciaActualAnterior { get; set; } = 0;
        public int IdPregunta { get; set; } = 0;
        public double PorcentajeActual { get; set; } = 0;
    }
    public class modelPermanencia
    {
        public int IdPregunta { get; set; } = 0;
        public string Pregunta { get; set; }
        public double Porcentaje { get; set; } = 0;
    }
    public class finalCols
    {
        public string type { get; set; }
        public string value { get; set; }
    }
    public class modelPermanenciaAbandono
    {
        public int IdPregunta { get; set; } = 0;
        public string Pregunta { get; set; } = string.Empty;
        public int Frecuencia { get; set; } = 0;
        public double Porcentaje { get; set; } = 0;
        public int IdEntidad { get; set; } = 0;
        public string EntidadNombre { get; set; } = string.Empty;
        public int IdTipoEntidad { get; set; } = 0;
    }
    public class modelComparativo66React
    {
        public int HC { get; set; } = 0;
        public string Entidad { get; set; } = string.Empty;
        public int Frecuencia { get; set; } = 0;
        public double Porcentaje { get; set; } = 0;
        public double Porcentaje86React { get; set; } = 0;
        public int tipoEntidad { get; set; } = 0;
        public string propiedadDemografica { get; set; } = string.Empty;
    }
}
