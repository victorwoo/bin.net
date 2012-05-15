// -----------------------------------------------------------------------
// <copyright file="Crc16Converter.cs" company="Microsoft">
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

    using HashLib;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Crc64Converter : IConverter
    {
        public string Convert(byte[] data)
        {
            byte[] hash = HashFactory.Checksum.CreateCRC64a().ComputeBytes(data).GetBytes();
            return BasicConverter.ByteArrayToHexString(hash, "{0:X2}");
        }

        public byte[] ConvertBack(string source)
        {
            throw new NotSupportedException();
        }
    }
}
