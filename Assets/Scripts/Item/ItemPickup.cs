using NUnit.Framework.Interfaces;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;
    private SpriteRenderer iconRenderer;

    private void OnValidate()
    {
        iconRenderer = GetComponent<SpriteRenderer>();

        if (item != null && iconRenderer != null)
        {
            iconRenderer.sprite = item.icon;
            gameObject.name = item.itemName + " Pickup";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Destroys pickup if function says that item added to inventory
            if(GameManager.Instance.InventoryManager.AddItem(item))
                Destroy(gameObject);
        }
    }
}