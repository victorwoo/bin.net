// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="">
//   
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BinNet
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using Eps.Sdk;

    using BinNet.Converters;

    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constants and Fields

        /// <summary>
        /// The ansi 919 converter.
        /// </summary>
        private readonly Ansi919Converter ansi919Converter = new Ansi919Converter();

        /// <summary>
        /// The ansi x 99 converter.
        /// </summary>
        private readonly AnsiX99Converter ansiX99Converter = new AnsiX99Converter();

        /// <summary>
        /// The des converter.
        /// </summary>
        private readonly DesConverter desConverter = new DesConverter();

        /// <summary>
        /// The byte array.
        /// </summary>
        private byte[] byteArray;

        /// <summary>
        /// The changing triggered.
        /// </summary>
        private bool changingTriggered;

        /// <summary>
        /// The current inputing bundle.
        /// </summary>
        private UIBundle currentInputingBundle;

        /// <summary>
        /// The initialize finished.
        /// </summary>
        private bool initializeFinished;

        /// <summary>
        /// The ui bundles.
        /// </summary>
        private List<UIBundle> uiBundles;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The bind ui bundle events.
        /// </summary>
        private void BindUIBundleEvents()
        {
            foreach (UIBundle uiBundle in this.uiBundles)
            {
                if (uiBundle.IsPastable)
                {
                    uiBundle.TextBox.TextChanged += this.tb_TextChanged;

                    if (uiBundle.PasteButton != null)
                    {
                        uiBundle.PasteButton.Click += this.btnPaste_Click;
                    }
                }

                if (uiBundle.CopyButton != null)
                {
                    uiBundle.CopyButton.Click += this.btnCopy_Click;
                }
            }
        }

        /// <summary>
        /// The initialize ui bundles.
        /// </summary>
        private void CreateUIBundles()
        {
            this.uiBundles = new List<UIBundle> {
                    new UIBundle
                        {
                            Converter = new NormalHexConverter(), 
                            CopyButton = this.btnCopyNormalHex, 
                            PasteButton = this.btnPasteNormalHex, 
                            TextBox = this.tbNormalHex, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new CompactHexConverter(), 
                            CopyButton = this.btnCopyCompactHex, 
                            PasteButton = this.btnPasteCompactHex, 
                            TextBox = this.tbCompactHex, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new CStyleHexConverter(), 
                            CopyButton = this.btnCopyCStryleHex, 
                            PasteButton = this.btnPasteCStyleHex, 
                            TextBox = this.tbCStyleHex, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new AnsiStringConverter(), 
                            CopyButton = this.btnCopyAnsiString, 
                            PasteButton = this.btnPasteAnsiString, 
                            TextBox = this.tbAnsiString, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Utf8StringConverter(), 
                            CopyButton = this.btnCopyUtf8String, 
                            PasteButton = this.btnPasteUtf8String, 
                            TextBox = this.tbUtf8, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new OppositeConverter(), 
                            CopyButton = this.btnCopyOpposite, 
                            PasteButton = this.btnPasteOpposite, 
                            TextBox = this.tbOpposite, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new SignedDecimalConverter(), 
                            CopyButton = this.btnCopySignedDec, 
                            PasteButton = this.btnPasteSignedDec, 
                            TextBox = this.tbSignedDec, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new MD5Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbMD5, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new MD4Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbMD4, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new MD2Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbMD2, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Sha1Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbSha1, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Sha256Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbSha256, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Sha384Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbSha384, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Sha512Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbSha512, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Crc32Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbCrc32, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Crc64Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbCrc64, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new Xor1Converter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbXor1, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new LrcConverter(), 
                            CopyButton = null, 
                            PasteButton = null, 
                            TextBox = this.tbLrc, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = this.desConverter, 
                            CopyButton = this.btnCopyDes, 
                            PasteButton = this.btnPasteDes, 
                            TextBox = this.tbDes, 
                            IsPastable = true, 
                        }, 
                    new UIBundle
                        {
                            Converter = new BitmapConverter(), 
                            CopyButton = this.btnCopyBitmap, 
                            PasteButton = null, 
                            TextBox = this.tbBitmap, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = this.ansiX99Converter, 
                            CopyButton = this.btnCopyAnsiX99, 
                            PasteButton = null, 
                            TextBox = this.tbAnsiX99, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = this.ansi919Converter, 
                            CopyButton = this.btnCopyAnsi919, 
                            PasteButton = null, 
                            TextBox = this.tbAnsi919, 
                            IsPastable = false, 
                        }, 
                    new UIBundle
                        {
                            Converter = new OutlineConverter(), 
                            CopyButton = this.btnCopyOutline, 
                            PasteButton = null, 
                            TextBox = this.tbOutline, 
                            IsPastable = false, 
                        }, 
                };
        }

        /// <summary>
        /// The initialize des options.
        /// </summary>
        private void InitializeDesOptions()
        {
            byte[] desKey;
            try
            {
                desKey = BasicConverter.StringToHexByteArray(this.tbDesKey.Text);
            }
            catch (Exception e)
            {
                desKey = null;
            }

            this.desConverter.Key = desKey;
        }

        /// <summary>
        /// The initialize mac options.
        /// </summary>
        private void InitializeMacOptions()
        {
            byte[] macKey;
            try
            {
                macKey = BasicConverter.StringToHexByteArray(this.tbMacKey.Text);
            }
            catch (Exception e)
            {
                macKey = null;
            }

            this.ansiX99Converter.MacKey = macKey;
            this.ansi919Converter.MacKey = macKey;
        }

        /// <summary>
        /// The initialize tool tip.
        /// </summary>
        private void InitializeToolTip()
        {
            var toolTip1 = new ToolTip { AutoPopDelay = 5000, InitialDelay = 500, ReshowDelay = 100, ShowAlways = true };

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(
                this.tbDesKey, 
                "Normal Hex or Compact Hex format. 8 bytes for Des, 16 bytes for 3Des(16), 24 bytes for 3Des(24).");
            toolTip1.SetToolTip(
                this.tbMacKey, "Normal Hex or Compact Hex format. 16 bytes for Ansi x9.9, 32 bytes for Ansi 919");
            toolTip1.SetToolTip(this.tbBitmap, "Binary data must be 4/16/32 bytes");
        }

        /// <summary>
        /// The main form_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitializeToolTip();
            this.Text = string.Format("Hex Tool v{0}", Application.ProductVersion);
            this.tbDesKey.Text = "1234567890ABCDEF";
            this.tbMacKey.Text = "1234567890ABCDEF";

            this.initializeFinished = true;
            this.InitializeDesOptions();
            this.InitializeMacOptions();
            this.CreateUIBundles();
            this.BindUIBundleEvents();
            this.tbAnsiString.Text = "福建实达电脑设备有限公司 - Fujian Start Computer Co,. LTD*******";

            // this.UpdateDisplay();
            this.tbNormalHex.Select(0, 0);
        }

        /// <summary>
        /// The update display.
        /// </summary>
        private void UpdateDisplay()
        {
            try
            {
                switch (this.desConverter.Key.Length)
                {
                    case 8:
                        this.lblDes.Text = "DES";
                        break;
                    case 16:
                        this.lblDes.Text = "3DES(16)";
                        break;
                    case 24:
                        this.lblDes.Text = "3DES(24)";
                        break;
                    default:
                        this.lblDes.Text = "DES/3DES";
                        break;
                }

                foreach (UIBundle uiBundle in this.uiBundles)
                {
                    if (this.currentInputingBundle == uiBundle)
                    {
                        continue;
                    }

                    try
                    {
                        uiBundle.TextBox.Text = uiBundle.Converter.Convert(this.byteArray);
                        Debug.WriteLine(string.Format("{0}-{1}", uiBundle.TextBox.Name, uiBundle.TextBox.Text));
                    }
                    catch (Exception e)
                    {
                        uiBundle.TextBox.Text = string.Empty;
                    }

                    this.tspbMain.PerformStep();
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
            finally
            {
                this.tspbMain.Visible = false;
            }
        }

        /// <summary>
        /// The btn copy_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.uiBundles.Single(item => item.CopyButton == sender).TextBox.Text);
        }

        /// <summary>
        /// The btn paste des key_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnPasteDesKey_Click(object sender, EventArgs e)
        {
            this.tbDesKey.Text = Clipboard.GetText();
        }

        /// <summary>
        /// The btn paste_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnPaste_Click(object sender, EventArgs e)
        {
            this.uiBundles.Single(item => item.PasteButton == sender).TextBox.Text = Clipboard.GetText();
        }

        /// <summary>
        /// The tb des key_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tbDesKey_TextChanged(object sender, EventArgs e)
        {
            if (!this.initializeFinished)
            {
                return;
            }

            this.changingTriggered = true;
            try
            {
                this.InitializeDesOptions();
                this.UpdateDisplay();
            }
            finally
            {
                this.changingTriggered = false;
            }
        }

        /// <summary>
        /// The tb mac key_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tbMacKey_TextChanged(object sender, EventArgs e)
        {
            if (!this.initializeFinished)
            {
                return;
            }

            this.changingTriggered = true;
            try
            {
                this.InitializeMacOptions();
                this.UpdateDisplay();
            }
            finally
            {
                this.changingTriggered = false;
            }
        }

        /// <summary>
        /// The tb_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tb_TextChanged(object sender, EventArgs e)
        {
            if (!this.initializeFinished || this.changingTriggered)
            {
                return;
            }

            this.changingTriggered = true;

            this.tspbMain.Visible = true;
            this.tspbMain.Maximum = this.uiBundles.Count + 1;
            UIBundle uiBundle = this.uiBundles.Single(item => item.TextBox == sender);
            this.currentInputingBundle = uiBundle;
            try
            {
                try
                {
                    this.byteArray = uiBundle.Converter.ConvertBack(uiBundle.TextBox.Text);
                }
                catch (Exception exception)
                {
                    this.byteArray = new byte[0];
                }

                this.tspbMain.PerformStep();
                this.UpdateDisplay();
            }
            finally
            {
                this.changingTriggered = false;
                this.currentInputingBundle = null;
            }
        }

        /// <summary>
        /// The tssl copy right_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tsslCopyRight_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:" + this.tsslCopyRight.Text);
        }

        #endregion
    }
}