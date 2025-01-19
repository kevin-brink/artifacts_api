using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    partial class APIHandler
    {
        public class ActionEndpoints
        {
            private readonly APIHandler _apiHandler;
            private const string _path = "action";

            public ActionEndpoints(APIHandler apiHandler)
            {
                _apiHandler = apiHandler;
            }

            public async Task<MoveResponse> Move(int x, int y)
            {
                var endpoint = $"{_path}/move";
                var body = new { x, y };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                return new MoveResponse(response);
            }
        }
    }
}
