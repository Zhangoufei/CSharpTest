using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerialUtil.until
{
    /// <summary>
    /// 字节顺序
    /// </summary>
    public enum ByteOrder
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

    public class ByteHelper
    {
        /// <summary>
        /// 获取多字节序
        /// </summary>
        /// <param name="value">要获取的值</param>
        /// <param name="lenOfByte">要获取的字节数</param>
        /// <param name="byteOrder">字节顺序</param>
        /// <returns>返回字节序列</returns>
        public static byte[] IntToBytes(Int32 value, Int32 lenOfByte, ByteOrder byteOrder)
        {
            byte[] ret = new byte[lenOfByte];

            if (byteOrder == ByteOrder.BigEndian)
            {
                for (Int32 i = lenOfByte - 1; i >= 0; i--)
                {
                    ret[lenOfByte - 1 - i] = (byte)(value >> (8 * i));
                }
            }
            else
            {
                for (Int32 i = 0; i < lenOfByte; i++)
                {
                    ret[i] = (byte)(value >> (8 * i));
                }
            }

            return ret;
        }

        /// <summary>
        /// 多字节序转整型
        /// </summary>
        /// <param name="bts">字节序</param>
        /// <param name="begin">起始索引</param>
        /// <param name="length">有效长度</param>
        /// <returns>返回实际值</returns>
        public static Int32 BytesToInt(byte[] bts, Int32 begin, Int32 length, ByteOrder dataType)
        {
            Int32 result = 0;

            if (dataType == ByteOrder.BigEndian)
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
        /// 查找数组中的子数据
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="key">待查找的数据</param>
        /// <returns>最后一次出现的索引</returns>
        public static Int32 LastIndexOf(byte[] source, byte[] key)
        {
            Int32 result = -1;

            for (int i = source.Length - key.Length; i >= 0; i--)
            {
                if (SequenceEqual(source, i, key, null))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 是否按顺序相同
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="firstIndex"></param>
        /// <param name="second"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool SequenceEqual<TSource>(IEnumerable<TSource> first, Int32 firstIndex, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }
            if (first == null)
            {
                return false;
            }
            if (second == null)
            {
                return false;
            }
            using (IEnumerator<TSource> enumerator = first.GetEnumerator())
            {
                using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
                {
                    while (firstIndex > 0 && enumerator.MoveNext())//跳到索引处
                    {
                        firstIndex--;
                    }

                    while (enumerator.MoveNext() && enumerator2.MoveNext())
                    {
                        if (!comparer.Equals(enumerator.Current, enumerator2.Current))
                        {
                            return false;
                        }
                    }
                    if (enumerator2.MoveNext())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
