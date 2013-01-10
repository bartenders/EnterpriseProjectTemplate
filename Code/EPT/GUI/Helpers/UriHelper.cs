using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace EPT.GUI.Helpers
{
    public static class UriHelper
    {
        private static string _assemblyShortName;


        /// <summary>
        /// Gets a pack URI of an Image relative to EPT.GUI.
        /// </summary>
        /// <param name="relativeFile">The relative filename e.g. (Images\Dark\appbar.acorn.png).</param>
        /// <returns>a formated Pack Uri</returns>
        public static Uri GetPackUri(string relativeFile)
        {
            var uriString = new StringBuilder(); 
            uriString.Append("pack://application:,,,");
            uriString.Append("/" + AssemblyShortName + ";component/" + relativeFile);
            
            return new Uri(uriString.ToString(), UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Gets a pack URI from a given Assembly.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeAssemblyFilePath">The relative assembly file path.</param>
        /// <returns></returns>
        public static Uri GetPackUriFrom<T>(string relativeAssemblyFilePath)
        {
            var uriString = new StringBuilder();
            uriString.Append("pack://application:,,,");
            uriString.Append("/" + typeof(T).Assembly.ToString().Split(',')[0] + ";component/" + relativeAssemblyFilePath);
            return new Uri(uriString.ToString(), UriKind.RelativeOrAbsolute);
        }

        private static string AssemblyShortName
        {
            get
            {
                if (_assemblyShortName == null)
                {
                    var a = typeof(UriHelper).Assembly;
                    // Pull out the short name.
                    _assemblyShortName = a.ToString().Split(',')[0];
                }

                return _assemblyShortName;
            }
        }

        public static String MakeAbsolutFilePath(string relativeFile)
        {
            if (string.IsNullOrEmpty(relativeFile)) return null;

            var assemblyDir =
                Path.Combine(
                    Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location));

            if (!Directory.Exists(assemblyDir)) return null;

            var fileName = Path.Combine(assemblyDir, relativeFile);
            if (!File.Exists(fileName)) return null;

            return fileName;
        }
    }
}