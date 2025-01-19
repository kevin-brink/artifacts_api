using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class MoveResponse
    {
        public StatusCode status_code { get; private set; }
        public HttpResponseMessage response { get; private set; }

        public Cooldown? cooldown { get; private set; }
        public Map? destination { get; private set; }
        public Character? character { get; private set; }

        public MoveResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content);

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code == StatusCode.OK)
            {
                cooldown = new Cooldown(json["cooldown"]!);
                destination = new Map(json["map"]!);
                character = new Character(json["character"]!);
            }
        }
    }
}
