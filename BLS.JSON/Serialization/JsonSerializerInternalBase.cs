// Copyright 2013 Brian David Patterson <pattersonbriandavid@gmail.com>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BLS.JSON.Utilities;

namespace BLS.JSON.Serialization
{
    internal abstract class JsonSerializerInternalBase
    {
        internal readonly JsonSerializer Serializer;
        internal readonly ITraceWriter TraceWriter;
        private readonly bool _serializing;
        private ErrorContext _currentErrorContext;
        private BidirectionalDictionary<string, object> _mappings;

        protected JsonSerializerInternalBase(JsonSerializer serializer)
        {
            ValidationUtils.ArgumentNotNull(serializer, "serializer");

            Serializer = serializer;
            TraceWriter = serializer.TraceWriter;

            // kind of a hack but meh. might clean this up later
            _serializing = (GetType() == typeof (JsonSerializerInternalWriter));
        }

        internal BidirectionalDictionary<string, object> DefaultReferenceMappings
        {
            get
            {
                // override equality comparer for object key dictionary
                // object will be modified as it deserializes and might have mutable hashcode
                if (_mappings == null)
                    _mappings = new BidirectionalDictionary<string, object>(
                        EqualityComparer<string>.Default,
                        new ReferenceEqualsEqualityComparer(),
                        "A different value already has the Id '{0}'.",
                        "A different Id has already been assigned for value '{0}'.");

                return _mappings;
            }
        }

        private ErrorContext GetErrorContext(object currentObject, object member, string path, Exception error)
        {
            if (_currentErrorContext == null)
                _currentErrorContext = new ErrorContext(currentObject, member, path, error);

            if (_currentErrorContext.Error != error)
                throw new InvalidOperationException("Current error context error is different to requested error.");

            return _currentErrorContext;
        }

        protected void ClearErrorContext()
        {
            if (_currentErrorContext == null)
                throw new InvalidOperationException("Could not clear error context. Error context is already null.");

            _currentErrorContext = null;
        }

        protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue,
                                      IJsonLineInfo lineInfo, string path, Exception ex)
        {
            ErrorContext errorContext = GetErrorContext(currentObject, keyValue, path, ex);

            if (TraceWriter != null && TraceWriter.LevelFilter >= TraceLevel.Error && !errorContext.Traced)
            {
                // only write error once
                errorContext.Traced = true;

                string message = (_serializing) ? "Error serializing" : "Error deserializing";
                if (contract != null)
                    message += " " + contract.UnderlyingType;
                message += ". " + ex.Message;

                // add line information to non-json.net exception message
                if (!(ex is JsonException))
                    message = JsonPosition.FormatMessage(lineInfo, path, message);

                TraceWriter.Trace(TraceLevel.Error, message, ex);
            }

            if (contract != null)
                contract.InvokeOnError(currentObject, Serializer.Context, errorContext);

            if (!errorContext.Handled)
                Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));

            return errorContext.Handled;
        }

        private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
        {
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }

            int IEqualityComparer<object>.GetHashCode(object obj)
            {
#if !(NETFX_CORE)
                // put objects in a bucket based on their reference
                return RuntimeHelpers.GetHashCode(obj);
#else
    // put all objects in the same bucket so ReferenceEquals is called on all
        return -1;
#endif
            }
        }
    }
}