using UnityEngine;

public class PortalScript : MonoBehaviour
{

    [SerializeField] private GameObject _button;




    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            _button.SetActive(true);
        }

    }



    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            _button.SetActive(false);
        }

    }

}
