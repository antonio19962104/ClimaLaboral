using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BL
{
    /// <summary>
    /// Documentación del proyecto
    /// </summary>
    public class Documentacion
    {
        /// <summary>
        /// Obtiene un objeto con la documentación de la capa de negocios
        /// </summary>
        /// <returns>Objeto con la documentacion de la capa de negocios</returns>
        public static Doc GetBussinessLayerDocumentation()
        {
            try
            {
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;
                string path = Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\documentation\BL.xml");
                xmlInputData = File.ReadAllText(path);

                Doc docObject = Deserialize<Doc>(xmlInputData); // c# object
                xmlOutputData = Serialize<Doc>(docObject); // xml object
                foreach (var item in docObject.Members.Member) {
                    item.Name = item.Name.Substring(5, item.Name.Length - 5);
                    item.Class = item.Name.Split('.')[0];
                    if (item.Name != item.Class)
                        item.Name = item.Name.Replace(item.Class + ".", "");
                }
                return docObject;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new Doc();
            }
        }
        /// <summary>
        /// Obtiene un objeto con la documentación de la capa de datos
        /// </summary>
        /// <returns>Objeto con la documentacion de la capa de datos</returns>
        public static Doc GetDataLayerDocumentation()
        {
            try
            {
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;
                string path = Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\documentation\DL.xml");
                xmlInputData = File.ReadAllText(path);

                Doc docObject = Deserialize<Doc>(xmlInputData); // c# object
                xmlOutputData = Serialize<Doc>(docObject); // xml object
                foreach (var item in docObject.Members.Member)
                {
                    item.Name = item.Name.Substring(5, item.Name.Length - 5);
                    item.Class = item.Name.Split('.')[0];
                    if (item.Name != item.Class)
                        item.Name = item.Name.Replace(item.Class + ".", "");
                }
                return docObject;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new Doc();
            }
        }
        /// <summary>
        /// Obtiene un objeto con la documentación de la capa de modelo
        /// </summary>
        /// <returns>Objeto con la documentacion de la capa de modelo</returns>
        public static Doc GetModelLayerDocumentation()
        {
            try
            {
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;
                string path = Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\documentation\ML.xml");
                xmlInputData = File.ReadAllText(path);

                Doc docObject = Deserialize<Doc>(xmlInputData); // c# object
                xmlOutputData = Serialize<Doc>(docObject); // xml object
                foreach (var item in docObject.Members.Member)
                {
                    item.Name = item.Name.Substring(5, item.Name.Length - 5);
                    item.Class = item.Name.Split('.')[0];
                    if (item.Name != item.Class)
                        item.Name = item.Name.Replace(item.Class + ".", "");
                }
                return docObject;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new Doc();
            }
        }
        /// <summary>
        /// Obtiene un objeto con la documentación de la capa de presentación
        /// </summary>
        /// <returns>Objeto con la documentacion de la capa de presentación</returns>
        public static Doc GetPresentationLayerDocumentation()
        {
            try
            {
                string xmlInputData = string.Empty;
                string xmlOutputData = string.Empty;
                string path = Path.GetFullPath(@"\\10.5.2.101\RHDiagnostics\documentation\PL.xml");
                xmlInputData = File.ReadAllText(path);

                Doc docObject = Deserialize<Doc>(xmlInputData); // c# object
                xmlOutputData = Serialize<Doc>(docObject); // xml object
                foreach (var item in docObject.Members.Member)
                {
                    item.Name = item.Name.Substring(5, item.Name.Length - 5);
                    item.Class = item.Name.Split('.')[0];
                    if (item.Name != item.Class)
                        item.Name = item.Name.Replace(item.Class + ".", "");
                }
                return docObject;
            }
            catch (Exception aE)
            {
                BL.NLogGeneratorFile.logError(aE, new StackTrace());
                return new Doc();
            }
        }
        /// <summary>
        /// Deserializa un xml a un objeto c#
        /// </summary>
        /// <typeparam name="T">Instancia del objeto especificado en la llamada al método</typeparam>
        /// <param name="input">Cadena xml</param>
        /// <returns>Objeto</returns>
        public static T Deserialize<T>(string input) where T : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }
        /// <summary>
        /// Serializa un objeto c# a un xml
        /// </summary>
        /// <typeparam name="T">Instancia del objeto especificado en la llamada al método</typeparam>
        /// <param name="ObjectToSerialize">Objeto c# a serializar</param>
        /// <returns>XML del objeto</returns>
        public static string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }

        //Models
        [XmlRoot(ElementName = "assembly")]
        public class Assembly
        {
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "param")]
        public class Param
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
            [XmlAttribute(AttributeName = "Type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "member")]
        public class Member
        {
            public string Class { get; set; }
            [XmlElement(ElementName = "summary")]
            public string Summary { get; set; }

            [XmlElement(ElementName = "param")]
            public List<Param> Param { get; set; }

            [XmlElement(ElementName = "returns")]
            public string Returns { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlText]
            public string Text { get; set; }

            [XmlElement(ElementName = "typeparam")]
            public List<Typeparam> Typeparam { get; set; }
        }

        [XmlRoot(ElementName = "typeparam")]
        public class Typeparam
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "members")]
        public class Members
        {
            [XmlElement(ElementName = "member")]
            public List<Member> Member { get; set; }
        }

        [XmlRoot(ElementName = "doc")]
        public class Doc
        {
            [XmlElement(ElementName = "assembly")]
            public Assembly Assembly { get; set; }

            [XmlElement(ElementName = "members")]
            public Members Members { get; set; }
        }
    }
}
