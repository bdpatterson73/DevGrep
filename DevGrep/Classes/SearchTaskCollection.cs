using System;
using System.Collections;

namespace DevGrep.Classes
{
    /// <summary>
    /// Summary description for SearchTaskCollection.
    /// </summary>
    public class SearchTaskCollection : CollectionBase
    {
        public SearchTaskCollection()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Add
        /// <summary>
        /// Adds a SearchTask object to the collection
        /// </summary>
        /// <param name="searchTask">Object to add</param>
        public void Add(SearchTask searchTask)
        {
            List.Add(searchTask);
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
        /// <returns>Returns a SearchTask object</returns>
        public SearchTask Item(int index)
        {
            return (SearchTask)List[index];
        }
        #endregion
    }
}