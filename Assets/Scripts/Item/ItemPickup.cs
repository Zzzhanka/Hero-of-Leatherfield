using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public SpriteRenderer iconRenderer;

    private void Start()
    {
        if (item != null && iconRenderer != null)
        {
            iconRenderer.sprite = item.itemIcon;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.InventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}