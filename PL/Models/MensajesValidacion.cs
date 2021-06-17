using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;

namespace PL.Models
{
    public class MensajesValidacion
    {
        public static List<ML.MensajesError> getMensajesValidacion(ModelStateDictionary modelState)
        {
            var list = new List<ML.MensajesError>();
            try
            {
                foreach (var item in modelState)
                {
                    foreach (var elem in item.Value.Errors)
                    {
                        if (!String.IsNullOrEmpty(elem.ErrorMessage))
                        {
                            list.Add(new ML.MensajesError { MensajeError = elem.ErrorMessage, Error = true });
                        }
                    }
                }
            }
            catch (Exception aE)
            {
                BL.LogReporteoClima.writteLog(aE, new StackTrace());
                list = new List<ML.MensajesError>();
                list.Add(new ML.MensajesError { MensajeError = aE.Message, Error = true });
                return list;
            }
            return list;
        }
    }
}