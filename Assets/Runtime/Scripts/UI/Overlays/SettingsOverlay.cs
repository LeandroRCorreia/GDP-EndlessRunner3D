using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOverlay : UiOverlay
{

    [Header("Ui Elements")]
    [SerializeField] private Button exitSettingsButton;
    [SerializeField] private Button deleteDataButton;
    [SerializeField] private TextMeshProUGUI textDeleteButton;
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("External Dependencies")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private UiAudioController uiAudioController;
    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private AudioMixerController audioMixerController;


    private void Awake()
    {
        EventButtons();
    }

    private void EventButtons()
    {
        exitSettingsButton.onClick.AddListener(OnExitSettingsButton);
        exitSettingsButton.onClick.AddListener(uiAudioController.PlayButtonPressSFX);

        deleteDataButton.onClick.AddListener(OnDeleteDataButton);
        deleteDataButton.onClick.AddListener(uiAudioController.PlayButtonPressSFX);

        mainVolumeSlider.onValueChanged.AddListener(OnMainVolumeChange);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChange);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChange);
    }

    private void OnEnable()
    {
        gameSaver.LoadGame();
        StartOverlay();
    }

    private void StartOverlay()
    {
        mainVolumeSlider.value = AudioMixerController.FromDBToPercent(audioMixerController.MainVolumeDB);
        musicSlider.value = AudioMixerController.FromDBToPercent(audioMixerController.MusicVolumeDB);
        sfxSlider.value = AudioMixerController.FromDBToPercent(audioMixerController.SfxVolumeDB);
        textDeleteButton.text = "DELETE ALL DATA";
    }

    private void OnDisable()
    {
        Close();
    }

    private void OnMainVolumeChange(float currentPercentChange)
    {
        audioMixerController.MainVolumeDB = AudioMixerController.FromPercentToDB(currentPercentChange);
    }

    private void OnMusicVolumeChange(float currentPercentChange)
    {
        audioMixerController.MusicVolumeDB = AudioMixerController.FromPercentToDB(currentPercentChange);
    }

    private void OnSfxVolumeChange(float currentPercentChange)
    {
        audioMixerController.SfxVolumeDB = AudioMixerController.FromPercentToDB(currentPercentChange);
    }

    private void OnExitSettingsButton()
    {
        gameMode.OnWaitStartGame();
    }

    private void Close()
    {
        var audioPreferencesData = new AudioPreferencesData()
        {
            mainVolumeDb = AudioMixerController.FromPercentToDB(mainVolumeSlider.value),
            musicVolumeDb = AudioMixerController.FromPercentToDB(musicSlider.value),
            sfxVolumeDb = AudioMixerController.FromPercentToDB(sfxSlider.value)
        };

        gameSaver.SaveAudioPreferences(audioPreferencesData);
        gameMode.OnWaitStartGame();
    }

    private void OnDeleteDataButton()
    {   
        gameSaver.DeleteAllData();
        deleteDataButton.interactable = false;
        textDeleteButton.text = "DELETED";

    }

}
