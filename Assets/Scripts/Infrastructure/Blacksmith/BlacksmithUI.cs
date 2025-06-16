using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BlacksmithUI : MonoBehaviour
{
    [Space(5), Header("Inventory Side")]
    [SerializeField] private GameObject weaponSlotPrefab;
    [SerializeField] private Transform weaponListGrid;

    [Space(8), Header("Operation Side")]
    [SerializeField] private GameObject InfoPart;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TMP_Text weaponStats;
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private Button boostButton;

    [Space(8), Header("Blacksmith Side")]
    [SerializeField] private GameObject boostSlotPrefab;
    [SerializeField] private Transform blacksmithGrid;

    private List<GameObject> blacksmithSlots = new List<GameObject>();
    private List<GameObject> weaponSlots = new List<GameObject>();

    private WeaponItemData chosenWeapon;
    private BoostSlot chosenBoost;

    private void Awake()
    {


        boostButton.onClick.RemoveAllListeners();
        boostButton.onClick.AddListener(() => { });
    }

    private void OnEnable()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        ChangeCoinsText();

        RefreshInventoryList();
    }

    private void RefreshInventoryList()
    {
        ClearWeaponList();

        List<ItemEntry> weaponList = GameManager.Instance.InventoryManager.GetSpecificEntries(ItemType.Weapon);
        CreateWeaponList(weaponList);
    }

    private void CreateWeaponList(List<ItemEntry> weaponList)
    {
        while (weaponSlots.Count < weaponList.Count)
        {
            GameObject newSlot = Instantiate(weaponSlotPrefab, weaponListGrid);
            weaponSlots.Add(newSlot);
        }

        for (int i = 0; i < weaponList.Count; ++i)
        {
            if (i < weaponList.Count)
            {
                WeaponSlot slot = weaponSlots[i].GetComponentInChildren<WeaponSlot>();
                slot.Setup(weaponList[i], ChooseInventoryItem);
                weaponSlots[i].SetActive(true);
            }

            else
            {
                weaponSlots[i].SetActive(false);
            }
        }
    }

    private void ChooseInventoryItem(ItemEntry entry)
    {
        chosenWeapon = entry.weapon;

        InfoPart.SetActive(true);
    }

    private void ClearWeaponList()
    {
        foreach (GameObject slot in weaponSlots)
            slot.SetActive(false);
    }

    private void ChangeCoinsText()
    {
        int number = GameManager.Instance.ScoreSystem.TotalCoins;
        CoinsText.text = number.ToString();
    }
}
