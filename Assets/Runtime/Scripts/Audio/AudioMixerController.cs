using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    [Header("External Dependencies")]

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameSaver gameSaver;


    private const float maxVolumeDB = 0f;
    private const float minVolumeDB = -60f;

    private const string MAIN_VOLUME_KEY = "MainVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";

    public float MainVolumeDB
    {
        get => GetAudioMixerVolume(MAIN_VOLUME_KEY);
        set => SetAudioMixerVolume(MAIN_VOLUME_KEY, value);
    }

    public float MusicVolumeDB
    {
        get => GetAudioMixerVolume(MUSIC_VOLUME_KEY);
        set => SetAudioMixerVolume(MUSIC_VOLUME_KEY, value);

    }

    public float SfxVolumeDB
    {
        get => GetAudioMixerVolume(SFX_VOLUME_KEY);
        set => SetAudioMixerVolume(SFX_VOLUME_KEY, value);
    }

    private void Start()
    {
        gameSaver.LoadGame();
        LoadSettingsData();
    }

    public void LoadSettingsData()
    {
        MainVolumeDB = gameSaver.CurrentAudioPreferencesData.mainVolumeDb;
        MusicVolumeDB = gameSaver.CurrentAudioPreferencesData.musicVolumeDb;
        SfxVolumeDB = gameSaver.CurrentAudioPreferencesData.sfxVolumeDb;

    }

    private float GetAudioMixerVolume(string key)
    {
        if(audioMixer.GetFloat(key, out float volume))
        {
            return volume;
        }
        return 0;
    }

    private void SetAudioMixerVolume(string key, float volume)
    {
        audioMixer.SetFloat(key, volume);
    }

    public static float FromPercentToDB(float percent)
    {
        return Mathf.Lerp(minVolumeDB, maxVolumeDB, percent);
    }

    public static float FromDBToPercent(float volumeDB)
    {
        return Mathf.InverseLerp(minVolumeDB, maxVolumeDB, volumeDB);
    }

}
