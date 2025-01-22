using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public class CombatClient(Client client)
        {
            private readonly Client client = client;

            public async Task GrindCombat(int target_level, Character character)
            {
                // TODO Actually calculate if a fight is winnable.
                // I dont think theres actually much randomnes here, so this should be fairly straighforward
                var targets = await FindTargets();
                int target = 0;

                bool has_leveled_since_loss = true;
                int level_before_fight = character.level;

                do
                {
                    await client.api.Actions.Move(targets[target].x, targets[target].y);

                    var fight = await client.api.Actions.Fight();

                    if (fight.status_code == StatusCode.InventoryFull)
                    {
                        await client.Combat.EmptyInventoryInBank();
                        continue;
                    }

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

                    if (has_leveled_since_loss)
                        target++;

                    fight.WaitForCooldown<FightResponse>();
                    var rest = await client.api.Actions.Rest();
                    character = rest.character!;
                } while (character.level < target_level);
            }

            private async Task<List<Map>> FindTargets()
            {
                var monsters = (await client.api.Maps.GetAllMapsContents())[ContentType.monster];
                List<Monster> monster_data = [];

                foreach (var monster in monsters)
                {
                    monster_data.Add((await client.api.Monsters.GetMonster(monster.Key)).data!);
                }

                monster_data = [.. monster_data.OrderBy(m => m.level)];
                List<Map> target_list = [];
                foreach (var monster in monster_data)
                {
                    var monster_of_type = monsters.Where(m => m.Key == monster.code).First().Value;
                    target_list.Add(client.Utility.GetNearestMap(monster_of_type)!);
                }

                return target_list;
            }

            public async Task EmptyInventoryInBank()
            {
                var curr_map = await client.api.Maps.GetMap(client.character.x, client.character.y);
                var maps = await client.api.Maps.GetAllMaps(content_type: ContentType.bank);
                var nearest_bank = client.Utility.GetNearestMap(maps.data);
                if (nearest_bank is null)
                {
                    Console.WriteLine("No bank found");
                    return;
                }

                await client.api.Actions.Move(nearest_bank);

                if (client.character.gold > 0)
                {
                    await client.api.Actions.DepositBankGold(client.character.gold);
                }

                foreach (var inventory in client.character.inventory)
                {
                    if (inventory is null || inventory.quantity == 0)
                        continue;

                    await client.api.Actions.DepositBank(inventory.code, inventory.quantity);
                }

                await client.api.Actions.Move(curr_map.data!);
            }
        }
    }
}
