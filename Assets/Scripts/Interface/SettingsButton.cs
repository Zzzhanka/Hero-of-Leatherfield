using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true); // Включаем панель
    }
}