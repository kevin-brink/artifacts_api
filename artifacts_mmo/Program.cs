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

var content = await api.Maps.GetAllMapsContents();
return 0;
