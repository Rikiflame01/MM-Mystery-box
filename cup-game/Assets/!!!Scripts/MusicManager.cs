using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip; // Reference to the music clip
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure this object persists across scenes
        DontDestroyOnLoad(gameObject);

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Assign the music clip to the audio source
        audioSource.clip = musicClip;

        // Enable looping
        audioSource.loop = true;
    }

    private void Start()
    {
        // Play the music
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
