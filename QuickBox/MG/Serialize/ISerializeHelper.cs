using System;

namespace QuickBox.MG.Serialize
{
    /// <summary>
    /// 序列化工具
    /// </summary>
    public interface ISerializeHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="item"></param>
        void Serialize<T>(System.IO.Stream stream, T item);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        T DeSerialize<T>(System.IO.Stream stream);
    }
}
