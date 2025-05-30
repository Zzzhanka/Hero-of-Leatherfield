using UnityEngine;

public class ItemPickupFactory : MonoBehaviour
{
    [Range(0f, 20f), SerializeField] private float maxRangeDrop;
    [SerializeField] private GameObject pickupPrefab;

    public void CreatePickup(Item droppedItem, int amount)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPosition = player.transform.position;
        Quaternion playerRotation = Quaternion.identity;

        GameObject dropped = Instantiate(pickupPrefab, playerPosition, playerRotation, null);

        ItemPickup pickup = dropped.GetComponent<ItemPickup>();
        pickup.SetItem(droppedItem, amount, true);
    }
}
