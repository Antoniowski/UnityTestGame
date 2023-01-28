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

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Transform.FindObjectOfType<PlayerMovement>();
        input = playerMovement.GetInput();
        animator = Transform.FindObjectOfType<Animator>();
    
        //Hashmap is useful to increase performances

        isMovingHash = Animator.StringToHash("IsMoving");
        isRunningHash = Animator.StringToHash("IsRunning");
    
    }

    // Update is called once per frame
    void Update()
    {
        input = playerMovement.GetInput();
        bool moving = animator.GetBool(isMovingHash);
        bool running = animator.GetBool(isRunningHash);

        if(input != Vector3.zero){
            if(!playerMovement.GetStatus().isRunning && moving == false){
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsRunning", false);
            }
            if(playerMovement.GetStatus().isRunning && running == false){
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsMoving", false);
            }
        }else{
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
        }        
    }
}
