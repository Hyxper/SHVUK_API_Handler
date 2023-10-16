using System;
using System.Net.Http;

namespace SHVUK_API_Handler.Classes
{
    /// <summary>
    /// Static class that allows us to see if a service is online (or not).
    /// </summary>
    public static class ServiceChecker
    {
        /// <summary>
        /// Sole method responsible for checking if a service is online.
        /// </summary>
        /// <param name="testUrl">a url to test.</param>
        /// <returns>True if endpoint was reached and responded. Otherwise throws a HttpRequestException.</returns>
        /// <exception cref="ArgumentNullException">If argument passed is null.</exception>
        /// <exception cref="HttpRequestException">If endpoint could not be reached.</exception>
        public static bool IsOnline(string testUrl)
        {
            if (testUrl == null) throw new ArgumentNullException(nameof(testUrl));

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(testUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new HttpRequestException($"Service returned a non-success status code: {(int)response.StatusCode}");
                }
            }
        }
    }
}
