using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public class Client(APIHandler api_handler, string character_name)
    {
        private readonly APIHandler _api_handler = api_handler;

        // TODO This will only be needed if we make Actions a dict
        private readonly string _character_name = character_name;

        public async Task GrindCombat(int target_level)
        {
            Character character;
            do
            {
                var fight = await _api_handler.Actions.Fight();
                var rest = await _api_handler.Actions.Rest();

                character = rest.character!;
            } while (character.level < target_level);
        }
    }
}
