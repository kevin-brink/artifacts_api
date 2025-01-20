using ArtifactsAPI;
using ArtifactsAPI.Client;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public CombatClient Combat => new(this);
        public CraftingClient Crafting => new(this);

        public readonly APIHandler api;
        public string character_name;

        public Client(APIHandler api_handler, string character_name)
        {
            this.api = api_handler;
            this.character_name = character_name;
        }
    }
}
