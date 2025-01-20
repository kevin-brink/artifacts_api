using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class MapResponse : BaseResponse
    {
        public Map? data { get; private set; }

        public MapResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            data = new Map(json[nameof(data)]!);
        }
    }

    public class MapsResponse : BaseResponse
    {
        public List<Map> data { get; private set; } = [];

        public int total { get; private set; }
        public int page { get; private set; }
        public int size { get; private set; }
        public int pages { get; private set; }

        public MapsResponse(HttpResponseMessage response)
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

            data = [.. json[nameof(data)]!.Select(map => new Map(json[nameof(map)]!))];
        }
    }
}
