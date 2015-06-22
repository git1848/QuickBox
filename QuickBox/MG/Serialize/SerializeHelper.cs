using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace QuickBox.MG.Serialize
{
    /// <summary>
    /// Xml序列化
    /// </summary>
    public class XmlSerializeHelper : ISerializeHelper
    {
        public void Serialize<T>(Stream stream, T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            stream.Seek(0, SeekOrigin.Begin);
            serializer.Serialize(stream, item);
        }

        public T DeSerialize<T>(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            stream.Seek(0, SeekOrigin.Begin);
            return (T)serializer.Deserialize(stream);
        }
    }

    /// <summary>
    /// 二进制序列化
    /// </summary>
    public class BinarySerializeHelper : ISerializeHelper
    {
        public void Serialize<T>(Stream stream, T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            formatter.Serialize(stream, item);
        }

        public T DeSerialize<T>(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    /// <summary>
    /// Soap序列化
    /// </summary>
    public class SoapSerializeHelper : ISerializeHelper
    {
        public void Serialize<T>(Stream stream, T item)
        {
            SoapFormatter formatter = new SoapFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            formatter.Serialize(stream, item);
        }

        public T DeSerialize<T>(Stream stream)
        {
            SoapFormatter formatter = new SoapFormatter();
            return (T)formatter.Deserialize(stream);
        }
    }

    /// <summary>
    /// Json序列化
    /// </summary>
    public class JsonSerializeHelper : ISerializeHelper
    {
        public void Serialize<T>(Stream stream, T item)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(T));
            stream.Seek(0, SeekOrigin.Begin);
            formatter.WriteObject(stream, item);
        }

        public T DeSerialize<T>(Stream stream)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(T));
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.ReadObject(stream);
        }
    }
}
