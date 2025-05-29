using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory / Weapon", order = 21)]
public class WeaponItemData : Item
{
    [Space(5), Header("Weapon Common Properties")]
    public int damage;
    public WeaponType type;
    public int critDamage;
    public float critChance = 0.2f;

    [Space(5), Header("Specific Properties")]
    public float reloadTime;
    public GameObject ProjectilePrefab = null; // Сам поставишь какой тип объекта нужно ставить
}
