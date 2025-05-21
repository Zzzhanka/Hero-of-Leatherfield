using UnityEngine;
using UnityEngine.UI;

public class BlacksmithScript : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _BlacksmithPanel;
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

    public void BlacksmithEntry()
    {
        _BlacksmithPanel.SetActive(true);
    }
    public void BlacksmithExit()
    {
        _BlacksmithPanel.SetActive(false);
    }
}
