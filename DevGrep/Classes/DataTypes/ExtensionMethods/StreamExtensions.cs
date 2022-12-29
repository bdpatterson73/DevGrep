#region Usings

using System.IO;
using System.Text;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// Extension methods for Streams
    /// </summary>
    internal static class StreamExtensions
    {
        #region Functions

        #region ReadAllBinary

        /// <summary>
        /// Takes all of the data in the stream and returns it as an array of bytes
        /// </summary>
        /// <param name="Input">Input stream</param>
        /// <returns>A byte array</returns>
        public static byte[] ReadAllBinary(this Stream Input)
        {
            Input.ThrowIfNull("Input");
            if (Input is MemoryStream)
                return ((MemoryStream) Input).ToArray();
            var Buffer = new byte[1024];
            byte[] ReturnValue = null;
            using (var Temp = new MemoryStream())
            {
                while (true)
                {
                    int Count = Input.Read(Buffer, 0, Buffer.Length);
                    if (Count <= 0)
                    {
                        ReturnValue = Temp.ToArray();
                        break;
                    }
                    Temp.Write(Buffer, 0, Count);
                }
                Temp.Close();
            }
            return ReturnValue;
        }

        #endregion

        #region ReadAll

        /// <summary>
        /// Takes all of the data in the stream and returns it as a string
        /// </summary>
        /// <param name="Input">Input stream</param>
        /// <param name="EncodingUsing">Encoding that the string should be in (defaults to UTF8)</param>
        /// <returns>A string containing the content of the stream</returns>
        public static string ReadAll(this Stream Input, Encoding EncodingUsing = null)
        {
            return Input.ReadAllBinary().ToEncodedString(EncodingUsing);
        }

        #endregion

        #endregion
    }
}