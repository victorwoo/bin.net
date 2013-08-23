// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BitmapConverter.cs" company="">
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
    public class BitmapConverter : IConverter
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
            return BasicConverter.GenerateBitmapString(data);
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