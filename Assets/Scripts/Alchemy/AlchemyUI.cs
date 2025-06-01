using System.Collections.Generic;
using UnityEngine;

public class AlchemyUI : MonoBehaviour
{

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform gridParent;

    private List<GameObject> slotInstances = new List<GameObject>();

    public void RefreshUI()
    {
        ClearUI();


        int requiredSlots = 20;

        for (int i = 0; i < requiredSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, gridParent);
            
            slotInstances.Add(slotInstance);
        }
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
