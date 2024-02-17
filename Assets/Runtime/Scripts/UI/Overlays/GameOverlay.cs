using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameOverlay : UiOverlay
{

    [Header("Ui Elements")]

    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI cherriesTxt;
    [SerializeField] private TextMeshProUGUI peanutTxt;
    [SerializeField] private TextMeshProUGUI travelledDistanceTxt;

    [SerializeField] private Image multiplierImage;


    [Header("External Dependencies")]

    [SerializeField] private GameMode gameMode;
    [SerializeField] private UiAudioController audioController;
    [SerializeField] private PlayerController player;

    [Header("CountDown")]

    [SerializeField] private TextMeshProUGUI countDownTxt;

    private void Awake()
    {
        pauseButton.onClick.AddListener(audioController.PlayButtonPressSFX);
        pauseButton.onClick.AddListener(gameMode.OnPauseGame);

    }

    void LateUpdate()
    {
        scoreTxt.text = $"Score: {gameMode.Score}";   
        travelledDistanceTxt.text = $"{player.TravalledDistance}m";
        cherriesTxt.text = $"{gameMode.CherriesPicked}";
        peanutTxt.text = $"{gameMode.PeanutPicked}";
        multiplierImage.gameObject.SetActive(gameMode.hasBuffScoreMultiply);

    }

    private void OnEnable()
    {
        gameMode.OnResumeGame();

    }

    private void StartCountDown(float timeCountDown, out float currentCountDownCount)
    {
        currentCountDownCount = timeCountDown;
        countDownTxt.gameObject.SetActive(true);
    }

    public IEnumerator WaitingCountDown(int timeCountDown)
    {
        if(timeCountDown <= 0) yield break;

        //TODO: Extract the implementation details to a specif class 
        StartCountDown(timeCountDown, out float currentCountDownCount);
        while (currentCountDownCount > 0)
        {
            float lastFormatedCountDown = Mathf.Clamp(Mathf.Floor(currentCountDownCount), 0, timeCountDown);

            countDownTxt.text = (lastFormatedCountDown + 1).ToString();

            while (currentCountDownCount >= lastFormatedCountDown)
            {
                currentCountDownCount -= Time.deltaTime;
                float timeElapsed = currentCountDownCount - lastFormatedCountDown;

                countDownTxt.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timeElapsed);
                
                yield return null;
            }
            audioController.PlayCountDownSFX();   
            yield return null;
        }
        EndCountDown();

    }

    private void EndCountDown()
    {
        countDownTxt.gameObject.SetActive(false);
        audioController.PlayEndCountDownSFX();   

    }

}
