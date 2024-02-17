using UnityEngine;
using UnityEngine.UI;

public class PauseOverlay : UiOverlay
{
    [Header("Ui Elements")]

    [SerializeField] private Button resumeButton;


    [Header("External Dependencies")]

    [SerializeField] private GameMode gameMode;
    [SerializeField] private UiAudioController uiAudioController;

    private void Start()
    {
        resumeButton.onClick.AddListener(gameMode.OnResumeGame);
        resumeButton.onClick.AddListener(uiAudioController.PlayButtonPressSFX);
    }

    private void OnEnable()
    {
        gameMode.OnPauseGame();
    }

}
