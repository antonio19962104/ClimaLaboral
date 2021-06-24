using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Queries
    {
        public string Pregunta { get; set; }
        public double Porcentaje { get; set; }
        public int Frecuencia { get; set; }
        public int IdPregunta { get; set; }
        #region queries 
        public static string QUERY_ENCUESTAS_ESPERADAS =
                            @"SELECT  Empleado.UnidadNegocio FROM Empleado
	                        INNER JOIN EstatusEncuesta on Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
	                        WHERE 
                            Empleado.{0} = '{1}' and EstatusEmpleado = 'Activo'";

        public static string QUERY_ENCUESTAS_TERMINADAS_LVL1Basic =
                            @"SELECT * 
                            FROM EMPLEADO 
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1} and EstatusEncuesta.Anio = {2}";
        public static string QUERY_ENCUESTAS_TERMINADAS_LVL1 =
                            @"SELECT * 
                            FROM EMPLEADO 
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1} and Empleado.UnidadNegocio = {2} AND EstatusEncuesta.Anio = {3}
                            AND Empleado.IdBaseDeDatos = {4}";

        public static string QUERY_ENCUESTAS_TERMINADAS_LVL2 =
                            @"SELECT * 
                            FROM EMPLEADO 
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1} AND Empleado.{2} = {3} and EstatusEncuesta.Anio = {4}
                            and Empleado.IdBaseDeDatos = {5}";

        public static string QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA = //AND EmpleadoRespuestas.IdPregunta NOT IN (36, 37, 38) 
                            @"SELECT TOP 10 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

        public static string QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA = //AND EmpleadoRespuestas.IdPregunta NOT IN (122, 123, 124)
                            @"SELECT TOP 10 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

        public static string QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA =
                            @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 ASC";

        public static string QUERY_PEORES_REACTIVOS_ENFOQUE_AREA =
                            @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 ASC";

        public static string QUERY_INDICADORES_DE_PERMANENCIA_GAFM = 
                            @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta = 177 and Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_INDICADORES_DE_ABANDONO_GAFM = 
                            @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta = 178 and Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM = 
                            @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (EmpleadoRespuestas.IdPregunta = 177 AND Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY EmpleadoRespuestas.RespuestaEmpleado ASC";

        public static string QUERY_COMPARATIVO_DE_ABANDONO_GAFM = 
                            @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (EmpleadoRespuestas.IdPregunta = 178 AND 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND  Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY EmpleadoRespuestas.RespuestaEmpleado ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD =
                            @"SELECT Empleado.RangoAntiguedad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoAntiguedad
                            ORDER BY Empleado.RangoAntiguedad ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD =
                            @"SELECT Empleado.RangoAntiguedad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoAntiguedad
                            ORDER BY Empleado.RangoAntiguedad ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO =
                            @"SELECT Empleado.Sexo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.Sexo
                            ORDER BY Empleado.Sexo ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO =
                            @"SELECT Empleado.Sexo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.Sexo
                            ORDER BY Empleado.Sexo ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO =
                            @"SELECT Empleado.GradoAcademico, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.GradoAcademico
                            ORDER BY Empleado.GradoAcademico ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO =
                            @"SELECT Empleado.GradoAcademico, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.GradoAcademico
                            ORDER BY Empleado.GradoAcademico ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO =
                            @"SELECT Empleado.CondicionTrabajo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.CondicionTrabajo
                            ORDER BY Empleado.CondicionTrabajo ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO =
                            @"SELECT Empleado.CondicionTrabajo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.CondicionTrabajo
                            ORDER BY Empleado.CondicionTrabajo ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD =
                            @"SELECT Empleado.RangoEdad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoEdad
                            ORDER BY Empleado.RangoEdad ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD =
                            @"SELECT Empleado.RangoEdad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoEdad
                            ORDER BY Empleado.RangoEdad ASC";

        //
        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION =
                            @"SELECT Empleado.TipoFuncion, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.TipoFuncion
                            ORDER BY Empleado.TipoFuncion ASC";

        public static string QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION =
                            @"SELECT Empleado.TipoFuncion, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.TipoFuncion
                            ORDER BY Empleado.TipoFuncion ASC";


        /*Comentarios Abiertos*/
        public static string QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1 =
                            @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 173 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            group by EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2 =
                            @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 174 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3 =
                            @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 175 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4 =
                            @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 176 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_MAYOR_CRECIMIENTO_EE =
                            @"SELECT Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque, COUNT(200) AS CONTEO FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            WHERE 
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado	 = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 1 AND 86 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Casi siempre es verdad'
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 1 AND 86 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Frecuentemente es verdad'
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque
                            order by Preguntas.IdPregunta";

        public static string QUERY_MAYOR_CRECIMIENTO_EA =
                            @"SELECT Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque, COUNT(200) AS CONTEO FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            WHERE 
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado	 = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 87 AND 172 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Casi siempre es verdad')
                            OR
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 87 AND 172 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Frecuentemente es verdad')
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque
                            order by Preguntas.IdPregunta";

        public static string QUERY_COMODIN_EE =
                            @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(5000.0) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(36, 37, 38) AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = '{1}')
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(36, 37, 38) AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = '{1}')
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

        public static string QUERY_COMODIN_EA =
                            @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(5000.0) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(122, 123, 124) AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = '{1}')
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(122, 123, 124) AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = '{1}')
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

        public static string QUERY_COMENTARIOS_BY_PALABRA =
                             @"SELECT RespuestaEmpleado, COUNT(20000) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' and Empleado.{0} = '{1}' AND EmpleadoRespuestas.IdPregunta = {2} and EmpleadoRespuestas.RespuestaEmpleado like '% {3} %')
                            group by EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

        public static string QUERY_REPORTE_CORPORATIVO =
                            @"SELECT 
                            EntidadId, EntidadNombre, Enfoque,
                            Promedio66R, NivelConfianza, NivelCompromiso, NivelColaboracion,
                            Creedibilidad, Imparcialidad, Orgullo, Respeto, Companierismo, IdTipoEntidad
                            FROM HistoricoClima 
                            WHERE 
                            EntidadId = {0} AND 
                            EntidadNombre = '{1}' AND 
                            --IdTipoEntidad = {2} AND 
                            Anio = {3} order by Enfoque desc";

        #endregion queries
        /// <summary>
        /// obtener query para encuestas terminadas
        /// </summary>
        /// <param name="filtro">Cualquier dato general del empleado</param>
        /// <param name="valor">Valor del dato general</param>
        /// <param name="UnidadNegocio"></param>
        /// <returns></returns>
        public static string getQueryTerminadas(string filtro, string valor, string UnidadNegocio, int anioActual, int idBD)
        {
            inicializarQueries();
            QUERY_ENCUESTAS_TERMINADAS_LVL1 = QUERY_ENCUESTAS_TERMINADAS_LVL1.Replace("{0}", filtro);
            QUERY_ENCUESTAS_TERMINADAS_LVL1 = QUERY_ENCUESTAS_TERMINADAS_LVL1.Replace("{1}", "'" + valor + "'");
            QUERY_ENCUESTAS_TERMINADAS_LVL1 = QUERY_ENCUESTAS_TERMINADAS_LVL1.Replace("{2}", "'" + UnidadNegocio + "'");
            QUERY_ENCUESTAS_TERMINADAS_LVL1 = QUERY_ENCUESTAS_TERMINADAS_LVL1.Replace("{3}", Convert.ToString(anioActual));
            QUERY_ENCUESTAS_TERMINADAS_LVL1 = QUERY_ENCUESTAS_TERMINADAS_LVL1.Replace("{4}", Convert.ToString(idBD));
            validaQuery(QUERY_ENCUESTAS_TERMINADAS_LVL1, filtro, valor, new StackTrace());
            return QUERY_ENCUESTAS_TERMINADAS_LVL1;
        }
        public static string getQueryTerminadas(string filtro, string valor, int anioActual)
        {
            inicializarQueries();
            QUERY_ENCUESTAS_TERMINADAS_LVL1Basic = QUERY_ENCUESTAS_TERMINADAS_LVL1Basic.Replace("{0}", filtro);
            QUERY_ENCUESTAS_TERMINADAS_LVL1Basic = QUERY_ENCUESTAS_TERMINADAS_LVL1Basic.Replace("{1}", "'" + valor + "'");
            QUERY_ENCUESTAS_TERMINADAS_LVL1Basic = QUERY_ENCUESTAS_TERMINADAS_LVL1Basic.Replace("{2}", Convert.ToString(anioActual));
            validaQuery(QUERY_ENCUESTAS_TERMINADAS_LVL1Basic, filtro, valor, new StackTrace());
            return QUERY_ENCUESTAS_TERMINADAS_LVL1Basic;
        }
        /// <summary>
        /// obtener query para encuestas terminadas segun un filtro de datos generales y una entidad moral del grupo
        /// </summary>
        /// <param name="filtro">Cualquier dato general del empleado</param>
        /// <param name="valor">Valor del dato general</param>
        /// <param name="filtroEntidadAFM">Jerarquia de la entidad moral</param>
        /// <param name="valorEntidadAFM">Valor de la entidad moral</param>
        /// <returns></returns>
        public static string getQueryTerminadas(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM, int anioActual, int IdBD)
        {
            inicializarQueries();
            QUERY_ENCUESTAS_TERMINADAS_LVL2 = QUERY_ENCUESTAS_TERMINADAS_LVL2.Replace("{0}", filtro);
            QUERY_ENCUESTAS_TERMINADAS_LVL2 = QUERY_ENCUESTAS_TERMINADAS_LVL2.Replace("{1}", "'" + valor + "'");
            QUERY_ENCUESTAS_TERMINADAS_LVL2 = QUERY_ENCUESTAS_TERMINADAS_LVL2.Replace("{2}", filtroEntidadAFM);
            QUERY_ENCUESTAS_TERMINADAS_LVL2 = QUERY_ENCUESTAS_TERMINADAS_LVL2.Replace("{3}", "'" + valorEntidadAFM + "'");
            QUERY_ENCUESTAS_TERMINADAS_LVL2 = QUERY_ENCUESTAS_TERMINADAS_LVL2.Replace("{4}", Convert.ToString(anioActual));
            QUERY_ENCUESTAS_TERMINADAS_LVL2 = QUERY_ENCUESTAS_TERMINADAS_LVL2.Replace("{5}", Convert.ToString(IdBD));
            validaQuery(QUERY_ENCUESTAS_TERMINADAS_LVL2, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_ENCUESTAS_TERMINADAS_LVL2;
        }
        public static string getQueryMejoresEE(string filtro, string valor, int anioActual, int IdBD)
        {
            inicializarQueries();
            QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{0}", filtro);
            QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{1}", "'" + valor.ToUpper() + "'");
            QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{2}", Convert.ToString(anioActual));
            QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{3}", Convert.ToString(IdBD));
            validaQuery(QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA, filtro, valor, new StackTrace());
            return QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA;
        }
        public static string getQueryMejoresEA(string filtro, string valor, int anioActual, int IdBD)
        {
            inicializarQueries();
            QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA = QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA.Replace("{0}", filtro);
            QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA = QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA.Replace("{1}", "'" + valor.ToUpper() + "'" );
            QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA = QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA.Replace("{2}", Convert.ToString(anioActual));
            QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA = QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA.Replace("{3}", Convert.ToString(IdBD));
            validaQuery(QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA, filtro, valor, new StackTrace());
            return QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA;
        }
        public static string getQueryPeoresEE(string filtro, string valor, int anioActual, int IdBD)
        {
            inicializarQueries();
            QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{0}", filtro);
            QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{1}", "'" + valor.ToUpper() + "'");
            QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{2}", Convert.ToString(anioActual));
            QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA = QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA.Replace("{3}", Convert.ToString(IdBD));
            validaQuery(QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA, filtro, valor, new StackTrace());
            return QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA;
        }
        public static string getQueryPeoresEA(string filtro, string valor, int anioActual, int IdBD)
        {
            inicializarQueries();
            QUERY_PEORES_REACTIVOS_ENFOQUE_AREA = QUERY_PEORES_REACTIVOS_ENFOQUE_AREA.Replace("{0}", filtro);
            QUERY_PEORES_REACTIVOS_ENFOQUE_AREA = QUERY_PEORES_REACTIVOS_ENFOQUE_AREA.Replace("{1}", "'" + valor.ToUpper() + "'");
            QUERY_PEORES_REACTIVOS_ENFOQUE_AREA = QUERY_PEORES_REACTIVOS_ENFOQUE_AREA.Replace("{2}", Convert.ToString(anioActual));
            QUERY_PEORES_REACTIVOS_ENFOQUE_AREA = QUERY_PEORES_REACTIVOS_ENFOQUE_AREA.Replace("{3}", Convert.ToString(IdBD));
            validaQuery(QUERY_PEORES_REACTIVOS_ENFOQUE_AREA, filtro, valor, new StackTrace());
            return QUERY_PEORES_REACTIVOS_ENFOQUE_AREA;
        }
        /*Seccion queries indicadores de permanencia*/
        public static string getQueryPermanenciaAFM(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_INDICADORES_DE_PERMANENCIA_GAFM = QUERY_INDICADORES_DE_PERMANENCIA_GAFM.Replace("{0}", filtro);
            QUERY_INDICADORES_DE_PERMANENCIA_GAFM = QUERY_INDICADORES_DE_PERMANENCIA_GAFM.Replace("{1}", "'" + valor + "'");
            validaQuery(QUERY_INDICADORES_DE_PERMANENCIA_GAFM, filtro, valor, new StackTrace());
            return QUERY_INDICADORES_DE_PERMANENCIA_GAFM;
        }
        /*Seccion queries indicadores Abandono*/
        public static string getQueryAbandonoAFM(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_INDICADORES_DE_ABANDONO_GAFM = QUERY_INDICADORES_DE_ABANDONO_GAFM.Replace("{0}", filtro);
            QUERY_INDICADORES_DE_ABANDONO_GAFM = QUERY_INDICADORES_DE_ABANDONO_GAFM.Replace("{1}", "'" + valor + "'");
            validaQuery(QUERY_INDICADORES_DE_ABANDONO_GAFM, filtro, valor, new StackTrace());
            return QUERY_INDICADORES_DE_ABANDONO_GAFM;
        }
        /*Seccion queries comparativo*/
        public static string getQueryComparativoPermanencia(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM = QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM.Replace("{0}", filtro);
            QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM = QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM.Replace("{1}", "'" + valor + "'");
            validaQuery(QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM, filtro, valor, new StackTrace());
            return QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM;
        }
        public static string getQueryComparativoAbandono(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_COMPARATIVO_DE_ABANDONO_GAFM = QUERY_COMPARATIVO_DE_ABANDONO_GAFM.Replace("{0}", filtro);
            QUERY_COMPARATIVO_DE_ABANDONO_GAFM = QUERY_COMPARATIVO_DE_ABANDONO_GAFM.Replace("{1}", "'" + valor + "'");
            validaQuery(QUERY_COMPARATIVO_DE_ABANDONO_GAFM, filtro, valor, new StackTrace());
            return QUERY_COMPARATIVO_DE_ABANDONO_GAFM;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueEmpresaByAntiguedad(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD.Replace("{0}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD.Replace("{1}", "'" + valorEntidadAFM + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD.Replace("{2}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD.Replace("{3}", "'" +  valor + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueAreaByAntiguedad(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueEmpresaByGenero(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueAreaByGenero(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueEmpresaByGradoAcademico(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueAreaByGradoAcademico(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueEmpresaByCondicionTrabajo(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueAreaByCondicionTrabajo(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO;
        }


        public static string getQueryResultadosGenerales66ReactivosEnfoqueEmpresaByRangoEdad(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueAreaByRangoEdad(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD;
        }

        public static string getQueryResultadosGenerales66ReactivosEnfoqueEmpresaByFuncion(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION;
        }
        public static string getQueryResultadosGenerales66ReactivosEnfoqueAreaByFuncion(string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM)
        {
            inicializarQueries();
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION.Replace("{0}", filtro);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION.Replace("{1}", "'" + valor + "'");
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION.Replace("{2}", filtroEntidadAFM);
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION = QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION.Replace("{3}", "'" + valorEntidadAFM + "'");
            validaQuery(QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION, filtro, valor, filtroEntidadAFM, valorEntidadAFM, new StackTrace());
            return QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION;
        }


        public static string getQueryComodinEE(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_COMODIN_EE = QUERY_COMODIN_EE.Replace("{0}", filtro);
            QUERY_COMODIN_EE = QUERY_COMODIN_EE.Replace("{1}", valor);
            validaQuery(QUERY_COMODIN_EE, filtro, valor, new StackTrace());
            return QUERY_COMODIN_EE;
        }
        public static string getQueryComodinEA(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_COMODIN_EA = QUERY_COMODIN_EA.Replace("{0}", filtro);
            QUERY_COMODIN_EA = QUERY_COMODIN_EA.Replace("{1}", valor);
            validaQuery(QUERY_COMODIN_EA, filtro, valor, new StackTrace());
            return QUERY_COMODIN_EA;
        }
        
        /*Nube*/
        public static string getQueryReporteComentariosAbiertos(ML.ReporteD4U model, int anioActual)
        {
            switch (model.IdPregunta)
            {
                case 1:
                    return getQueryReporteComentariosAbiertosPregunta1(model, anioActual);
                    break;
                case 2:
                    return getQueryReporteComentariosAbiertosPregunta2(model, anioActual);
                    break;
                case 3:
                    return getQueryReporteComentariosAbiertosPregunta3(model, anioActual);
                    break;
                case 4:
                    return getQueryReporteComentariosAbiertosPregunta4(model, anioActual);
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        public static string getQueryReporteComentariosAbiertosPregunta1(ML.ReporteD4U model, int anioActual)
        {
            inicializarQueries();
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1.Replace("{0}", model.filtro);
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1.Replace("{1}", "'" + model.filtroValor + "'");
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1.Replace("{2}", Convert.ToString(anioActual));
            validaQuery(QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1, model.filtro, model.filtroValor, new StackTrace());
            return QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1;
        }
        public static string getQueryReporteComentariosAbiertosPregunta2(ML.ReporteD4U model, int anioActual)
        {
            inicializarQueries();
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2.Replace("{0}", model.filtro);
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2.Replace("{1}", "'" + model.filtroValor + "'");
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2.Replace("{2}", Convert.ToString(anioActual));
            validaQuery(QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2, model.filtro, model.filtroValor, new StackTrace());
            return QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2;
        }
        public static string getQueryReporteComentariosAbiertosPregunta3(ML.ReporteD4U model, int anioActual)
        {
            inicializarQueries();
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3.Replace("{0}", model.filtro);
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3.Replace("{1}", "'" + model.filtroValor + "'");
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3.Replace("{2}", Convert.ToString(anioActual));
            validaQuery(QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3, model.filtro, model.filtroValor, new StackTrace());
            return QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3;
        }
        public static string getQueryReporteComentariosAbiertosPregunta4(ML.ReporteD4U model, int anioActual)
        {
            inicializarQueries();
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4.Replace("{0}", model.filtro);
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4.Replace("{1}", "'" + model.filtroValor + "'");
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4 = QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4.Replace("{2}", Convert.ToString(anioActual));
            validaQuery(QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4, model.filtro, model.filtroValor, new StackTrace());
            return QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4;
        }

        public static string getQueryReactivosMayorCrecimientoEE(string filtro, string valor, int anioActual, int IdBD)
        {
            //QUERY_MAYOR_CRECIMIENTO_EE
            inicializarQueries();
            QUERY_MAYOR_CRECIMIENTO_EE = QUERY_MAYOR_CRECIMIENTO_EE.Replace("{0}", filtro);
            QUERY_MAYOR_CRECIMIENTO_EE = QUERY_MAYOR_CRECIMIENTO_EE.Replace("{1}", valor);
            QUERY_MAYOR_CRECIMIENTO_EE = QUERY_MAYOR_CRECIMIENTO_EE.Replace("{2}", Convert.ToString(anioActual));
            if (IdBD == 0)
                QUERY_MAYOR_CRECIMIENTO_EE = QUERY_MAYOR_CRECIMIENTO_EE.Replace("{3}", "is not null");
            else
                QUERY_MAYOR_CRECIMIENTO_EE = QUERY_MAYOR_CRECIMIENTO_EE.Replace("{3}", Convert.ToString(IdBD));
            validaQuery(QUERY_MAYOR_CRECIMIENTO_EE, filtro, valor, new StackTrace());
            return QUERY_MAYOR_CRECIMIENTO_EE;
        }
        public static string getQueryReactivosMayorCrecimientoEA(string filtro, string valor)
        {
            //QUERY_MAYOR_CRECIMIENTO_EE
            inicializarQueries();
            QUERY_MAYOR_CRECIMIENTO_EA = QUERY_MAYOR_CRECIMIENTO_EA.Replace("{0}", filtro);
            QUERY_MAYOR_CRECIMIENTO_EA = QUERY_MAYOR_CRECIMIENTO_EA.Replace("{1}", valor);
            validaQuery(QUERY_MAYOR_CRECIMIENTO_EA, filtro, valor, new StackTrace());
            return QUERY_MAYOR_CRECIMIENTO_EA;
        }
        public static string getQueryEsperadas(string filtro, string valor)
        {
            inicializarQueries();
            QUERY_ENCUESTAS_ESPERADAS = QUERY_ENCUESTAS_ESPERADAS.Replace("{0}", filtro);
            QUERY_ENCUESTAS_ESPERADAS = QUERY_ENCUESTAS_ESPERADAS.Replace("{1}", valor);
            validaQuery(QUERY_ENCUESTAS_ESPERADAS, filtro, valor, new StackTrace());
            return QUERY_ENCUESTAS_ESPERADAS;
        }
        public static string getQueryComentariosByPalabra(string filtro, string valor, string idPregunta, string palabra)
        {
            // (Empleado.{0} = '{1}' AND EmpleadoRespuestas.IdPregunta = {2} and EmpleadoRespuestas.RespuestaEmpleado like '%{3}%')
            inicializarQueries();
            QUERY_COMENTARIOS_BY_PALABRA = QUERY_COMENTARIOS_BY_PALABRA.Replace("{0}", filtro);
            QUERY_COMENTARIOS_BY_PALABRA = QUERY_COMENTARIOS_BY_PALABRA.Replace("{1}", valor);
            QUERY_COMENTARIOS_BY_PALABRA = QUERY_COMENTARIOS_BY_PALABRA.Replace("{2}", idPregunta);
            QUERY_COMENTARIOS_BY_PALABRA = QUERY_COMENTARIOS_BY_PALABRA.Replace("{3}", palabra);
            validaQuery(QUERY_COMENTARIOS_BY_PALABRA, filtro, valor, idPregunta, palabra, new StackTrace());
            return QUERY_COMENTARIOS_BY_PALABRA;
        }

        //Reporte Corporativo
        //aHistorico.EntidadId, aHistorico.EntidadNombre, aHistorico.IdTipoEntidad, aHistorico.Anio
        public static string getQueryReporteCorpo(int? entidadId, string entidadNombre, int? idTipoEntidad, int? AnioActual)
        {
            inicializarQueries();
            //QUERY_REPORTE_CORPORATIVO =
            //                @"SELECT * FROM HistoricoClima 
            //                WHERE 
            //                EntidadId = {0} AND 
            //                EntidadNombre = '{1}' AND 
            //                IdTipoEntidad = {2} AND 
            //                Anio = {3} AND 
            //                Enfoque = '{4}'";
            QUERY_REPORTE_CORPORATIVO = QUERY_REPORTE_CORPORATIVO.Replace("{0}", Convert.ToString(entidadId));
            QUERY_REPORTE_CORPORATIVO = QUERY_REPORTE_CORPORATIVO.Replace("{1}", entidadNombre);
            //QUERY_REPORTE_CORPORATIVO = QUERY_REPORTE_CORPORATIVO.Replace("{2}", Convert.ToString(idTipoEntidad));
            QUERY_REPORTE_CORPORATIVO = QUERY_REPORTE_CORPORATIVO.Replace("{3}", Convert.ToString(AnioActual));
            validaQuery(QUERY_REPORTE_CORPORATIVO, entidadNombre, Convert.ToString(entidadId), new StackTrace());
            return QUERY_REPORTE_CORPORATIVO;
        }


        /*Escribir Log de validacion de queries*/
        public static bool validaQuery(string query, string filtro, string valor, StackTrace st)
        {
            if (!query.Contains(filtro) || !query.Contains(valor))
            {
                writteLog("Falló al crear el query con los parametros correctos", st.GetFrame(0).GetMethod().Name);
                return false;
            }
            if (query.Contains("{") || query.Contains("}"))
                writteLog("El query ha fallado: " + query + Environment.NewLine, "ANTONIOG");
            writteLogQueries(query, st.GetFrame(0).GetMethod().Name, filtro, valor);
            return true;
        }
        public static bool validaQuery(string query, string filtro, string valor, string filtroEntidadAFM, string valorEntidadAFM, StackTrace st)
        {
            if (!query.Contains(filtro) || !query.Contains(valor) || !query.Contains(filtroEntidadAFM) || !query.Contains(valorEntidadAFM))
            {
                writteLog("Falló al crear el query con los parametros correctos", st.GetFrame(0).GetMethod().Name);
                return false;
            }
            if (query.Contains("{") || query.Contains("}"))
                writteLog("El query ha fallado: " + query + Environment.NewLine, "ANTONIOG");
            writteLogQueries(query, st.GetFrame(0).GetMethod().Name, filtro, valor);
            return true;
        }
        public static bool writteLog(string excepcionMessage, string metodo)
        {
            try
            {
                var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogReporteoClima.log";
                var fullPath1 = Path.GetFullPath(path1);
                if (!File.Exists(fullPath1))
                {
                    string createText = "Log Create At " + DateTime.Now + Environment.NewLine;
                    File.WriteAllText(fullPath1, createText);
                }
                string appendText1 = "Metodo: " + metodo + ". Excepcion: " + excepcionMessage + " " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(fullPath1, appendText1);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool writteLogQueries(string query, string metodo, string filtro, string valor)
        {
            try
            {
                if (metodo.Contains("getQueryMejoresE"))
                {
                    //Mejores
                    //var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogQueries" + filtro + "_" + valor + ".sql";
                    //var fullPath1 = Path.GetFullPath(path1);
                    //if (!File.Exists(fullPath1))
                    //{
                    //    string createText = "--Log Create At " + DateTime.Now + Environment.NewLine;
                    //    File.WriteAllText(fullPath1, createText);
                    //}
                    //string appendText1 = Environment.NewLine + "--Query: " + DateTime.Now + " Method: " + metodo + Environment.NewLine;
                    //appendText1 += query + Environment.NewLine;
                    //File.AppendAllText(fullPath1, appendText1);
                    ////Peores
                    //query = query.Replace("DESC", "ASC");
                    //metodo = metodo.Replace("getQueryMejoresE", "getQueryPeoresE");
                    //if (!File.Exists(fullPath1))
                    //{
                    //    string createText = "--Log Create At " + DateTime.Now + Environment.NewLine;
                    //    File.WriteAllText(fullPath1, createText);
                    //}
                    //string appendText2 = Environment.NewLine + "--Query: " + DateTime.Now + " Method: " + metodo + Environment.NewLine;
                    //appendText2 += query + Environment.NewLine;
                    //File.AppendAllText(fullPath1, appendText2);
                }
                else
                {
                    //var path1 = @"\\10.5.2.101\RHDiagnostics\log\LogQueries" + filtro + "_" + valor + ".sql";
                    //var fullPath1 = Path.GetFullPath(path1);
                    //if (!File.Exists(fullPath1))
                    //{
                    //    string createText = "--Log Create At " + DateTime.Now + Environment.NewLine;
                    //    File.WriteAllText(fullPath1, createText);
                    //}
                    //string appendText1 = Environment.NewLine + "--Query: " + DateTime.Now + " Method: " + metodo + Environment.NewLine;
                    //appendText1 += query + Environment.NewLine;
                    //File.AppendAllText(fullPath1, appendText1);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /*Inicializar queries*/
        public static void inicializarQueries()
        {
            #region queries 
            QUERY_ENCUESTAS_ESPERADAS =
                                @"SELECT  Empleado.UnidadNegocio FROM Empleado
	                        INNER JOIN EstatusEncuesta on Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
	                        WHERE 
                            Empleado.{0} = '{1}' and EstatusEmpleado = 'Activo'";

            QUERY_ENCUESTAS_TERMINADAS_LVL1Basic =
                                @"SELECT * 
                            FROM EMPLEADO 
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1} and EstatusEncuesta.Anio = {2}";
            QUERY_ENCUESTAS_TERMINADAS_LVL1 =
                                @"SELECT * 
                            FROM EMPLEADO 
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1} and Empleado.UnidadNegocio = {2} AND EstatusEncuesta.Anio = {3}
                            AND Empleado.IdBaseDeDatos = {4}";

            QUERY_ENCUESTAS_TERMINADAS_LVL2 =
                                @"SELECT * 
                            FROM EMPLEADO 
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1} AND Empleado.{2} = {3} and EstatusEncuesta.Anio = {4}
                            and Empleado.IdBaseDeDatos = {5}";

            QUERY_MEJORES_REACTIVOS_ENFOQUE_EMPRESA = //AND EmpleadoRespuestas.IdPregunta NOT IN (36, 37, 38) 
                                @"SELECT TOP 10 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

            QUERY_MEJORES_REACTIVOS_ENFOQUE_AREA = //AND EmpleadoRespuestas.IdPregunta NOT IN (122, 123, 124)
                                @"SELECT TOP 10 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

            QUERY_PEORES_REACTIVOS_ENFOQUE_EMPRESA =
                                @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 1 AND 86 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 ASC";

            QUERY_PEORES_REACTIVOS_ENFOQUE_AREA =
                                @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta  BETWEEN 87 AND 172 AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = {1}
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 ASC";

            QUERY_INDICADORES_DE_PERMANENCIA_GAFM =
                                @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta = 177 and Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_INDICADORES_DE_ABANDONO_GAFM =
                                @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta = 178 and Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_COMPARATIVO_DE_PERMANENCIA_GAFM =
                                @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (EmpleadoRespuestas.IdPregunta = 177 AND Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY EmpleadoRespuestas.RespuestaEmpleado ASC";

            QUERY_COMPARATIVO_DE_ABANDONO_GAFM =
                                @"SELECT EmpleadoRespuestas.RespuestaEmpleado, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE 
                            (EmpleadoRespuestas.IdPregunta = 178 AND 
                            Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND  Empleado.{0} = {1})
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY EmpleadoRespuestas.RespuestaEmpleado ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_ANTIGUEDAD =
                                @"SELECT Empleado.RangoAntiguedad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoAntiguedad
                            ORDER BY Empleado.RangoAntiguedad ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_ANTIGUEDAD =
                                @"SELECT Empleado.RangoAntiguedad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoAntiguedad
                            ORDER BY Empleado.RangoAntiguedad ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GENERO =
                                @"SELECT Empleado.Sexo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.Sexo
                            ORDER BY Empleado.Sexo ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GENERO =
                                @"SELECT Empleado.Sexo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.Sexo
                            ORDER BY Empleado.Sexo ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_GRADO_ACADEMICO =
                                @"SELECT Empleado.GradoAcademico, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.GradoAcademico
                            ORDER BY Empleado.GradoAcademico ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_GRADO_ACADEMICO =
                                @"SELECT Empleado.GradoAcademico, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.GradoAcademico
                            ORDER BY Empleado.GradoAcademico ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_CONDICION_TRABAJO =
                                @"SELECT Empleado.CondicionTrabajo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.CondicionTrabajo
                            ORDER BY Empleado.CondicionTrabajo ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_CONDICION_TRABAJO =
                                @"SELECT Empleado.CondicionTrabajo, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.CondicionTrabajo
                            ORDER BY Empleado.CondicionTrabajo ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_RANGO_EDAD =
                                @"SELECT Empleado.RangoEdad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoEdad
                            ORDER BY Empleado.RangoEdad ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_RANGO_EDAD =
                                @"SELECT Empleado.RangoEdad, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.RangoEdad
                            ORDER BY Empleado.RangoEdad ASC";

            //
            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_EMPRESA_BY_FUNCION =
                                @"SELECT Empleado.TipoFuncion, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 1 AND 66 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.TipoFuncion
                            ORDER BY Empleado.TipoFuncion ASC";

            QUERY_RESULTADOS_GENERALES_66_REACTIVOS_ENFOQUE_AREA_BY_FUNCION =
                                @"SELECT Empleado.TipoFuncion, COUNT(200) AS Frecuencia
                            FROM EmpleadoRespuestas
                            left JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            left JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            left JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado
                            WHERE
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Casi siempre es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'TERMINADA' AND Empleado.EstatusEmpleado = 'ACTIVO' AND Preguntas.IdPregunta BETWEEN 87 AND 152 AND EmpleadoRespuestas.RespuestaEmpleado = 'Frecuentemente es verdad'  AND Empleado.{0} = {1} AND Empleado.{2} = {3})
                            GROUP BY Empleado.TipoFuncion
                            ORDER BY Empleado.TipoFuncion ASC";


            /*Comentarios Abiertos*/
            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_1 =
                                @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 173 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            group by EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_2 =
                                @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 174 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_3 =
                                @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 175 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_RESULTADOS_COMENTARIOS_ABIERTOS_4 =
                                @"SELECT TOP 85 RespuestaEmpleado, COUNT(200) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            WHERE 
                            (Empleado.{0} = {1} AND EmpleadoRespuestas.IdPregunta = 176 and EmpleadoRespuestas.Anio = {2} and RespuestaEmpleado != 'NADA' and RespuestaEmpleado != 'SIN COMENTARIOS' and RespuestaEmpleado != 'NINGUNA' and RespuestaEmpleado != 'SIN COMENTARIO' and RespuestaEmpleado != 'NINGUNO' and RespuestaEmpleado != 'NO' and RespuestaEmpleado != 'TODO BIEN' and RespuestaEmpleado != 'SI' and RespuestaEmpleado != 'NO APLICA' and RespuestaEmpleado != 'NO LO SE' and RespuestaEmpleado != 'TODAS' and RespuestaEmpleado != 'N/A' and RespuestaEmpleado != '.' and RespuestaEmpleado != 'NO SE' and RespuestaEmpleado != 'TODO ESTA BIEN' and RespuestaEmpleado != 'NA' and RespuestaEmpleado != 'nada.' and RespuestaEmpleado != 'no cambiaria nada' and RespuestaEmpleado != '...' and RespuestaEmpleado != '....' and RespuestaEmpleado != '..' and RespuestaEmpleado != 'X' and RespuestaEmpleado != 'NOSE' and RespuestaEmpleado != 'ninguna.' and RespuestaEmpleado != 'No tiene' and RespuestaEmpleado != 'a' and RespuestaEmpleado != 'SIN COMENTAR' and RespuestaEmpleado != 'sin comentarios.' and RespuestaEmpleado != 'NADA TODO ESTA BIEN' and RespuestaEmpleado != '-' and RespuestaEmpleado != 'en nada' and RespuestaEmpleado != 'S/C' and RespuestaEmpleado != 'NO LOSE' and RespuestaEmpleado != 'si con mentario' AND RespuestaEmpleado != 'desconosco' AND RespuestaEmpleado != 'no se.')
                            GROUP BY EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_MAYOR_CRECIMIENTO_EE =
                                @"SELECT Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque, COUNT(200) AS CONTEO FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            WHERE 
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado	 = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 1 AND 86 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Casi siempre es verdad'
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            OR
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 1 AND 86 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Frecuentemente es verdad'
                            and EmpleadoRespuestas.Anio = {2} and Empleado.IdBaseDeDatos = {3})
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque
                            order by Preguntas.IdPregunta";

            QUERY_MAYOR_CRECIMIENTO_EA =
                                @"SELECT Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque, COUNT(200) AS CONTEO FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN EstatusEncuesta ON Empleado.IdEmpleado = EstatusEncuesta.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            WHERE 
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado	 = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 87 AND 172 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Casi siempre es verdad')
                            OR
                            (EstatusEncuesta.Estatus = 'Terminada' AND Empleado.EstatusEmpleado = 'Activo' AND Empleado.{0} = '{1}' AND Preguntas.IdPregunta BETWEEN 87 AND 172 AND EmpleadoRespuestas.RespuestaEmpleado  = 'Frecuentemente es verdad')
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta, Preguntas.Enfoque
                            order by Preguntas.IdPregunta";

            QUERY_COMODIN_EE =
                                @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(5000.0) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(36, 37, 38) AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = '{1}')
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(36, 37, 38) AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = '{1}')
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

            QUERY_COMODIN_EA =
                                @"SELECT TOP 5 Preguntas.IdPregunta, Preguntas.Pregunta, COUNT(5000.0) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN Preguntas ON EmpleadoRespuestas.IdPregunta = Preguntas.IdPregunta
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(122, 123, 124) AND RespuestaEmpleado = 'Casi siempre es verdad' and Empleado.{0} = '{1}')
                            OR
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' AND EmpleadoRespuestas.IdPregunta in(122, 123, 124) AND RespuestaEmpleado = 'Frecuentemente es verdad' and Empleado.{0} = '{1}')
                            GROUP BY Preguntas.IdPregunta, Preguntas.Pregunta
                            ORDER BY 3 DESC";

            QUERY_COMENTARIOS_BY_PALABRA =
                                 @"SELECT RespuestaEmpleado, COUNT(20000) AS mas_popular
                            FROM EmpleadoRespuestas
                            INNER JOIN Empleado ON EmpleadoRespuestas.IdEmpleado = Empleado.IdEmpleado
                            INNER JOIN EstatusEncuesta ON EmpleadoRespuestas.IdEmpleado = EstatusEncuesta.IdEmpleado 
                            WHERE 
                            (Empleado.EstatusEmpleado = 'Activo' AND EstatusEncuesta.Estatus = 'TERMINADA' and Empleado.{0} = '{1}' AND EmpleadoRespuestas.IdPregunta = {2} and EmpleadoRespuestas.RespuestaEmpleado like '% {3} %')
                            group by EmpleadoRespuestas.RespuestaEmpleado
                            ORDER BY 2 DESC";

            QUERY_REPORTE_CORPORATIVO =
                                @"SELECT 
                            EntidadId, EntidadNombre, Enfoque,
                            Promedio66R, NivelConfianza, NivelCompromiso, NivelColaboracion,
                            Creedibilidad, Imparcialidad, Orgullo, Respeto, Companierismo, IdTipoEntidad
                            FROM HistoricoClima 
                            WHERE 
                            EntidadId = {0} AND 
                            EntidadNombre = '{1}' AND 
                            --IdTipoEntidad = {2} AND 
                            Anio = {3} order by Enfoque desc";

            #endregion queries



            QUERY_REPORTE_CORPORATIVO =
                            @"SELECT 
                            EntidadId, EntidadNombre, Enfoque,
                            Promedio66R, NivelConfianza, NivelCompromiso, NivelColaboracion,
                            Creedibilidad, Imparcialidad, Orgullo, Respeto, Companierismo, IdTipoEntidad
                            FROM HistoricoClima 
                            WHERE 
                            EntidadId = {0} AND 
                            EntidadNombre = '{1}' AND 
                            --IdTipoEntidad = {2} AND 
                            Anio = {3} order by Enfoque desc";
        }
    }
}
