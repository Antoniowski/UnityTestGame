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
        {
            animationHandler.PlayAnimationTarget("PunchLeft", true);
            lastAttack = "PunchLeft";
            return;
        }
            
        
        if(lastAttack == "PunchLeft")
        {
            animationHandler.PlayAnimationTarget("PunchRight", true);
            lastAttack = "PunchRight"; 
            return;
        }
            

    }

    public void EnablePunchCollider(string punch)
    {
        if(punch == "right")
        {
            GameObject rightHand = GameObject.FindGameObjectWithTag("RightHand");
            rightHand.GetComponentInChildren<SphereCollider>().enabled = true;
        }
        else
        {
            GameObject leftHand = GameObject.FindGameObjectWithTag("LeftHand");
            leftHand.GetComponentInChildren<SphereCollider>().enabled = true;
        }
            
    }

    public void DisablePunchCollider(string punch)
    {
        if(punch == "right")
        {
            GameObject rightHand = GameObject.FindGameObjectWithTag("RightHand");
            rightHand.GetComponentInChildren<SphereCollider>().enabled = false;
        }
        else
        {
            GameObject leftHand = GameObject.FindGameObjectWithTag("LeftHand");
            leftHand.GetComponentInChildren<SphereCollider>().enabled = false;
        }
    }
}
