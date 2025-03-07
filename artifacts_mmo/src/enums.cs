namespace ArtifactsAPI
{
    public enum StatusCode
    {
        OK = 200,
        NotFound = 404,
        ActionInProgress = 486,
        AtDestination = 490,
        NotRequiredSkillLevel = 493,
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
}
