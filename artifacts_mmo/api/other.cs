using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    partial class APIHandler
    {
        public class OtherEndpoints
        {
            private readonly APIHandler _apiHandler;

            public OtherEndpoints(APIHandler apiHandler)
            {
                _apiHandler = apiHandler;
            }

            public async Task<StatusResponse> GetStatus()
            {
                var endpoint = "";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new StatusResponse(response);
            }

            public async Task<LogResponse> GetAllCharacterLogs(int page = 1, int size = 50)
            {
                var endpoint = "my/logs";

                if (page < 1)
                {
                    throw new ArgumentException("Page must be greater than 0");
                }

                if (size < 1 || size > 100)
                {
                    throw new ArgumentException("Size must be between 1 and 100");
                }

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
