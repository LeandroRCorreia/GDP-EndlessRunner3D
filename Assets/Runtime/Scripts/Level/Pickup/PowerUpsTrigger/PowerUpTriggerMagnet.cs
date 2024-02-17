using UnityEngine;

public class PowerUpTriggerMagnet : Pickup
{

    protected override void ExecutePickupBehaviour(in IPlayerInfo playerInfo)
    {
        playerInfo.powerUpController.ActivePowerUp(typeof(PowerUpBuffMagnet));

    }

    
}
