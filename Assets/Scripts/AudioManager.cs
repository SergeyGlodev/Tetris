using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public float MasterVolumePercent {get; private set; }
    public float SfxVolumePercent {get; private set; }
    public float MusicVolumePercent {get; private set; }
    
    int activeMusicSourceIndex;
    AudioSource sfx2DSource;
    AudioSource[] musicSources;
    SoundLibrary library;
    AudioClip currentAudioClip;
    bool musicPlayFirstTime = true;

    public AudioClip CurrentAudioClip => currentAudioClip;
    
    public void SetVolume(float volumPercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                MasterVolumePercent = volumPercent;
                break;
            case AudioChannel.Sfx:
                SfxVolumePercent = volumPercent;
                break;
            case AudioChannel.Music:
                MusicVolumePercent = volumPercent;
                break;
        }

        musicSources[activeMusicSourceIndex].volume = MusicVolumePercent * MasterVolumePercent;

        PlayerPrefs.SetFloat("Master volume", MasterVolumePercent);
        PlayerPrefs.SetFloat("Sfx volume", SfxVolumePercent);
        PlayerPrefs.SetFloat("Music volume", MusicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), SfxVolumePercent * MasterVolumePercent);
    }

    public void PlayMusic(AudioClip clip, float fade)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();
        currentAudioClip = musicSources[activeMusicSourceIndex].clip;

        if (musicPlayFirstTime)
        {
            musicSources[activeMusicSourceIndex].DOFade(MusicVolumePercent * MasterVolumePercent,  0).SetEase(Ease.Linear);
            musicSources[1 - activeMusicSourceIndex].DOFade(0, 0).SetEase(Ease.Linear);
            musicPlayFirstTime = false;
        }
        else
        {
            musicSources[activeMusicSourceIndex].DOFade(MusicVolumePercent * MasterVolumePercent,  fade).SetEase(Ease.Linear);
            musicSources[1 - activeMusicSourceIndex].DOFade(0, fade).SetEase(Ease.Linear);
        }
    }

    void Awake() 
    {
        library = GetComponent<SoundLibrary>();
        musicSources = new AudioSource[2];

        for (int i = 0; i < 2; i++)
        {
            GameObject newMusicSource = new GameObject("Music source " + (i+1));
            musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
            musicSources[i].loop = true;
        }

        GameObject newSfx2DSource = new GameObject("2D Sound Sfx Source");
        sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
        newSfx2DSource.transform.parent =  transform;

        MasterVolumePercent = PlayerPrefs.GetFloat("Master volume", defaultValue: 1);
        SfxVolumePercent = PlayerPrefs.GetFloat("Sfx volume", defaultValue: 1);
        MusicVolumePercent = PlayerPrefs.GetFloat("Music volume", defaultValue: 1);
    }
}
