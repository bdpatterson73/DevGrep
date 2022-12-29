// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#if NET20
using BLS.JSON.Utilities.LinqBridge;
#else

#endif

namespace BLS.JSON.Utilities
{
    internal static class JavaScriptUtils
    {
        private const string EscapedUnicodeText = "!";
        internal static readonly bool[] SingleQuoteCharEscapeFlags = new bool[128];
        internal static readonly bool[] DoubleQuoteCharEscapeFlags = new bool[128];
        internal static readonly bool[] HtmlCharEscapeFlags = new bool[128];

        static JavaScriptUtils()
        {
            IList<char> escapeChars = new List<char>
                {
                    '\n',
                    '\r',
                    '\t',
                    '\\',
                    '\f',
                    '\b',
                };
            for (int i = 0; i < ' '; i++)
            {
                escapeChars.Add((char) i);
            }

            foreach (var escapeChar in escapeChars.Union(new[] {'\''}))
            {
                SingleQuoteCharEscapeFlags[escapeChar] = true;
            }
            foreach (var escapeChar in escapeChars.Union(new[] {'"'}))
            {
                DoubleQuoteCharEscapeFlags[escapeChar] = true;
            }
            foreach (var escapeChar in escapeChars.Union(new[] {'"', '\'', '<', '>', '&'}))
            {
                HtmlCharEscapeFlags[escapeChar] = true;
            }
        }

        public static void WriteEscapedJavaScriptString(TextWriter writer, string s, char delimiter,
                                                        bool appendDelimiters,
                                                        bool[] charEscapeFlags,
                                                        StringEscapeHandling stringEscapeHandling,
                                                        ref char[] writeBuffer)
        {
            // leading delimiter
            if (appendDelimiters)
                writer.Write(delimiter);

            if (s != null)
            {
                int lastWritePosition = 0;

                for (int i = 0; i < s.Length; i++)
                {
                    var c = s[i];

                    if (c < charEscapeFlags.Length && !charEscapeFlags[c])
                        continue;

                    string escapedValue;

                    switch (c)
                    {
                        case '\t':
                            escapedValue = @"\t";
                            break;
                        case '\n':
                            escapedValue = @"\n";
                            break;
                        case '\r':
                            escapedValue = @"\r";
                            break;
                        case '\f':
                            escapedValue = @"\f";
                            break;
                        case '\b':
                            escapedValue = @"\b";
                            break;
                        case '\\':
                            escapedValue = @"\\";
                            break;
                        case '\u0085': // Next Line
                            escapedValue = @"\u0085";
                            break;
                        case '\u2028': // Line Separator
                            escapedValue = @"\u2028";
                            break;
                        case '\u2029': // Paragraph Separator
                            escapedValue = @"\u2029";
                            break;
                        default:
                            if (c < charEscapeFlags.Length ||
                                stringEscapeHandling == StringEscapeHandling.EscapeNonAscii)
                            {
                                if (c == '\'' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
                                {
                                    escapedValue = @"\'";
                                }
                                else if (c == '"' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
                                {
                                    escapedValue = @"\""";
                                }
                                else
                                {
                                    if (writeBuffer == null)
                                        writeBuffer = new char[6];

                                    StringUtils.ToCharAsUnicode(c, writeBuffer);

                                    // slightly hacky but it saves multiple conditions in if test
                                    escapedValue = EscapedUnicodeText;
                                }
                            }
                            else
                            {
                                escapedValue = null;
                            }
                            break;
                    }

                    if (escapedValue == null)
                        continue;

                    bool isEscapedUnicodeText = string.Equals(escapedValue, EscapedUnicodeText);

                    if (i > lastWritePosition)
                    {
                        int length = i - lastWritePosition + ((isEscapedUnicodeText) ? 6 : 0);
                        int start = (isEscapedUnicodeText) ? 6 : 0;

                        if (writeBuffer == null || writeBuffer.Length < length)
                        {
                            char[] newBuffer = new char[length];

                            // the unicode text is already in the buffer
                            // copy it over when creating new buffer
                            if (isEscapedUnicodeText)
                                Array.Copy(writeBuffer, newBuffer, 6);

                            writeBuffer = newBuffer;
                        }

                        s.CopyTo(lastWritePosition, writeBuffer, start, length - start);

                        // write unchanged chars before writing escaped text
                        writer.Write(writeBuffer, start, length - start);
                    }

                    lastWritePosition = i + 1;
                    if (!isEscapedUnicodeText)
                        writer.Write(escapedValue);
                    else
                        writer.Write(writeBuffer, 0, 6);
                }

                if (lastWritePosition == 0)
                {
                    // no escaped text, write entire string
                    writer.Write(s);
                }
                else
                {
                    int length = s.Length - lastWritePosition;

                    if (writeBuffer == null || writeBuffer.Length < length)
                        writeBuffer = new char[length];

                    s.CopyTo(lastWritePosition, writeBuffer, 0, length);

                    // write remaining text
                    writer.Write(writeBuffer, 0, length);
                }
            }

            // trailing delimiter
            if (appendDelimiters)
                writer.Write(delimiter);
        }

        public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
        {
            using (StringWriter w = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
            {
                char[] buffer = null;
                WriteEscapedJavaScriptString(w, value, delimiter, appendDelimiters,
                                             (delimiter == '"')
                                                 ? DoubleQuoteCharEscapeFlags
                                                 : SingleQuoteCharEscapeFlags, StringEscapeHandling.Default, ref buffer);
                return w.ToString();
            }
        }
    }
}