using System.Collections;
using UnityEngine;

public struct PlayerAnimatorStateNameConstants
{
    public const string RunAnimationStateName = "Run";
}

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void Start()
    {
        player.PlayerDeathEvent += OnPlayerDieEvent;
    }

    private void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
    }

    public IEnumerator WaitAnimationToStartGame()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);

        while(!animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimatorStateNameConstants.RunAnimationStateName))
        {
            yield return null;
        }

    }

    private void OnPlayerDieEvent()
    {
        Die();
    }

    private void Die()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }

    void OnDestroy()
    {
        player.PlayerDeathEvent -= OnPlayerDieEvent;
    }

}
