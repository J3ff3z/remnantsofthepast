using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip loop;
    [SerializeField] AudioClip end;
    
    AudioSource audioSource;
    AudioClip nextClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = intro;
        nextClip = loop;
        audioSource.Play();
    }

    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextClip();
            audioSource.Play();
        }
    }

    void PlayNextClip()
    {
        audioSource.clip = nextClip;
    }

    void End()
    {
        nextClip = end;
    }
}
