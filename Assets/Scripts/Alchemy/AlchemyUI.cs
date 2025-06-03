using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform gridParent;

    private List<GameObject> slotInstances = new List<GameObject>();
    private Receipt chosenReceipt;

    [Space(5), Header("Receipt Details Part")]
    [SerializeField] private GameObject DetailsPanel;
    [SerializeField] private TMP_Text PotionName;
    [SerializeField] private Image PotionIcon;
    [SerializeField] private TMP_Text PotionStats;

    [SerializeField] private Button CreateButton;

    private void OnEnable()
    {
        RefreshUI();
        ChooseFirstSlot();
    }

    public void RefreshUI()
    {
        ClearUI();

        List<Receipt> receipts = GameManager.Instance.AlchemyManager.ReceiptList;

        for (int i = 0; i < receipts.Count; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, gridParent);
            PotionSlot slotScript = slotInstance.GetComponentInChildren<PotionSlot>();
            slotScript.Setup(receipts[i], ShowPotionDetails);
            slotInstances.Add(slotInstance);
        }
    }

    private void ShowPotionDetails(Receipt receipt)
    {
        if (receipt == null) return;

        DetailsPanel.SetActive(true);
        PotionName.text = receipt.receiptName;
        PotionIcon.sprite = receipt.receiptSprite;
    }

    private void ShowPotionComponents(Receipt receipt)
    {
        if (receipt == null) return;

        List<Component> components = receipt.GetComponents();

        foreach (Component component in components) 
        {
            //
        }
    }

    private void ChooseFirstSlot()
    {
        if (slotInstances.Count == 0)
        {
            DetailsPanel.SetActive(false);
            return;
        }

        PotionSlot slot = slotInstances[0].GetComponentInChildren<PotionSlot>();
        if (slot.GetReceipt() == null)
        {
            DetailsPanel.SetActive(false);
            return;
        }

        DetailsPanel.SetActive(true);
        ShowPotionDetails(slot.GetReceipt());
    }

    private void ClearUI()
    {
        foreach (GameObject slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();
    }
}
