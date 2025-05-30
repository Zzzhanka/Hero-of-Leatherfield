using UnityEngine;

public enum PotionType
{
    None = 0,
    Health = 1,
    Mana = 2,
}

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory / Potion", order = 21)]
public class PotionItemData : Item
{
    public override ItemType itemType => ItemType.Potion;

    [Space(5), Header("Potion Properties")]
    public int potionHealAmount;
    public float potionUseTime;
    public PotionType potionType = PotionType.None;
}