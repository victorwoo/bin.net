namespace BinNet
{
    /// <summary>
    /// The current typing text box.
    /// </summary>
    public enum BundleType
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = -1,

        /// <summary>
        /// The normal hex.
        /// </summary>
        NormalHex = 0,

        /// <summary>
        /// The compact hex.
        /// </summary>
        CompactHex = 1,

        /// <summary>
        /// The c style hex.
        /// </summary>
        CStyleHex = 2,

        /// <summary>
        /// The ansi string.
        /// </summary>
        AnsiString = 3,

        /// <summary>
        /// The utf 8 string.
        /// </summary>
        Utf8String = 4,

        /// <summary>
        /// The bitmap.
        /// </summary>
        Bitmap = 5,
    }
}