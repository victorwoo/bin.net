namespace BinNet
{
    public interface IConverter
    {
        string Convert(byte[] data);

        byte[] ConvertBack(string source);
    }
}