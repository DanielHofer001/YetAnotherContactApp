using System.Net.Http;
using System.Net.Http.Headers;

namespace YetAnotherContactApp
{
    public static class ContactApi
    {
        public static HttpClient? ApiClient { get; set; }

        /// <summary>
        /// Creates an HttpClient to allow calls to be made to the API and initializes a global variable called 'ApiClient'.
        /// Use 'ApiClient' to make API calls. Should only be used once: in the constructor that is at the start of the application.
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7081")
            };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
