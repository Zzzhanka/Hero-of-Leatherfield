using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject componentPrefab;
    [SerializeField] private Transform potionListGridParent;
    [SerializeField] private Transform componentsListgridParent;

    private List<GameObject> slotInstances = new List<GameObject>();
    private List<GameObject> componentInstances = new List<GameObject>();
    private Receipt chosenReceipt;

    [Space(5), Header("Receipt Details Part")]
    [SerializeField] private GameObject DetailsPanel;
    [SerializeField] private TMP_Text PotionName;
    [SerializeField] private Image PotionIcon;
    [SerializeField] private TMP_Text PotionStats;

    [SerializeField] private Button CreateButton;

    private PotionSlot chosenSlot;

    private void Awake()
    {
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
            GameObject slotInstance = Instantiate(slotPrefab, potionListGridParent);
            PotionSlot slotScript = slotInstance.GetComponentInChildren<PotionSlot>();
            slotScript.Setup(receipts[i], ShowPotionDetails);
            slotInstances.Add(slotInstance);
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
        PotionName.text = receipt.receiptName;
        PotionIcon.sprite = receipt.receiptSprite;

        ShowPotionComponents(receipt);
    }

    private void ShowPotionComponents(Receipt receipt)
    {
        if (receipt == null) return;

        List<Component> components = receipt.GetComponents();
        componentInstances.Clear();

        foreach (Component component in components) 
        {
            GameObject componentInstance = Instantiate(componentPrefab, componentsListgridParent);
            PotionComponentSlot componentScript = componentInstance.GetComponentInChildren<PotionComponentSlot>();
            componentScript.Setup(component);
            componentInstances.Add(componentInstance);
        }

        CheckAvailability();
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
        {
            Destroy(slot);
        }

        slotInstances.Clear();
    }

    private void ClearComponentsList()
    {
        foreach (GameObject slot in componentInstances)
        {
            Destroy(slot);
        }

        componentInstances.Clear();
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
