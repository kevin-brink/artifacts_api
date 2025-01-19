using System.Runtime.InteropServices;
using ArtifactsAPI;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string name = "Auranus";
string api_key =
    "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImticmluazE3MEBnbWFpbC5jb20iLCJwYXNzd29yZF9jaGFuZ2VkIjoiIn0.j0jrus4jUQGyfQMwipWthHhIHfZrAeyAvav1eWhc0Q8";
APIHandler api = new APIHandler(api_key, name);

var result = await api.Actions.Move(0, 1);
var status_code = result.status_code;
if (status_code == StatusCode.AtDestination)
{
    Console.WriteLine("Already at destination");
}
else if (status_code == StatusCode.OK)
{
    Console.WriteLine("Moved");
}
else
{
    Console.WriteLine("Error");
}

Console.WriteLine(result.character?.name ?? "No character");

return 0;
