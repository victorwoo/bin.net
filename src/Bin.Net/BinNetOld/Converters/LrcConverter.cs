// -----------------------------------------------------------------------
// <copyright file="LrcConverter.cs" company="Microsoft">
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
    public class LrcConverter : IConverter
    {
        public string Convert(byte[] data)
        {
            int temp = 0;
            foreach (var b in data)
            {
                temp += b;
            }

            byte result = (byte)(0x100 - (temp & 0xFF));
            return BasicConverter.ByteArrayToHexString(new[] { result }, "{0:X2}");
        }

        public byte[] ConvertBack(string source)
        {
            throw new NotSupportedException();
        }
    }
}
