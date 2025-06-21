using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Space(5), Header("Inventory Slots Part")]
    [Range(30, 100)] public int MinSlots = 56;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private TMP_Text CoinsText;

    [Space(8), Header("Item Details Part")]
    [SerializeField] private GameObject DetailsPanel;
    [SerializeField] private TMP_Text ItemName;
    [SerializeField] private TMP_Text ItemDescription;
    [SerializeField] private Image ItemIcon;

    [Space(8)]
    [SerializeField] private Transform ItemStatsGrid;
    [SerializeField] private GameObject ItemStatsPrefab;

    [Space(8)]
    [SerializeField] private Button UseButton;
    [SerializeField] private Button DropButton;

    [Space(8), Header("Item Popup Part")]
    [SerializeField] private GameObject DropPopup;
    [SerializeField] private Slider itemCountSlider;
    [SerializeField] private TMP_Text itemCountText;
    [SerializeField] private Button DropConfirmButton;
    [SerializeField] private Button DropCancelButton;

    private ItemEntry chosenEntry;
    private int chosenItemCount;

    private List<GameObject> slotInstances = new List<GameObject>();

    private void Awake()
    {
        this.gameObject.SetActive(false);
        DropPopup.SetActive(false);

        DropButton.onClick.RemoveAllListeners();
        DropButton.onClick.AddListener(() =>
        {
            DropPopup.SetActive(false);
            DropButton.interactable = false;
            TooglePopup();
        });

        UseButton.onClick.RemoveAllListeners();
        UseButton.onClick.AddListener(() => UseItem(chosenEntry.item));

        itemCountSlider.onValueChanged.AddListener(UpdateCount);
        DropConfirmButton.onClick.AddListener(() =>
        {
            DropConfirmButton.interactable = false;
            DropPopup.SetActive(false);
            DropItem();
        });
    }

    private void OnEnable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged += RefreshUI;
        RefreshUI();
        ChooseFirstSlot();
        
    }

    private void OnDisable()
    {
        GameManager.Instance.InventoryManager.OnInventoryChanged -= RefreshUI;
    }

    public void RefreshUI()
    {
        ChangeCoinsText(GameManager.Instance.ScoreSystem.TotalCoins);

        List<ItemEntry> entries = GameManager.Instance.InventoryManager.GetAllEntries();
        int requiredSlots = Mathf.Max(MinSlots, entries.Count);

        // Create more slot instances if not enough
        while (slotInstances.Count < requiredSlots)
        {
            GameObject slotInstance = Instantiate(slotPrefab, gridParent);
            slotInstance.SetActive(false);
            slotInstances.Add(slotInstance);
        }

        // Setup and enable required slots
        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slotObject = slotInstances[i];
            slotObject.SetActive(true);
            InventorySlot slot = slotObject.GetComponentInChildren<InventorySlot>();

            if (i < entries.Count)
            {
                slot.Setup(entries[i], ShowItemDetails);
            }
            else
            {
                slot.Setup(null, ShowItemDetails);
            }
        }

        // Disable any extra slots not needed
        for (int i = requiredSlots; i < slotInstances.Count; i++)
        {
            slotInstances[i].SetActive(false);
        }
    }


    private void ShowItemDetails(ItemEntry entry)
    {
        if (entry == null || entry.item == null)
        {
            DetailsPanel.SetActive(false);
            return;
        }
        else DetailsPanel.SetActive(true);


        chosenEntry = entry;

        ItemName.text = entry.item.itemName + (entry.quantity > 1 ? "(" + entry.quantity + ")" : "");
        ItemDescription.text = entry.item.description != "" ? entry.item.description : "-";
        ItemIcon.sprite = entry.item.icon;

        ShowItemStats(entry);
    }

    private void ChooseFirstSlot()
    {
        InventorySlot slot = slotInstances[0].GetComponentInChildren<InventorySlot>();

        if (slot.GetEntry() == null || slot.GetEntry().item == null)
        {
            DetailsPanel.SetActive(false);
            return;
        }

        DetailsPanel.SetActive(true);
        ShowItemDetails(slot.GetEntry());
    }

    //private void ClearUI()
    //{
    //    foreach (GameObject slot in slotInstances)
    //    {
    //        Destroy(slot);
    //    }
    //    slotInstances.Clear();
    //}

    private void DropItem()
    {
        if (chosenEntry == null)
        {
            Debug.LogError("[Inventory] Entry with " + chosenEntry.item.itemName + "(" + chosenEntry.item.itemID + ") is NULL");
            return;
        }

        GameManager.Instance.InventoryManager.RemoveItem(chosenEntry.item, true, chosenItemCount);
        
        DropConfirmButton.interactable = true;
        DropButton.interactable = true;

        RefreshUI();
        ChooseFirstSlot();
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                Debug.Log($"[Inventory] Used Weapon {item.itemName}");
                break;
            case ItemType.Armor:
                Debug.Log($"[Inventory] Weared {item.itemName}");
                break;
            case ItemType.Potion:
                Debug.Log($"[Inventory] {item.itemName} was used");
                break;
        }
    }

    public void TooglePopup()
    {
        if (chosenEntry.quantity <= 1)
        {
            chosenItemCount = 1;
            DropItem();
            return;
        }

        itemCountSlider.minValue = 1;
        itemCountSlider.maxValue = chosenEntry.quantity;
        itemCountSlider.value = 1;
        itemCountText.text = "1";
        chosenItemCount = 1;
        DropPopup.SetActive(true);
        
    }

    private void UpdateCount(float value)
    {
        chosenItemCount = Mathf.RoundToInt(value);
        itemCountText.text = chosenItemCount.ToString();
    }

    private void ChangeCoinsText(float number)
    {
        CoinsText.text = ((int)number).ToString();
    }

    private void ShowItemStats(ItemEntry entry)
    {
        foreach (Transform child in ItemStatsGrid)
        {
            Destroy(child.gameObject);
        }

        Item item = entry.item;

        switch (item.itemType)
        {
            case ItemType.Weapon:
                {
                    WeaponInstance weapon = entry.weapon;

                    CreateStatRow("<color=#FF9349>Damage</color>", weapon.TotalDamage.ToString("F0") + "HP");
                    CreateStatRow("<color=#FF4628>Crit. HP/%</color>", weapon.TotalCritDamage + "HP/" + (weapon.TotalCritChance * 100).ToString("F0"));
                    CreateStatRow("<color=#51C328>Reload Time</color>", weapon.TotalReloadTime.ToString("F1"));

                    break;
                }

            case ItemType.Potion:
                {
                    PotionItemData potion = item as PotionItemData;
                    string colorStart = "";
                    string colorEnd = "</color>";

                    switch (potion.potionType)
                    {
                        case PotionType.Health:
                            colorStart = "<color=green>";
                            break;

                        case PotionType.Mana:
                            colorStart = "<color=blue>";
                            break;

                        case PotionType.Speed:
                            colorStart = "<color=yellow>";
                            break;

                        case PotionType.Damage:
                            colorStart = "<color=red>";
                            break;

                        default:
                            colorEnd = "";
                            break;
                    }

                    CreateStatRow("Type", colorStart + potion.potionType.ToString() + colorEnd);
                    CreateStatRow("Instant", potion.isInstant ? "Yes" : "No");
                    CreateStatRow("Increase Unit", potion.potionIncreaseAmount + " PU / " + 
                        (!potion.isInstant ? potion.potionIncreaseInterval : "1") + " s");

                    if (!potion.isInstant)
                    {
                        CreateStatRow("Action Type", potion.potionActionType.ToString());
                        CreateStatRow("Duration", potion.potionDuration + " s");
                    }

                    break;
                }

            default: break;
        }
    }

    private void CreateStatRow(string label, string value)
    {
        GameObject statsObj = Instantiate(ItemStatsPrefab, ItemStatsGrid);
        StatsScript stat = statsObj.GetComponentInChildren<StatsScript>();
        stat.Setup(label, value);
    }

}
