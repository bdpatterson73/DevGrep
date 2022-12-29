#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using DevGrep.Classes.DataTypes.Comparison;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// IDictionary extensions
    /// </summary>
    internal static class IDictionaryExtensions
    {
        #region Functions

        #region GetValue

        /// <summary>
        /// Gets the value from a dictionary or the default value if it isn't found
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="Dictionary">Dictionary to get the value from</param>
        /// <param name="Key">Key to look for</param>
        /// <param name="Default">Default value if the key is not found</param>
        /// <returns>The value associated with the key or the default value if the key is not found</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the dictionary is null</exception>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> Dictionary, TKey Key,
                                                    TValue Default = default(TValue))
        {
            Dictionary.ThrowIfNull("Dictionary");
            TValue ReturnValue = Default;
            return Dictionary.TryGetValue(Key, out ReturnValue) ? ReturnValue : Default;
        }

        #endregion

        #region Sort

        /// <summary>
        /// Sorts a dictionary
        /// </summary>
        /// <typeparam name="T1">Key type</typeparam>
        /// <typeparam name="T2">Value type</typeparam>
        /// <param name="Dictionary">Dictionary to sort</param>
        /// <param name="Comparer">Comparer used to sort (defaults to GenericComparer)</param>
        /// <returns>The sorted dictionary</returns>
        public static IDictionary<T1, T2> Sort<T1, T2>(this IDictionary<T1, T2> Dictionary,
                                                       IComparer<T1> Comparer = null)
            where T1 : IComparable
        {
            Dictionary.ThrowIfNull("Dictionary");
            return Dictionary.Sort(x => x.Key, Comparer);
        }

        /// <summary>
        /// Sorts a dictionary
        /// </summary>
        /// <typeparam name="T1">Key type</typeparam>
        /// <typeparam name="T2">Value type</typeparam>
        /// <typeparam name="T3">Order by type</typeparam>
        /// <param name="Dictionary">Dictionary to sort</param>
        /// <param name="OrderBy">Function used to order the dictionary</param>
        /// <param name="Comparer">Comparer used to sort (defaults to GenericComparer)</param>
        /// <returns>The sorted dictionary</returns>
        public static IDictionary<T1, T2> Sort<T1, T2, T3>(this IDictionary<T1, T2> Dictionary,
                                                           Func<KeyValuePair<T1, T2>, T3> OrderBy,
                                                           IComparer<T3> Comparer = null)
            where T3 : IComparable
        {
            Dictionary.ThrowIfNull("Dictionary");
            OrderBy.ThrowIfNull("OrderBy");
            return Dictionary.OrderBy(OrderBy, Comparer.NullCheck(new GenericComparer<T3>())).ToDictionary(x => x.Key,
                                                                                                           x => x.Value);
        }

        #endregion

        #endregion
    }
}