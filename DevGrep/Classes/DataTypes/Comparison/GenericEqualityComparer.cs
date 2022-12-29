#region Usings

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace DevGrep.Classes.DataTypes.Comparison
{
    /// <summary>
    /// Generic equality comparer
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    internal class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        #region Functions

        /// <summary>
        /// Determines if the two items are equal
        /// </summary>
        /// <param name="x">Object 1</param>
        /// <param name="y">Object 2</param>
        /// <returns>True if they are, false otherwise</returns>
        public bool Equals(T x, T y)
        {
            if (!typeof (T).IsValueType
                || (typeof (T).IsGenericType
                    && typeof (T).GetGenericTypeDefinition().IsAssignableFrom(typeof (Nullable<>))))
            {
                if (Object.Equals(x, default(T)))
                    return Object.Equals(y, default(T));
                if (Object.Equals(y, default(T)))
                    return false;
            }
            if (x.GetType() != y.GetType())
                return false;
            if (x is IEnumerable && y is IEnumerable)
            {
                var Comparer = new GenericEqualityComparer<object>();
                IEnumerator XEnumerator = ((IEnumerable) x).GetEnumerator();
                IEnumerator YEnumerator = ((IEnumerable) y).GetEnumerator();
                while (true)
                {
                    bool XFinished = !XEnumerator.MoveNext();
                    bool YFinished = !YEnumerator.MoveNext();
                    if (XFinished || YFinished)
                        return XFinished & YFinished;
                    if (!Comparer.Equals(XEnumerator.Current, YEnumerator.Current))
                        return false;
                }
            }
            if (x is IEquatable<T>)
                return ((IEquatable<T>) x).Equals(y);
            if (x is IComparable<T>)
                return ((IComparable<T>) x).CompareTo(y) == 0;
            if (x is IComparable)
                return ((IComparable) x).CompareTo(y) == 0;
            return x.Equals(y);
        }

        /// <summary>
        /// Get hash code
        /// </summary>
        /// <param name="obj">Object to get the hash code of</param>
        /// <returns>The object's hash code</returns>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}