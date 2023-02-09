using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationHandler : MonoBehaviour
{

    private PlayerMovement playerMovement;

    private Vector3 input;

    private Animator animator;

    private int isMovingHash;
    private int isRunningHash;
    private int attackStateHash;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Transform.FindObjectOfType<PlayerMovement>();
        input = playerMovement.GetInput();
        animator = Transform.FindObjectOfType<Animator>();
    
        //Hashmap is useful to increase performances

        isMovingHash = Animator.StringToHash("IsMoving");
        isRunningHash = Animator.StringToHash("IsRunning");
        attackStateHash = Animator.StringToHash("AttackState");
    
    }

    // Update is called once per frame
    void Update()
    {
        input = playerMovement.GetInput();
        bool moving = animator.GetBool(isMovingHash);
        bool running = animator.GetBool(isRunningHash);
        bool attacking = animator.GetBool(attackStateHash);

        if(input != Vector3.zero){
            //Non corre e non dodgia - Cammina
            if(!playerMovement.GetStatus().isRunning && !playerMovement.GetStatus().isDodging && moving == false){
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsRunning", false);
            }
            //Se sta correndo
            if(playerMovement.GetStatus().isRunning && running == false){
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsMoving", false);
            }
        }else{
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
            if(playerMovement.GetStatus().attackState) animator.SetBool("AttackState", true);
        }        
    }

    float GetAnimationDuration(string animationName){
        RuntimeAnimatorController ac = FindObjectOfType<RuntimeAnimatorController>();
        foreach (AnimationClip clip in ac.animationClips)
        {
            if(clip.name == animationName){
                return clip.length;
            }
        }
        return 0;
    }
}
