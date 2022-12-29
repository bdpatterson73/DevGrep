using System;
using System.Collections;

namespace DevGrep.Classes
{
    /// <summary>
    /// Summary description for MatchLocationCollection.
    /// </summary>
    public class MatchLocationCollection : CollectionBase
    {
        public MatchLocationCollection()
        {
        }

        #region Add
        /// <summary>
        /// Adds a CSelectionSchema object to the collection
        /// </summary>
        /// <param name="matchLocation">Object to add</param>
        public void Add(MatchLocation matchLocation)
        {
            List.Add(matchLocation);
        }
        #endregion

        #region Remove
        /// <summary>
        /// Removes an item
        /// </summary>
        /// <param name="index">Index of the item to remove</param>
        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            {
                throw new Exception("Invalid index.");
            }
            else
            {
                List.RemoveAt(index);
            }
        }
        #endregion

        #region Item
        /// <summary>
        /// Returns a single item from the collection
        /// </summary>
        /// <param name="index">Index of the item to return</param>
        /// <returns>Returns a CSelectionSchema object</returns>
        public MatchLocation Item(int index)
        {
            try
            {
                return (MatchLocation)List[index];
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
        #endregion
    }
}