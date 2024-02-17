using System.Collections;
using UnityEngine;

public struct PowerUpContext
{
    public PlayerController playerController;
    public GameMode gameMode;

}

public abstract class PowerUpBehaviour : MonoBehaviour
{   
    [SerializeField] protected GameObject playerParticle;

    public PowerUpContext PowerUpContext {get; set;}
    public bool IsPowerUpActivated => Time.time <= endTimePowerUp;

    protected float endTimePowerUp = 0;

    public void ActiveForDuration(float powerUpTime)
    {
        var wasActive = IsPowerUpActivated;
        endTimePowerUp = Time.time + powerUpTime;
        if(!wasActive)
        {
            StartCoroutine(ExecutePowerUpCoro());
        }
                
    }

    private IEnumerator ExecutePowerUpCoro()
    {
        EnterPowerUp();
        var particle = Instantiate(playerParticle, PowerUpContext.playerController.transform);

        while (IsPowerUpActivated)
        {
            UpdatePowerUp();
            yield return null;
        }

        Destroy(particle);
        EndPowerUp();
    }

    protected abstract void EnterPowerUp();
    protected abstract void UpdatePowerUp();
    protected abstract void EndPowerUp();

}
