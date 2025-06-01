using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    [SerializeField] private GameObject interactionButton;

    private IInteractable currentInteractable;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        interactionButton.SetActive(false);
        interactionButton.GetComponent<Button>().onClick.AddListener(Interact);
    }

    public void ShowButton(IInteractable interactable)
    {
        currentInteractable = interactable;
        interactionButton.SetActive(true);
    }

    public void HideButton(IInteractable interactable)
    {
        if (currentInteractable == interactable)
        {
            currentInteractable = null;
            interactionButton.SetActive(false);
        }
    }

   public void Interact()
    {
        currentInteractable?.Interact();
        Debug.Log("Кнопка нажата. Выполняем действие...");
    }
}
