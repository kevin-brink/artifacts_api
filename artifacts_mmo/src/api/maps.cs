using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class MapsEndpoints
        {
            private readonly APIHandler _apiHandler;
            private string _path => $"maps";

            public MapsEndpoints(APIHandler apiHandler)
            {
                _apiHandler = apiHandler;
            }

            public async Task<MapResponse> GetAllMap()
            {
                var endpoint = $"{_path}";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new MapResponse(response);
            }

            public async Task<MapResponse> GetMap(int x, int y)
            {
                var endpoint = $"{_path}/{x}/{y}";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new MapResponse(response);
            }
        }
    }
}
