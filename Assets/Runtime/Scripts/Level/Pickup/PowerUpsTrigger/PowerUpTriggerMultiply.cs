using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTriggerMultiply : Pickup
{
    
    protected override void ExecutePickupBehaviour(in IPlayerInfo playerInfo)
    {
        playerInfo.powerUpController.ActivePowerUp(typeof(PowerUpBuffMultiply));
    }

}
