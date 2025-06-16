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
