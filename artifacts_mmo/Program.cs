using ArtifactsAPI;
using ArtifactsAPI.Client;
using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string name = "Auranus";
string api_key =
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImticmluazE3MEBnbWFpbC5jb20iLCJwYXNzd29yZF9jaGFuZ2VkIjoiIn0.j0jrus4jUQGyfQMwipWthHhIHfZrAeyAvav1eWhc0Q8";
APIHandler api = new(api_key, name);
var characters = await api.Other.GetCharacters();
var character = characters.data.Where(c => c.name == name).First();

var client = new Client(api, character);

// await client.Combat.GrindCombat(40);

var highest_items = await client.Crafting.GetHighestLevelItems();

var all_items = (await api.Items.GetAllItemsTotal()).data;
foreach (var slot in Enum.GetValues<Slot>().Cast<Slot>())
{
    var type = ArtifactsAPI.Convert.SlotToItemType(slot);

    var equipped = client.character.GetEquipmentSlot(slot);
    var equipped_item = all_items.FirstOrDefault(x => x.code == equipped);

    var item = highest_items[type];
    if (item is null)
        continue;

    if (equipped_item is not null && equipped_item.level >= item!.level)
        continue;
    else
        await client.api.Actions.UnequipItem(slot);

    await client.Crafting.AcquireItem(item);
}

return 0;
