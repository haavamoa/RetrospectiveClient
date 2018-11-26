using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Retrospective.Service.Tests")]

namespace Retrospective.Service.Repositories
{
    public class JsonRepositoryConfiguration
    {
        public JsonRepositoryConfiguration()
        {
            JsonFilesPrefix = "retro";
            PathForJsonFiles = CurrentApplicationDirectory();
            JsonDateFormat = "yy.dd.MM";
        }

        private static string CurrentApplicationDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var fileinfo = new FileInfo(codeBase);
            return fileinfo.DirectoryName;
        }

        /// <summary>
        /// Path to save the retrospective json
        /// <remarks>Defaults is directory of application</remarks>
        /// </summary>
        public string PathForJsonFiles { get; set; }

        /// <summary>
        /// Prefix for the retrospective json filename
        /// <remarks>Default is "retro"</remarks>
        /// </summary>
        public string JsonFilesPrefix { get; set; }
        /// <summary>
        /// The format of the endtime used in the retrospective json filename
        /// <remarks>Default is yy.dd.MM</remarks>
        /// </summary>
        public string JsonDateFormat { get; set; }
    }
}