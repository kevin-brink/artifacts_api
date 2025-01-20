using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class MonstersEndpoints(APIHandler apiHandler)
        {
            private readonly APIHandler _apiHandler = apiHandler;
            private string _path => $"monsters";

            public async Task<MonstersResponse> GetAllMonstersTotal()
            {
                int size = 100;
                MonstersResponse response = await GetAllMonsters(size: size);

                for (int page = response.page + 1; page <= response.pages; page++)
                {
                    MonstersResponse next_response = await GetAllMonsters(page: page, size: size);
                    response.data.AddRange(next_response.data);
                }

                return response;
            }

            public async Task<MonstersResponse> GetAllMonsters(
                string drop = "",
                int max_level = 0,
                int min_level = 0,
                int page = 1,
                int size = 50
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
                ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(size, 100);

                var endpoint = $"{_path}";

                var query = new Dictionary<string, string>();
                if (drop != "")
                    query.Add("drop", drop);
                if (max_level > 0)
                    query.Add("max_level", max_level.ToString());
                if (min_level > 0)
                    query.Add("min_level", min_level.ToString());
                if (page != 1)
                    query.Add("page", page.ToString());
                if (size != 50)
                    query.Add("size", size.ToString());

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Get,
                    urlParams: query
                );

                return new MonstersResponse(response);
            }

            public async Task<MonsterResponse> GetMonster(string code)
            {
                var endpoint = $"{_path}/{code}";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new MonsterResponse(response);
            }
        }
    }
}
