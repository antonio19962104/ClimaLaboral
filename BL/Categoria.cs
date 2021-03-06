using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.ComponentModel;


namespace BL
{
    public class Categoria
    {
        public static ML.Result getAllCategorias(string idUsuarioAdmin)
        {
            ML.Result result = new ML.Result();
            DataSet ds = new DataSet();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    string SqlQuery = "SELECT S.IdCategoria ,STUFF((SELECT R.Nombre + ' - ' + S.Nombre FROM Categoria R WHERE R.IdCategoria = S.IdPadre  ORDER BY R.Nombre FOR XML PATH('')),1,0,'') as Nombre, S.IdPadre FROM Categoria S Where S.IdPadre IS NOT NULL and S.Estatus = 1 and S.IdCategoria < 40 or S.IdAdminCreate = {0}";
                    //string SqlQuery = "SELECT S.IdCategoria,STUFF((SELECT R.Nombre + ' - ' + S.Nombre FROM Categoria R WHERE R.IdCategoria = S.IdPadre ORDER BY R.Nombre FOR XML PATH('')),1,0,'') as Nombre,S.IdPadre, case when(s.IdPadre is null or s.IdCategoria = s.IdPadre) then S.Nombre end Descripcion FROM Categoria S Where  S.Estatus = 1 and S.IdCategoria < 40 or S.IdAdminCreate = {0} ";
                    SqlQuery = SqlQuery.Replace("{0}", idUsuarioAdmin);
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                    {
                        try
                        {
                            connection.Open();
                            SqlDataAdapter dat = new SqlDataAdapter(SqlQuery, connection);
                            dat.Fill(ds,"dat");
                            connection.Close();                           
                        }
                        catch (Exception exe)
                        {
                            Console.WriteLine(exe.Message);
                        }
                    }
                    //Itera ds
                    result.EditaEncuesta = new ML.Encuesta();
                    result.EditaEncuesta.ListCategorias = new List<ML.Categoria>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ML.Categoria cat = new ML.Categoria();
                        cat.IdCategoria = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                        cat.Nombre = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                        cat.IdPadre = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]);
                        result.EditaEncuesta.ListCategorias.Add(cat);
                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();                
            }
            return result;
            

        }
        public static ML.Result getAllCatIdUsuario(string idUsuario) {
            ML.Result result = new ML.Result();
            try
            {
                int idAdmin =Convert.ToInt32(idUsuario);
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Categoria.Select(o => o).Where(o => o.IdAdminCreate == idAdmin && o.IdPadre == null && o.IdCategoria == o.IdPadre).ToList();
                    if (query.Count() > 0)
                    {
                        foreach (var item in query)
                        {
                            ML.Categoria cat = new ML.Categoria();
                            cat.IdCategoria = item.IdCategoria;
                            cat.Nombre = item.Nombre;
                            cat.IdPadre = (Int32)item.IdPadre;                            
                        }



                    }
                }
            }
            catch (Exception aE)
            {
                result.Correct = false;
                result.ErrorMessage = aE.Message.ToString();
            }


            return result;
        }
        public static List<ML.Categoria> getAllConfiguration(int idEncuesta)
        {
            var lists = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionSubcategoriaPorCategoria.Select(o => o).Where(o => o.IdEncuesta==idEncuesta).ToList();
                    if (query.Count() > 0)
                    {
                        var sinDuplicados = query.Select(o => o.IdCategoria).Distinct();
                        foreach (var item in sinDuplicados)
                        {
                            ML.Categoria cat = getCatByIdCat((Int32)item);                                                        
                            cat.Subcategorias = new List<ML.Categoria>();
                            cat.Subcategorias = getSubCatByIdCat(idEncuesta,(Int32)item);
                            lists.Add(cat);
                        }
                    }
                }
                return lists.OrderBy(o =>o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }
            
        }

        //Opcion 2 Columna 2
        public static List<ML.Categoria> getAllConfiguration(int idEncuesta, int idCat)
        {
            var lists = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionSubcategoriaPorCategoria.Select(o => o).Where(o => o.IdEncuesta == idEncuesta && o.IdCategoria == idCat).ToList();
                    if (query.Count() > 0)
                    {
                        var sinDuplicados = query.Select(o => o.IdCategoria).Distinct();
                        foreach (var item in sinDuplicados)
                        {
                            ML.Categoria cat = getCatByIdCat((Int32)item);
                            cat.Subcategorias = new List<ML.Categoria>();
                            cat.Subcategorias = getSubCatByIdCat(idEncuesta, (Int32)item);
                            lists.Add(cat);
                        }
                    }
                }
                return lists.OrderBy(o => o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }

        }

        //columna 3
        public static List<ML.Categoria> getAllConfigurationPreSubCat(int idEncuesta)
        {
            var lists = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionPreguntaPorSubcategoria.Select(o => o).Where(o => o.IdEncuesta == idEncuesta).ToList();
                    if (query.Count() > 0)
                    {
                        var sinDuplicados = query.Select(o => o.IdSubcategoria).Distinct();
                        foreach (var item in sinDuplicados)
                        {
                            ML.Categoria cat = getCatByIdCat((Int32)item);
                            cat.Preguntas = new List<ML.Categoria>();
                            cat.Preguntas = getPregByIdCat(idEncuesta, (Int32)item);
                            lists.Add(cat);
                        }
                    }
                }
                return lists.OrderBy(o => o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }

        }
        public static List<ML.Categoria> getAllConfigurationPreSubCat(int idEncuesta,int idCat)
        {
            var lists = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionPreguntaPorSubcategoria.Select(o => o).Where(o => o.IdEncuesta == idEncuesta && o.IdSubcategoria == idCat).ToList();
                    if (query.Count() > 0)
                    {
                        var sinDuplicados = query.Select(o => o.IdSubcategoria).Distinct();
                        foreach (var item in sinDuplicados)
                        {
                            ML.Categoria cat = getCatByIdCat((Int32)item);
                            cat.Preguntas = new List<ML.Categoria>();
                            cat.Preguntas = getPregByIdCat(idEncuesta, (Int32)item);
                            lists.Add(cat);
                        }
                    }
                }
                return lists.OrderBy(o => o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }

        }
        public static ML.Categoria getCatByIdCat(int idCat)
        {
            ML.Categoria cat = new ML.Categoria();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Categoria.Select(o => o).Where(o => o.IdCategoria == idCat);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                           
                            cat.IdCategoria = idCat;
                            cat.Nombre = item.Nombre;
                            cat.Descripcion =item.Descripcion;
                            cat.IdPadre =item.IdPadre != null ?(Int32)item.IdPadre : 0;
                            cat.Valoracion = item.Valoracion != null ? (decimal)item.Valoracion : 0;
                        }

                    }
                }
                    return cat;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE,new System.Diagnostics.StackTrace());
                return new ML.Categoria();
            }
        }
        public static List<ML.Categoria> getSubCatByIdCat(int idEncuesta, int idCat)
        {
            var list = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Categoria.Select(o => o).Where(o => o.IdPadre == idCat && o.Estatus == 1).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Categoria cat = new ML.Categoria();
                            cat.IdCategoria = item.IdCategoria;
                            cat.Nombre = item.Nombre;
                            cat.Descripcion = item.Descripcion;
                            cat.IdPadre = (Int32)item.IdPadre;
                            cat.Valoracion = BL.ValoracionSubcategoriaPorCategoria.getValConfig(idEncuesta,item.IdCategoria);//(decimal)item.Valoracion;
                            list.Add(cat);
                        }

                    }
                }

                return list.OrderBy(o => o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE,new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }

        }
        public static List<ML.Categoria> getPregByIdCat(int idEncuesta, int idCat)
        {
            var list = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionPreguntaPorSubcategoria.Select(o => o).Where(o => o.IdEncuesta == idEncuesta && o.IdSubcategoria == idCat).ToList();
                    if (query != null)
                    {
                        foreach (var item2 in query)
                        {
                            ML.Categoria cat2 = new ML.Categoria();
                            cat2.IdCategoria = (Int32)item2.IdSubcategoria;
                            cat2.IdPadre = (Int32)item2.IdPregunta;                        
                            cat2.Nombre = getNombrePreguntaByIdPregunta((Int32)item2.IdPregunta);//busca Pregunta por item.IdPregunta;                                                        
                            cat2.Valoracion = (decimal)item2.Valor;                            
                            list.Add(cat2);
                        }

                    }
                }

                return list.OrderBy(o => o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }

        }
        /// <summary>
        /// consulta de SubCategorias por Id de Categoria Padre
        /// </summary>
        /// <param name="idEncuesta"></param>
        /// <param name="idCat"></param>
        /// <returns>Listado de SubCategorias</returns>
        public static List<ML.Categoria> getSubCatIdCat(int idEncuesta, int idCat)
        {
            var list = new List<ML.Categoria>();
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.ValoracionSubcategoriaPorCategoria.Select(o => o).Where(o => o.IdEncuesta == idEncuesta && o.IdCategoria == idCat).ToList();
                    if (query != null)
                    {
                        foreach (var item2 in query)
                        {
                            ML.Categoria cat2 = new ML.Categoria();
                            cat2.IdCategoria = (Int32)item2.IdSubcategoria;
                            cat2.IdPadre = idCat;
                            cat2.Nombre = getNombreCatByIdCat((Int32)item2.IdSubcategoria);//busca Pregunta por item.IdPregunta;                                                        
                            cat2.Valoracion = (decimal)item2.Valor;
                            list.Add(cat2);
                        }

                    }
                }

                return list.OrderBy(o => o.Nombre).ToList();
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }

        }
        public static string getNombrePreguntaByIdPregunta(int idPregunta)
        {
            var nombreP = "";
            try
            {
                using (DL.RH_DesEntities contex = new DL.RH_DesEntities())
                {
                    var query = contex.Preguntas.Select(o => o).Where(o => o.IdPregunta== idPregunta);
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            nombreP = item.Pregunta;
                        }
                    }
                return nombreP;
                }
                
            }
            catch (Exception aE)
            {

                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return nombreP;
            }
        }
        public static string getNombreCatByIdCat(int idCat) {
            var nombreC = "";
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities()) {
                    var query = context.Categoria.Select(o => o).Where(o => o.IdCategoria == idCat).ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            nombreC = item.Nombre;
                        }
                    }

                }
                return nombreC;
            }
            catch (Exception aE)
            {

                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return nombreC;
            }
        }
        public static List<ML.Categoria> getCategorias()
        {
            try
            {
                var list = new List<ML.Categoria>();
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.Categoria.ToList();
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.Categoria cat = new ML.Categoria()
                            {
                                IdCategoria = item.IdCategoria,
                                Nombre = item.Nombre,
                                IdPadre = (int?)item.IdPadre == null ? 0 : (int)item.IdPadre
                            };
                            list.Add(cat);
                        }
                        return list;
                    }
                    return list;
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new System.Diagnostics.StackTrace());
                return new List<ML.Categoria>();
            }
        }

    }
}
