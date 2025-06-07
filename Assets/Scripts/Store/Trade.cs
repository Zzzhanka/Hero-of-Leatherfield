using UnityEngine;

[CreateAssetMenu(fileName = "New Trade Schema", menuName = "Interactable / Trade Schema", order = 23)]
public class Trade : ScriptableObject
{
    public Item tradeItem;
    public int tradeItemNumber = 1;
    public int tradeCost;
}
