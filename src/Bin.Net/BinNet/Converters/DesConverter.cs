// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesConverter.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BinNet.Converters
{
    using System;
    using System.Reflection;
    using System.Security.Cryptography;

    using Eps.Sdk;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DesConverter : IConverter
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Key.
        /// </summary>
        public byte[] Key { get; set; }

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
            byte[] result = null;
            var iv = new byte[8];
            switch (this.Key.Length)
            {
                case 8:
                    result = DesCalculator.DesEncrypt(data, this.Key, new byte[8]);
                    break;
                case 16:
                    var key16Left = new byte[8];
                    var key16Right = new byte[8];
                    Array.Copy(this.Key, key16Left, 8);
                    Array.Copy(this.Key, 8, key16Right, 0, 8);

                    result = DesCalculator.DesEncrypt(data, key16Left, iv);
                    result = DesCalculator.DesDecrypt(result, key16Right, iv);
                    result = DesCalculator.DesEncrypt(result, key16Left, iv);
                    break;
                case 24:
                    var key24Left = new byte[8];
                    var key24Middle = new byte[8];
                    var key24Right = new byte[8];

                    result = DesCalculator.DesEncrypt(data, key24Left, iv);
                    result = DesCalculator.DesDecrypt(result, key24Middle, iv);
                    result = DesCalculator.DesEncrypt(result, key24Right, iv);
                    break;
            }

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
        public byte[] ConvertBack(string source)
        {
            byte[] data = BasicConverter.StringToHexByteArray(source);
            return DesCalculator.DesDecrypt(data, this.Key, new byte[8]);
        }

        #endregion
    }
}