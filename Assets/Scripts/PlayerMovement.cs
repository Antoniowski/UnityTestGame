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
    private const float GRAVITY = 1f;

//DODGE VARIABLES
    private float DODGE_DURATION = 0.75f;
    private float DODGE_CD = 1f;
    //AnimationCurve permette ci modificare dinamicamente dei valori in funzione del tempo
    [SerializeField] private AnimationCurve dodgeSpeedCurve;
        
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
        public bool isDodging;

        public PlayerStatus(bool initialStatus){
            isMoving = initialStatus;
            isRunning = initialStatus;
            isDodging = initialStatus;
        }
    };
    private PlayerStatus status;




    void Awake(){

        controller = Transform.FindObjectOfType<CharacterController>();        
        inputController = new PlayerInputController();
        status = new PlayerStatus(false);

        //movement
        inputController.CharacterInputController.Move.started += OnMovement;
        inputController.CharacterInputController.Move.canceled += OnMovement;
        //performed is needed for controllers sticks
        inputController.CharacterInputController.Move.performed += OnMovement;

        //run
        inputController.CharacterInputController.Run.performed += OnRun;
        inputController.CharacterInputController.Run.canceled += OnRun;

        //dodge
        inputController.CharacterInputController.Dodge.performed += OnDodge;
    }



    void Start()
    {

    }



    // Update is called once per frame
    void Update()
   {
        if(!status.isDodging){
            if(input != Vector3.zero){
                velocity += status.isRunning ? new Vector2(input.x, input.z)*ACCELERATION*2f*Time.deltaTime : new Vector2(input.x, input.z)*ACCELERATION*Time.deltaTime;
                velocity = status.isRunning ? Vector2.ClampMagnitude(velocity, MAX_SPEED*1.5f) : Vector2.ClampMagnitude(velocity, MAX_SPEED);
                HandlePlayerOrientation();
            }else{
                velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION);
            }
        }

        //GRAVITA'
        verticalVelocity -= GRAVITY*Time.deltaTime;
        if(!status.isDodging) controller.Move(new Vector3(velocity.x, verticalVelocity, velocity.y)*Time.deltaTime);
    }



//************FUNZIONI SUPPLEMENTATI***************

    //funzione di movimento - ottiene ed elabora l'input dal nuovo input controller
    void OnMovement(InputAction.CallbackContext context){
        inputDecoder = context.ReadValue<Vector2>();

        if(status.isDodging == false){
            input.x = inputDecoder.x;
            input.z = inputDecoder.y;
            input = Quaternion.Euler(new Vector3(0,45,0))*input;


            status.isMoving = inputDecoder.x != 0 || inputDecoder.y != 0;
            }
    }

    void OnRun(InputAction.CallbackContext context){
        status.isRunning = context.ReadValueAsButton();
    }

    void OnDodge(InputAction.CallbackContext context){
        if(!status.isDodging && input != Vector3.zero){
            StartCoroutine(DodgeAction());
        }
    }

    IEnumerator DodgeAction(){
        status.isDodging = true;
        //sostituire con effettiva durata dell'animazione
        float timer = DODGE_DURATION+0.15f;

        float rollTime = 0f;
        float rollSpeed = dodgeSpeedCurve.Evaluate(rollTime);

        //Dodge direction
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(input.x, 0, input.z), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation,1);

        //Cerca Animator per iniziare l'animazione
        FindObjectOfType<Animator>().SetTrigger("IsDodging");
        while(timer > 0){
            timer -= Time.deltaTime;
            rollTime += Time.deltaTime;
            rollSpeed = dodgeSpeedCurve.Evaluate(rollTime);
            controller.Move(new Vector3(input.x, 0, input.z)*Time.deltaTime*rollSpeed);

            yield return null;
        }
        //check per input dal player
        input.x = inputDecoder.x;
        input.z = inputDecoder.y;
        input = Quaternion.Euler(new Vector3(0,45,0))*input;

        status.isDodging = false;
    }

    void OnEnable(){
        inputController.CharacterInputController.Enable();
    }

    void OnDisable(){
        inputController.CharacterInputController.Disable();
    }


    void HandlePlayerOrientation(){
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
