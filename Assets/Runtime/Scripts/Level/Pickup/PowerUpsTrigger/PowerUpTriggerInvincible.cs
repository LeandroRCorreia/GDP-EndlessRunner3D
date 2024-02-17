using UnityEngine;

public class PowerUpTriggerInvincible : Pickup
{
    protected override void ExecutePickupBehaviour(in IPlayerInfo playerInfo)
    {
        playerInfo.powerUpController.ActivePowerUp(typeof(PowerUpBuffInvincible));
    }

}
