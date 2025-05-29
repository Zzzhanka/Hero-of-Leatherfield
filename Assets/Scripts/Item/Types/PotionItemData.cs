using UnityEngine;

public enum PotionType
{
    None = 0, // Nothing do
    Health = 1,
    Mana = 2,
}

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory / Potion", order = 21)]
public class PotionItemData : Item
{
    [Space(10), Header("Potion Properties")]
    public int potionHealAmount;
    public float potionUseTime;
    public PotionType potionType = PotionType.None;
}