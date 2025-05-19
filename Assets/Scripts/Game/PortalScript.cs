using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject button;
    [SerializeField] bool isPlayerInZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            button.SetActive(true);
            Debug.Log("dgdfg");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            button.SetActive(false);
            Debug.Log("dgdfg");
        }
    }
}
