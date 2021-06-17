using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Autenticar
    {
        public static ML.Result Autentication(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result(); 
            
            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    context.Database.ExecuteSqlCommand("select Empleado.IdEmpleado, Empleado.Nombre, Empleado.ApellidoPaterno, Empleado.ApellidoMaterno, Empleado.IdDepartamento, Departamento.Nombre,"
                        + "Area.Nombre, Company.CompanyName, CompanyCategoria.Descripcion from Empleado inner join Departamento ON Empleado.IdDepartamento = Departamento.IdDepartamento"
                        + "inner join Area on Departamento.IdArea = Area.IdArea inner join Company on Area.CompanyId = Company.CompanyId "
                        + "inner join CompanyCategoria on Company.IdCompanyCategoria = CompanyCategoria.IdCompanyCategoria where IdEmpleado = {0}", empleado.IdEmpleado);
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
