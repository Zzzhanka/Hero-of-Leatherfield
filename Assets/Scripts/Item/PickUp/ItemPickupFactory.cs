using UnityEngine;

public class ItemPickupFactory : MonoBehaviour
{
    [Range(0f, 2f), SerializeField] private float maxRangeDrop = 0.2f;
    [SerializeField] private GameObject pickupPrefab;

    public void CreatePickup(Item droppedItem, int amount, WeaponInstance weapon = null)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 dropPos = GetRandomDropPosition(player.transform.position);
        Quaternion playerRotation = Quaternion.identity;

        GameObject dropped = Instantiate(pickupPrefab, dropPos, playerRotation, null);

        ItemPickup pickup = dropped.GetComponent<ItemPickup>();
        pickup.SetItem(droppedItem, amount, true, weapon);
    }

    public void CreatePickup(Transform enemyPos, Item droppedItem, int amount, WeaponInstance weapon = null)
    {
        GameObject dropped = Instantiate(pickupPrefab, enemyPos.position, enemyPos.rotation, null);

        ItemPickup pickup = dropped.GetComponent<ItemPickup>();
        pickup.SetItem(droppedItem, amount, true, weapon);
    }

    private Vector3 GetRandomDropPosition(Vector3 playerPosition)
    {
        Vector2 randomOffset = Random.insideUnitCircle * maxRangeDrop;
        Vector3 dropPosition = playerPosition + new Vector3(randomOffset.x, randomOffset.y, 0);
        return dropPosition;
    }
}
