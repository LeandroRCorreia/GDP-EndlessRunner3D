using System;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpBehaviour[] powerUpBehaviours;

    [SerializeField] private GameMode gameMode;
    [SerializeField] private PlayerController playerController;

    private const float powerUpTime = 30;

    public void ActivePowerUp(Type powerUpBuffType)
    {
        var powerUpBuff = FindPowerUpByType(powerUpBuffType);

        ProcessActivePowerUp(powerUpBuff, powerUpTime);
        
    }

    private PowerUpBehaviour FindPowerUpByType(Type powerUpBuffType)
    {   
        foreach (var powerUpBuff in powerUpBehaviours)
        {
            if(powerUpBuffType == powerUpBuff.GetType())
            {
                return powerUpBuff;
            }

        }

        Debug.LogError($"Power-up of type {powerUpBuffType} not found.");
        return null;
    }

    private void ProcessActivePowerUp(PowerUpBehaviour powerUpBuff, float PowerUpTime)
    {
        powerUpBuff.gameObject.SetActive(true);

        PowerUpContext powerUpContext = new()
        {
            playerController = playerController,
            gameMode = gameMode
        };

        powerUpBuff.PowerUpContext = powerUpContext;

        powerUpBuff.ActiveForDuration(PowerUpTime);
    }

}
