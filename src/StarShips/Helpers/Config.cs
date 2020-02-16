using System.Configuration;

namespace Helpers
{
    /// <summary>
    /// General Information that it will get in the App.config
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Base URL for Web API
        /// </summary>
        public static string BaseURL
        {
            get
            {
                return EndsWithSlash(ConfigurationManager.AppSettings["baseURL"]);
            }
        }

        /// <summary>
        /// URL Segment to get the full URL
        /// </summary>
        public static string GetMethodName
        {
            get
            {
                return EndsWithSlash(ConfigurationManager.AppSettings["getMethodName"]);
            }
        }

        /// <summary>
        /// Treatment for slash in the end of URL
        /// </summary>
        /// <param name="value">URL</param>
        /// <returns>URL treated</returns>
        public static string EndsWithSlash(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                if (!value.TrimEnd().EndsWith("/"))
                    value = $"{value.TrimEnd()}/";

            return value;
        }
    }
}
