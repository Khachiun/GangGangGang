using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Czaplicki.Universal.IO
{
    static class XML
    {
        public static void Save<T>(T IClass, string filename) where T : class
        {
            StreamWriter writer = null;
            try
            {
                XmlSerializer xmls = new XmlSerializer(IClass.GetType());
                writer = new StreamWriter(filename);
                xmls.Serialize(writer, IClass);
            }
            finally
            {
                if (writer != null)
                    writer.Close();

                writer = null;
            }
        }
        public static void Save<T>(T IClass, string filename, params Type[] internalTypes) where T : class
        {
            StreamWriter writer = null;
            try
            {
                XmlSerializer xmls = new XmlSerializer(IClass.GetType(), internalTypes);
                writer = new StreamWriter(filename);
                xmls.Serialize(writer, IClass);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
                writer = null;
            }
        }


        public static T Load<T>(string filename)
        {
            T result;
            XmlSerializer xmls = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            result = (T)xmls.Deserialize(fs);
            fs.Close();
            return result;
        }
        public static T Load<T>(string filename, params Type[] internalTypes)
        {
            T result;
            XmlSerializer xmls = new XmlSerializer(typeof(T), internalTypes);
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            result = (T)xmls.Deserialize(fs);
            fs.Close();
            return result;
        }
    }
}