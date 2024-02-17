using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip rollSFX;
    [SerializeField] private AudioClip jumpSFX;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayRollAudioClip()
    {
        AudioSource.PlayAudioCue(rollSFX);
    }

    public void PlayJumpAudioClip()
    {
        AudioSource.PlayAudioCue(jumpSFX);

    }

}
