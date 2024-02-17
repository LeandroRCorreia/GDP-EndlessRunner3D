using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitStartGameOverlay : UiOverlay
{

    [Header("UI Elements")]
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button runButton;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private TextMeshProUGUI highestScoreTxt;
    [SerializeField] private TextMeshProUGUI lastScoreTxt;
    [SerializeField] private TextMeshProUGUI totalCherriesCollected;
    [SerializeField] private TextMeshProUGUI totalPeanutCollected;


    [Header("External Dependencies")]
    [SerializeField] private UiAudioController uiAudioController;
    [SerializeField] private GameMode gameMode;
    [SerializeField] GameSaver gameSaver;

    private void Start()
    {
        settingsMenuButton.onClick.AddListener(uiAudioController.PlayButtonPressSFX);
        settingsMenuButton.onClick.AddListener(OnSettingsMenuButton);

        exitGameButton.onClick.AddListener(OnExitGameButton);
        exitGameButton.onClick.AddListener(uiAudioController.PlayButtonPressSFX);

        runButton.onClick.AddListener(OnRunButton);
        runButton.onClick.AddListener(uiAudioController.PlayButtonPressSFX);

    }

    private void OnEnable()
    {
        gameSaver.LoadGame();
        UpdateUi();
    }

    private void UpdateUi()
    {
        highestScoreTxt.text = $"HighestScore\n{gameSaver.CurrentScoreData.bestScore}";
        lastScoreTxt.text = $"LastScore\n{gameSaver.CurrentScoreData.lastScore}";
        totalCherriesCollected.text = gameSaver.CurrentScoreData.totalCherriesCollected.ToString();
        totalPeanutCollected.text = gameSaver.CurrentScoreData.totalPeanutCollected.ToString();
    }

    private void OnSettingsMenuButton()
    {
        gameMode.OnSettingsMenu();
    }

    private void OnExitGameButton()
    {   
        gameMode.OnCloseGame();
    }

    private void OnRunButton()
    {
        gameMode.OnPreparingToStartRun();

    }

}
