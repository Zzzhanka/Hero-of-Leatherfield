using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryPanel;

    public void ToggleInventoryPanel()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}