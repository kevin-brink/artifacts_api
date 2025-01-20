using ArtifactsAPI.Models;
using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public class CraftingClient(Client client)
        {
            private readonly Client client = client;

            // TODO: Implement crafting methods here
        }
    }
}
