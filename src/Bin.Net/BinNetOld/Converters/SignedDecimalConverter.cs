// -----------------------------------------------------------------------
// <copyright file="SignedDecimalConverter.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BinNet.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SignedDecimalConverter : IConverter
    {
        public string Convert(byte[] data)
        {
            var sb = new StringBuilder();
            sb.Append("[ ");
            foreach (var b in data)
            {
                int i = b >= 0x80 ? b - 0x100 : b;
                sb.Append(i.ToString());
                sb.Append(", ");
            }

            sb.Append("]");
            return sb.ToString();
        }

        public byte[] ConvertBack(string source)
        {
            var items = source.Split(new char[] { '[', ']', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return items.Select(
                item =>
                    {
                        var i = int.Parse(item);
                        return (byte)(i < 0 ? i + 256 : i);
                    }).ToArray();
        }
    }
}
