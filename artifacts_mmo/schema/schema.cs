using Newtonsoft.Json.Linq;

namespace ArtifactsAPI.Schemas
{
    public class Cooldown(JToken json)
    {
        public int total_seconds = (int)json["total_seconds"]!;
        public int remaining_seconds = (int)json["remaining_seconds"]!;
        public DateTime started_at = (DateTime)json["started_at"]!;
        public DateTime expiration = (DateTime)json["expiration"]!;
        public string reason = (string)json["reason"]!;
    }

    public class Map(JToken json)
    {
        public string name = (string)json["name"]!;
        public string skin = (string)json["skin"]!;
        public int x = (int)json["x"]!;
        public int y = (int)json["y"]!;
        public MapContent content = new MapContent(json["content"]!);
    }

    public class MapContent(JToken json)
    {
        public string type = (string)json["type"]!;
        public string code = (string)json["code"]!;
    }

    public class Character(JToken json)
    {
        public string name = (string)json["name"]!;
        public string account = (string)json["account"]!;
        public string skin = (string)json["skin"]!;
        public int level = (int)json["level"]!;
        public int xp = (int)json["xp"]!;
        public int max_xp = (int)json["max_xp"]!;
        public int gold = (int)json["gold"]!;
        public int speed = (int)json["speed"]!;
        public int mining_level = (int)json["mining_level"]!;
        public int mining_xp = (int)json["mining_xp"]!;
        public int mining_max_xp = (int)json["mining_max_xp"]!;
        public int woodcutting_level = (int)json["woodcutting_level"]!;
        public int woodcutting_xp = (int)json["woodcutting_xp"]!;
        public int woodcutting_max_xp = (int)json["woodcutting_max_xp"]!;
        public int fishing_level = (int)json["fishing_level"]!;
        public int fishing_xp = (int)json["fishing_xp"]!;
        public int fishing_max_xp = (int)json["fishing_max_xp"]!;
        public int weaponcrafting_level = (int)json["weaponcrafting_level"]!;
        public int weaponcrafting_xp = (int)json["weaponcrafting_xp"]!;
        public int weaponcrafting_max_xp = (int)json["weaponcrafting_max_xp"]!;
        public int gearcrafting_level = (int)json["gearcrafting_level"]!;
        public int gearcrafting_xp = (int)json["gearcrafting_xp"]!;
        public int gearcrafting_max_xp = (int)json["gearcrafting_max_xp"]!;
        public int jewelrycrafting_level = (int)json["jewelrycrafting_level"]!;
        public int jewelrycrafting_xp = (int)json["jewelrycrafting_xp"]!;
        public int jewelrycrafting_max_xp = (int)json["jewelrycrafting_max_xp"]!;
        public int cooking_level = (int)json["cooking_level"]!;
        public int cooking_xp = (int)json["cooking_xp"]!;
        public int cooking_max_xp = (int)json["cooking_max_xp"]!;
        public int alchemy_level = (int)json["alchemy_level"]!;
        public int alchemy_xp = (int)json["alchemy_xp"]!;
        public int alchemy_max_xp = (int)json["alchemy_max_xp"]!;
        public int hp = (int)json["hp"]!;
        public int max_hp = (int)json["max_hp"]!;
        public int haste = (int)json["haste"]!;
        public int critical_strike = (int)json["critical_strike"]!;
        public int stamina = (int)json["stamina"]!;
        public int attack_fire = (int)json["attack_fire"]!;
        public int attack_earth = (int)json["attack_earth"]!;
        public int attack_water = (int)json["attack_water"]!;
        public int attack_air = (int)json["attack_air"]!;
        public int dmg_fire = (int)json["dmg_fire"]!;
        public int dmg_earth = (int)json["dmg_earth"]!;
        public int dmg_water = (int)json["dmg_water"]!;
        public int dmg_air = (int)json["dmg_air"]!;
        public int res_fire = (int)json["res_fire"]!;
        public int res_earth = (int)json["res_earth"]!;
        public int res_water = (int)json["res_water"]!;
        public int res_air = (int)json["res_air"]!;
        public int x = (int)json["x"]!;
        public int y = (int)json["y"]!;
        public int cooldown = (int)json["cooldown"]!;
        public DateTime cooldown_expiration = (DateTime)json["cooldown_expiration"]!;
        public string weapon_slot = (string)json["weapon_slot"]!;
        public string shield_slot = (string)json["shield_slot"]!;
        public string helmet_slot = (string)json["helmet_slot"]!;
        public string body_armor_slot = (string)json["body_armor_slot"]!;
        public string leg_armor_slot = (string)json["leg_armor_slot"]!;
        public string boots_slot = (string)json["boots_slot"]!;
        public string ring1_slot = (string)json["ring1_slot"]!;
        public string ring2_slot = (string)json["ring2_slot"]!;
        public string amulet_slot = (string)json["amulet_slot"]!;
        public string artifact1_slot = (string)json["artifact1_slot"]!;
        public string artifact2_slot = (string)json["artifact2_slot"]!;
        public string artifact3_slot = (string)json["artifact3_slot"]!;
        public string utility1_slot = (string)json["utility1_slot"]!;
        public int utility1_slot_quantity = (int)json["utility1_slot_quantity"]!;
        public string utility2_slot = (string)json["utility2_slot"]!;
        public int utility2_slot_quantity = (int)json["utility2_slot_quantity"]!;
        public string task = (string)json["task"]!;
        public string task_type = (string)json["task_type"]!;
        public int task_progress = (int)json["task_progress"]!;
        public int task_total = (int)json["task_total"]!;
        public int inventory_max_items = (int)json["inventory_max_items"]!;
        public List<Inventory> inventory =
        [
            .. json["inventory"]!.Select(item => new Inventory(item)),
        ];
    }

    public class Inventory(JToken json)
    {
        public int slot = (int)json["slot"]!;
        public string code = (string)json["code"]!;
        public int quantity = (int)json["quantity"]!;
    }

    public class Fight(JToken json)
    {
        public int xp = (int)json["xp"]!;
        public int gold = (int)json["gold"]!;
        public List<Drop> drops = json["drops"]!.Select(item => new Drop(item)).ToList();
        public int turns = (int)json["turns"]!;
        public BlockedHits monster_blocked_hits = new BlockedHits(json["monster_blocked_hits"]!);
        public BlockedHits player_blocked_hits = new BlockedHits(json["player_blocked_hits"]!);
        public List<string> logs = json["logs"]!.Select(log => (string)log!).ToList();
        public string result = (string)json["result"]!;
    }

    public class Drop(JToken json)
    {
        public string code = (string)json["code"]!;
        public int quantity = (int)json["quantity"]!;
    }

    public class BlockedHits(JToken json)
    {
        public int fire = (int)json["fire"]!;
        public int earth = (int)json["earth"]!;
        public int water = (int)json["water"]!;
        public int air = (int)json["air"]!;
        public int total = (int)json["total"]!;
    }

    public class Status(JToken json)
    {
        public string status = (string)json["status"]!;
        public string version = (string)json["version"]!;
        public int max_level = (int)json["max_level"]!;
        public int characters_online = (int)json["characters_online"]!;
        public DateTime server_time = (DateTime)json["server_time"]!;
        public List<Announcement> announcements =
        [
            .. json["announcements"]!.Select(item => new Announcement(item)),
        ];
        public string last_wipe = (string)json["last_wipe"]!;
        public string next_wipe = (string)json["next_wipe"]!;
    }

    public class Announcement(JToken json)
    {
        public string message = (string)json["message"]!;
        public DateTime created_at = (DateTime)json["created_at"]!;
    }
}
