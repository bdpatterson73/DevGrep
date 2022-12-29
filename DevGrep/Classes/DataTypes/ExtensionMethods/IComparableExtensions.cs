#region Usings

using System;
using System.Collections.Generic;
using DevGrep.Classes.DataTypes.Comparison;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// IComparable extensions
    /// </summary>
    internal static class IComparableExtensions
    {
        #region Functions

        #region Between

        /// <summary>
        /// Checks if an item is between two values
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="Value">Value to check</param>
        /// <param name="Min">Minimum value</param>
        /// <param name="Max">Maximum value</param>
        /// <param name="Comparer">Comparer used to compare the values (defaults to GenericComparer)"</param>
        /// <returns>True if it is between the values, false otherwise</returns>
        public static bool Between<T>(this T Value, T Min, T Max, IComparer<T> Comparer = null)
            where T : IComparable
        {
            Comparer = Comparer.NullCheck(new GenericComparer<T>());
            return Comparer.Compare(Max, Value) >= 0 && Comparer.Compare(Value, Min) >= 0;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// Clamps a value between two values
        /// </summary>
        /// <param name="Value">Value sent in</param>
        /// <param name="Max">Max value it can be (inclusive)</param>
        /// <param name="Min">Min value it can be (inclusive)</param>
        /// <param name="Comparer">Comparer to use (defaults to GenericComparer)</param>
        /// <returns>The value set between Min and Max</returns>
        public static T Clamp<T>(this T Value, T Max, T Min, IComparer<T> Comparer = null)
            where T : IComparable
        {
            Comparer = Comparer.NullCheck(new GenericComparer<T>());
            if (Comparer.Compare(Max, Value) < 0)
                return Max;
            if (Comparer.Compare(Value, Min) < 0)
                return Min;
            return Value;
        }

        #endregion

        #region Max

        /// <summary>
        /// Returns the maximum value between the two
        /// </summary>
        /// <param name="InputA">Input A</param>
        /// <param name="InputB">Input B</param>
        /// <param name="Comparer">Comparer to use (defaults to GenericComparer)</param>
        /// <returns>The maximum value</returns>
        public static T Max<T>(this T InputA, T InputB, IComparer<T> Comparer = null)
            where T : IComparable
        {
            Comparer = Comparer.NullCheck(new GenericComparer<T>());
            return Comparer.Compare(InputA, InputB) < 0 ? InputB : InputA;
        }

        #endregion

        #region Min

        /// <summary>
        /// Returns the minimum value between the two
        /// </summary>
        /// <param name="InputA">Input A</param>
        /// <param name="InputB">Input B</param>
        /// <param name="Comparer">Comparer to use (defaults to GenericComparer)</param>
        /// <returns>The minimum value</returns>
        public static T Min<T>(this T InputA, T InputB, IComparer<T> Comparer = null)
            where T : IComparable
        {
            Comparer = Comparer.NullCheck(new GenericComparer<T>());
            return Comparer.Compare(InputA, InputB) > 0 ? InputB : InputA;
        }

        #endregion

        #endregion
    }
}