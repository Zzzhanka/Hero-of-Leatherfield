using UnityEngine;

public enum WeaponType
{
    Generic = 0,
    Knife = 1,
    Sword = 2,
    Axe = 3,
    Hammer = 4,
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory / Weapon", order = 21)]
public class WeaponItemData : ItemData
{
    [Space(10), Header("Weapon Properties")]
    public int damage;
    public float attackSpeed;
    public float range;
    public WeaponType type;
}
