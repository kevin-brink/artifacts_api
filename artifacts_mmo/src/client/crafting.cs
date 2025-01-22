using ArtifactsAPI.Schemas;

namespace ArtifactsAPI.Client
{
    public partial class Client
    {
        public class CraftingClient(Client client)
        {
            private readonly Client client = client;
            private List<Item> items = [];

            public static Dictionary<ItemType, List<Item>> Organizeitems(List<Item> items)
            {
                var organized_items = new Dictionary<ItemType, List<Item>>();
                foreach (var item in items)
                {
                    ItemType type = Enum.TryParse<ItemType>(item.type, out var item_type)
                        ? item_type
                        : ItemType.any;

                    if (!organized_items.ContainsKey(type))
                        organized_items[type] = [];

                    organized_items[type].Add(item);
                }

                foreach (var item_type in organized_items.Keys)
                {
                    organized_items[item_type] =
                    [
                        .. organized_items[item_type].OrderBy(i => i.level),
                    ];
                }

                return organized_items;
            }

            public async Task<bool> AcquireItem(Item item_to_get)
            {
                // TODO Go through tree and get initial count of items
                // Then visit bank and remove everything not on list
                // and grab everything that is.
                if (items.Count == 0)
                    items = (await client.api.Items.GetAllItemsTotal()).data!;

                if (item_to_get.craft is not null)
                {
                    Craft craft = item_to_get.craft!;
                    if (craft.level > client.character.GetSkillLevel(craft.skill))
                    {
                        Console.WriteLine(
                            $"Need `{craft.skill}` level {craft.level} to craft `{item_to_get.name}`"
                        );
                        return false;
                    }

                    foreach (var craft_item in craft.items)
                    {
                        while (true)
                        {
                            var inv = client.character.inventory;
                            Inventory? item = inv.Where(i => i.code == craft_item.code)
                                .FirstOrDefault();

                            if (item is not null && item.quantity >= craft_item.quantity)
                                break;

                            Item next_item = items.Where(i => i.code == craft_item.code).First();
                            switch (Enum.Parse<ItemType>(next_item.type))
                            {
                                case ItemType.resource:
                                    await get_resource(next_item, craft_item.quantity);
                                    break;
                                default:
                                    throw new Exception("Unknown item type");
                            }
                        }
                    }
                }

                return true;
            }

            public async Task get_resource(Item item_to_get, int needed)
            {
                /* TODO
                    Get all resources
                    Filter to resources which drop this item
                    Sort resources by drop rate
                    Go down list and grab the first one which:
                        - You have the skill to gather
                        - You can find a map for
                        Sort maps by distance to player
                    Get {needed} of the resource
                */
                var maps = await client.api.Maps.GetAllMaps(
                    content_type: ContentType.resource,
                    size: 100
                );

                var more_maps = await client.api.Maps.GetAllMapsTotal();
            }
        }
    }
}
