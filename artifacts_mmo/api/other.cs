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
                var endpoint = "status";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new StatusResponse(response);
            }
        }
    }
}
