using System;
using System.Collections.Generic;
using System.Text;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// This class calculates clusters from the groups that the column uses.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is the default strategy for all non-date, filterable columns.
    /// </para>
    /// <para>
    /// This class does not strictly mimic the groups created by the given column.
    /// In particular, if the programmer changes the default grouping technique
    /// by listening for grouping events, this class will not mimic that behaviour.
    /// </para>
    /// </remarks>
    public class ClustersFromGroupsStrategy : ClusteringStrategy {

        /// <summary>
        /// Get the cluster key by which the given model will be partitioned by this strategy
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override object GetClusterKey(object model) {
            return this.Column.GetGroupKey(model);
        }

        /// <summary>
        /// Gets the display label that the given cluster should use
        /// </summary>
        /// <param name="cluster"></param>
        /// <returns></returns>
        public override string GetClusterDisplayLabel(ICluster cluster) {
            string s = this.Column.ConvertGroupKeyToTitle(cluster.ClusterKey);
            if (String.IsNullOrEmpty(s)) 
                s = EMPTY_LABEL;
            return this.ApplyDisplayFormat(cluster, s);
        }
    }
}
