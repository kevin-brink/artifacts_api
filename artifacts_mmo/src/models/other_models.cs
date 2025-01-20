using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class StatusResponse
    {
        public StatusCode status_code { get; private set; }
        public HttpResponseMessage response { get; private set; }

        public Status status { get; private set; }

        public StatusResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content);

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            status = new Status(json);
        }
    }

    public class LogResponse
    {
        public StatusCode status_code { get; private set; }
        public HttpResponseMessage response { get; private set; }

        public List<Log>? logs { get; private set; }
        public int? total { get; private set; }
        public int? page { get; private set; }
        public int? size { get; private set; }
        public int? pages { get; private set; }

        public LogResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JArray.Parse(content);

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
            {
                return;
            }

            logs = new List<Log>();
            foreach (var log in json["data"]!)
            {
                logs.Add(new Log(log));
            }

            total = (int)json["total"]!;
            page = (int)json["page"]!;
            size = (int)json["size"]!;
            pages = (int)json["pages"]!;
        }
    }

    public class CharactersResponse
    {
        public StatusCode status_code { get; private set; }
        public HttpResponseMessage response { get; private set; }

        public List<Character> data { get; private set; }

        public CharactersResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            data = [.. json[nameof(data)]!.Select(character => new Character(character))]!;
        }
    }
}
