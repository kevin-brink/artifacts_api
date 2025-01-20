using ArtifactsAPI;
using ArtifactsAPI.Client;
using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string name = "Auranus";
string api_key =
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImticmluazE3MEBnbWFpbC5jb20iLCJwYXNzd29yZF9jaGFuZ2VkIjoiIn0.j0jrus4jUQGyfQMwipWthHhIHfZrAeyAvav1eWhc0Q8";
APIHandler api = new APIHandler(api_key, name);

// var client = new Client(api, name);
// await api.Actions.Move(0, 1);
// await client.GrindCombat(10);

// _ = await api.Actions.Move(0, 0);
// var move = await api.Actions.Move(-1, 0);
// var gather = await api.Actions.Gathering();

var maps = await api.Maps.GetAllMapsTotal();
var maps_with_content = maps.data.Where(x => x.content is not null).ToList();
var content = new Dictionary<ContentType, Dictionary<string, List<Map>>>();
foreach (var map in maps_with_content)
{
    var content_type = Enum.TryParse<ContentType>(map.content!.type, out var type)
        ? type
        : ContentType.any;
    if (!content.ContainsKey(content_type))
        content[content_type] = [];

    if (!content[content_type].ContainsKey(map.content!.code))
        content[content_type][map.content!.code] = new List<Map>();

    content[content_type][map.content!.code].Add(map);
}
return 0;
