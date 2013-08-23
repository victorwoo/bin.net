// -----------------------------------------------------------------------
// <copyright file="Ansi919Converter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BinNet.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Eps.Sdk;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Ansi919Converter : IConverter
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The convert.
        /// </returns>
        public string Convert(byte[] data)
        {
            if (this.MacKey.Length != 16)
            {
                throw new InvalidOperationException("MacKey length error!");
            }

            var keyL = new byte[8];
            Array.Copy(this.MacKey, 0, keyL, 0, 8);
            var keyR = new byte[8];
            Array.Copy(this.MacKey, 8, keyR, 0, 8);

            var iv = new byte[8];
            var result = EncryptByAnsi919(keyL, keyR, data, iv);
            return BasicConverter.ByteArrayToHexString(result, "{0:X2}");
        }

        /// <summary>
        /// 用Ansi9.19算法生成加密数据
        /// 参见\Application\work\EasyPaymentSystem\doc\握奇卡\TimeCOS_PSAM通用技术参考手册1.0.pdf 23页
        /// </summary>
        /// <param name="keyL">密钥的左半部份</param>
        /// <param name="keyR">密钥的右半部份</param>
        /// <param name="macData">要进行加密的数据</param>
        /// <param name="iv">初始向量</param>
        /// <returns>生成好的加密数据</returns>
        public static byte[] EncryptByAnsi919(byte[] keyL, byte[] keyR, byte[] macData, byte[] iv)
        {
            var resultTemp = new byte[8]; //用于保存上次密后的结果
            for (int i = 0; i < macData.Length / 8; i++)
            {
                if (i == 0)
                {
                    var temp = new byte[8];
                    Array.Copy(macData, (i * 8), temp, 0, 8);

                    /*初始值与BLOCK进行异或 */
                    temp = BasicConverter.ExclusiveOr(temp, iv);

                    /* DES加密 */
                    resultTemp = DesCalculator.DesEncrypt(temp, keyL, new byte[8]);
                }
                else
                {
                    var temp = new byte[8];
                    Array.Copy(macData, (i * 8), temp, 0, 8);

                    //非第一块加密，后一块与前一块的加密后的结果进行异或
                    temp = BasicConverter.ExclusiveOr(temp, resultTemp);

                    /* DES加密 */
                    resultTemp = DesCalculator.DesEncrypt(temp, keyL, new byte[8]);
                }
            }
            /* 用takR对最后一次加密结果解密 */
            byte[] takRDecrypt = DesCalculator.DesDecrypt(resultTemp, keyR, new byte[8]);

            /* 用TAKL对解密结果加密 */
            byte[] takLEncrypt = DesCalculator.DesEncrypt(takRDecrypt, keyL, new byte[8]);

            /* 得到最后4字节MAC */
            var result = new byte[4];
            Array.Copy(takLEncrypt, 0, result, 0, 4);

            return result;
        }


        public byte[] MacKey { get; set; }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// </exception>
        public byte[] ConvertBack(string source)
        {
            throw new NotSupportedException();
        }
    }
}
