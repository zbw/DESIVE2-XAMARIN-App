using System;
using System.Collections.Generic;
using System.Text;

namespace Desive2.Services
{
    /// <summary>
    /// Defines the contract for retrieving the application version and build number.
    /// </summary>
    public interface IAppVersionAndBuild
    {
        /// <summary>
        /// Gets the version number of the application.
        /// </summary>
        /// <returns>The version number as a string.</returns>
        string GetVersionNumber();

        /// <summary>
        /// Gets the build number of the application.
        /// </summary>
        /// <returns>The build number as a string.</returns>
        string GetBuildNumber();
    }

}
