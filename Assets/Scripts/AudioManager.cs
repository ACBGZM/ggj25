using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource bgmAudioSource;
    private AudioSource sfxAudioSource;

    public AudioClip bgmClip;

    public AudioClip[] soundEffects;
    
    private const int MaxSfxSources = 5;
    private AudioSource[] sfxSources;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        bgmAudioSource.loop = true;

        sfxAudioSource = gameObject.AddComponent<AudioSource>();

        sfxSources = new AudioSource[MaxSfxSources];
        for (int i = 0; i < MaxSfxSources; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].spatialBlend = 0; // 2D
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmAudioSource.clip != clip)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        for (int i = 0; i < MaxSfxSources; i++)
        {
            if (!sfxSources[i].isPlaying)
            {
                sfxSources[i].PlayOneShot(clip);
                return;
            }
        }

        sfxSources[0].Stop();
        sfxSources[0].PlayOneShot(clip);
    }

    public void SetBGMVolume(float volume)
    {
        bgmAudioSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var source in sfxSources)
        {
            source.volume = volume;
        }
    }
}