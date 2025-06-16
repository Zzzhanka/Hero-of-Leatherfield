using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject targetPanel;

    public void TogglePanel()
    {
        if (targetPanel != null)
        {
            bool temp = targetPanel.activeSelf;
            
            targetPanel.SetActive(!targetPanel.activeSelf);

            // Avoid the bug when Panel won't activate on first press
            if (targetPanel.activeSelf == temp)
                targetPanel.SetActive(!targetPanel.activeSelf);

            this.gameObject.SetActive(temp);

            Time.timeScale = temp == true ? 1 : 0;
        }
    }
}
