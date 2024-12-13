using UnityEngine;

public class OnLoop : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the animation has completed its loop
        if (stateInfo.normalizedTime >= 1.0f)
        {
            // Restart the animation from the beginning
            animator.Play(stateInfo.fullPathHash, layerIndex, 0f);
        }
    }
}
