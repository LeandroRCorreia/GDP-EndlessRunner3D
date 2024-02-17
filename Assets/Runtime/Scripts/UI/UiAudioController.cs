using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UiAudioController : MonoBehaviour
{

    [SerializeField] private AudioClip buttonPressSFX;
    [SerializeField] private AudioClip countDownSFX;
    [SerializeField] private AudioClip endCountDownSFX;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayButtonPressSFX()
    {
        AudioSource.PlayAudioCue(buttonPressSFX);
    }

    public void PlayCountDownSFX()
    {
        AudioSource.PlayAudioCue(countDownSFX);

    }

    public void PlayEndCountDownSFX()
    {
        AudioSource.PlayAudioCue(endCountDownSFX);
    }

}
