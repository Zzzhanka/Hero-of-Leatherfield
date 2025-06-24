using UnityEngine;

public class BackButton_AboutUs : MonoBehaviour
{
    [SerializeField] private GameObject aboutUsPanel; 

    public void CloseAboutUsPanel()
    {
        aboutUsPanel.SetActive(false); 
    }
}