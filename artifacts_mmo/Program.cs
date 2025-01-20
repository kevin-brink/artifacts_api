﻿using ArtifactsAPI;
using ArtifactsAPI.Client;
using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string name = "Auranus";
string api_key =
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImticmluazE3MEBnbWFpbC5jb20iLCJwYXNzd29yZF9jaGFuZ2VkIjoiIn0.j0jrus4jUQGyfQMwipWthHhIHfZrAeyAvav1eWhc0Q8";
APIHandler api = new APIHandler(api_key, name);
var characters = await api.Other.GetCharacters();
var character = characters.data.Where(c => c.name == name).First();

// var client = new Client(api, name);
// await client.GrindCombat(10, character);

await api.Actions.Move(-1, 0);
Inventory logs;
do
{
    var gather = await api.Actions.Gathering();
    logs = gather.character!.inventory.Where(i => i.code == "ash_wood").First();
} while (logs.quantity < 4);

var map = await api.Maps.GetAllMaps(content_type: ContentType.workshop);
var workshop = map.data.Where(m => m.name == "weaponcraft").First();
var move = await api.Actions.Move(workshop.x, workshop.y);

// _ = await api.Actions.Move(0, 0);
// var move = await api.Actions.Move(-1, 0);
// var gather = await api.Actions.Gathering();

return 0;
