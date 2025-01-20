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

            cooldown = new Cooldown(json[nameof(cooldown)]!);
            destination = new Map(json[nameof(destination)]!);
            character = new Character(json[nameof(character)]!);
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

            cooldown = new Cooldown(json[nameof(cooldown)]!);
            hp_restored = (int)json[nameof(hp_restored)]!;
            character = new Character(json[nameof(character)]!);
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

            cooldown = new Cooldown(json[nameof(cooldown)]!);
            fight = new Fight(json[nameof(fight)]!);
            character = new Character(json[nameof(character)]!);
        }
    }

    public class GatheringResponse : BaseResponse
    {
        public SkillInfo? details { get; private set; }
        public Character? character { get; private set; }

        public GatheringResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)["data"]!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            cooldown = new Cooldown(json[nameof(cooldown)]!);
            details = new SkillInfo(json[nameof(details)]!);
            character = new Character(json[nameof(character)]!);
        }
    }

    public class CraftingResponse : BaseResponse
    {
        public SkillInfo? details { get; private set; }
        public Character? character { get; private set; }

        public CraftingResponse(HttpResponseMessage response)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(content)["data"]!;

            status_code = (StatusCode)response.StatusCode;
            this.response = response;

            if (status_code != StatusCode.OK)
                return;

            cooldown = new Cooldown(json[nameof(cooldown)]!);
            details = new SkillInfo(json[nameof(details)]!);
            character = new Character(json[nameof(character)]!);
        }
    }
}
