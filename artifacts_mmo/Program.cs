using ArtifactsAPI;
using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string name = "Auranus";
string api_key =
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImticmluazE3MEBnbWFpbC5jb20iLCJwYXNzd29yZF9jaGFuZ2VkIjoiIn0.j0jrus4jUQGyfQMwipWthHhIHfZrAeyAvav1eWhc0Q8";
APIHandler api = new APIHandler(api_key, name);

var result = await api.Actions.Move(0, 1, wait_for_cooldown: true);

Console.WriteLine(result.character?.name ?? "No character");

Character character;
do
{
    FightResponse fight = await api.Actions.Fight(wait_for_cooldown: true);
    RestResponse rest = await api.Actions.Rest(wait_for_cooldown: true);
    character = rest.character!;
} while (character.level < 2);
return 0;
