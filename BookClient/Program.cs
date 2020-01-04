using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var text = await ReadAuthors(client);
                Console.WriteLine($"Authors:\n{text}");

                var response = await AddAuthor(client);
                Console.WriteLine($"Added author status:\n\t{response.StatusCode}");
            }
        }

        private static async Task<string> ReadAuthors(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://localhost:5001/authors");

            return await stringTask;
        }

        private static async Task<HttpResponseMessage> AddAuthor(HttpClient client)
        {
            string author = "{\"name\": \"Anne Frank\"}";

            HttpContent httpContent = new StringContent(author, Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://localhost:5001/authors", httpContent);
            return response;
        }
    }
}