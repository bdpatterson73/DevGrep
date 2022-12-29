using System;
using System.Collections.Generic;
using System.Text;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// Concrete implementation of the ICluster interface.
    /// </summary>
    public class Cluster : ICluster {

        #region Life and death

        /// <summary>
        /// Create a cluster
        /// </summary>
        /// <param name="key">The key for the cluster</param>
        public Cluster(object key) {
            this.Count = 1;
            this.ClusterKey = key;
        }

        #endregion

        #region Public overrides

        /// <summary>
        /// Return a string representation of this cluster
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return this.DisplayLabel ?? "[empty]";
        }

        #endregion

        #region Implementation of ICluster

        /// <summary>
        /// Gets or sets how many items belong to this cluster
        /// </summary>
        public int Count {
            get { return count; }
            set { count = value; }
        }
        private int count;

        /// <summary>
        /// Gets or sets the label that will be shown to the user to represent
        /// this cluster
        /// </summary>
        public string DisplayLabel {
            get { return displayLabel; }
            set { displayLabel = value; }
        }
        private string displayLabel;

        /// <summary>
        /// Gets or sets the actual data object that all members of this cluster
        /// have commonly returned.
        /// </summary>
        public object ClusterKey {
            get { return clusterKey; }
            set { clusterKey = value; }
        }
        private object clusterKey;

        #endregion

        #region Implementation of IComparable

        /// <summary>
        /// Return an indication of the ordering between this object and the given one
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(object other) {
            if (other == null || other == System.DBNull.Value)
                return 1;

            ICluster otherCluster = other as ICluster;
            if (otherCluster == null)
                return 1;

            string keyAsString = this.ClusterKey as string;
            if (keyAsString != null)
                return String.Compare(keyAsString, otherCluster.ClusterKey as string, StringComparison.CurrentCultureIgnoreCase);

            IComparable keyAsComparable = this.ClusterKey as IComparable;
            if (keyAsComparable != null)
                return keyAsComparable.CompareTo(otherCluster.ClusterKey);

            return -1;
        }

        #endregion
    }
}
