﻿// -----------------------------------------------------------------------
// <copyright file="Sha256Converter.cs" company="Microsoft">
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
    public class Sha256Converter : IConverter
    {
        public string Convert(byte[] data)
        {
            var hash = HashFactory.Crypto.CreateSHA256().ComputeBytes(data).GetBytes();
            return BasicConverter.ByteArrayToHexString(hash, "{0:X2}");
        }

        public byte[] ConvertBack(string source)
        {
            throw new NotSupportedException();
        }
    }
}
