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
    [SerializeField] private TMP_Text CoinsText;
    [SerializeField] private Button boostButton;

    [SerializeField] private TMP_Text statsDamage;
    [SerializeField] private TMP_Text statsCritical;
    [SerializeField] private TMP_Text statsReloadTime;

    [Space(8), Header("Blacksmith Side")]
    [SerializeField] private GameObject boostSlotPrefab;
    [SerializeField] private Transform boostGrid;

    private List<GameObject> boostSlots = new List<GameObject>();
    private List<GameObject> weaponSlots = new List<GameObject>();

    private WeaponInstance chosenWeapon;
    private Boost chosenBoost;

    private void Awake()
    {
        this.gameObject.SetActive(false);

        boostButton.onClick.RemoveAllListeners();
        boostButton.onClick.AddListener(Boost);
    }

    private void OnEnable()
    {
        RefreshUI();
        ChooseFirstSlot();

        GameManager.Instance.InventoryManager.OnInventoryChanged += RefreshUI;
    }

    private void OnDisable()
    {
        chosenBoost = null;
        GameManager.Instance.InventoryManager.OnInventoryChanged -= RefreshUI;
    }

    public void Boost()
    {
        GameManager.Instance.BlacksmithManager.BoostWeapon(chosenWeapon, chosenBoost);

        RefreshUI();
        ClearStats();
    }

    private void RefreshUI()
    {
        ChangeCoinsText();

        RefreshInventoryList();
        RefreshBoostList();

        ChangeBoostButton();
    }

    private void RefreshInventoryList()
    {
        ClearWeaponList();

        List<ItemEntry> weaponList = GameManager.Instance.InventoryManager.GetSpecificEntries(ItemType.Weapon);
        CreateWeaponList(weaponList);
    }

    private void RefreshBoostList()
    {
        ClearBoostList();

        List<Boost> boostList = GameManager.Instance.BlacksmithManager.BoostList;
        CreateBoostList(boostList);
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

    private void CreateBoostList(List<Boost> boostList)
    {
        // Ensure we have enough slots
        while (boostSlots.Count < boostList.Count)
        {
            GameObject newSlot = Instantiate(boostSlotPrefab, boostGrid);
            boostSlots.Add(newSlot);
        }

        for (int i = 0; i < boostSlots.Count; ++i)
        {
            if (i < boostList.Count)
            {
                BoostSlot slot = boostSlots[i].GetComponentInChildren<BoostSlot>();
                slot.Setup(boostList[i], ChooseBoost);
                boostSlots[i].SetActive(true);
            }
            else
            {
                boostSlots[i].SetActive(false);
            }
        }
    }


    private void ChooseInventoryItem(ItemEntry entry)
    {
        if (entry == null || entry.weapon == null)
        {
            chosenWeapon = null;
            ChangeBoostButton();

            weaponIcon.enabled = false;
            weaponName.text = "-";

            statsDamage.text = "-";
            statsCritical.text = "- / -";
            statsReloadTime.text = "- s";
            return;
        }

        chosenWeapon = entry.weapon;
        ChangeBoostButton();

        weaponIcon.enabled = true;
        weaponIcon.sprite = chosenWeapon.BaseItem.icon;
        weaponName.text = chosenWeapon.BaseItem.itemName;

        statsDamage.text = chosenWeapon.TotalDamage.ToString();
        statsCritical.text =
            chosenWeapon.TotalCritDamage
            + " / " + 
            (chosenWeapon.TotalCritChance * 100f);

        statsReloadTime.text = chosenWeapon.TotalReloadTime + " s";
    }

    private void ChooseBoost(Boost boost)
    {
        ClearStats();
        chosenBoost = boost;
        ChangeBoostButton();

        if (chosenWeapon == null) return;

        switch (boost.boostType)
        {
            case BoostType.Damage:
                statsDamage.text = $"<color=green>{chosenWeapon.TotalDamage + Mathf.Round(boost.boostAmount)}</color>"; 
                break;

            case BoostType.CritDamage:
                statsCritical.text = $"<color=green>{chosenWeapon.TotalCritDamage + boost.boostAmount}</color> / {chosenWeapon.TotalCritChance * 100f}";
                break;

            case BoostType.CritChance:
                statsCritical.text = $"{chosenWeapon.TotalCritDamage} / <color=green>{(chosenWeapon.TotalCritChance + boost.boostAmount) * 100f}</color>";
                break;

            case BoostType.ReloadTime:
                statsReloadTime.text = $"<color=green>{chosenWeapon.TotalReloadTime - boost.boostAmount} s</color>"; 
                break;
        }
    }

    private void ChooseFirstSlot(int chosenSlotPosition = 0)
    {
        chosenWeapon = null;
        if (weaponSlots.Count == 0)
        {
            ChooseInventoryItem(null);
            return;
        }

        WeaponSlot slot = weaponSlots[chosenSlotPosition].GetComponentInChildren<WeaponSlot>();
        
        InfoPart.SetActive(true);
        ChooseInventoryItem(slot.GetEntry());
    }


    private void ClearWeaponList()
    {
        foreach (GameObject slot in weaponSlots)
        {
            slot.GetComponentInChildren<WeaponSlot>().Clear();
            slot.SetActive(false);
        }
    }

    private void ClearBoostList()
    {
        foreach (GameObject slot in boostSlots)
            slot.SetActive(false);
    }

    private void ChangeCoinsText()
    {
        while(GameManager.Instance == null)
        {

        }

        int number = GameManager.Instance.ScoreSystem.TotalCoins;
        CoinsText.text = number.ToString();
    }

    private void ClearStats()
    {
        if (chosenWeapon != null)
        {
            statsDamage.text = $"<color=white>{chosenWeapon.TotalDamage}</color>";
            statsCritical.text = $"<color=white>{chosenWeapon.TotalCritDamage} / {chosenWeapon.TotalCritChance * 100}</color>";
            statsReloadTime.text = $"<color=white>{chosenWeapon.TotalReloadTime} s</color>";
        }
    }

    private void ChangeBoostButton()
    {
        bool temp1 = (chosenWeapon != null && chosenBoost != null) && chosenWeapon.CurrentBoostLevel < 3;
        bool temp2 = false;

        if (chosenBoost != null)
            temp2 = chosenBoost.boostCost <= GameManager.Instance.ScoreSystem.TotalCoins;
        
        boostButton.interactable = temp1 && temp2;
    }
}
