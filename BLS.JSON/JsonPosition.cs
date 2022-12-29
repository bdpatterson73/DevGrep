// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BLS.JSON.Utilities;

namespace BLS.JSON
{
    internal enum JsonContainerType
    {
        None,
        Object,
        Array,
        Constructor
    }

    internal struct JsonPosition
    {
        internal bool HasIndex;
        internal int Position;
        internal string PropertyName;
        internal JsonContainerType Type;

        public JsonPosition(JsonContainerType type)
        {
            Type = type;
            HasIndex = TypeHasIndex(type);
            Position = -1;
            PropertyName = null;
        }

        internal void WriteTo(StringBuilder sb)
        {
            switch (Type)
            {
                case JsonContainerType.Object:
                    if (sb.Length > 0)
                        sb.Append(".");
                    sb.Append(PropertyName);
                    break;
                case JsonContainerType.Array:
                case JsonContainerType.Constructor:
                    sb.Append("[");
                    sb.Append(Position);
                    sb.Append("]");
                    break;
            }
        }

        internal static bool TypeHasIndex(JsonContainerType type)
        {
            return (type == JsonContainerType.Array || type == JsonContainerType.Constructor);
        }

        internal static string BuildPath(IEnumerable<JsonPosition> positions)
        {
            StringBuilder sb = new StringBuilder();

            foreach (JsonPosition state in positions)
            {
                state.WriteTo(sb);
            }

            return sb.ToString();
        }

        internal static string FormatMessage(IJsonLineInfo lineInfo, string path, string message)
        {
            // don't add a fullstop and space when message ends with a new line
            if (!message.EndsWith(Environment.NewLine))
            {
                message = message.Trim();

                if (!message.EndsWith("."))
                    message += ".";

                message += " ";
            }

            message += "Path '{0}'".FormatWith(CultureInfo.InvariantCulture, path);

            if (lineInfo != null && lineInfo.HasLineInfo())
                message += ", line {0}, position {1}".FormatWith(CultureInfo.InvariantCulture, lineInfo.LineNumber,
                                                                 lineInfo.LinePosition);

            message += ".";

            return message;
        }
    }
}