// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MD4Converter.cs" company="">
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

    using HashLib;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MD4Converter : IConverter
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
            byte[] hash = HashFactory.Crypto.CreateMD4().ComputeBytes(data).GetBytes();
            return BasicConverter.ByteArrayToHexString(hash, "{0:X2}");
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