using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

class ArtifactsAPI
{
    private const string url = "https://api.artifactsmmo.com/";
    private readonly string character_name;
    private Dictionary<string, string> headers;

    private HttpClient client;

    public ArtifactsAPI(string api_key, string character_name)
    {
        this.character_name = "my/" + character_name;

        this.headers = new Dictionary<string, string>()
        {
            { "Accept", "application/json" },
            { "Content-Type", "application/json" },
            { "Authorization", api_key },
        };

        this.client = new HttpClient();
        client.BaseAddress = new Uri(ArtifactsAPI.url);
    }

    public void request_handler(string endpoint, string method, Dictionary<string, string> body)
    {
        string full_endpoint = url + character_name + "/" + endpoint;
    }
}

class Action
{
    public const string url_path = "action/";

    public void move(int x, int y)
    {
        string endpoint = "move/";
    }
}
