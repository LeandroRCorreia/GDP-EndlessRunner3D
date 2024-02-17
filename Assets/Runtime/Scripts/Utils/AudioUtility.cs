using System;
using UnityEngine;

public static class AudioUtility
{

    public static void PlayAudioCue(this AudioSource audioSource, AudioClip clipSFX)
    {
        if(audioSource.outputAudioMixerGroup == null) Debug.LogError("AudioMixerGroup must be assigned");

        audioSource.clip = clipSFX;
        audioSource.loop = false;
        audioSource.Play();

    }

    public static void PlayMusic(this AudioSource audioSource, AudioClip clipMusic)
    {
        if(audioSource.outputAudioMixerGroup == null) Debug.LogError("AudioMixerGroup must be assigned");

        audioSource.clip = clipMusic;
        audioSource.loop = true;
        audioSource.Play();


    }

    public static void StopSFX(this AudioSource audioSource)
    {
        audioSource.Stop();
    }

}
