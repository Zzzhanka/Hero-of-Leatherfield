using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] private WeaponInstance weaponInstance;
    [SerializeField] private Item coinItemRef;
    [SerializeField] private Item item;
    [SerializeField] private int quantity = 1;
    private SpriteRenderer iconRenderer;

    private bool canBePickedUp = true;
    private bool playerWasInside = false;

    [SerializeField] private float DelayPickup = 1.5f;

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
        Initialize();

        StartCoroutine(DelayedEnablePickup(DelayPickup));
    }

    private void Initialize()
    {
        if(item == null)
            Destroy(gameObject);

        if(item is WeaponItemData weaponData && weaponInstance.BaseItem == null)
        {
            weaponInstance = new WeaponInstance(weaponData);
        }
    }

    public void SetItem(Item item, int quantity, bool isDropped, WeaponInstance weapon)
    {
        this.item = item;
        this.quantity = quantity;

        if (item.itemType == ItemType.Weapon && weapon != null)
            this.weaponInstance = weapon;

        else this.weaponInstance = null;

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
            StartCoroutine(DelayedEnablePickup(DelayPickup));
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

        if(item == coinItemRef)
        {
            GameManager.Instance.ScoreSystem.AddCoins(quantity);
            Destroy(gameObject);
            return;
        }

        if (GameManager.Instance.InventoryManager.AddItem(item, quantity, weaponInstance))
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
