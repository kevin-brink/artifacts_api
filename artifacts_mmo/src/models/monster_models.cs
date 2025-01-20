using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class MonsterResponse : BaseResponse
    {
        public Monster? data { get; private set; }

        public MonsterResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            data = new Monster(json[nameof(data)]!);
        }
    }

    public class MonstersResponse : BaseResponse
    {
        public List<Monster> data { get; private set; } = [];

        public int total { get; private set; }
        public int page { get; private set; }
        public int size { get; private set; }
        public int pages { get; private set; }

        public MonstersResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            total = (int)json[nameof(total)]!;
            page = (int)json[nameof(page)]!;
            size = (int)json[nameof(size)]!;
            pages = (int)json[nameof(pages)]!;

            data = [.. json[nameof(data)]!.Select(monster => new Monster(monster))]!;
        }
    }
}
