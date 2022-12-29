using System;

namespace SmartSearch.Util
{
	/// <summary> Use by certain classes to match version compatibility
	/// across releases of Lucene.
    ///  <p/>
    ///  <b>WARNING</b>: When changing the version parameter
    ///  that you supply to components in Lucene, do not simply
    ///  change the version at search-time, but instead also adjust
    ///  your indexing code to match, and re-index.
	/// </summary>
	public enum Version
	{
        /// <summary>Match settings and bugs in SmartSearch's 2.0 release.</summary>
		SmartSearch_20,

        /// <summary>Match settings and bugs in SmartSearch's 2.1 release. </summary>
		SmartSearch_21,

        /// <summary>Match settings and bugs in SmartSearch's 2.2 release. </summary>
		SmartSearch_22,

        /// <summary>Match settings and bugs in SmartSearch's 2.3 release.</summary>
		SmartSearch_23,

        /// <summary>Match settings and bugs in SmartSearch's 2.4 release.</summary>
		SmartSearch_24,

        /// <summary>Match settings and bugs in SmartSearch's 2.9 release.</summary>
		SmartSearch_29,

        /// <summary>
        /// Match settings and bugs in SmartSearch's 3.0 release.
        /// <para>
        /// Use this to get the latest and greatest settings, bug fixes,
        /// etc, for SmartSearch.
        /// </para>
        /// </summary>
        SmartSearch_30,

        // NOTE: Add new constants for later versions **here** to respect order!
		
        /// <summary>
		/// <p/><b>WARNING</b>: if you use this setting, and then
		/// upgrade to a newer release of Lucene, sizable changes
		/// may happen.  If precise back compatibility is important
		/// then you should instead explicitly specify an actual
		/// version.
        /// If you use this constant then you may need to
        /// <b>re-index all of your documents</b> when upgrading
        /// Lucene, as the way text is indexed may have changed.
        /// Additionally, you may need to <b>re-test your entire
        /// application</b> to ensure it behaves as expected, as
        /// some defaults may have changed and may break functionality
        /// in your application.
		/// </summary>
        [Obsolete("Use an actual version instead.")]
		SmartSearch_CURRENT,
	}

    public static class VersionEnumExtensions
    {
		public static bool OnOrAfter(this Version first, Version other)
		{
		    return first.CompareTo(other) >= 0;
		}
    }
}