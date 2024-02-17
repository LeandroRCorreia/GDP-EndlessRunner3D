using UnityEngine;

public static class RatMovementAnimationConstants
{
    public static readonly string RunMultiplier = "RunMultiplier";
    public static readonly string DeathTrigger = "DeathTrigger";
}

public class RatMovementAnimationState : StateMachineBehaviour
{

    private ObstacleMoving obstacleMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        var clipInfos = animator.GetCurrentAnimatorClipInfo(layerIndex);
        
        if(clipInfos.Length > 0)
        {
            AnimatorClipInfo runAnimationClip = clipInfos[0];
            obstacleMovement = animator.transform.parent.parent.parent.GetComponent<ObstacleMoving>();
            float timeToCompleteAnimationCycle = obstacleMovement.SideToSideMoveTime * 2;

            float speedMultiplier = runAnimationClip.clip.length / timeToCompleteAnimationCycle;

            animator.SetFloat(RatMovementAnimationConstants.RunMultiplier, speedMultiplier);
        }

    }

}
