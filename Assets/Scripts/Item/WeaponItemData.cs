using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory / Weapon", order = 21)]
public class WeaponItemData : ItemData
{
    [Space(10), Header("Weapon Properties")]
    public int damage;
    public float attackSpeed;
    public float range;
}
