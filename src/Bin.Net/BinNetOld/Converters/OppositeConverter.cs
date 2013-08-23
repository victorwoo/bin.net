// -----------------------------------------------------------------------
// <copyright file="OppositeConverter.cs" company="Microsoft">
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
    public class OppositeConverter : IConverter
    {
        public string Convert(byte[] data)
        {
            var result = Opposite(data);
            return BasicConverter.ByteArrayToHexString(result, "{0:X2}");
        }

        private static byte[] Opposite(byte[] data)
        {
            var result = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(0x100 - data[i]);
            }
            return result;
        }

        public byte[] ConvertBack(string source)
        {
            byte[] data = BasicConverter.StringToHexByteArray(source);
            var result = Opposite(data);

            return result;
        }
    }
}
