public enum UIHintType
{
    TooFar
}

public enum AchievementType
{
    PlantTypes,
    PlantAmount,
    Gold
}


public enum QuestID
{
    Undefined = 0,

    SellGold,
    RewardPirate,
    BuyCoal,
    BuyWeapon,
    CheckLocation,
    GoToCastle,
    Kill5Dragon,
    KillBoss,

}
/// <summary>
/// Отдельный enum для всех «ключей квестов». 
/// В него входят пары: <QuestID>_Title и <QuestID>_Description.
/// Эти ключи используются только внутри QuestDefinition, 
/// чтобы не мешать основным ключам локализации.
/// </summary>
public enum QuestKeyTypeTitle
{
    None = 0,

    Quest_SellGold_Title,
    Quest_RewardPirate_Title,
    Quest_BuyCoal_Title,
    Quest_BuyWeapon_Title,
    Quest_CheckLocation_Title,
    Quest_GoToCastle_Title,
    Quest_Kill5Dragon_Title,
    Quest_KillBoss_Title,
}
/// <summary>
/// Отдельный enum для всех «ключей квестов». 
/// В него вход <QuestID>_Description.
/// Эти ключи используются только внутри QuestDefinition, 
/// чтобы не мешать основным ключам локализации.
/// </summary>
public enum QuestKeyTypeDescription
{
    None = 0,

    Quest_SellGold_Description,
    Quest_RewardPirate_Description,
    Quest_BuyCoal_Description,
    Quest_BuyWeapon_Description,
    Quest_CheckLocation_Description,
    Quest_GoToCastle_Description,
    Quest_Kill5Dragon_Description,
    Quest_KillBoss_Description,
}


public enum InteractType
{
    Location,
    City
}

public enum ItemTag
{
    Fuel,
    Trash,
    Valuable,
    Weapon,
    Medicine,
    Dead,
    Ammo,
    Reward
}


public enum CurrencyType
{
    Coins,
    Gems,
    Real
}

public enum LocalizationKeyType
{
    Settings,
    Item,
    Tag,
    Achievement,
    Level,
    Quest
}


public enum RouletteRewardType
{
    Gems,
    Item
}

public enum ItemStatus
{

}


public enum SaveKey
{
    MusicVolume,
    SoundVolume,
    Score_,
    LevelUnlock_,
    LevelWin_,
    Gems,
    Save,
    LobbyItems,
    Distance,
    InventoryList,
    Boardlist,
    Fuel,
    AttachedItems,
    Ammo_,
    Wins,
    LevelId,
    RouletteLastDate,
    Health,
    Coins,
    EndTutorial,
    QuestProgress,


}