﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnsiStringConverter.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BinNet.Converters
{
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class AnsiStringConverter : IConverter
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
            return Encoding.Default.GetString(data);
        }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// </returns>
        public byte[] ConvertBack(string source)
        {
            return Encoding.Default.GetBytes(source);
        }

        #endregion
    }
}