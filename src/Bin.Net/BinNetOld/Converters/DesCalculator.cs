// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DesCalculator.cs" company="">
//   
// </copyright>
// <summary>
//   TODO: Update summary.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BinNet.Converters
{
    using System;
    using System.Reflection;
    using System.Security.Cryptography;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DesCalculator
    {
        #region Public Methods and Operators

        /// <summary>
        /// 比较两个字节数组是否相等。
        /// </summary>
        /// <param name="array1">
        /// 字节数组1.
        /// </param>
        /// <param name="array2">
        /// 字节数组2.
        /// </param>
        /// <returns>
        /// <value>
        /// true
        /// </value>
        /// 代表两个数组相同。
        /// <value>
        /// false
        /// </value>
        /// 代表两个数组不同。
        /// </returns>
        public static bool Compare(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 3DES解密算法
        /// </summary>
        /// <param name="cipherText">
        /// 密文
        /// </param>
        /// <param name="key">
        /// 密钥
        /// </param>
        /// <param name="iv">
        /// 向量
        /// </param>
        /// <returns>
        /// 明文
        /// </returns>
        public static byte[] Des3Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            if (key.Length % 2 == 1)
            {
                throw new ArgumentException("密钥所含数据个数必须为2的倍数！", "key");
            }

            var desProvider = new TripleDESCryptoServiceProvider
                {
                    // 密钥
                    Key = key, 
                    // 向量
                    IV = iv, 
                    // 每块独立解密
                    Mode = CipherMode.ECB, 
                    // 无需填充
                    Padding = PaddingMode.None
                };

            return Decrypt(cipherText, key, iv, desProvider);
        }

        /// <summary>
        /// 使用3DES加密算法，利用KEY对BLOCK数据块进行加密
        /// </summary>
        /// <param name="plainText">
        /// 明文数据块
        /// </param>
        /// <param name="key">
        /// 密钥(16字节)
        /// </param>
        /// <param name="iv">
        /// IV(8字节为空)
        /// </param>
        /// <returns>
        /// 密文数据块
        /// </returns>
        public static byte[] Des3Encrypt(byte[] plainText, byte[] key, byte[] iv)
        {
            if (key.Length % 2 == 1)
            {
                throw new ArgumentException("密钥所含数据个数必须为2的倍数！", "key");
            }

            SymmetricAlgorithm desProvider;

            desProvider = new TripleDESCryptoServiceProvider
                {
                    // 密钥
                    // Key = key, 
                    // 向量
                    IV = iv, 
                    // 每块独立解密
                    Mode = CipherMode.ECB, 
                    // 无需填充
                    Padding = PaddingMode.None
                };

            return Encrypt(plainText, key, iv, desProvider);
        }

        /// <summary>
        /// DES解密算法
        /// </summary>
        /// <param name="cipherText">
        /// 密文
        /// </param>
        /// <param name="key">
        /// 密钥
        /// </param>
        /// <param name="iv">
        /// 向量
        /// </param>
        /// <returns>
        /// 明文
        /// </returns>
        public static byte[] DesDecrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            var desProvider = new DESCryptoServiceProvider
                {
                    // 向量
                    IV = iv, 
                    // 每块独立加密
                    Mode = CipherMode.ECB, 
                    // 无需填充
                    Padding = PaddingMode.None
                };

            return Decrypt(cipherText, key, iv, desProvider);
        }

        /// <summary>
        /// 使用DES加密算法，利用KEY对BLOCK数据块进行加密
        /// </summary>
        /// <param name="plainText">
        /// 明文数据块
        /// </param>
        /// <param name="key">
        /// 密钥(8字节)
        /// </param>
        /// <param name="iv">
        /// IV(8字节)
        /// </param>
        /// <returns>
        /// 密文数据块
        /// </returns>
        public static byte[] DesEncrypt(byte[] plainText, byte[] key, byte[] iv)
        {
            var desProvider = new DESCryptoServiceProvider
                {
                    // 向量
                    IV = iv, 
                    // 每块独立加密
                    Mode = CipherMode.ECB, 
                    // 无需填充
                    Padding = PaddingMode.None
                };

            return Encrypt(plainText, key, iv, desProvider);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The decrypt.
        /// </summary>
        /// <param name="cipherText">
        /// The cipher text.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="iv">
        /// The iv.
        /// </param>
        /// <param name="desProvider">
        /// The des provider.
        /// </param>
        /// <returns>
        /// </returns>
        private static byte[] Decrypt(byte[] cipherText, byte[] key, byte[] iv, SymmetricAlgorithm desProvider)
        {
            /* DES解密 */

            // ICryptoTransform cryptoTransform = desProvider.CreateDecryptor(desProvider.Key, desProvider.IV);
            // 密钥
            MethodInfo mi = desProvider.GetType().GetMethod(
                "_NewEncryptor", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] param = { key, CipherMode.ECB, iv, desProvider.FeedbackSize, 1 };
            var desEncrypt = (ICryptoTransform)mi.Invoke(desProvider, param);

            // var ms = new MemoryStream();
            // var myCryptoStream = new CryptoStream(ms, desEncrypt, CryptoStreamMode.Write);
            // myCryptoStream.Write(cipherText, 0, cipherText.Length);
            // return ms.ToArray();
            return desEncrypt.TransformFinalBlock(cipherText, 0, cipherText.Length);
        }

        /// <summary>
        /// The encrypt.
        /// </summary>
        /// <param name="plainText">
        /// The plain text.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="iv">
        /// The iv.
        /// </param>
        /// <param name="desProvider">
        /// The des provider.
        /// </param>
        /// <returns>
        /// </returns>
        private static byte[] Encrypt(byte[] plainText, byte[] key, byte[] iv, SymmetricAlgorithm desProvider)
        {
            int appendLength = 8 - (plainText.Length % 8);
            appendLength = appendLength == 8 ? 0 : appendLength;
            if (appendLength != 0)
            {
                var newArray = new byte[plainText.Length + appendLength];
                Array.Copy(plainText, newArray, plainText.Length);
                plainText = newArray;
            }

            // ICryptoTransform cryptoTransform = desProvider.CreateEncryptor(desProvider.Key, desProvider.IV);

            // 密钥
            MethodInfo mi = desProvider.GetType().GetMethod(
                "_NewEncryptor", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] param = { key, CipherMode.ECB, iv, desProvider.FeedbackSize, 0 };
            var desEncrypt = (ICryptoTransform)mi.Invoke(desProvider, param);

            // var ms = new MemoryStream();
            // var myCryptoStream = new CryptoStream(ms, desEncrypt, CryptoStreamMode.Write);
            // myCryptoStream.Write(plainText, 0, plainText.Length);
            // /* 密文 */
            // return ms.ToArray();
            return desEncrypt.TransformFinalBlock(plainText, 0, plainText.Length);
        }

        #endregion
    }
}