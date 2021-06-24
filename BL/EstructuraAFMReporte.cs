using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BL
{
    public class EstructuraAFMReporte
    {
        public static List<string> GetPerfilesTipoFuncion(string IdBaseDeDatos)
        {
            var list = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select distinct TipoFuncion from Empleado where TipoFuncion is not null and IdBaseDeDatos = {0}", IdBaseDeDatos);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(row.ItemArray[0].ToString() + "_TF");
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            return list;
        }
        public static List<string> GetCondicionTrabajo(string IdBaseDeDatos)
        {
            var list = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select distinct CondicionTrabajo from Empleado where CondicionTrabajo is not null and IdBaseDeDatos = {0}", IdBaseDeDatos);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(row.ItemArray[0].ToString() + "_CT");
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            return list;
        }
        public static List<string> GetGradoAcademico(string IdBaseDeDatos)
        {
            var list = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select distinct GradoAcademico from Empleado where GradoAcademico is not null and IdBaseDeDatos = {0}", IdBaseDeDatos);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(row.ItemArray[0].ToString() + "_GA");
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            return list;
        }
        public static List<string> GetAntiguedad(string IdBaseDeDatos)
        {
            var list = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select distinct RangoAntiguedad from Empleado where RangoAntiguedad is not null and IdBaseDeDatos = {0}", IdBaseDeDatos);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(row.ItemArray[0].ToString() + "_RA");
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            return list;
        }
        public static List<string> GetRangoEdad(string IdBaseDeDatos)
        {
            var list = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select distinct RangoEdad from Empleado where RangoEdad is not null and IdBaseDeDatos = {0}", IdBaseDeDatos);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(row.ItemArray[0].ToString() + "_RE");
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            return list;
        }
        public static List<string> GetEstructuraDemografica(string IdBaseDeDatos)
        {
            var list = new List<string>();
            list.AddRange(GetPerfilesTipoFuncion(IdBaseDeDatos));
            list.AddRange(GetCondicionTrabajo(IdBaseDeDatos));
            list.AddRange(GetGradoAcademico(IdBaseDeDatos));
            list.AddRange(GetAntiguedad(IdBaseDeDatos));
            list.AddRange(GetRangoEdad(IdBaseDeDatos));
            return list;
        }
        public static List<string> GetCompanyCategoria(string IdBaseDeDatos)
        {
            var list = new List<string>();
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    string query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0}", IdBaseDeDatos);
                    SqlDataAdapter data = new SqlDataAdapter(query, conn);
                    data.Fill(ds, "data");
                }
                foreach (DataRow row in ds.Tables[0].Rows)
                    list.Add(row.ItemArray[0].ToString());
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            return list;
        }
        public static List<string> GetEstructuraGAFM(string IdBaseDeDatos, List<string> listU, List<string> level)
        {
            var list = new List<string>();
            string query = string.Empty;
            if (level == null)
                level = new List<string>();
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0}", IdBaseDeDatos);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_CompanyCategoria, "data");

                    foreach (DataRow row_CompanyCategoria in ds_CompanyCategoria.Tables[0].Rows)
                    {
                        if (listU.Contains(row_CompanyCategoria.ItemArray[0].ToString()))
                        {
                            if (row_CompanyCategoria.ItemArray[0].ToString() != "-")
                                list.Add("UNeg=>" + row_CompanyCategoria.ItemArray[0].ToString());
                            DataSet ds_Company = new DataSet();
                            query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, row_CompanyCategoria.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Company, "data");
                            foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                            {
                                if (row_Company.ItemArray[0].ToString() != "-" && level.Contains("company"))
                                    list.Add("Comp=>" + row_Company.ItemArray[0].ToString());
                                DataSet ds_Area = new DataSet();
                                query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_Area, "data");
                                foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                                {
                                    if (row_Area.ItemArray[0].ToString() != "-" && level.Contains("area"))
                                        list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                                    DataSet ds_Departamento = new DataSet();
                                    query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                                    data = new SqlDataAdapter(query, conn);
                                    data.Fill(ds_Departamento, "data");
                                    foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                                    {
                                        if (row_Depto.ItemArray[0].ToString() != "-" && level.Contains("departamento"))
                                            list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                        DataSet ds_SubDepartamento = new DataSet();
                                        query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                        data = new SqlDataAdapter(query, conn);
                                        data.Fill(ds_SubDepartamento, "data");
                                        foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                        {
                                            if (row_Subd.ItemArray[0].ToString() != "-" && level.Contains("subdepartamento"))
                                                list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new List<string>();
            }
            list = list.Where(o => !o.Equals("") && !o.Equals("-") && o.Length > 6).ToList();
            return list;
        }
    }
}
