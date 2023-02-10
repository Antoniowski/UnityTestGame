using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerAttackHandler : MonoBehaviour
{
    NewAnimationHandler animationHandler;
    PlayerInputHandler inputHandler;

    private string lastAttack;

    void Start()
    {
        animationHandler = GetComponent<NewAnimationHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }
    public void HandleAttack()
    {
        animationHandler.PlayAnimationTarget("PunchRight", true);
        lastAttack = "PunchRight";

    }

    public void HandleCombo()
    {
        if(!inputHandler.comboFlag)
            return;
        
        animationHandler.animator.SetBool("canDoCombo", false);

        if(lastAttack == "PunchRight")
            animationHandler.PlayAnimationTarget("PunchLeft", true);
            lastAttack = "PunchLeft";
        
        if(lastAttack == "PunchLeft")
            animationHandler.PlayAnimationTarget("PunchRight", true);
            lastAttack = "PunchRight"; 

    }
}
