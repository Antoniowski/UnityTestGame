using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimatorInt : StateMachineBehaviour
{

    public string intName;
    public int setTo;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(intName, setTo);
    }
}
