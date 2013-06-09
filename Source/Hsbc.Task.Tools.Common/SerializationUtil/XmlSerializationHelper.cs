using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Hsbc.Task.Tools.CommonUtil.SerializationUtil
{
    public class XmlSerializationHelper
    {
        public static string Serialize<T>(T anyObject)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, anyObject);
                return stringWriter.ToString();
            }
        }

        public static TResult DeSerialize<TResult>(string serializedXml)
        {
            var serializer = new XmlSerializer(typeof(TResult));
            using (var stringReader = new StringReader(serializedXml))
            {
                return (TResult) serializer.Deserialize(stringReader);
            }
        }
        
        public static TResult DeSerializeFromFile<TResult>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(TResult));
            //File.ReadAllText(filePath);
            //serializedXml.
            using (var file = new FileStream(filePath, FileMode.Open))
            {
                return (TResult) serializer.Deserialize(file);
            }
        }
    }
}