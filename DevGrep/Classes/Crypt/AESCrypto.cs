using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrep.Classes.Crypt
{
    /// <summary>
    /// AES Encryption class
    /// </summary>
    public static class AESCrypto
    {
        /// <summary>
        /// The size of buffer
        /// </summary>
        private const int SizeOfBuffer = 1024 * 8;
        /// <summary>
        /// The DEFAUL t_ SALT
        /// </summary>
        private static readonly byte[] DEFAULT_SALT = Encoding.ASCII.GetBytes("!icu812O");

        /// <summary>
        /// The DEFAUL t_ SECRET
        /// </summary>
        private static string DEFAULT_SECRET = "B0pP01icyS3rv1c3s#/}~*:)";

        /// <summary>
        /// Encrypts a string with a default Key and Salt value.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>System.String.</returns>
        public static string EncryptStringAES(string plainText)
        {
            return EncryptStringAES(plainText, DEFAULT_SECRET, DEFAULT_SALT);
        }

        /// <summary>
        /// Encrypts the string AES.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="sharedSecret">The shared secret.</param>
        /// <returns>System.String.</returns>
        public static string EncryptStringAES(string plainText, string sharedSecret)
        {
            return EncryptStringAES(plainText, sharedSecret, DEFAULT_SALT);
        }


        /// <summary>
        /// Encrypts a string and returns a Base64 string object.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="sharedSecret">The shared secret.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">sharedSecret</exception>
        public static string EncryptStringAES(string plainText, string sharedSecret, byte[] salt)
        {
            // A null or empty string should be returned as-is, so it can remain null/blank for "Required" validations.
            if (string.IsNullOrEmpty(plainText))
                return "";

            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            string outStr = null;
            RijndaelManaged aesAlg = null;

            try
            {
                // Generate the key from the shared secret and the salt.
                var key = new Rfc2898DeriveBytes(sharedSecret, salt);

                // Create a RijndaelManaged object with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                        swEncrypt.Write(plainText);

                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return outStr;
        }

        /// <summary>
        /// Decrypts a string using a default Key and Salt value.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <returns>System.String.</returns>
        public static string DecryptStringAES(string cipherText)
        {
            return DecryptStringAES(cipherText, DEFAULT_SECRET, DEFAULT_SALT);
        }

        /// <summary>
        /// Decrypts the string AES.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="sharedSecret">The shared secret.</param>
        /// <returns>System.String.</returns>
        public static string DecryptStringAES(string cipherText, string sharedSecret)
        {
            return DecryptStringAES(cipherText, sharedSecret, DEFAULT_SALT);
        }

        /// <summary>
        /// Encrypts the file.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="password">The password.</param>
        internal static void EncryptFile(string inputPath, string outputPath, string password)
        {
            var input = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var output = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);

            // Essentially, if you want to use RijndaelManaged as AES you need to make sure that:
            // 1.The block size is set to 128 bits
            // 2.You are not using CFB mode, or if you are the feedback size is also 128 bits

            var algorithm = new RijndaelManaged { KeySize = 256, BlockSize = 128 };
            var key = new Rfc2898DeriveBytes(password, DEFAULT_SALT);

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            using (var encryptedStream = new CryptoStream(output, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
            {
                CopyStream(input, encryptedStream);
            }
        }

        /// <summary>
        /// Copies the stream.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="output">The output.</param>
        private static void CopyStream(Stream input, Stream output)
        {
            using (output)
            using (input)
            {
                var buffer = new byte[SizeOfBuffer];
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    output.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>
        /// Decrypts the file.
        /// </summary>
        /// <param name="inputPath">The input path.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="System.IO.InvalidDataException">Please supply a correct password</exception>
        internal static void DecryptFile(string inputPath, string outputPath, string password)
        {
            var input = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            var output = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write);

            // Essentially, if you want to use RijndaelManaged as AES you need to make sure that:
            // 1.The block size is set to 128 bits
            // 2.You are not using CFB mode, or if you are the feedback size is also 128 bits
            var algorithm = new RijndaelManaged { KeySize = 256, BlockSize = 128 };
            var key = new Rfc2898DeriveBytes(password, DEFAULT_SALT);

            algorithm.Key = key.GetBytes(algorithm.KeySize / 8);
            algorithm.IV = key.GetBytes(algorithm.BlockSize / 8);

            try
            {
                using (
                    var decryptedStream = new CryptoStream(output, algorithm.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    CopyStream(input, decryptedStream);
                }
            }
            catch (CryptographicException)
            {
                throw new InvalidDataException("Please supply a correct password");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed. You may have the wrong password");
            }
        }

        /// <summary>
        /// Decrypts a Base64 encode string object.
        /// </summary>
        /// <param name="cipherText">The cipher text.</param>
        /// <param name="sharedSecret">The shared secret.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">sharedSecret</exception>
        public static string DecryptStringAES(string cipherText, string sharedSecret, byte[] salt)
        {
            // A null or empty string was not encrypted and should be returned as-is.
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");
            if (salt == null || salt.Length == 0)
                throw new ArgumentNullException("salt");

            RijndaelManaged aesAlg = null;
            string plaintext = null;

            try
            {
                // Generate the key from the shared secret and the salt
                var key = new Rfc2898DeriveBytes(sharedSecret, salt);

                // Create a RijndaelManaged object with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDecrypt))
                        plaintext = srDecrypt.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }
    }
}
