using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyUI : MonoBehaviour
{
    [Space(8), Header("Potions List Part")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject componentPrefab;
    [SerializeField] private Transform potionListGridParent;
    [SerializeField] private Transform componentsListgridParent;

    [Space(5), Header("Receipt Details Part")]
    [SerializeField] private GameObject DetailsPanel;
    [SerializeField] private TMP_Text PotionName;
    [SerializeField] private Image PotionIcon;

    [Space(5)]
    [SerializeField] private Transform ItemStatsGrid;
    [SerializeField] private GameObject ItemStatsPrefab;

    [Space(5)]
    [SerializeField] private Button CreateButton;

    

    private List<GameObject> slotInstances = new List<GameObject>();
    private List<GameObject> componentInstances = new List<GameObject>();
    private Receipt chosenReceipt;
    private PotionSlot chosenSlot;

    private void Awake()
    {
        this.gameObject.SetActive(false);

        CreateButton.onClick.RemoveAllListeners();
        CreateButton.onClick.AddListener(MakeDeal);
    }

    private void OnEnable()
    {
        RefreshUI();
        ChooseFirstSlot();
    }

    public void RefreshUI()
    {
        ClearPotionList();
        ClearComponentsList();

        List<Receipt> receipts = GameManager.Instance.AlchemyManager.ReceiptList;

        for (int i = 0; i < receipts.Count; i++)
        {
            GameObject slotInstance;

            if (i < slotInstances.Count)
            {
                slotInstance = slotInstances[i];
                slotInstance.SetActive(true);
            }
            else
            {
                slotInstance = Instantiate(slotPrefab, potionListGridParent);
                slotInstances.Add(slotInstance);
            }

            PotionSlot slotScript = slotInstance.GetComponentInChildren<PotionSlot>();
            slotScript.Setup(receipts[i], ShowPotionDetails);
        }

        // Deactivate extra slots
        for (int i = receipts.Count; i < slotInstances.Count; i++)
        {
            slotInstances[i].SetActive(false);
        }
    }

    private void ShowPotionDetails(Receipt receipt, PotionSlot slot)
    {
        ClearComponentsList();

        if (receipt == null) return;

        if (chosenSlot != null)
        {
            chosenSlot.transform.parent.GetComponent<Button>().interactable = true;
        }

        chosenSlot = slot;
        chosenSlot.transform.parent.GetComponent<Button>().interactable = false;

        chosenReceipt = receipt;

        DetailsPanel.SetActive(true);
        PotionName.text = receipt.receiptItemRef.itemName;
        PotionIcon.sprite = receipt.receiptItemRef.icon;

        ShowPotionStats(receipt.receiptItemRef);

        ShowPotionComponents(receipt);
    }

    private void ShowPotionComponents(Receipt receipt)
    {
        if (receipt == null) return;

        List<Component> components = receipt.GetComponents();

        for (int i = 0; i < components.Count; i++)
        {
            GameObject componentInstance;

            if (i < componentInstances.Count)
            {
                componentInstance = componentInstances[i];
                componentInstance.SetActive(true);
            }
            else
            {
                componentInstance = Instantiate(componentPrefab, componentsListgridParent);
                componentInstances.Add(componentInstance);
            }

            PotionComponentSlot componentScript = componentInstance.GetComponentInChildren<PotionComponentSlot>();
            componentScript.Setup(components[i]);
        }

        // Deactivate extra component slots
        for (int i = components.Count; i < componentInstances.Count; i++)
        {
            componentInstances[i].SetActive(false);
        }

        CheckAvailability();
    }

    private void ShowPotionStats(Item item)
    {
        foreach (Transform child in ItemStatsGrid)
        {
            Destroy(child.gameObject);
        }

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
    }

    private void ChooseFirstSlot(int chosenSlotPosition = 0)
    {
        if (slotInstances.Count == 0)
        {
            chosenReceipt = null;
            DetailsPanel.SetActive(false);
            return;
        }

        PotionSlot slot = slotInstances[chosenSlotPosition].GetComponentInChildren<PotionSlot>();
        if (slot.GetReceipt() == null)
        {
            chosenReceipt = null;
            DetailsPanel.SetActive(false);
            return;
        }

        DetailsPanel.SetActive(true);
        ShowPotionDetails(slot.GetReceipt(), slot);
    }

    private void ClearPotionList()
    {
        foreach (GameObject slot in slotInstances)
            slot.SetActive(false);
    }

    private void ClearComponentsList()
    {
        foreach (GameObject slot in componentInstances)
            slot.SetActive(false);
    }

    private void CreateStatRow(string label, string value)
    {
        GameObject statsObj = Instantiate(ItemStatsPrefab, ItemStatsGrid);
        StatsScript stat = statsObj.GetComponentInChildren<StatsScript>();
        stat.Setup(label, value);
    }

    private void CheckAvailability()
    {
        if (chosenReceipt != null)
        {
            bool isAvailable = GameManager.Instance.AlchemyManager.CheckAvailability(chosenReceipt);
            CreateButton.interactable = isAvailable;
        }
    }

    private void MakeDeal()
    {
        GameManager.Instance.AlchemyManager.MakeDeal(chosenReceipt);
        ClearComponentsList();
        ShowPotionDetails(chosenReceipt, chosenSlot);
    }
}
