using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{

    CharacterController characterController;
    PlayerInputHandler inputHandler;

    #region Movement variables
    
    //CONSTANTS
    private const float ACCELERATION = 50f;
    private const float MAX_SPEED = 5f;
    private const float FRICTION = 20f;
    private const float ROTATION_SPEED = 15f;
    private const float JUMP_POWER = 10f;
    private const float GRAVITY = 1f;

    //VECTORS
    [HideInInspector]
    public Vector2 velocity;
    private Vector2 verticalVelocity;

    #endregion

    void Awake(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        MovePlayer(delta);
    }


    void MovePlayer(float delta){
        if(inputHandler.horizontal != 0 && inputHandler.vertical != 0)
        {
            velocity += new Vector2(inputHandler.horizontal, inputHandler.vertical)*ACCELERATION*delta;
            velocity = Vector2.ClampMagnitude(velocity, MAX_SPEED);
            HandlePlayerOrientation(delta);
        }
        else
        {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, FRICTION);
        }
        characterController.Move(new Vector3(velocity.x, 0, velocity.y)*delta);
    }

    void HandlePlayerOrientation(float delta)
    {
        Quaternion toRotation = Quaternion.LookRotation(new Vector3(inputHandler.horizontal, 0, inputHandler.vertical), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, ROTATION_SPEED*delta);
    }

}
