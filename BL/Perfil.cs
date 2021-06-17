using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Perfil
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RH_DesEntities context = new DL.RH_DesEntities())
                {
                    var query = context.PerfilGetAll().ToList();
                    result.Objects = new List<Object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Perfil perfil = new ML.Perfil();

                            perfil.IdPerfil = obj.IdPerfil;
                            perfil.Descripcion = obj.Descripcion;

                            result.Objects.Add(perfil);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudieron obtener los perfiles";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
