using UnityEngine;

public class PortalScript : MonoBehaviour
{

    public GameObject button;
    [SerializeField] bool isPlayerInZone = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(true);
            Debug.Log("dgdfg");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            button.SetActive(false);
            Debug.Log("dgdfg");
        }
    }

}
