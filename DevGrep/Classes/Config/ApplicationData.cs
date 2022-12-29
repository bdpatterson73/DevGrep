using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DevGrep.Classes.Config
{
    /// <summary>
    /// Class ApplicationData
    /// </summary>
    internal class ApplicationData
    {
        /// <summary>
        /// Application Name
        /// </summary>
        private readonly string _appName;

        /// <summary>
        /// Company Name
        /// </summary>
        private readonly string _company;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationData" /> class.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <param name="appName">Name of the app.</param>
        internal ApplicationData()
        {
            //_company = company;
            //_appName = appName;
            _company = Program.COMPANY_NAME;
            _appName = Program.APP_NAME;
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        /// <value>The folder path.</value>
        internal string FolderPath
        {
            get
            {
                string newPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                              _company);
                newPath = Path.Combine(newPath, _appName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);

                return newPath;
            }
        }
    }
}
