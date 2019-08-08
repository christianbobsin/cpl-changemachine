using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ChangeMachineWF
{
    public static class Persistence
    {        
        public static void Serialize<T>(this T value)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(value.GetType());
                StringWriter serializedClass = new StringWriter();
                xml.Serialize(serializedClass, value);
                
                File.WriteAllText("Coins.data", serializedClass.ToString());
            }
            catch
            {
                throw;
            }
        }
        public static object Deserialize(Type tipo)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(tipo);

                var serializedClass = new StringReader( File.ReadAllText("Coins.data"));
                return xml.Deserialize(serializedClass);
            }
            catch
            {
                throw;
            }
        }
    }
}
