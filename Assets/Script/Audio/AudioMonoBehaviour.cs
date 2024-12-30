using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    public AudioClip sound;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.volume = 0.5f;
    }



    public void PlaySound()
    {
        audioSource.Play();
    }
}
