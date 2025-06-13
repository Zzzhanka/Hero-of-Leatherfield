using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    private AudioMixerGroup masterMixerGroup;

    private void Awake()
    {
        
    }

    private void ChangeVolume(float value)
    {

    }
}
