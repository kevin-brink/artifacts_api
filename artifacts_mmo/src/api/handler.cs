using System.Text;
using System.Text.Json;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        private const string base_url = "https://api.artifactsmmo.com";
        private HttpClient _client;

        // TODO Should endpoints each be dict grabbed by character names?
        // ie api_hander.Actions["character_name"].Move(0, 1)
        public ActionEndpoints Actions => new ActionEndpoints(this);
        public ItemsEndpoints Items => new ItemsEndpoints(this);
        public MapsEndpoints Maps => new MapsEndpoints(this);
        public MonstersEndpoints Monsters => new MonstersEndpoints(this);
        public OtherEndpoints Other => new OtherEndpoints(this);

        public string character_name;

        public APIHandler(string api_key, string character_name = "")
        {
            _client = new HttpClient { BaseAddress = new Uri($"{base_url}/") };
            this.character_name = character_name;

            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {api_key}");
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
    }
}
