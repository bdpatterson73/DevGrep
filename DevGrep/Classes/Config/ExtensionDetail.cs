using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SmartAssembly.Attributes;

namespace DevGrep.Classes.Config
{
    /// <summary>
    /// Class VerbDetail
    /// </summary>
    [Serializable]
    [DoNotObfuscateType]
    internal class ExtensionDetail
    {
        internal ExtensionDetail()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerbDetail" /> class.
        /// </summary>
        /// <param name="verb">The verb.</param>
        /// <param name="replaceWith">The replace with.</param>
        internal ExtensionDetail(string extension, string group)
        {
            Extension = extension;
            Group = group;
        }

        /// <summary>
        /// Gets or sets the verb.
        /// </summary>
        /// <value>The verb.</value>
        internal string Extension { get; set; }
        /// <summary>
        /// Gets or sets the replace with.
        /// </summary>
        /// <value>The replace with.</value>
        internal string Group { get; set; }
    }
}
