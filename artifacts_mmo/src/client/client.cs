using ArtifactsAPI;
using ArtifactsAPI.Client;
using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public CombatClient Combat => new(this);
        public CraftingClient Crafting => new(this);
        public UtilityClient Utility => new(this);

        public readonly APIHandler api;
        public Character character;

        public Client(APIHandler api_handler, Character character)
        {
            this.api = api_handler;
            this.character = character;
        }
    }
}
