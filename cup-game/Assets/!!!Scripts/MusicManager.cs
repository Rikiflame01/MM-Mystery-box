using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
    }

    private void Start()
    {
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
