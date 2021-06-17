using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Generales
    {
        /// <summary>
        /// convierte un DataSet a entidades Entity Framework
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns>objeto del del namespace DL</returns>
        public static T GetEntity<T>(DataRow row) where T : new()
        {
            var entity = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                //Get the description attribute and set value in DataSet to Entity
                var descriptionAttribute = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), true).SingleOrDefault();
                if (descriptionAttribute == null)
                    continue;

                property.SetValue(entity, row[descriptionAttribute.Description]);
            }
            return entity;
        }

        /// <summary>
        /// itera los valores de una lista de objetos e imprime los valores de las propiedades en un log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static T print_r<T>(List<T> data, StackTrace st) where T : new()
        {
            foreach (var elem in data)
            {
                BL.NLogGeneratorFile.logData("/------------------------------------------------------------------------/");
                BL.NLogGeneratorFile.logData("Tipo: " + typeof(T).FullName);
                BL.NLogGeneratorFile.logData("Method: " + st.GetFrame(0).GetMethod());
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var value = Convert.ToString(property.GetValue(elem));
                    if (!string.IsNullOrEmpty(value))
                        BL.NLogGeneratorFile.logData("Propiedad: " + property.Name + ". Valor: " + value);
                }
            }
            return new T();
        }

        /// <summary>
        /// itera las propiedades de un objeto y las imprime en un log
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static T print_r<T>(object data, StackTrace st) where T : new()
        {
            BL.NLogGeneratorFile.logData("/------------------------------------------------------------------------/");
            BL.NLogGeneratorFile.logData("Tipo: " + typeof(T).FullName);
            BL.NLogGeneratorFile.logData("Method: " + st.GetFrame(0).GetMethod());
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var value = Convert.ToString(property.GetValue(data));
                if (!string.IsNullOrEmpty(value))
                    BL.NLogGeneratorFile.logData("Propiedad: " + property.Name + ". Valor: " + value);
            }
            return new T();
        }
    }
}
