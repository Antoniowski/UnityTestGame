using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    Animator animator;
    PlayerCollisionHandler collisionHandler;
    PlayerInventoryHandler inventory;
    NewAnimationHandler animationHandler;
    CharacterController controller;
    PlayerStats playerStats;
    PlayerMovementHandler playerMovementHandler;

    [Header("Flags")]   
    private float distToGround; 
    public bool isGrounded;
    public bool isInAir;
    public bool canDoCombo;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        collisionHandler = GetComponent<PlayerCollisionHandler>();
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<PlayerInventoryHandler>();
        animationHandler = GetComponent<NewAnimationHandler>();
        playerMovementHandler = GetComponent<PlayerMovementHandler>();
        
        playerStats = GetComponent<PlayerStats>();
        playerStats.Init(10);

        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        inputHandler.canBuffer = animator.GetBool("canBuffer");
        inputHandler.TickInput(delta);
        if(!inputHandler.isInteracting) inventory.WeaponInteractionHandle(delta);
    }

    void FixedUpdate()
    {
        isGrounded = IsGroundedCheck();
    }

    void LateUpdate()
    {
        //Un possibile metodo per resettare i pulsanti dopo l'update
        inputHandler.interactionFlag = false;
        inputHandler.attackFlag = false;
        inputHandler.rollFlag = false;
    }

    private bool IsGroundedCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround+0.1f);
    }

}
