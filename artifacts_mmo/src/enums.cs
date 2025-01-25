namespace ArtifactsAPI
{
    public enum StatusCode
    {
        OK = 200,
        NotFound = 404,
        TransactionInProgress = 461,
        BankFull = 462,
        MissingItem = 478,
        NotEnoughHP = 483,
        TooManyUtilities = 484,
        AlreadyEquipped = 485,
        ActionInProgress = 486,
        AtDestination = 490,
        SlotEmpty = 491,
        InsufficientGold = 492,
        NotRequiredSkillLevel = 493,
        LevelTooLow = 496,
        InventoryFull = 497,
        CharacterNotFound = 498,
        OnCooldown = 499,
        TargetNotOnMap = 598,
    }

    public enum ContentType
    {
        any,
        monster,
        resource,
        workshop,
        bank,
        grand_exchange,
        tasks_master,
    }

    public enum FightResult
    {
        win,
        loss,
    }

    public enum Slot
    {
        weapon,
        shield,
        helmet,
        body_armor,
        leg_armor,
        boots,
        ring1,
        ring2,
        amulet,
        artifact1,
        artifact2,
        artifact3,
        utility1,
        utility2,
    }

    public enum CraftSkill
    {
        any,
        weaponcrafting,
        gearcrafting,
        jewelrycrafting,
        cooking,
        woodcutting,
        mining,
        alchemy,
    }

    public enum ItemType
    {
        any,
        utility,
        body_armor,
        weapon,
        resource,
        leg_armor,
        helmet,
        boots,
        shield,
        amulet,
        ring,
        artifact,
        currency,
        consumable,
    }

    public enum Element
    {
        fire,
        water,
        earth,
        air,
    }

    public class Convert
    {
        public static ItemType SlotToItemType(Slot slot)
        {
            return slot switch
            {
                Slot.weapon => ItemType.weapon,
                Slot.shield => ItemType.shield,
                Slot.helmet => ItemType.helmet,
                Slot.body_armor => ItemType.body_armor,
                Slot.leg_armor => ItemType.leg_armor,
                Slot.boots => ItemType.boots,
                Slot.ring1 or Slot.ring2 => ItemType.ring,
                Slot.amulet => ItemType.amulet,
                Slot.artifact1 or Slot.artifact2 or Slot.artifact3 => ItemType.artifact,
                Slot.utility1 or Slot.utility2 => ItemType.utility,
                _ => ItemType.any,
            };
        }
    }
}
