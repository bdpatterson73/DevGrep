using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.SubApps.DupFileScan.Classes
{
    /// <summary>
    ///     Hash methods.
    /// </summary>
    /// <remarks></remarks>
    internal static class RIPEMD160Hash
    {
        /// <summary>
        ///     Returns a SHA1 hash for a file.
        /// </summary>
        /// <param name="fileNamePath">The file name path.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static string HashFile(string fileNamePath)
        {
            using (HashAlgorithm hashAlg = new RIPEMD160Managed())
            {
                try
                {
                    using (Stream file = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read))
                    {
                        byte[] hash = hashAlg.ComputeHash(file);
                        return BitConverter.ToString(hash).Replace("-", "");
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     Returns a SHA1 hash for a string.
        /// </summary>
        /// <param name="inString">The in string.</param>
        /// <returns>
        ///     <c>String</c> object containing the SHA1 hash.
        /// </returns>
        /// <remarks></remarks>
        internal static string HashString(string inString)
        {
            using (HashAlgorithm hashAlg = new RIPEMD160Managed())
            {
                try
                {
                    var encoding = new ASCIIEncoding();
                    byte[] hash = hashAlg.ComputeHash(encoding.GetBytes(inString));
                    return BitConverter.ToString(hash).Replace("-", "");
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        ///     Hashes the objects.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static string HashObjects(params Object[] args)
        {
            var objectArray = new List<byte[]>();
            // Build a binary string of all objects.
            long ttlSize = 0;
            foreach (object o in args)
            {
                if (o != null)
                {
                    var encoding = new ASCIIEncoding();
                    string temp = o.ToString();
                    byte[] converted = encoding.GetBytes(o.ToString());
                    objectArray.Add(converted);
                    ttlSize += converted.Length;
                }
            }
            var arrayObject = new byte[ttlSize];
            int copied = 0;
            foreach (var ar in objectArray)
            {
                Buffer.BlockCopy(ar, 0, arrayObject, copied, ar.Length);
                copied += ar.Length;
            }

            using (HashAlgorithm hashAlg = new RIPEMD160Managed())
            {
                try
                {
                    var encoding = new ASCIIEncoding();
                    byte[] hash = hashAlg.ComputeHash(arrayObject);
                    return BitConverter.ToString(hash).Replace("-", "");
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
