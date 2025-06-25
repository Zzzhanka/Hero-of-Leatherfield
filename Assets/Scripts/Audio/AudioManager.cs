using UnityEngine;

public class AudioManager : MonoBehaviour
{ 

    [Tooltip("Tag to filter audio sources (optional). Leave empty to affect all.")]
    public string audioTag = ""; // Set to "AudioStatic" if you want to filter

    /// <summary>
    /// Pauses all sounds globally using AudioListener.
    /// </summary>
    public void PauseAllAudio()
    {
        AudioListener.pause = true;
    }

    /// <summary>
    /// Resumes all paused sounds globally using AudioListener.
    /// </summary>
    public void ResumeAllAudio()
    {
        AudioListener.pause = false;
    }

    /// <summary>
    /// Optional: Manually pause specific tagged audio sources (if needed).
    /// </summary>
    public void PauseTaggedAudio()
    {
        if (string.IsNullOrEmpty(audioTag)) return;

        var objects = GameObject.FindGameObjectsWithTag(audioTag);
        foreach (var obj in objects)
        {
            var audio = obj.GetComponent<AudioSource>();
            if (audio != null && audio.isPlaying)
                audio.Pause();
        }
    }

    /// <summary>
    /// Optional: Resume specific tagged audio sources.
    /// </summary>
    public void ResumeTaggedAudio()
    {
        if (string.IsNullOrEmpty(audioTag)) return;

        var objects = GameObject.FindGameObjectsWithTag(audioTag);
        foreach (var obj in objects)
        {
            var audio = obj.GetComponent<AudioSource>();
            if (audio != null)
                audio.UnPause();
        }
    }
}
