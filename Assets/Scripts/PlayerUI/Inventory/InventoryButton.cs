using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            bool temp = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);

            // Avoid the bug when Inventory Panel won't activate on first press
            if(inventoryPanel.activeSelf == temp)
                inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}