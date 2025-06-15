using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private int quantity = 1;
    private SpriteRenderer iconRenderer;

    private bool canBePickedUp = true;
    private bool playerWasInside = false;

    private void Awake()
    {
        iconRenderer = GetComponent<SpriteRenderer>();
        if (item != null && iconRenderer != null)
        {
            iconRenderer.sprite = item.icon;
            gameObject.name = item.itemName + " Pickup";
        }
    }

    private void Start()
    {
        
        StartCoroutine(DelayedEnablePickup(1.5f));
    }

    public void SetItem(Item item, int quantity, bool isDropped)
    {
        this.item = item;
        this.quantity = quantity;

        if (iconRenderer == null)
            iconRenderer = GetComponent<SpriteRenderer>();

        if (item != null && iconRenderer != null)
        {
            iconRenderer.sprite = item.icon;
            gameObject.name = item.itemName + " Pickup";
        }

        if (isDropped)
        {
            canBePickedUp = false;
            playerWasInside = true; // Prevent pickup until player exits and re-enters
            StartCoroutine(DelayedEnablePickup(1.5f));
        }
        else
        {
            canBePickedUp = true;
        }
    }

    private IEnumerator DelayedEnablePickup(float delay)
    {
        yield return new WaitForSeconds(delay);
        canBePickedUp = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!canBePickedUp || playerWasInside)
            return;

        if (GameManager.Instance.InventoryManager.AddItem(item, quantity))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerWasInside = false;
        }
    }
}
