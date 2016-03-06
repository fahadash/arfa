using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LogicLayer
{
    public class SerializationHelper
    {

        public static string SerializeToXml(object o, Type type)
        {
            MemoryStream s = new MemoryStream();

            XmlSerializer serializer = new XmlSerializer(type);

            serializer.Serialize(s, o);

            return Encoding.Default.GetString(s.GetBuffer());
        }
    }
}
