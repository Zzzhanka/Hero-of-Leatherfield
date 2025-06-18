using UnityEngine;

public enum WeaponType { Melee, Bow, Staff }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory / Weapon", order = 21)]
public class WeaponItemData : Item
{
    public override ItemType itemType => ItemType.Weapon;

    [Space(5), Header("Weapon Common Properties")]
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private WeaponType type;
    [SerializeField] private int baseCritDamage = 1;
    [Range(0f, 1f), SerializeField] private float baseCritChance = 0.1f;

    [Space(5), Header("Specific Properties")]
    [Tooltip("Reloading time in seconds")]
    [SerializeField] private float baseReloadTime = 2f;
    [SerializeField] private GameObject projectilePrefab = null;

    [Space(8), Header("Weapon Limits")]
    [SerializeField, Range(0, 200)] private int damageLimit = 50;
    [SerializeField, Range(0f, 5f)] private float critDamageLimit = 2f;
    [SerializeField, Range(0f, 1f)] private float critChanceLimit = 0.5f;
    [SerializeField, Range(0f, 10f)] private float minusReloadTimeLimit = 1.5f;

    // Hidden in Inspector, available at runtime
    [HideInInspector] public int BaseDamage => baseDamage;
    [HideInInspector] public WeaponType Type => type;
    [HideInInspector] public int BaseCritDamage => baseCritDamage;
    [HideInInspector] public float BaseCritChance => baseCritChance;
    [HideInInspector] public float BaseReloadTime => baseReloadTime;
    [HideInInspector] public GameObject ProjectilePrefab => projectilePrefab;

    [HideInInspector] public int DamageLimit => damageLimit;
    [HideInInspector] public float CritDamageLimit => critDamageLimit;
    [HideInInspector] public float CritChanceLimit => critChanceLimit;
    [HideInInspector] public float MinusReloadTimeLimit => minusReloadTimeLimit;
}
