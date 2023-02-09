using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool isRunning;

    PlayerInputController inputController;

    Vector2 inputDecoder;

    public void OnEnable(){
        if(inputController == null){
            inputController = new PlayerInputController();
            inputController.CharacterInputController.Move.started += OnMovement;
            inputController.CharacterInputController.Move.canceled += OnMovement;
            inputController.CharacterInputController.Move.performed += OnMovement;
            inputController.CharacterInputController.Run.performed += inputController => isRunning = inputController.ReadValueAsButton();

        }

        inputController.Enable();
    }

    public void OnDisable(){
        inputController.Disable();
    }

    void OnMovement(InputAction.CallbackContext context){
        inputDecoder = context.ReadValue<Vector2>();

        Vector3 adaptedInput = new Vector3(inputDecoder.x, 0, inputDecoder.y);
        adaptedInput = Quaternion.Euler(0,45,0)*adaptedInput;
        adaptedInput = adaptedInput.normalized;
        horizontal = adaptedInput.x;
        vertical = adaptedInput.z;
        
    }
}
