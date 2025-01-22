using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public class UtilityClient(Client client)
        {
            private readonly Client client = client;

            public Map GetNearestMap(List<Map> maps)
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(maps.Count, 1);

                return maps.OrderBy(map =>
                        Math.Sqrt(
                            Math.Pow(map.x - client.character.x, 2)
                                + Math.Pow(map.y - client.character.y, 2)
                        )
                    )
                    .First();
            }
        }
    }
}
