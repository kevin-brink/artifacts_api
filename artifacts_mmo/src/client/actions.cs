using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public class Client(APIHandler api_handler, string character_name)
    {
        private readonly APIHandler _api_handler = api_handler;

        // TODO This will only be needed if we make Actions a dict
        private readonly string _character_name = character_name;

        public async Task GrindCombat(int target_level, Character character)
        {
            // TODO Actually calculate if a fight is winnable.
            // I dont think theres actually much randomnes here, so this should be fairly straighforward
            var targets = await FindTargets(character);
            int target = 0;

            bool has_leveled_since_loss = true;
            int level_before_fight = character.level;

            do
            {
                await _api_handler.Actions.Move(targets[target].x, targets[target].y);

                var fight = await _api_handler.Actions.Fight(false); // WaitForCooldown later
                if (Enum.Parse<FightResult>(fight.fight!.result) == FightResult.loss)
                {
                    target--;
                    has_leveled_since_loss = false;
                }

                character = fight.character!;
                if (character.level > level_before_fight)
                {
                    has_leveled_since_loss = true;
                    level_before_fight = character.level;
                }

                if (has_leveled_since_loss && character.hp > character.max_hp * 0.10)
                {
                    target++;
                }

                fight.WaitForCooldown<FightResponse>();
                var rest = await _api_handler.Actions.Rest();
                character = rest.character!;
            } while (character.level < target_level);
        }

        private async Task<List<Map>> FindTargets(Character character)
        {
            var monsters = (await _api_handler.Maps.GetAllMapsContents())[ContentType.monster];
            List<Monster> monster_data = [];

            foreach (var monster in monsters)
            {
                monster_data.Add((await _api_handler.Monsters.GetMonster(monster.Key)).data!);
            }

            monster_data = [.. monster_data.OrderBy(m => m.level)];
            List<Map> target_list = [];
            foreach (var monster in monster_data)
            {
                target_list.Add(monsters.Where(m => m.Key == monster.code).First().Value.First());
            }

            return target_list;
        }
    }
}
