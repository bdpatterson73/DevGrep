using System;
using System.Collections.Generic;
using System.Text;

namespace DevGrep.Controls.DLV
{

    /// <summary>
    /// This class provides a useful base implemention of a clustering
    /// strategy where the clusters are grouped around the value of a given column.
    /// </summary>
    public class ClusteringStrategy : IClusteringStrategy {

        #region Static properties

        /// <summary>
        /// This field is the text that will be shown to the user when a cluster
        /// key is null. It is exposed so it can be localized.
        /// </summary>
        static public string NULL_LABEL = "[null]";

        /// <summary>
        /// This field is the text that will be shown to the user when a cluster
        /// key is empty (i.e. a string of zero length). It is exposed so it can be localized.
        /// </summary>
        static public string EMPTY_LABEL = "[empty]";

        /// <summary>
        /// Gets or sets the format that will be used by default for clusters that only
        /// contain 1 item. The format string must accept two placeholders:
        /// - {0} is the cluster key converted to a string
        /// - {1} is the number of items in the cluster (always 1 in this case)
        /// </summary>
        static public string DefaultDisplayLabelFormatSingular {
            get { return defaultDisplayLabelFormatSingular; }
            set { defaultDisplayLabelFormatSingular = value; }
        }
        static private string defaultDisplayLabelFormatSingular = "{0} ({1} item)";

        /// <summary>
        /// Gets or sets the format that will be used by default for clusters that 
        /// contain 0 or two or more items. The format string must accept two placeholders:
        /// - {0} is the cluster key converted to a string
        /// - {1} is the number of items in the cluster
        /// </summary>
        static public string DefaultDisplayLabelFormatPlural {
            get { return defaultDisplayLabelFormatPural; }
            set { defaultDisplayLabelFormatPural = value; }
        }
        static private string defaultDisplayLabelFormatPural = "{0} ({1} items)";

        #endregion

        #region Life and death

        /// <summary>
        /// Create a clustering strategy
        /// </summary>
        public ClusteringStrategy() {
            this.DisplayLabelFormatSingular = DefaultDisplayLabelFormatSingular;
            this.DisplayLabelFormatPlural = DefaultDisplayLabelFormatPlural;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the column upon which this strategy is operating
        /// </summary>
        public OLVColumn Column {
            get { return column; }
            set { column = value; }
        }
        private OLVColumn column;

        /// <summary>
        /// Gets or sets the format that will be used when the cluster
        /// contains only 1 item. The format string must accept two placeholders:
        /// - {0} is the cluster key converted to a string
        /// - {1} is the number of items in the cluster (always 1 in this case)
        /// </summary>
        /// <remarks>If this is not set, the value from 
        /// ClusteringStrategy.DefaultDisplayLabelFormatSingular will be used</remarks>
        public string DisplayLabelFormatSingular {
            get { return displayLabelFormatSingular; }
            set { displayLabelFormatSingular = value; }
        }
        private string displayLabelFormatSingular;

        /// <summary>
        /// Gets or sets the format that will be used when the cluster 
        /// contains 0 or two or more items. The format string must accept two placeholders:
        /// - {0} is the cluster key converted to a string
        /// - {1} is the number of items in the cluster
        /// </summary>
        /// <remarks>If this is not set, the value from 
        /// ClusteringStrategy.DefaultDisplayLabelFormatPlural will be used</remarks>
        public string DisplayLabelFormatPlural {
            get { return displayLabelFormatPural; }
            set { displayLabelFormatPural = value; }
        }
        private string displayLabelFormatPural;

        #endregion

        #region ICluster implementation

        /// <summary>
        /// Get the cluster key by which the given model will be partitioned by this strategy
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        virtual public object GetClusterKey(object model) {
            return this.Column.GetValue(model);
        }

        /// <summary>
        /// Create a cluster to hold the given cluster key
        /// </summary>
        /// <param name="clusterKey"></param>
        /// <returns></returns>
        virtual public ICluster CreateCluster(object clusterKey) {
            return new Cluster(clusterKey);
        }

        /// <summary>
        /// Gets the display label that the given cluster should use
        /// </summary>
        /// <param name="cluster"></param>
        /// <returns></returns>
        virtual public string GetClusterDisplayLabel(ICluster cluster) {
            string s = this.Column.ValueToString(cluster.ClusterKey) ?? NULL_LABEL;
            if (String.IsNullOrEmpty(s)) 
                s = EMPTY_LABEL;
            return this.ApplyDisplayFormat(cluster, s);
        }

        /// <summary>
        /// Create a label that combines the string representation of the cluster
        /// key with a format string that holds an "X [N items in cluster]" type layout.
        /// </summary>
        /// <param name="cluster"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        virtual protected string ApplyDisplayFormat(ICluster cluster, string s) {
            string format = (cluster.Count == 1) ? this.DisplayLabelFormatSingular : this.DisplayLabelFormatPlural;
            return String.IsNullOrEmpty(format) ? s : String.Format(format, s, cluster.Count);
        }

        #endregion
    }
}
