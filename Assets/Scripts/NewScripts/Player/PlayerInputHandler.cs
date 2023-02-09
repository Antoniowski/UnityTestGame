using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float inputMagnitude;
    public bool isRunning;

    public bool isInteracting;

    PlayerInputController inputController;
    
    [HideInInspector]
    public Vector2 inputDecoder;

    public void OnEnable(){
        if(inputController == null){
            inputController = new PlayerInputController();
            inputController.CharacterInputController.Move.started += HandleMovement;
            inputController.CharacterInputController.Move.canceled += HandleMovement;
            inputController.CharacterInputController.Move.performed += HandleMovement;

            inputController.CharacterInputController.Run.started += inputController => isRunning = inputController.ReadValueAsButton();
            inputController.CharacterInputController.Run.canceled += inputController => isRunning = inputController.ReadValueAsButton();

        }

        inputController.Enable();
    }

    public void OnDisable(){
        inputController.Disable();
    }

    void CheckInput(float delta)
    {

    }

    void HandleMovement(InputAction.CallbackContext context){
        inputDecoder = context.ReadValue<Vector2>();

        Vector3 adaptedInput = new Vector3(inputDecoder.x, 0, inputDecoder.y);
        adaptedInput = Quaternion.Euler(0,45,0)*adaptedInput;
        adaptedInput = adaptedInput.normalized;
        horizontal = adaptedInput.x;
        vertical = adaptedInput.z;
        inputMagnitude = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        
    }
}
