using UnityEngine;

public class ObstacleDecorationMoving : ObstacleDecoration
{
    [SerializeField] private Animator animator;

    public override void PlayCollisionFeedBack()
    {
        base.PlayCollisionFeedBack();
        if(animator != null)
        {
            animator.SetTrigger(RatMovementAnimationConstants.DeathTrigger);
        }

    }

}
