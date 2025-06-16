using UnityEngine;

public enum WeaponType { Melee, Bow, Staff }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory / Weapon", order = 21)]
public class WeaponItemData : Item
{
    public override ItemType itemType => ItemType.Weapon;

    [Space(5), Header("Weapon Common Properties")]

    public int baseDamage;
    public WeaponType type;
    public int baseCritDamage;
    public float baseCritChance = 0.2f;

    [Space(5), Header("Specific Properties")]
    public float baseReloadTime;
    public GameObject ProjectilePrefab = null; // Сам поставишь какой тип объекта нужно ставить
}
