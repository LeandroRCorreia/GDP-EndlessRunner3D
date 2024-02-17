using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip mainTrackMusic;
    [SerializeField] private AudioClip menuThemeMusic;
    [SerializeField] private AudioClip gameOverMusic;

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayMainTrackMusic()
    {
        AudioSource.PlayMusic(mainTrackMusic);
    }

    public void PlayMenuThemeMusic()
    {
        AudioSource.PlayMusic(menuThemeMusic);
    }

    public void PlayDeathMusic()
    {
        AudioSource.PlayMusic(gameOverMusic);
   
    }

    public void StopCurrentMusic()
    {
        AudioSource.Stop();
    }

}
