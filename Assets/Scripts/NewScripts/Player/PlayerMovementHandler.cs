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
    //[SerializeField] private AnimationCurve SPEED_CURVE;
    //private float movementTimer;
    private const float FRICTION = 20f;
    private const float ROTATION_SPEED = 15f;
    private const float JUMP_POWER = 10f;
    private const float GRAVITY = 1.0f;
    [SerializeField] AnimationCurve ROLL_SPEED_CURVE;

    //VECTORS
    [HideInInspector]
    public Vector2 velocity;
    private float verticalVelocity;

    private float inAirTimer = 0;

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
        ApplyGravity(delta);
        if(!inputHandler.isInteracting) MovePlayer(delta);
        HandleFall(delta, new Vector3(velocity.x, 0, velocity.y));
        if(manager.isInAir) inAirTimer += delta;
    }


    void MovePlayer(float delta){
        if(inputHandler.horizontal != 0 || inputHandler.vertical != 0)
        {
            //movementTimer += delta;
            //velocity =  inputHandler.runFlag ? new Vector2(inputHandler.horizontal, inputHandler.vertical) * SPEED_CURVE.Evaluate(movementTimer)*2 : new Vector2(inputHandler.horizontal, inputHandler.vertical) * SPEED_CURVE.Evaluate(movementTimer) ;
            velocity += inputHandler.runFlag ? new Vector2(inputHandler.horizontal, inputHandler.vertical)*ACCELERATION*2f*delta : new Vector2(inputHandler.horizontal, inputHandler.vertical)*ACCELERATION*delta;
            velocity = inputHandler.runFlag ? Vector2.ClampMagnitude(velocity, MAX_SPEED*1.5f) : Vector2.ClampMagnitude(velocity, MAX_SPEED);
            HandlePlayerOrientation(delta);
        }
        else
        {
            //movementTimer = 0;
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION);
        }

        //ATTIVA LE ANIMAZIONI DI CAMMINATA
        animatorHandler.UpdateAnimatorMovementValues(inputHandler.inputMagnitude,0, inputHandler.runFlag);
        characterController.Move(new Vector3(velocity.x, 0, velocity.y)*delta);
    }

    void HandlePlayerOrientation(float delta)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(inputHandler.horizontal, 0, inputHandler.vertical), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, ROTATION_SPEED*delta);
    }



    #region ROLL
    
    public IEnumerator RollAction(){
        animatorHandler.PlayAnimationTarget("RollForward", true);
        float timer = 0.8f; //DURATA POCO PIU PICCOLA DELL'ANIMAZIONE ANIMAZIONE
        float rollTime = 0f;
        float rollSpeed = ROLL_SPEED_CURVE.Evaluate(rollTime);
        Vector3 lookDirection = new Vector3(inputHandler.horizontal, 0, inputHandler.vertical);

        while(timer > 0){
            //Dodge direction
            Quaternion toRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation,1);

            timer -= Time.deltaTime;
            rollTime += Time.deltaTime;
            rollSpeed = ROLL_SPEED_CURVE.Evaluate(rollTime);
            characterController.Move(new Vector3(inputHandler.horizontal, 0, inputHandler.vertical)*Time.deltaTime*rollSpeed);
            animatorHandler.UpdateAnimatorMovementValues(0,0,inputHandler.runFlag);

            yield return null;
        }       

    }

    public IEnumerator RollAction(Vector3 newDir){
        animatorHandler.PlayAnimationTarget("RollForward", true);
        float timer = 0.8f; //DURATA ANIMAZIONE
        float rollTime = 0f;
        float rollSpeed = ROLL_SPEED_CURVE.Evaluate(rollTime);

        while(timer > 0){
            //Direzione Dodge
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z), Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.5f);

            timer -= Time.deltaTime;
            rollTime += Time.deltaTime;
            rollSpeed = ROLL_SPEED_CURVE.Evaluate(rollTime);
            characterController.Move(new Vector3(newDir.x, 0, newDir.z)*Time.deltaTime*rollSpeed);
            animatorHandler.UpdateAnimatorMovementValues(0,0,inputHandler.runFlag); //Necessario per evitare di mantenere in memoria il movimento pre dodge

            yield return null;
        }       

    }
    #endregion

    #region FALL

    void HandleFall(float delta, Vector3 moveDir)
    {
        //Se non tocca e non è considerato ancota in aria
        if(!manager.isGrounded)
        {
            if(!manager.isInAir) manager.isInAir = true;
            if(inAirTimer>0.15f)
            {
                if(!inputHandler.isInteracting)
                    animatorHandler.PlayAnimationTarget("FallingLoop", true);
                characterController.Move(moveDir/8*delta);
            }

        }

        if(manager.isGrounded)
        {
            if(manager.isInAir){
                inAirTimer = 0;
                manager.isInAir = false;
                animatorHandler.PlayAnimationTarget("Empty", false);
            } 

        }
    }

    #endregion


    #region GRAVITY

    void ApplyGravity(float delta)
    {
        verticalVelocity = -GRAVITY;
        characterController.Move(new Vector3(0,verticalVelocity,0)*delta);
    }

    #endregion
}
