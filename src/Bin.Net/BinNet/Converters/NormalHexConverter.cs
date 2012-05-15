namespace BinNet.Converters
{
    using Eps.Sdk;

    public class NormalHexConverter : IConverter
    {
        public string Convert(byte[] data)
        {
            return BasicConverter.ByteArrayToHexString(data, "{0:X2} ");
        }

        public byte[] ConvertBack(string source)
        {
            return BasicConverter.StringToHexByteArray(source);
        }
    }
}