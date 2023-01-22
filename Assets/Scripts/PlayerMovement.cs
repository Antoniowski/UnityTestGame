using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller = null;
    private Animator animator = null;
    private int isMovingHash;
    private int isJumpingHash;
    private int isRunningHash;

    //CONST VALUES
    private const float ACCELERATION = 50f;
    private const float MAX_SPEED = 5f;
    private const float FRICTION = 20f;
    private const float ROTATION_SPEED = 15f;
    private const float JUMP_POWER = 10f;
    private const float GRAVITY = 2f;

    //VETTORI
    private Vector2 velocity = new Vector2();
    private float verticalVelocity = 0;
    private Vector3 input = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        controller = Transform.FindObjectOfType<CharacterController>();
        animator = Transform.FindObjectOfType<Animator>();
        
        //Useful to increase performances
        isMovingHash = Animator.StringToHash("IsMoving");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isRunningHash = Animator.StringToHash("IsRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = animator.GetBool(isMovingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        //if(isJumping == true && controller.isGrounded == true){
        //    animator.SetBool("IsJumping", false);
        //}

        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        input = Quaternion.Euler(new Vector3(0,45,0))*input;
        input = input.normalized;
        if(input != Vector3.zero){
            if(!Input.GetButton("Sprint") && isMoving == false){
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsRunning", false);
            }
            if(Input.GetButton("Sprint") && isRunning == false){
                animator.SetBool("IsRunning", true);
                animator.SetBool("IsMoving", false);
            }
            velocity += Input.GetButton("Sprint") ? new Vector2(input.x, input.z)*ACCELERATION*2f*Time.deltaTime : new Vector2(input.x, input.z)*ACCELERATION*Time.deltaTime;
            velocity = Input.GetButton("Sprint") ? Vector2.ClampMagnitude(velocity, MAX_SPEED*1.5f) : Vector2.ClampMagnitude(velocity, MAX_SPEED);
            SetCorrectPlayerOrientation();
        }else{
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsRunning", false);
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION);
        }


        //NON FUNZIONA
        if(Input.GetButton("Jump") && controller.isGrounded){
            //animator.SetBool("IsJumping", true);
            verticalVelocity = JUMP_POWER;
        }

        //GRAVITY
        verticalVelocity -= GRAVITY*Time.deltaTime;
        controller.Move(new Vector3(velocity.x, verticalVelocity, velocity.y)*Time.deltaTime);
    }

    void SetCorrectPlayerOrientation(){
        //Faster method
        //transform.forward = input;

        Quaternion toRotation = Quaternion.LookRotation(input, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, ROTATION_SPEED*Time.deltaTime);
    }
}
