
public sealed class Cherrie : Pickup
{
    protected sealed override void ExecutePickupBehaviour(in IPlayerInfo gameOverInfo)
    {
        gameOverInfo.gameMode.OnCherriePicked();
    }


}
