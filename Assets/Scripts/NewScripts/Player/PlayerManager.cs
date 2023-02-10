using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject rightHand;

    PlayerInputHandler inputHandler;
    Animator animator;
    PlayerCollisionHandler collisionHandler;
    PlayerInventoryHandler inventory;
    NewAnimationHandler animationHandler;
    CharacterController controller;

    public bool isGrounded;
    public bool isInAir;
    public bool canDoCombo;

    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand");

        inputHandler = GetComponent<PlayerInputHandler>();
        collisionHandler = GetComponent<PlayerCollisionHandler>();
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<PlayerInventoryHandler>();
        animationHandler = GetComponent<NewAnimationHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        isGrounded = controller.isGrounded;
        isInAir = !controller.isGrounded;
        inputHandler.TickInput(delta);
        if(!inputHandler.isInteracting) inventory.WeaponInteractionHandle(delta);
    }


    void LateUpdate()
    {
        //Un possibile metodo per resettare i pulsanti dopo l'update
        inputHandler.interactionFlag = false;
        inputHandler.attackFlag = false;
    }

}
