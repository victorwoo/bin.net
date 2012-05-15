// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utf8StringConverter.cs" company="">
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
    public class Utf8StringConverter : IConverter
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
            return Encoding.UTF8.GetString(data);
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
            return Encoding.UTF8.GetBytes(source);
        }

        #endregion
    }
}