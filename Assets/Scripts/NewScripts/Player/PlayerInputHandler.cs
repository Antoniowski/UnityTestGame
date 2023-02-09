using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float inputMagnitude;
    
    #region FLAGS

    public bool runFlag;
    public bool rollFlag;
    public bool isInteracting;

    #endregion

    PlayerInputController inputController;
    
    [HideInInspector]
    public Vector2 inputDecoder;

    public void OnEnable(){
        if(inputController == null){
            inputController = new PlayerInputController();
            SetupMovementInput();
            SetupRunInput();
            SetupRollInput();
            

        }

        inputController.Enable();
    }

    public void OnDisable(){
        inputController.Disable();
    }

    void CheckInput(float delta)
    {

    }

    void SetupMovementInput(){
        inputController.CharacterInputController.Move.started += inputController => inputDecoder = inputController.ReadValue<Vector2>();
        inputController.CharacterInputController.Move.canceled += inputController => inputDecoder = inputController.ReadValue<Vector2>();
        inputController.CharacterInputController.Move.performed += inputController => inputDecoder = inputController.ReadValue<Vector2>();
    }

    void SetupRunInput(){
        inputController.CharacterInputController.Run.started += inputController => runFlag = inputController.ReadValueAsButton();
        inputController.CharacterInputController.Run.canceled += inputController => runFlag = inputController.ReadValueAsButton();
    }

    void SetupRollInput(){
        inputController.CharacterInputController.Dodge.started += inputController => rollFlag = inputController.ReadValueAsButton();
        inputController.CharacterInputController.Dodge.canceled += inputController => rollFlag = inputController.ReadValueAsButton();
    }

    //Da usare negli update ogni volte ci sia bisogno degli input
    public void TickInput(float delta)
    {
        if(!isInteracting) MoveInput(delta);
    }

    //Serve ad aggiornare tutte le variabili di input usate dagli altri script
    //per creare movimento
    void MoveInput(float delta)
    {
        Vector3 adaptedInput = new Vector3(inputDecoder.x, 0, inputDecoder.y);
        adaptedInput = Quaternion.Euler(0,45,0)*adaptedInput;
        adaptedInput = adaptedInput.normalized;
        horizontal = adaptedInput.x;
        vertical = adaptedInput.z;
        inputMagnitude = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }
}
