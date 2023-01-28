using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller = null;
    private PlayerInputController inputController;

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

    //SERVE PER DECODIFICARE GLI  INPUT DAL NUOVO INPUTSYTSYEM
    private Vector2 inputDecoder;
    private Vector3 input = new Vector3();


    //STATUS BOOL
    public struct PlayerStatus{
        public bool isMoving;
        public bool isRunning;

        public PlayerStatus(bool initialStatus){
            isMoving = initialStatus;
            isRunning = initialStatus;
        }
    };
    private PlayerStatus status;


    // Start is called before the first frame update

    void Awake(){

        controller = Transform.FindObjectOfType<CharacterController>();        
        inputController = new PlayerInputController();
        status = new PlayerStatus(false);

        inputController.CharacterInputController.Move.started += OnMovement;
        inputController.CharacterInputController.Move.canceled += OnMovement;
        //performed is needed for controllers sticks
        inputController.CharacterInputController.Move.performed += OnMovement;
        inputController.CharacterInputController.Run.started += OnRun;
        inputController.CharacterInputController.Run.canceled += OnRun;




    }

    void OnMovement(InputAction.CallbackContext context){
        inputDecoder = context.ReadValue<Vector2>();

        input.x = inputDecoder.x;
        input.z = inputDecoder.y;
        input = Quaternion.Euler(new Vector3(0,45,0))*input;

        status.isMoving = inputDecoder.x != 0 || inputDecoder.y != 0;
    }

    void OnRun(InputAction.CallbackContext context){
        status.isRunning = context.ReadValueAsButton();
    }

    void OnEnable(){
        inputController.CharacterInputController.Enable();
    }

    void OnDisable(){
        inputController.CharacterInputController.Disable();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(input != Vector3.zero){

            velocity += status.isRunning ? new Vector2(input.x, input.z)*ACCELERATION*2f*Time.deltaTime : new Vector2(input.x, input.z)*ACCELERATION*Time.deltaTime;
            velocity = status.isRunning ? Vector2.ClampMagnitude(velocity, MAX_SPEED*1.5f) : Vector2.ClampMagnitude(velocity, MAX_SPEED);
            HandlePlayerOrientation();
        }else{

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

    void HandlePlayerOrientation(){
        //Faster method
        //transform.forward = input;

        Quaternion toRotation = Quaternion.LookRotation(input, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, ROTATION_SPEED*Time.deltaTime);
    }

    public Vector3 GetInput(){
        return input;
    }

    public PlayerStatus GetStatus(){
        return status;
    }
}
