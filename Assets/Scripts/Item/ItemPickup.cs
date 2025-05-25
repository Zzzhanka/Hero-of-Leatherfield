using NUnit.Framework.Interfaces;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private SpriteRenderer iconRenderer;
    [SerializeField] private int quantity = 1;

    private void OnValidate()
    {
        iconRenderer = GetComponent<SpriteRenderer>();

        if (itemData != null && iconRenderer != null)
        {
            iconRenderer.sprite = itemData.icon;
            gameObject.name = "PickUp_" + itemData.itemName;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Item item = new Item
            {
                data = itemData,
                itemQuantity = quantity
            };

            GameManager.Instance.InventoryManager.AddItem(item);
            Debug.Log($"Picked up {item.data.itemName} x{item.itemQuantity}");

            Destroy(gameObject);
        }
    }
}