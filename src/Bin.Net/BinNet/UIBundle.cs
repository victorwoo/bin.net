// -----------------------------------------------------------------------
// <copyright file="UIBundle.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BinNet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UIBundle
    {
        public TextBox TextBox { get; set; }

        public Button CopyButton { get; set; }

        public Button PasteButton { get; set; }

        public bool IsPastable { get; set; }

        public IConverter Converter { get; set; }

        public bool IsInputSource { get; set; }
    }
}
