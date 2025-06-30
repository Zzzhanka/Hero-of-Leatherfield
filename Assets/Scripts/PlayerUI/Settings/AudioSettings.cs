using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        soundSlider.onValueChanged.RemoveAllListeners();
        soundSlider.onValueChanged.AddListener((value) => ChangeVolume(value, "Sound"));
        musicSlider.onValueChanged.RemoveAllListeners();
        musicSlider.onValueChanged.AddListener((value) => ChangeVolume(value, "Music"));

        soundSlider.minValue = .0001f;
        musicSlider.minValue = .0001f;
    }

    private void ChangeVolume(float value, string MixerGroup)
    {
        audioMixer.SetFloat(MixerGroup, Mathf.Log10(value) * 20f);
    }
}
