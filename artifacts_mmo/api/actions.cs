using ArtifactsAPI.Models;

namespace ArtifactsAPI
{
    partial class APIHandler
    {
        public class ActionEndpoints
        {
            private readonly APIHandler _apiHandler;
            private string _path => $"/my/{_apiHandler.character_name}/action";

            public ActionEndpoints(APIHandler apiHandler)
            {
                _apiHandler = apiHandler;
            }

            public async Task<MoveResponse> Move(int x, int y, bool wait_for_cooldown = false)
            {
                var endpoint = $"{_path}/move";
                var body = new { x, y };

                var response = await _apiHandler.handle_request(
                    endpoint,
                    HttpMethod.Post,
                    body: body
                );

                MoveResponse move = new MoveResponse(response);
                if (wait_for_cooldown)
                {
                    return move.WaitForCooldown<MoveResponse>();
                }

                return move;
            }

            public async Task<RestResponse> Rest(bool wait_for_cooldown = false)
            {
                var endpoint = $"{_path}/rest";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Post);

                RestResponse rest = new RestResponse(response);
                if (wait_for_cooldown)
                {
                    return rest.WaitForCooldown<RestResponse>();
                }

                return rest;
            }

            // EquipItem
            // UnequipItem
            // UseItem

            public async Task<FightResponse> Fight(bool wait_for_cooldown = false)
            {
                var endpoint = $"{_path}/fight";

                var response = await _apiHandler.handle_request(endpoint, HttpMethod.Post);

                FightResponse fight = new FightResponse(response);
                if (wait_for_cooldown)
                {
                    return fight.WaitForCooldown<FightResponse>();
                }

                return fight;
            }
        }
    }
}
