using System;
using System.Collections.Generic;
using System.Text;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// A cluster is a like collection of objects that can be usefully filtered
    /// as whole using the filtering UI provided by the ObjectListView.
    /// </summary>
    public interface ICluster : IComparable {
        /// <summary>
        /// Gets or sets how many items belong to this cluster
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Gets or sets the label that will be shown to the user to represent
        /// this cluster
        /// </summary>
        string DisplayLabel { get; set; }

        /// <summary>
        /// Gets or sets the actual data object that all members of this cluster
        /// have commonly returned.
        /// </summary>
        object ClusterKey { get; set; }
    }
}
