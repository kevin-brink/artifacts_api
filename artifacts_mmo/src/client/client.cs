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

        public APIHandler api;
        public Character character;

        private Status status;

        public Client(APIHandler api_handler, Character character)
        {
            this.api = api_handler;
            this.character = character;

            var response = api_handler.GetStatus();
            var result = response.GetAwaiter().GetResult();
            status = result.status;
        }
    }
}
