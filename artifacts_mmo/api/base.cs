using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtifactsAPI
{
    class APIHandler
    {
        private const string base_url = "https://api.artifactsmmo.com";
        private HttpClient _client;

        public ActionEndpoints Actions => new ActionEndpoints(this);

        public APIHandler(string api_key, string character_name)
        {
            _client = new HttpClient { BaseAddress = new Uri($"{base_url}/my/{character_name}/") };

            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {api_key}");
            // _client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        private async Task<HttpResponseMessage> handle_request(
            string endpoint,
            HttpMethod method,
            Dictionary<string, string>? urlParams = null,
            object? body = null
        )
        {
            var url = endpoint;

            if (urlParams != null)
            {
                var queryString = string.Join(
                    "&",
                    urlParams.Select(kvp =>
                        $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"
                    )
                );
                url += "?" + queryString;
            }

            var request = new HttpRequestMessage(method, url);

            if (body != null)
            {
                var jsonBody = JsonSerializer.Serialize(body);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            return await _client.SendAsync(request);
        }

        public class ActionEndpoints
        {
            private readonly APIHandler _apiHandler;
            private const string _path = "action";

            public ActionEndpoints(APIHandler apiHandler)
            {
                _apiHandler = apiHandler;
            }

            public async Task<HttpResponseMessage> move(int x, int y)
            {
                var endpoint = $"{_path}/move";
                var body = new { x, y };

                return await _apiHandler.handle_request(endpoint, HttpMethod.Post, body: body);
            }
        }
    }
}
