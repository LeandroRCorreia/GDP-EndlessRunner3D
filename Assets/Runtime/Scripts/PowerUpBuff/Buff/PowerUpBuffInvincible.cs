using UnityEngine;

public class PowerUpBuffInvincible : PowerUpBehaviour
{

    protected override void EnterPowerUp()
    {
        PowerUpContext.playerController.IsInvincible = true;
        
    }

    protected override void UpdatePowerUp()
    {

    }

    protected override void EndPowerUp()
    {
        PowerUpContext.playerController.IsInvincible = false;

    }

}
