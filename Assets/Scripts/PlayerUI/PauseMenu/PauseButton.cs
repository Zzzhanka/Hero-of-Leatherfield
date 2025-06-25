using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [Header("Panel to toggle (pause menu)")]
    public GameObject targetPanel;

    private bool isPaused = false;

    public void TogglePanel()
    {
        if (targetPanel == null)
            return;

        isPaused = !isPaused;

        targetPanel.SetActive(isPaused);

        gameObject.SetActive(!isPaused);
 
        // Set timescale
        Time.timeScale = isPaused ? 0 : 1;

        // Audio pause/resume via AudioManager (global)
        if (isPaused)
            GameManager.Instance?.AudioManager.PauseAllAudio();
        else
            GameManager.Instance?.AudioManager.ResumeAllAudio();
    }
}
