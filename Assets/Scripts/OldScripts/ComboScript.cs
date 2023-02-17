using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : StateMachineBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private bool isLastHit = true;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerInputController pic = animator.gameObject.GetComponentInParent<PlayerMovement>().GetInputController();
        if(pic.CharacterInputController.Attack.IsPressed()){
            isLastHit = false;
            animator.gameObject.GetComponentInParent<PlayerMovement>().SetStatus(PlayerMovement.PlayerStatusEnum.IS_ATTACKING, true);
            Debug.Log(animator.gameObject.GetComponentInParent<PlayerMovement>().GetStatus().isAttacking);
            animator.SetTrigger("Attack2");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isLastHit){
            animator.gameObject.GetComponentInParent<PlayerMovement>().SetStatus(PlayerMovement.PlayerStatusEnum.IS_ATTACKING, false);
            Debug.Log(animator.gameObject.GetComponentInParent<PlayerMovement>().GetStatus().isAttacking);

        }    
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
