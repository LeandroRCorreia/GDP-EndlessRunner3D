using UnityEngine;

public class PowerUpBuffMultiply : PowerUpBehaviour
{

    protected override void EnterPowerUp()
    {
        PowerUpContext.gameMode.TemporaryScoreMultiplier = 2;
        
    }

    protected override void UpdatePowerUp()
    {

    }
    
    protected override void EndPowerUp()
    {
        PowerUpContext.gameMode.TemporaryScoreMultiplier = 1;

    }

}
