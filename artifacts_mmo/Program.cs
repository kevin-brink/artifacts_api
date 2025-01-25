using ArtifactsAPI;
using ArtifactsAPI.Client;
using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

// https://www.artifactsmmo.com/client

string name = "Auranus";
string api_key =
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImticmluazE3MEBnbWFpbC5jb20iLCJwYXNzd29yZF9jaGFuZ2VkIjoiIn0.j0jrus4jUQGyfQMwipWthHhIHfZrAeyAvav1eWhc0Q8";
APIHandler api = new(api_key, name);
var characters = await api.Other.GetCharacters();
var character = characters.data.Where(c => c.name == name).First();

Console.WriteLine($"Welcome, {character.name}!");
Console.WriteLine($"Currently level {character.level}.\n");

var client = new Client(api, character);

// await client.Combat.GrindCombat(40);


var all_items = (await api.Items.GetAllItemsTotal()).data;
var highest_items = await client.Crafting.GetBestItems(all_items);
List<Slot> skip_for_now =
[
    Slot.artifact1,
    Slot.artifact2,
    Slot.artifact3,
    Slot.utility1,
    Slot.utility2,
];
foreach (var slot in Enum.GetValues<Slot>().Except(skip_for_now))
{
    var type = ArtifactsAPI.Convert.SlotToItemType(slot);

    var equipped = client.character.GetEquipmentSlot(slot);
    var equipped_item = all_items.FirstOrDefault(x => x.code == equipped);

    var item = highest_items[type];
    if (item is null)
        continue;

    bool currently_equipped = equipped_item is not null;
    if (equipped_item is not null && equipped_item.level >= item!.level)
        continue;

    if (!await client.Crafting.AcquireItem(item))
        continue;

    if (currently_equipped)
        await client.api.Actions.UnequipItem(slot);
    await client.api.Actions.EquipItem(item.code, slot);
}

return 0;
