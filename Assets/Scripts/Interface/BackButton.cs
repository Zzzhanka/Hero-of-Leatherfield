using UnityEngine;
public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // Выключаем панель
    }
}