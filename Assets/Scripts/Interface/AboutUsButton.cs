using UnityEngine;

public class AboutUsButton : MonoBehaviour
{
    [SerializeField] private GameObject aboutUsPanel;

    public void OpenAboutUs()
    {
        aboutUsPanel.SetActive(true);
    }
}