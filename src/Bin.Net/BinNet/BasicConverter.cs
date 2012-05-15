// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicConverter.cs" company="">
//   
// </copyright>
// <summary>
//   The converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Eps.Sdk
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The converter.
    /// </summary>
    public abstract class BasicConverter
    {
        #region Public Methods and Operators

        /// <summary>
        /// ASCII数组转换成字符串
        /// </summary>
        /// <param name="buf">
        /// 字节数组
        /// </param>
        /// <returns>
        /// The asc to string.
        /// </returns>
        public static string AscToString(byte[] buf)
        {
            return Encoding.ASCII.GetString(buf);
        }

        /// <summary>
        /// 把BCD数组转换成字符串
        /// </summary>
        /// <param name="buf">
        /// BCD数组
        /// </param>
        /// <returns>
        /// The bcd to string.
        /// </returns>
        public static string BcdToString(byte[] buf)
        {
            string result = string.Empty;
            foreach (byte b in buf)
            {
                result += (b >> 4) + (((byte)(b << 4)) >> 4).ToString();
            }

            return result;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="byteArray">
        /// 字节数组
        /// </param>
        /// <returns>
        /// The byte array to hex string.
        /// </returns>
        public static string ByteArrayToCStyleHexString(byte[] byteArray)
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException("byteArray");
            }

            const string defaultPattern = "0x{0:X2}, ";
            return string.Format(
                "byte[{0}] {{ {1}}}", byteArray.Length, ByteArrayToHexString(byteArray, defaultPattern));
        }

        /// <summary>
        /// 将数组的指定个数元素按要求格式输出
        /// </summary>
        /// <param name="byteArray">
        /// </param>
        /// <param name="len">
        /// 要格式化的数组元素个数
        /// </param>
        /// <returns>
        /// The byte array to c style hex string.
        /// </returns>
        public static string ByteArrayToCStyleHexString(byte[] byteArray, int len)
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException("byteArray");
            }

            const string defaultPattern = "0x{0:X2}, ";
            return string.Format("byte[{0}] {{ {1}}}", len, ByteArrayToHexString(byteArray, defaultPattern, len));
        }

        /// <summary>
        /// The byte array to hex string.
        /// </summary>
        /// <param name="byteArray">
        /// The byte array.
        /// </param>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The byte array to hex string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static string ByteArrayToHexString(byte[] byteArray, string pattern)
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException("byteArray");
            }

            var sb = new StringBuilder();
            foreach (byte oneByte in byteArray)
            {
                sb.Append(string.Format(pattern, oneByte));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将数组的指定个数元素转换为字符串
        /// </summary>
        /// <param name="byteArray">
        /// </param>
        /// <param name="pattern">
        /// </param>
        /// <param name="len">
        /// 要转换的数组元素个数
        /// </param>
        /// <returns>
        /// The byte array to hex string.
        /// </returns>
        public static string ByteArrayToHexString(byte[] byteArray, string pattern, int len)
        {
            if (byteArray == null)
            {
                throw new ArgumentNullException("byteArray");
            }

            int count = 0;
            var sb = new StringBuilder();
            foreach (byte oneByte in byteArray)
            {
                sb.Append(string.Format(pattern, oneByte));
                count++;
                if (count == len)
                {
                    break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 截去字节数组尾部重复的字节，并保留制定个数个重复的字节。
        /// </summary>
        /// <param name="data">
        /// 原始数据。
        /// </param>
        /// <param name="reservedBytes">
        /// 保留字节个数。
        /// </param>
        /// <returns>
        /// 截尾后的字节数组。
        /// </returns>
        public static byte[] CutTail(byte[] data, int reservedBytes)
        {
            byte lastByte = data[data.Length - 1];
            int i;
            for (i = data.Length - 1; i >= 0; i--)
            {
                if (data[i] != lastByte)
                {
                    break;
                }
            }

            int len = Math.Min(data.Length, i + 1 + reservedBytes);
            var result = new byte[len];
            Array.Copy(data, result, i + 1);
            for (int j = i + 1; j < len; j++)
            {
                result[j] = lastByte;
            }

            return result;
        }

        /// <summary>
        /// 对两个BYTE数组进行异或运算
        /// </summary>
        /// <param name="block1">
        /// 运算对象一
        /// </param>
        /// <param name="block2">
        /// 运算对象二
        /// </param>
        /// <returns>
        /// 异或后的结果BYTE数组
        /// </returns>
        public static byte[] ExclusiveOr(byte[] block1, byte[] block2)
        {
            var result = new byte[block1.Length];
            if (block1.Length == block2.Length)
            {
                for (int i = 0; i < block1.Length; i++)
                {
                    result[i] = Convert.ToByte(block1[i] ^ block2[i]);
                }
            }
            else
            {
                throw new ArgumentException("进行异或的两个数据长度不相等！");
            }

            return result;
        }

        /// <summary>
        /// The generate bitmap string.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <returns>
        /// The generate bitmap string.
        /// </returns>
        public static string GenerateBitmapString(byte[] bytes)
        {
            if (bytes.Length != 4 && bytes.Length != 16 && bytes.Length != 32)
            {
                throw new ArgumentException("bytes");
            }

            var bitmap = new BitArray(bytes);
            var sb = new StringBuilder(bitmap.Length * 3);
            int lim = bitmap.Length;
            sb.Append("{ ");
            for (int i = 0; i < lim; i++)
            {
                int bit = ((i / 8) * 16) + 7 - i;
                if (bitmap.Get(bit))
                {
                    sb.Append(string.Format("{0}, ", i + 1));
                }
            }

            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 生成十六进制映射字符串
        /// </summary>
        /// <returns>
        /// The generate hex map.
        /// </returns>
        public static string GenerateHexMap(byte[] source)
        {
            var sb = new StringBuilder();
            int line = 1;

            int pos = 0;
            while (pos < source.Length)
            {
                int lineSize = Math.Min(source.Length - pos, 16);
                var lineArray = new byte[lineSize];
                Array.Copy(source, pos, lineArray, 0, lineSize);

                sb.Append(String.Format("[{0:X8}]: ", (line - 1) * 16));
                line++;

                for (int n = 0; n < 16; n++)
                {
                    if (n == 8)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(n < lineSize ? String.Format("{0:X2} ", lineArray[n]) : "   ");
                }

                sb.Append("; ");

                for (int n = 0; n < lineSize; n++)
                {
                    if (lineArray[n] >= 0 && lineArray[n] < 32)
                    {
                        lineArray[n] = 0x2E;
                    }

                    if ((n == lineSize - 1) && lineArray[n] >= 128)
                    {
                        var hightBitArray = new byte[lineSize + 1];
                        Array.Copy(lineArray, 0, hightBitArray, 0, lineArray.Length);
                        hightBitArray[hightBitArray.Length - 1] = 0x20;
                        lineArray = hightBitArray;
                    }
                }

                sb.Append(Encoding.Default.GetString(lineArray, 0, lineArray.Length));

                sb.AppendLine();
                pos += lineSize;
            }

            return sb.ToString();
        }

        /// <summary>
        /// CRC16校验
        /// </summary>
        /// <param name="buff">
        /// 带校验的数组
        /// </param>
        /// <returns>
        /// 校验后的值
        /// </returns>
        public static byte[] GetCrc16Code(byte[] buff)
        {
            byte j;
            ushort i, wCRC;
            var wCRCTable = new ushort[256];

            // CRC16
            for (i = 0; i < 256; i++)
            {
                wCRC = i;
                for (j = 0; j < 8; j++)
                {
                    if ((wCRC & 1) != 0)
                    {
                        wCRC >>= 1;
                        wCRC ^= 0xA001;
                    }
                    else
                    {
                        wCRC >>= 1;
                    }
                }

                wCRCTable[i] = wCRC;
            }

            wCRC = 0x0000;
            for (i = 0; i < buff.Length; i++)
            {
                j = (byte)(buff[i] ^ wCRC);
                wCRC >>= 8;
                wCRC ^= wCRCTable[j];
            }

            return UInt16ToByteArr(wCRC);
        }

        /// <summary>
        /// 获取数据包Mac
        /// </summary>
        /// <param name="buffer">
        /// 数据包
        /// </param>
        /// <returns>
        /// The get lrc code.
        /// </returns>
        public static byte GetLrcCode(byte[] buffer)
        {
            if (buffer.Length == 0 || buffer == null)
            {
                throw new NullReferenceException("buffer");
            }

            byte count = 0;
            for (int i = 0; i < buffer.Length - 1; i++)
            {
                count += buffer[i];
            }

            return (byte)(256 - count);
        }

        /// <summary>
        /// 转换16进制字节数组为整型
        /// </summary>
        /// <param name="byteArray">
        /// 16进制字节数组
        /// </param>
        /// <returns>
        /// The hex to int string.
        /// </returns>
        public static int HexToInt(byte[] byteArray)
        {
            int retult = 0;
            for (int i = 0; i < byteArray.Length; i++)
            {
                byte temp = byteArray[i];
                retult += (temp & 0xFF) << (8 * i);
            }

            return retult;
        }

        /// <summary>
        /// 转换16进制字节数组为整型字符串
        /// </summary>
        /// <param name="byteArray">
        /// 16进制字节数组
        /// </param>
        /// <param name="format">
        /// 整型字符串格式
        /// </param>
        /// <returns>
        /// The hex to int string.
        /// </returns>
        public static string HexToIntString(byte[] byteArray, string format)
        {
            uint retult = 0;
            for (int i = 0; i < byteArray.Length; i++)
            {
                byte temp = byteArray[i];
                retult += (uint)(temp & 0xFF) << (8 * i);
            }

            return retult.ToString(format);
        }

        /// <summary>
        /// 把Hex字节数组转换成字符串
        /// </summary>
        /// <param name="buf">
        /// Hex字节数组
        /// </param>
        /// <param name="encoding">
        /// 字符编码
        /// </param>
        /// <returns>
        /// The hex to string.
        /// </returns>
        public static string HexToString(byte[] buf, Encoding encoding)
        {
            return encoding.GetString(buf);
        }

        /// <summary>
        /// 将int转换为指定长度的ASC数组，在前面补0x30。
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="byteArrayLength">
        /// The byte array length.
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] IntStringToAsc(string value, int byteArrayLength)
        {
            byte[] valueArray = StringToAsc(value);

            // 如果长度转换为Asc后字节少于byteArrayLength个，在前面补0x30。
            if (valueArray.Length < byteArrayLength)
            {
                var toAdd = new byte[byteArrayLength - valueArray.Length];
                for (int j = 0; j < toAdd.Length; j++)
                {
                    toAdd[j] = 0x30;
                }

                valueArray = MergeByteArray(toAdd, valueArray);
            }

            return valueArray;
        }

        /// <summary>
        /// 转换整型字符串为16进制字节数组
        /// </summary>
        /// <param name="source">
        /// 整型字符串
        /// </param>
        /// <param name="byteArrayLength">
        /// 16进制字节数组长度
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] IntStringToHex(string source, int byteArrayLength)
        {
            if (byteArrayLength > 4)
            {
                throw new ArgumentOutOfRangeException();
            }

            int iSource = int.Parse(source);
            var buf = new byte[byteArrayLength];
            for (int i = 0; i < byteArrayLength; i++)
            {
                buf[i] = (byte)(iSource >> 8 * i & 0xFF);
            }

            return buf;
        }

        /// <summary>
        /// 判断两个字节数组是否相等。
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The is byte array equal.
        /// </returns>
        public static bool IsByteArrayEqual(byte[] x, byte[] y)
        {
            if (x == null ^ y == null)
            {
                return false;
            }

            if (x == null)
            {
                return true;
            }

            if (x.Length != y.Length)
            {
                return false;
            }

            return !x.Where((t, i) => t != y[i]).Any();
        }

        /// <summary>
        /// 判断是否中文字符
        /// </summary>
        /// <param name="input">
        /// 输入字符串
        /// </param>
        /// <returns>
        /// <c>true</c>如果是中文字符;否则, <c>false</c>.
        /// </returns>
        public static bool IsChineseLetter(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var rx = new Regex("^[\u4e00-\u9fa5]$");
                if (rx.IsMatch(input[i].ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 合并两个字节数组
        /// </summary>
        /// <param name="beginBuffer">
        /// The begin buffer.
        /// </param>
        /// <param name="endBuffer">
        /// The end buffer.
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] MergeByteArray(byte[] beginBuffer, byte[] endBuffer)
        {
            if (beginBuffer == null && endBuffer == null)
            {
                return null;
            }

            if (beginBuffer == null)
            {
                return endBuffer;
            }

            if (endBuffer == null)
            {
                return beginBuffer;
            }

            var buffer = new byte[beginBuffer.Length + endBuffer.Length];
            beginBuffer.CopyTo(buffer, 0);
            endBuffer.CopyTo(buffer, beginBuffer.Length);
            return buffer;
        }

        /// <summary>
        /// 转换成定长的ASCII数组
        /// </summary>
        /// <param name="value">
        /// 字符串
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] StringToAsc(string value)
        {
            return IsChineseLetter(value)
                       ? Encoding.GetEncoding("gb2312").GetBytes(value)
                       : Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// 把字符串转换成BCD数组
        /// </summary>
        /// <param name="value">
        /// 字符串
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] StringToBcd(string value)
        {
            decimal byteLength = Math.Ceiling(decimal.Parse(value.Length.ToString()) / 2);
            var bytes = new byte[int.Parse(byteLength.ToString())];
            for (int i = 0; i < value.Length / 2; i++)
            {
                bytes[i] = (byte)(((value[i * 2] - 0x30) << 4) + (value[i * 2 + 1] - 0x30));
            }

            return bytes;
        }

        /// <summary>
        /// 把字符串转换成Hex字节数组
        /// </summary>
        /// <param name="value">
        /// 数值型字符串
        /// </param>
        /// <param name="encoding">
        /// 字符编码
        /// </param>
        /// <returns>
        /// </returns>
        /// <remarks>
        /// value不能超出Uint64 18446744073709551616 表示范围
        /// </remarks>
        public static byte[] StringToHex(string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString">
        /// 16进制字符串
        /// </param>
        /// <returns>
        /// </returns>
        public static byte[] StringToHexByteArray(string hexString)
        {
            if (hexString == null)
            {
                throw new ArgumentNullException("hexString");
            }

            hexString = hexString.Replace(" ", string.Empty);
            hexString = hexString.Replace("0x", string.Empty);
            hexString = hexString.Replace("0X", string.Empty);
            hexString = hexString.Replace(",", string.Empty);
            if ((hexString.Length % 2) != 0)
            {
                throw new ArgumentException("Argument fromat error!", "hexString");
            }

            var returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return returnBytes;
        }

        /// <summary>
        /// Uint16转化成BYTE[]
        /// </summary>
        /// <param name="data">
        /// 待转化的UINT16
        /// </param>
        /// <returns>
        /// BYTE[]
        /// </returns>
        public static byte[] UInt16ToByteArr(ushort data)
        {
            var result = new byte[2];

            // 高8位
            result[0] = Convert.ToByte(data / 256);

            // 低8位
            result[1] = Convert.ToByte(data % 256);
            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The print buffer.
        /// </summary>
        /// <param name="intPtr">
        /// The int ptr.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// </returns>
        private static byte[] PrintBuffer(IntPtr intPtr, int count)
        {
            var result = new byte[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Marshal.ReadByte(intPtr, i);
            }

            return result;
        }

        #endregion
    }
}