// -----------------------------------------------------------------------
// <copyright file="CompactHexConverter.cs" company="Microsoft">
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
    public class CompactHexConverter : IConverter
    {
        public string Convert(byte[] data)
        {
            return BasicConverter.ByteArrayToHexString(data, "{0:X2}");
        }

        public byte[] ConvertBack(string source)
        {
            return BasicConverter.StringToHexByteArray(source);
        }
    }
}
