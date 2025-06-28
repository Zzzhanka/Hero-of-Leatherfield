using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchPlayer : MonoBehaviour
{
    [Header("Pitch Settings")]
    [Tooltip("Minimum random pitch")]
    [Range(0f, 1f)] public float minPitch = 0.9f;

    [Tooltip("Maximum random pitch")]
    [Range(1f, 2f)] public float maxPitch = 1.1f;

    private AudioSource audioSource;
    private AudioClip clip;
    private float timer;

    [Header("Auto Play Settings")]
    [SerializeField] private float playMinInterval;
    [SerializeField] private float playMaxInterval;


    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        clip = audioSource.clip;
        playMinInterval = clip.length;
    }

    void Awake()
    {
        
        playMaxInterval = clip.length + 3;
        timer = Random.Range(playMinInterval, playMaxInterval);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            float pitch = Random.Range(minPitch, maxPitch);
            PlayWithRandomPitch(pitch);
            timer = Random.Range(playMinInterval, playMaxInterval) * (1.9f - pitch);
        }
    }

    private void PlayWithRandomPitch(float pitch)
    {
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
