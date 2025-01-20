namespace ArtifactsAPI
{
    public enum StatusCode
    {
        OK = 200,
        NotFound = 404,
        MissingItem = 478,
        NotEnoughHP = 483,
        TooManyUtilities = 484,
        AlreadyEquipped = 485,
        ActionInProgress = 486,
        AtDestination = 490,
        SlotEmpty = 491,
        NotRequiredSkillLevel = 493,
        LevelTooLow = 496,
        InventoryFull = 497,
        CharacterNotFound = 498,
        OnCooldown = 499,
        TargetNotFound = 598,
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
}
