using UnityEngine;

public enum PotionType
{
    None, Health, Mana, Speed, Damage,
}

public enum PotionActionType
{
    Adding, // Add multiple times over
    Activating, // Activates once
}

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory / Potion", order = 21)]
public class PotionItemData : Item
{
    public override ItemType itemType => ItemType.Potion;

    [Space(5), Header("Potion Properties")]
    public PotionType potionType = PotionType.None;

    [Space(4)]
    [Tooltip("Unit of increasing amount (e.g., 1 HP / sec")]
    public float potionIncreaseAmount = 1;

    [Tooltip("Time interval in seconds between 2 units")]
    public float potionIncreaseInterval = 3;

    [Space(4)]
    [Tooltip("Is Potion increase instantly?")]
    public bool isInstant = false;

    [Space(4)]
    [Tooltip("Is effect activating once or adding multiple units over time?\nUseless if IsInstant is True")]
    public PotionActionType potionActionType = PotionActionType.Adding;

    [Tooltip("Time period of potion\nUseless if IsInstant is True")]
    public float potionUseTime = 0;
}