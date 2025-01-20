using System.Formats.Tar;
using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class MapsEndpoints(APIHandler apiHandler)
        {
            private readonly APIHandler _apiHandler = apiHandler;
            private string _path => $"maps";

            public async Task<MapsResponse> GetAllMapsTotal()
            {
                int size = 100;
                MapsResponse response = await GetAllMaps(size: size);

                for (int page = response.page + 1; page <= response.pages; page++)
                {
                    MapsResponse next_response = await GetAllMaps(page: page, size: size);
                    response.data.AddRange(next_response.data);
                }

                return response;
            }

            public async Task<MapsResponse> GetAllMaps(
                string content_code = "",
                ContentType content_type = ContentType.any,
                int page = 1,
                int size = 50
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
                ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(size, 100);

                var endpoint = $"{_path}";

                var query = new Dictionary<string, string>();
                if (content_code != "")
                    query.Add("content_code", content_code);
                if (content_type != ContentType.any)
                    query.Add("content_type", content_type.ToString());
                if (page != 1)
                    query.Add("page", page.ToString());
                if (size != 50)
                    query.Add("size", size.ToString());

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Get,
                    urlParams: query
                );

                return new MapsResponse(response);
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
