// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Xor1Converter.cs" company="">
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
    public class Xor1Converter : IConverter
    {
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
            int temp = 0;
            foreach (byte b in data)
            {
                temp = temp ^ b;
            }

            var result = (byte)(temp & 0xFF);
            return BasicConverter.ByteArrayToHexString(new[] { result }, "{0:X2}");
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