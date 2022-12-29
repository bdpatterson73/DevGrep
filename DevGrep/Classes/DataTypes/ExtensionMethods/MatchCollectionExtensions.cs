#region Usings

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#endregion

namespace DevGrep.Classes.DataTypes.ExtensionMethods
{
    /// <summary>
    /// MatchCollection extensions
    /// </summary>
    internal static class MatchCollectionExtensions
    {
        #region Functions

        #region Where

        /// <summary>
        /// Gets a list of items that satisfy the predicate from the collection
        /// </summary>
        /// <param name="Collection">Collection to search through</param>
        /// <param name="Predicate">Predicate that the items must satisfy</param>
        /// <returns>The matches that satisfy the predicate</returns>
        public static IEnumerable<Match> Where(this MatchCollection Collection, Predicate<Match> Predicate)
        {
            if (Collection.IsNull())
                return null;
            Predicate.ThrowIfNull("Predicate");
            var Matches = new List<Match>();
            foreach (Match Item in Collection)
                if (Predicate(Item))
                    Matches.Add(Item);
            return Matches;
        }

        #endregion

        #endregion
    }
}