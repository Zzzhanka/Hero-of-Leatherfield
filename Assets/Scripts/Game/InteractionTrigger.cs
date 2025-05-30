using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private IInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<IInteractable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactable?.OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactable?.OnPlayerExit();
        }
    }
}
