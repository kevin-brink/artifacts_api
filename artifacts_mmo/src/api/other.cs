using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class OtherEndpoints(APIHandler apiHandler)
        {
            private readonly APIHandler _apiHandler = apiHandler;

            public async Task<StatusResponse> GetStatus()
            {
                var endpoint = "";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new StatusResponse(response);
            }

            public async Task<LogResponse> GetAllCharacterLogs(int page = 1, int size = 50)
            {
                var endpoint = "my/logs";

                ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
                ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(size, 100);

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Get,
                    urlParams: new Dictionary<string, string>
                    {
                        { "page", page.ToString() },
                        { "size", size.ToString() },
                    }
                );

                return new LogResponse(response);
            }

            public async Task<CharactersResponse> GetCharacters()
            {
                var endpoint = "my/characters";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new CharactersResponse(response);
            }
        }
    }
}
