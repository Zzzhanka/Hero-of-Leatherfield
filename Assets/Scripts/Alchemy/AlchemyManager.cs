using UnityEngine;
using System.Collections.Generic;

public class AlchemyManager : MonoBehaviour
{
    [SerializeField] private List<Receipt> receiptLists;

    public List<Receipt> ReceiptList => receiptLists;

    public bool CheckAvailability(Receipt receipt)
    {
        return false;
    }

    public void MakeDeal()
    {

    }
}
