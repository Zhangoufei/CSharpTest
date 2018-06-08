using System;
using System.Collections.Generic;
using System.Linq;

namespace SerialUtil
{
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 高字节在前，低字节在后
        /// </summary>
        BigEndian,
        /// <summary>
        /// 低字节在前，高字节在后;
        /// </summary>
        LittleEndian
    }

    public class BytesHelper
    {
        /// <summary>
        /// 多字节序转整型
        /// </summary>
        /// <param name="bts">数据</param>
        /// <param name="begin">起始索引</param>
        /// <param name="length">有效长度</param>
        /// <param name="dataType">字节序</param>
        /// <returns>返回实际值</returns>
        public static Int32 BytesToInt(IList<byte> bts, Int32 begin, Int32 length, DataType dataType)
        {
            Int32 result = 0;

            if (dataType == DataType.BigEndian)
            {
                Int32 endIndex = begin + length - 1;

                for (Int32 i = begin; i < begin + length; i++)
                {
                    result += bts[i] << (8 * (endIndex - i));
                }
            }
            else
            {
                for (Int32 i = begin; i < begin + length; i++)
                {
                    result += bts[i] << (8 * (i - begin));
                }
            }

            return result;
        }

        /// <summary>
        /// 获取多字节序
        /// </summary>
        /// <param name="value">要获取的值</param>
        /// <param name="num">要获取的字节数</param>
        /// <param name="dataType">字节序</param>
        /// <returns>数据</returns>
        public static byte[] IntToBytes(Int32 value, Int32 num, DataType dataType)
        {
            byte[] ret = new byte[num];

            if (dataType == DataType.BigEndian)
            {
                for (Int32 i = num - 1; i >= 0; i--)
                {
                    ret[num - 1 - i] = (byte)(value >> (8 * i));
                }
            }
            else
            {
                for (Int32 i = 0; i < num; i++)
                {
                    ret[i] = (byte)(value >> (8 * i));
                }
            }

            return ret;
        }

        public static IEnumerable<Int32> AllIndexOf(ICollection<byte> source, byte[] pattern, Int32 startIndex)
        {
            return Enumerable.Range(startIndex, source.Count - 1)
                .Where(n => source.Skip(n).Take(pattern.Length).SequenceEqual(pattern));
        }

        public static Int32 IndexOf(ICollection<byte> source, byte[] pattern)
        {
            var index = Enumerable.Range(0, source.Count - 1).
                Cast<int?>().FirstOrDefault(n => source.Skip(n ?? 0).Take(pattern.Length).SequenceEqual(pattern));
            return index ?? -1;
        }

        public static Int32 LastIndexOf(ICollection<byte> source, byte[] pattern, Int32 startIndex, Int32 endIndex)
        {
            if (startIndex<0 || startIndex>= source.Count) throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex.ToString(), String.Empty);
            if(endIndex<0 || endIndex>= source.Count) throw new ArgumentOutOfRangeException(nameof(endIndex), endIndex.ToString(), String.Empty);

            var index = Enumerable.Range(startIndex, endIndex).Reverse().
                Cast<int?>().FirstOrDefault(n => source.Skip(n ?? 0).Take(pattern.Length).SequenceEqual(pattern));
            return index ?? -1;
        }
    }
}
