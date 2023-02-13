using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{

    CharacterController characterController;
    PlayerInputHandler inputHandler;
    PlayerManager manager;

    [HideInInspector]
    public NewAnimationHandler animatorHandler;

    #region Movement variables
    
    //CONSTANTS
    private const float ACCELERATION = 50f;
    private const float MAX_SPEED = 5f;
    private const float FRICTION = 20f;
    private const float ROTATION_SPEED = 15f;
    private const float JUMP_POWER = 10f;
    private const float GRAVITY = 10f;
    [SerializeField] AnimationCurve ROLL_SPEED_CURVE;

    //VECTORS
    [HideInInspector]
    public Vector2 velocity;
    private Vector3 verticalVelocity;

    #endregion

    void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();
        animatorHandler = GetComponentInChildren<NewAnimationHandler>();
        manager = GetComponent<PlayerManager>();

        animatorHandler.Init();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if(!inputHandler.isInteracting) MovePlayer(delta);
        HandleRoll(delta);
    }


    void MovePlayer(float delta){
        if(inputHandler.horizontal != 0 || inputHandler.vertical != 0)
        {
            velocity += inputHandler.runFlag ? new Vector2(inputHandler.horizontal, inputHandler.vertical)*ACCELERATION*2f*delta : new Vector2(inputHandler.horizontal, inputHandler.vertical)*ACCELERATION*delta;
            velocity = inputHandler.runFlag ? Vector2.ClampMagnitude(velocity, MAX_SPEED*1.5f) : Vector2.ClampMagnitude(velocity, MAX_SPEED);
            HandlePlayerOrientation(delta);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION);
        }

        //APPLICO LA GRAVITA'
        verticalVelocity = Vector3.down*GRAVITY*delta;

        //ATTIVA LE ANIMAZIONI DI CAMMINATA
        animatorHandler.UpdateAnimatorMovementValues(inputHandler.inputMagnitude,0, inputHandler.runFlag);
        characterController.Move(new Vector3(velocity.x, verticalVelocity.y, velocity.y)*delta);
    }

    void HandlePlayerOrientation(float delta)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(inputHandler.horizontal, 0, inputHandler.vertical), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, ROTATION_SPEED*delta);
    }



    #region ROLL
    void HandleRoll(float delta)
    {
        //Salta l'azione se è già impegnato
        if(animatorHandler.animator.GetBool("isInteracting"))
            return;
            
        if(inputHandler.rollFlag)
        {
            if(inputHandler.inputMagnitude != 0) StartCoroutine(RollAction());
        }
    }
    
    IEnumerator RollAction(){
        animatorHandler.PlayAnimationTarget("RollForward", true);
        float timer = 1f; //DURATA ANIMAZIONE
        float rollTime = 0f;
        float rollSpeed = ROLL_SPEED_CURVE.Evaluate(rollTime);
        
        //Dodge direction
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(inputHandler.horizontal, 0, inputHandler.vertical), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation,1);

        while(timer > 0){
            timer -= Time.deltaTime;
            rollTime += Time.deltaTime;
            rollSpeed = ROLL_SPEED_CURVE.Evaluate(rollTime);
            characterController.Move(new Vector3(inputHandler.horizontal, 0, inputHandler.vertical)*Time.deltaTime*rollSpeed);
            animatorHandler.UpdateAnimatorMovementValues(0,0,inputHandler.runFlag);

            yield return null;
        }       

    }
    #endregion
}
