using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Security.Claims;

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
                                            if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, Convert.ToInt32(IdBaseDeDatos)))
                                                list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                        DataSet ds_SubDepartamento = new DataSet();
                                        query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                        data = new SqlDataAdapter(query, conn);
                                        data.Fill(ds_SubDepartamento, "data");
                                        foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                        {
                                            if (row_Subd.ItemArray[0].ToString() != "-" && level.Contains("subdepartamento"))
                                                if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, Convert.ToInt32(IdBaseDeDatos)))
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
            list = list.Where(o => o.Contains(" - -") == false).ToList();//
            //list = list.Where(o => o.Contains(" - ") == false).ToList();
            list = list.Where(o => !o.Equals("") && !o.Equals("-") && o.Length > 6).ToList();
            return list;
        }


        /*Metodos para BackGroundJob*/
        public static List<string> GetCompaniesByCompanyCategoria(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and unidadNegocio = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_CompanyCategoria, "data");
                    foreach (DataRow row_CompanyCategoria in ds_CompanyCategoria.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_CompanyCategoria.ItemArray[0].ToString(), 1, IdBaseDeDatos))
                            list.Add(row_CompanyCategoria.ItemArray[0].ToString());
                        DataSet ds_Company = new DataSet();
                        query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, row_CompanyCategoria.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Company, "data");
                        foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                                list.Add(row_Company.ItemArray[0].ToString());
                            /*DataSet ds_Area = new DataSet();
                            query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Area, "data");
                            foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                            {
                                if (downLevel >= 3)
                                    list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                                DataSet ds_Departamento = new DataSet();
                                query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_Departamento, "data");
                                foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                                {
                                    if (downLevel >= 4)
                                        list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                    DataSet ds_SubDepartamento = new DataSet();
                                    query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                    data = new SqlDataAdapter(query, conn);
                                    data.Fill(ds_SubDepartamento, "data");
                                    foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                    {
                                        if (downLevel >= 5)
                                            list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
                                    }
                                }
                            }*/
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

        public static List<string> GetAreasByCompany(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Company = new DataSet();
                    query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Company, "data");
                    foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                            list.Add(row_Company.ItemArray[0].ToString());
                        DataSet ds_Area = new DataSet();
                        query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Area, "data");
                        foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                                list.Add(row_Area.ItemArray[0].ToString());
                            /*DataSet ds_Departamento = new DataSet();
                            query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Departamento, "data");
                            foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                            {
                                if (downLevel >= 4)
                                    list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                DataSet ds_SubDepartamento = new DataSet();
                                query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_SubDepartamento, "data");
                                foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                {
                                    if (downLevel >= 5)
                                        list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
                                }
                            }*/
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

        public static List<string> GetDepartamentosByArea(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Area = new DataSet();
                    query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Area, "data");
                    foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                            list.Add(row_Area.ItemArray[0].ToString());
                        DataSet ds_Departamento = new DataSet();
                        query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Departamento, "data");
                        foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                list.Add(row_Depto.ItemArray[0].ToString());
                            /*DataSet ds_SubDepartamento = new DataSet();
                            query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_SubDepartamento, "data");
                            foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                            {
                                if (downLevel >= 5)
                                    list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
                            }*/
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

        public static List<string> GetSubDepartamentosByDepartamento(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Departamento = new DataSet();
                    query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Departamento, "data");
                    foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                    {
                        list.Add(row_Depto.ItemArray[0].ToString());
                        DataSet ds_SubDepartamento = new DataSet();
                        query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_SubDepartamento, "data");
                        foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                        {
                            list.Add(row_Subd.ItemArray[0].ToString());
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

        public static List<string> GetEstructuraGAFMForJob_lvl1(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_CompanyCategoria, "data");
                    foreach (DataRow row_CompanyCategoria in ds_CompanyCategoria.Tables[0].Rows)
                    {
                        if (row_CompanyCategoria.ItemArray[0].ToString() != "-") { 
                            if (hasEmpleado(row_CompanyCategoria.ItemArray[0].ToString(), 1, IdBaseDeDatos))
                                list.Add("UNeg=>" + row_CompanyCategoria.ItemArray[0].ToString());
                        }
                        DataSet ds_Company = new DataSet();
                        query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, row_CompanyCategoria.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Company, "data");
                        foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                                list.Add("Comp=>" + row_Company.ItemArray[0].ToString());
                            DataSet ds_Area = new DataSet();
                            query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Area, "data");
                            foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                                    list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                                DataSet ds_Departamento = new DataSet();
                                query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_Departamento, "data");
                                foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                                {
                                    if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                        list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                    DataSet ds_SubDepartamento = new DataSet();
                                    query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                    data = new SqlDataAdapter(query, conn);
                                    data.Fill(ds_SubDepartamento, "data");
                                    foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                    {
                                        if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                            list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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
            list = list.Where(o => !o.Equals("") && !o.Equals("-") && o.Length > 7).ToList();
            return list;
        }

        public static List<string> GetEstructuraGAFMForJob_lvl2(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Company = new DataSet();
                    query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos,entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Company, "data");
                    foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                            list.Add("Comp=>" + row_Company.ItemArray[0].ToString());
                        DataSet ds_Area = new DataSet();
                        query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Area, "data");
                        foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                                list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                            DataSet ds_Departamento = new DataSet();
                            query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Departamento, "data");
                            foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                    list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                DataSet ds_SubDepartamento = new DataSet();
                                query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_SubDepartamento, "data");
                                foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                {
                                    if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                    list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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

        public static List<string> GetEstructuraGAFMForJob_lvl3(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Area = new DataSet();
                    query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Area, "data");
                    foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                            list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                        DataSet ds_Departamento = new DataSet();
                        query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Departamento, "data");
                        foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                            DataSet ds_SubDepartamento = new DataSet();
                            query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_SubDepartamento, "data");
                            foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                    list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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

        public static List<string> GetEstructuraGAFMForJob_lvl4(int IdBaseDeDatos, string entidadNombre)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Departamento = new DataSet();
                    query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Departamento, "data");
                    foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                            list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                        DataSet ds_SubDepartamento = new DataSet();
                        query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_SubDepartamento, "data");
                        foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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

        public static ML.Result GetEstructuraGAFMForPlanesAccion(int IdBaseDeDatos)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            var list = new List<string>();
            string query = string.Empty;
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
                        if (row_CompanyCategoria.ItemArray[0].ToString() != "-")
                        {
                            if (hasEmpleado(row_CompanyCategoria.ItemArray[0].ToString(), 1, IdBaseDeDatos))
                                list.Add("UNeg=>" + row_CompanyCategoria.ItemArray[0].ToString());
                        }
                        DataSet ds_Company = new DataSet();
                        query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, row_CompanyCategoria.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Company, "data");
                        foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                                list.Add("Comp=>" + row_Company.ItemArray[0].ToString());
                            DataSet ds_Area = new DataSet();
                            query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Area, "data");
                            foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                                    list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                                /*DataSet ds_Departamento = new DataSet();
                                query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_Departamento, "data");
                                foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                                {
                                    if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                        list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                    DataSet ds_SubDepartamento = new DataSet();
                                    query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                    data = new SqlDataAdapter(query, conn);
                                    data.Fill(ds_SubDepartamento, "data");
                                    foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                    {
                                        if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                            list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
                                    }
                                }*/
                            }
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                result.Correct = false;
                result.ErrorMessage = aE.Message;
                result.ex = aE;
            }
            list = list.Where(o => !o.Equals("") && !o.Equals("-") && o.Length > 7).ToList();
            result.Objects.Add(list);
            result.Correct = true;
            return result;
        }

        public static bool hasEmpleado(string entidadNombre, int idTipoEntidad, int idBD)
        {
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var data = new DL.Empleado();
                    switch (idTipoEntidad)
                    {
                        case 1:
                            data = context.Empleado.Where(o => o.UnidadNegocio == entidadNombre && o.IdBaseDeDatos == idBD && o.EstatusEmpleado == "Activo").FirstOrDefault();
                            break;
                        case 2:
                            data = context.Empleado.Where(o => o.DivisionMarca == entidadNombre && o.IdBaseDeDatos == idBD && o.EstatusEmpleado == "Activo").FirstOrDefault();
                            break;
                        case 3:
                            data = context.Empleado.Where(o => o.AreaAgencia == entidadNombre && o.IdBaseDeDatos == idBD && o.EstatusEmpleado == "Activo").FirstOrDefault();
                            break;
                        case 4:
                            data = context.Empleado.Where(o => o.Depto == entidadNombre && o.IdBaseDeDatos == idBD && o.EstatusEmpleado == "Activo").FirstOrDefault();
                            break;
                        case 5:
                            data = context.Empleado.Where(o => o.Subdepartamento == entidadNombre && o.IdBaseDeDatos == idBD && o.EstatusEmpleado == "Activo").FirstOrDefault();
                            break;
                        default:
                            break;
                    }
                    if (data != null)
                    {
                        if (!string.IsNullOrEmpty(data.Nombre))
                        {
                            if (data.IdEmpleado > 0)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logErrorModuloEncuestas(aE, new StackTrace());
                return false;
            }
            return false;
        }

        // encrypt
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }


        public static List<string> GetEstructuraGAFMForJob_lvl1ForRDinamico(int IdBaseDeDatos, string entidadNombre, List<string> niveles)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    query = string.Format("select distinct UnidadNegocio from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_CompanyCategoria, "data");
                    foreach (DataRow row_CompanyCategoria in ds_CompanyCategoria.Tables[0].Rows)
                    {
                        if (row_CompanyCategoria.ItemArray[0].ToString() != "-")
                        {
                            if (hasEmpleado(row_CompanyCategoria.ItemArray[0].ToString(), 1, IdBaseDeDatos))
                                list.Add("UNeg=>" + row_CompanyCategoria.ItemArray[0].ToString());
                        }
                        DataSet ds_Company = new DataSet();
                        query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and UnidadNegocio = '{1}'", IdBaseDeDatos, row_CompanyCategoria.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Company, "data");
                        foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                                if (niveles.Contains("company"))
                                    list.Add("Comp=>" + row_Company.ItemArray[0].ToString());
                            DataSet ds_Area = new DataSet();
                            query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Area, "data");
                            foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                                    if (niveles.Contains("area"))
                                        list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                                DataSet ds_Departamento = new DataSet();
                                query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_Departamento, "data");
                                foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                                {
                                    if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                        if (niveles.Contains("departamento"))
                                            list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                    DataSet ds_SubDepartamento = new DataSet();
                                    query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                    data = new SqlDataAdapter(query, conn);
                                    data.Fill(ds_SubDepartamento, "data");
                                    foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                    {
                                        if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                            if (niveles.Contains("subdepartamento"))
                                                list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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
            list = list.Where(o => !o.Equals("") && !o.Equals("-") && o.Length > 7).ToList();
            return list;
        }

        public static List<string> GetEstructuraGAFMForJob_lvl2ForRDinamico(int IdBaseDeDatos, string entidadNombre, List<string> niveles)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Company = new DataSet();
                    query = string.Format("select distinct DivisionMarca from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Company, "data");
                    foreach (DataRow row_Company in ds_Company.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Company.ItemArray[0].ToString(), 2, IdBaseDeDatos))
                            list.Add("Comp=>" + row_Company.ItemArray[0].ToString());
                        DataSet ds_Area = new DataSet();
                        query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and DivisionMarca = '{1}'", IdBaseDeDatos, row_Company.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Area, "data");
                        foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                                list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                            DataSet ds_Departamento = new DataSet();
                            query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_Departamento, "data");
                            foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                    list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                                DataSet ds_SubDepartamento = new DataSet();
                                query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                                data = new SqlDataAdapter(query, conn);
                                data.Fill(ds_SubDepartamento, "data");
                                foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                                {
                                    if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                        list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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

        public static List<string> GetEstructuraGAFMForJob_lvl3ForRDinamico(int IdBaseDeDatos, string entidadNombre, List<string> niveles)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Area = new DataSet();
                    query = string.Format("select distinct AreaAgencia from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Area, "data");
                    foreach (DataRow row_Area in ds_Area.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Area.ItemArray[0].ToString(), 3, IdBaseDeDatos))
                            list.Add("Area=>" + row_Area.ItemArray[0].ToString());
                        DataSet ds_Departamento = new DataSet();
                        query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and AreaAgencia = '{1}'", IdBaseDeDatos, row_Area.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_Departamento, "data");
                        foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                                list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                            DataSet ds_SubDepartamento = new DataSet();
                            query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                            data = new SqlDataAdapter(query, conn);
                            data.Fill(ds_SubDepartamento, "data");
                            foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                            {
                                if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                    list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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

        public static List<string> GetEstructuraGAFMForJob_lvl4ForRDinamico(int IdBaseDeDatos, string entidadNombre, List<string> niveles)
        {
            var list = new List<string>();
            string query = string.Empty;
            SqlDataAdapter data = new SqlDataAdapter();
            try
            {
                DataSet ds_CompanyCategoria = new DataSet();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ToString()))
                {
                    DataSet ds_Departamento = new DataSet();
                    query = string.Format("select distinct Depto from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, entidadNombre);
                    data = new SqlDataAdapter(query, conn);
                    data.Fill(ds_Departamento, "data");
                    foreach (DataRow row_Depto in ds_Departamento.Tables[0].Rows)
                    {
                        if (hasEmpleado(row_Depto.ItemArray[0].ToString(), 4, IdBaseDeDatos))
                            list.Add("Dpto=>" + row_Depto.ItemArray[0].ToString());
                        DataSet ds_SubDepartamento = new DataSet();
                        query = string.Format("select distinct SubDepartamento from Empleado where IdBaseDeDatos = {0} and Depto = '{1}'", IdBaseDeDatos, row_Depto.ItemArray[0].ToString());
                        data = new SqlDataAdapter(query, conn);
                        data.Fill(ds_SubDepartamento, "data");
                        foreach (DataRow row_Subd in ds_SubDepartamento.Tables[0].Rows)
                        {
                            if (hasEmpleado(row_Subd.ItemArray[0].ToString(), 5, IdBaseDeDatos))
                                list.Add("SubD=>" + row_Subd.ItemArray[0].ToString());
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
