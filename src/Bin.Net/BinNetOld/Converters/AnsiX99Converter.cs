// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnsiX99Converter.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BinNet.Converters
{
    using System;

    using Eps.Sdk;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AnsiX99Converter : IConverter
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets MacKey.
        /// </summary>
        public byte[] MacKey { get; set; }

        #endregion

        #region Public Methods and Operators

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
            var resultTemp = new byte[8];
            Array.Copy(data, 0, resultTemp, 0, 8);

            for (int i = 1; i < data.Length / 8; i++)
            {
                var temp = new byte[8];
                Array.Copy(data, i * 8, temp, 0, 8);

                /*初始值与BLOCK进行异或 */
                resultTemp = BasicConverter.ExclusiveOr(temp, resultTemp);
            }

            byte[] result = DesCalculator.DesEncrypt(resultTemp, this.MacKey, new byte[8]);
            return BasicConverter.ByteArrayToHexString(result, "{0:X2}");
        }

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

        #endregion
    }
}