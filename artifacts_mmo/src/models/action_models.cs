using ArtifactsAPI.Schemas;
using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Models
{
    public class MoveResponse : BaseResponse
    {
        public Map? destination { get; private set; }
        public Character? character { get; private set; }

        public MoveResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)["data"]!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            cooldown = new Cooldown(json["cooldown"]!);
            destination = new Map(json["map"]!);
            character = new Character(json["character"]!);
        }
    }

    public class RestResponse : BaseResponse
    {
        public int hp_restored { get; private set; }
        public Character? character { get; private set; }

        public RestResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)["data"]!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            cooldown = new Cooldown(json["cooldown"]!);
            hp_restored = (int)json["hp_restored"]!;
            character = new Character(json["character"]!);
        }
    }

    public class FightResponse : BaseResponse
    {
        public Fight? fight { get; private set; }
        public Character? character { get; private set; }

        public FightResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)["data"]!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            cooldown = new Cooldown(json["cooldown"]!);
            fight = new Fight(json["fight"]!);
            character = new Character(json["character"]!);
        }
    }

    public class GatherResponse : BaseResponse
    {
        public SkillInfo? details { get; private set; }
        public Character? character { get; private set; }

        public GatherResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)["data"]!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            cooldown = new Cooldown(json["cooldown"]!);
            details = new SkillInfo(json["details"]!);
            character = new Character(json["character"]!);
        }
    }
}
