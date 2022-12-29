using System;
using System.Collections.Generic;
using System.Text;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// Implementation of this interface control the selecting of cluster keys
    /// and how those clusters will be presented to the user
    /// </summary>
    public interface IClusteringStrategy {

        /// <summary>
        /// Gets or sets the column upon which this strategy will operate
        /// </summary>
        OLVColumn Column { get; set; }

        /// <summary>
        /// Get the cluster key by which the given model will be partitioned by this strategy
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        object GetClusterKey(object model);

        /// <summary>
        /// Create a cluster to hold the given cluster key
        /// </summary>
        /// <param name="clusterKey"></param>
        /// <returns></returns>
        ICluster CreateCluster(object clusterKey);

        /// <summary>
        /// Gets the display label that the given cluster should use
        /// </summary>
        /// <param name="cluster"></param>
        /// <returns></returns>
        string GetClusterDisplayLabel(ICluster cluster);
    }
}
