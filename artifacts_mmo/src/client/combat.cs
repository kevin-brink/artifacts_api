using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public class CombatClient(Client client)
        {
            private readonly Client client = client;

            public async Task GrindCombat(int target_level)
            {
                // TODO Actually calculate if a fight is winnable.
                // I dont think theres actually much randomness here, so this should be fairly straighforward
                var targets = await FindAllTargets();
                int target_index = 2;
                Map target = client.Utility.GetNearestMap(targets[target_index]);

                bool has_leveled_since_loss = true;
                int level_before_fight = client.character.level;

                if (client.character.hp < client.character.max_hp)
                    await client.api.Actions.Rest();

                do
                {
                    await client.api.Actions.Move(target);

                    var fight = await client.api.Actions.Fight();

                    if (fight.status_code == StatusCode.InventoryFull)
                    {
                        await client.Combat.EmptyInventoryInBank();
                        target = client.Utility.GetNearestMap(targets[target_index]);
                        await client.api.Actions.Move(target);
                        fight = await client.api.Actions.Fight();
                    }

                    while (fight.status_code == StatusCode.OnCooldown)
                    {
                        Thread.Sleep(1000);
                        fight = await client.api.Actions.Fight();
                    }

                    if (Enum.Parse<FightResult>(fight.fight!.result) == FightResult.loss)
                    {
                        target_index--;
                        target = client.Utility.GetNearestMap(targets[target_index]);
                        has_leveled_since_loss = false;
                    }

                    client.character = fight.character!;
                    if (client.character.level > level_before_fight)
                    {
                        has_leveled_since_loss = true;
                        level_before_fight = client.character.level;
                    }

                    if (has_leveled_since_loss)
                    {
                        target_index++;
                        target = client.Utility.GetNearestMap(targets[target_index]);
                    }

                    fight.WaitForCooldown<FightResponse>();
                    var rest = await client.api.Actions.Rest();
                    client.character = rest.character!;
                } while (client.character.level < target_level);

                await client.api.Actions.Rest();
            }

            public async Task GrindTasks()
            {
                return; // TODO
            }

            private async Task<List<List<Map>>> FindAllTargets()
            {
                var monsters = (await client.api.Maps.GetAllMapsContents())[ContentType.monster];
                List<Monster> monster_data = [];

                foreach (var monster in monsters)
                {
                    monster_data.Add((await client.api.Monsters.GetMonster(monster.Key)).data!);
                }

                monster_data = [.. monster_data.OrderBy(m => m.level)];
                List<List<Map>> target_list = [];
                foreach (var monster in monster_data)
                {
                    target_list.Add(monsters.Where(m => m.Key == monster.code).First().Value);
                }

                return target_list;
            }

            public async Task EmptyInventoryInBank()
            {
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
            }
        }
    }
}
