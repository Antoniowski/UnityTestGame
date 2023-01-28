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
    private int isJumpingHash;
    private int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Transform.FindObjectOfType<PlayerMovement>();
        input = playerMovement.GetInput();
        animator = Transform.FindObjectOfType<Animator>();
    
        //Hashmap is useful to increase performances

        isMovingHash = Animator.StringToHash("IsMoving");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isRunningHash = Animator.StringToHash("IsRunning");
    
    }

    // Update is called once per frame
    void Update()
    {
        input = playerMovement.GetInput();
        bool isMoving = animator.GetBool(isMovingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if(input != Vector3.zero){
            if(!playerMovement.GetStatus().isRunning && isMoving == false){
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsRunning", false);
            }
            if(playerMovement.GetStatus().isRunning && isRunning == false){
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsMoving", false);
            }
        }else{
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
        }        
    }
}
