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

await client.Combat.GrindCombat(40, character);

int item_number = 0;
var items = await api.Items.GetAllItemsTotal();
await client.Crafting.AcquireItem(items.data[item_number]);

var organized_items = Client.CraftingClient.Organizeitems(items.data);

return 0;
