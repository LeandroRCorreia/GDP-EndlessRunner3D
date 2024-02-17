using UnityEngine;

interface ICollideableReact
{
    void PlayCollisionFeedBack(in IPlayerInfo playerInfo);

}

public struct IPlayerInfo
{
    public GameMode gameMode;
    public PlayerController player;
    public PlayerAnimationController animationController;
    public Collider objectCollided;
    public PowerUpController powerUpController;

}

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private PowerUpController powerUpController;
    private PlayerController playerController;
    private PlayerAnimationController animationController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ICollideableReact collideable))
        {
            var playerInfo = new IPlayerInfo
            {
                gameMode = gameMode,
                player = playerController,
                animationController = animationController,
                objectCollided = other,
                powerUpController = powerUpController

            };

            collideable.PlayCollisionFeedBack(playerInfo);
        }

    }
}
