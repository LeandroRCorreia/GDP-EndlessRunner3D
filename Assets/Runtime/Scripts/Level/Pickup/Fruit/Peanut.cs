
public sealed class Peanut : Pickup
{
    protected sealed override void ExecutePickupBehaviour(in IPlayerInfo playerInfo)
    {
        playerInfo.gameMode.OnPeanutPicked();
    }
}
