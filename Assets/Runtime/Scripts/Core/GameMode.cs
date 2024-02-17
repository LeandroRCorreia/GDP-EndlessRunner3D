using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameMode : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnimation;
    [SerializeField] private MusicPlayer musicPlayer;
    [SerializeField] [Range(0, 5)] private int countDownTime;
    [SerializeField] [Range(0, 5)] private float reloadGameDelay = 3;
    [SerializeField] private GameOverlay gameOverlay;
    [SerializeField] private MainHUD mainHud;
    [SerializeField] private GameSaver gameSaver;

    private bool isGameRunning = false;
    private float startGameTime = 0f;
    public int CherriesPicked {get; private set;}
    public int PeanutPicked {get; private set;}


    [Header("Score")]
    [SerializeField] [Range(1f, 2)] private float baseScoreMultiplier = 1.25f;
    private float score;
    private float extraScoreMultiplier = 1;
    private float ExtraScoreMultiplier 
    {

        get
        {
            return extraScoreMultiplier;
        }
        
        set
        {
            const float minExtraScoreValue = 1;
            float lastScoreMultiplier = extraScoreMultiplier;
            extraScoreMultiplier = Mathf.Max(minExtraScoreValue, lastScoreMultiplier, value);
        }
        
    }

    private float temporaryScoreMultiplier = 1;

    public float TemporaryScoreMultiplier 
    {
        get => temporaryScoreMultiplier;

        set
        {
            float canonizedValue = Mathf.Max(value, 1);

            temporaryScoreMultiplier = canonizedValue;
        }
        
    }
    
    public bool hasBuffScoreMultiply => TemporaryScoreMultiplier > 1;
    
    public int Score => Mathf.RoundToInt(score);

    [Header("Difficult Params ")]
    [SerializeField] private float playerInitialSpeed;
    [SerializeField][Range(1f, 50f)] private float playerMaxSpeed = 30;
    [SerializeField][Range(45, 300)] private float timeInSecondsToReachMaxSpeed;

    private void Awake()
    {
        gameSaver.LoadGame();
        OnWaitingStartRun();

    }

    void Start()
    {
        player.PlayerDeathEvent += OnPlayerDieEvent;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            UpdateDifficultyParams();
        }

    }

    #region StartRunFunctions

    private void OnWaitingStartRun()
    {
        player.enabled = false;
        musicPlayer.PlayMenuThemeMusic();

        mainHud.ShowOverlay<WaitStartGameOverlay>();
    }

    public void OnPreparingToStartRun()
    {
        StartCoroutine(StartRunCor());
    }

    private IEnumerator StartRunCor()
    {
        musicPlayer.StopCurrentMusic();

        mainHud.ShowOverlay<GameOverlay>();

        yield return gameOverlay.WaitingCountDown(countDownTime);
        yield return playerAnimation.WaitAnimationToStartGame();

        player.enabled = true;
        musicPlayer.PlayMainTrackMusic();
        isGameRunning = true;
        startGameTime = Time.time;
        player.ForwardSpeed = playerInitialSpeed;
    }

    #endregion

    public void OnPauseGame()
    {
        mainHud.ShowOverlay<PauseOverlay>();
        Time.timeScale = 0;
    }

    public void OnResumeGame()
    {
        mainHud.ShowOverlay<GameOverlay>();
        Time.timeScale = 1;
    }
    
    public void OnSettingsMenu()
    {
        mainHud.ShowOverlay<SettingsOverlay>();

    }

    public void OnWaitStartGame()
    {
        mainHud.ShowOverlay<WaitStartGameOverlay>();
    }

    public void OnCherriePicked()
    {
        CherriesPicked++;
    }

    public void OnPeanutPicked()
    {
        PeanutPicked++;
    }

    private void UpdateDifficultyParams()
    {
        float maxSpeedPercent = Mathf.Clamp01((Time.time - startGameTime) / timeInSecondsToReachMaxSpeed);
        player.ForwardSpeed = Mathf.Lerp(playerInitialSpeed, playerMaxSpeed, maxSpeedPercent);

        ExtraScoreMultiplier =  1 + maxSpeedPercent;
        score += baseScoreMultiplier * ExtraScoreMultiplier * player.ForwardSpeed * Time.deltaTime;
    }

    private void OnPlayerDieEvent()
    {
        OnGameOver();
    }
    
    private void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        SaveGame();
        musicPlayer.PlayDeathMusic();
        isGameRunning = false;
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SaveGame()
    {
        bool isBestScore = Score > gameSaver.CurrentScoreData.bestScore;

        var gameData = new ScoreData()
        {
            bestScore = isBestScore ? Score : gameSaver.CurrentScoreData.bestScore,
            lastScore = Score,
            totalCherriesCollected = gameSaver.CurrentScoreData.totalCherriesCollected + CherriesPicked,
            totalPeanutCollected = gameSaver.CurrentScoreData.totalPeanutCollected + PeanutPicked
        };
        gameSaver.SaveScoreData(gameData);
    }

    public void OnCloseGame()
    {
        #if UNITY_EDITOR
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #else
        {
            Application.Quit();
        }
        #endif
    }

    void OnDestroy()
    {
        player.PlayerDeathEvent += OnPlayerDieEvent;
    }

}
