using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerAttackHandler : MonoBehaviour
{
    NewAnimationHandler animationHandler;
    PlayerInputHandler inputHandler;
    PlayerInventoryHandler inventory;

    private string lastAttack;

    void Start()
    {
        animationHandler = GetComponent<NewAnimationHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
        inventory = GetComponent<PlayerInventoryHandler>();
    }

    public void HandleAttack()
    {
        if(!inventory.isEquipped){
            animationHandler.PlayAnimationTarget("PunchRight", true);
            lastAttack = "PunchRight";
            return;
        }

        animationHandler.PlayAnimationTarget(inventory.rightHandWeapon.weaponAnimation, true);
        lastAttack = inventory.rightHandWeapon.weaponAnimation;

    }

    public void HandleCombo()
    {
        if(!inputHandler.comboFlag)
            return;
        
        animationHandler.animator.SetBool("canDoCombo", false);


        if(!inventory.isEquipped)
        {
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

        if(inventory.isEquipped)
        {            
            if(lastAttack == inventory.rightHandWeapon.weaponAnimation)
            {
                if(inventory.rightHandWeapon.weaponAnimation02 == "")
                    return;
                animationHandler.PlayAnimationTarget(inventory.rightHandWeapon.weaponAnimation02, true);
                lastAttack = inventory.rightHandWeapon.weaponAnimation02;
                return;
            }

            if(lastAttack == inventory.rightHandWeapon.weaponAnimation02)
            {
                if(inventory.rightHandWeapon.weaponAnimation03 == "")
                    return;

                animationHandler.PlayAnimationTarget(inventory.rightHandWeapon.weaponAnimation03, true);
                lastAttack = null;
                return;
            }


               
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

    public void EnableHitCollider()
    {
        if(inventory.isEquipped)
        {
            Transform weapon = GameObject.FindGameObjectWithTag("RightHand").transform.GetChild(0);
            weapon.GetComponent<BoxCollider>().enabled = true;
        }
    }


    public void DisableHitCollider()
    {
       if(inventory.isEquipped)
       {
            Transform weapon = GameObject.FindGameObjectWithTag("RightHand").transform.GetChild(0);
            weapon.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
