using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class BankResponse : BaseResponse
    {
        public int slots { get; private set; }
        public int expansions { get; private set; }
        public int next_expansion_cost { get; private set; }
        public int gold { get; private set; }

        public BankResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            slots = (int)json[nameof(slots)]!;
            expansions = (int)json[nameof(expansions)]!;
            next_expansion_cost = (int)json[nameof(next_expansion_cost)]!;
            gold = (int)json[nameof(gold)]!;
        }
    }

    public class BankItemResponse : BaseResponse
    {
        public List<Drop> data { get; private set; }

        public int total { get; private set; }
        public int page { get; private set; }
        public int size { get; private set; }
        public int pages { get; private set; }

        public BankItemResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            data = [.. json[nameof(data)]!.Select(drop => new Drop(drop))]!;

            total = (int)json[nameof(total)]!;
            page = (int)json[nameof(page)]!;
            size = (int)json[nameof(size)]!;
            pages = (int)json[nameof(pages)]!;
        }
    }
}
