using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{

    [Header("Fuentes de audio")]
    AudioSource audioMain, audioLevel1, audioLevel2,
        audioLevel3, audioLevel4, audioLevel5;

    [Header("Clips de audio")]
    public AudioClip clipMain, clipLevel1, clipLevel2,
        clipLevel3, clipLevel4, clipLevel5;

    [Header("Master")]
    public AudioMixerGroup master;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        audioMain = AddClip(clipMain, true, true);
        audioLevel1 = AddClip(clipLevel1, true, true);
        audioLevel2 = AddClip(clipLevel2, true, true);
        audioLevel3 = AddClip(clipLevel3, true, true);
        audioLevel4 = AddClip(clipLevel4, true, true);
        audioLevel5 = AddClip(clipLevel5, true, true);

        PlayMusic(audioMain);
    }

    public void PlayMusic(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
            return;
        audioSource.Play();
    }

    public void StopMusic(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void StopAll()
    {
        audioMain.Stop();
        audioLevel1.Stop();
        audioLevel2.Stop();
        audioLevel3.Stop();
        audioLevel4.Stop();
        audioLevel5.Stop();
    }

    public void StopLevelMusic()
    {
        audioLevel1.Stop();
        audioLevel2.Stop();
        audioLevel3.Stop();
        audioLevel4.Stop();
        audioLevel5.Stop();
    }

    AudioSource AddClip(AudioClip clip, bool isLooping, bool isOnAwake)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();

        newAudio.clip = clip;
        newAudio.outputAudioMixerGroup = master;
        newAudio.loop = isLooping;
        newAudio.playOnAwake = isOnAwake;

        return newAudio;
    }

    public AudioSource GetAudioSource(int audioIndex)
    {
        switch (audioIndex)
        {
            case 0:
                return audioMain;
            case 1:
                return audioLevel1;
            case 2:
                return audioLevel2;
            case 3:
                return audioLevel3;
            case 4:
                return audioLevel4;
            case 5:
                return audioLevel5;
            default:
                return audioMain;
        }
    }

}
