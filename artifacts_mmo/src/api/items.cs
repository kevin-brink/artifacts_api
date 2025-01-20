using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    public partial class APIHandler
    {
        public class ItemsEndpoints
        {
            private readonly APIHandler _apiHandler;
            private string _path => $"items";

            public ItemsEndpoints(APIHandler apiHandler)
            {
                _apiHandler = apiHandler;
            }

            public async Task<ItemsResponse> GetAllItemsTotal()
            {
                int size = 100;
                ItemsResponse response = await GetAllItems(size: size);

                for (int page = response.page + 1; page <= response.pages; page++)
                {
                    ItemsResponse next_response = await GetAllItems(page: page, size: size);
                    response.data.AddRange(next_response.data);
                }

                return response;
            }

            public async Task<ItemsResponse> GetAllItems(
                string craft_material = "",
                CraftSkill craft_skill = CraftSkill.any,
                int max_level = 0,
                int min_level = 0,
                string name = "",
                int page = 1,
                int size = 50,
                ItemType type = ItemType.any
            )
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(max_level, 0);
                ArgumentOutOfRangeException.ThrowIfLessThan(min_level, 0);
                ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
                ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(size, 100);

                var endpoint = $"{_path}";

                var query = new Dictionary<string, string>();
                if (craft_material != "")
                    query.Add("craft_material", craft_material);
                if (craft_skill != CraftSkill.any)
                    query.Add("craft_skill", craft_skill.ToString());
                if (max_level != 0)
                    query.Add("max_level", max_level.ToString());
                if (min_level != 0)
                    query.Add("min_level", min_level.ToString());
                if (name != "")
                    query.Add("name", name);
                if (page != 1)
                    query.Add("page", page.ToString());
                if (size != 50)
                    query.Add("size", size.ToString());
                if (type != ItemType.any)
                    query.Add("type", type.ToString());

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Get,
                    urlParams: query
                );

                return new ItemsResponse(response);
            }

            public async Task<ItemResponse> GetItem(int id)
            {
                var endpoint = $"{_path}/{id}";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Get);

                return new ItemResponse(response);
            }
        }
    }
}
