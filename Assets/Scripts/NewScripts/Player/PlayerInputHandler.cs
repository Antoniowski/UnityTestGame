using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    PlayerInputController inputController;
    NewPlayerAttackHandler attackHandler;
    PlayerMovementHandler movementHandler;
    PlayerManager manager;

    [HideInInspector]
    public float horizontal;
    public float vertical;
    private Vector3 adaptedInput;
    public float inputMagnitude;
    
    #region FLAGS
    //flag per indicare la pressione di un pulsante o un interazione in corso
    public bool runFlag;//SHIFT
    public bool rollFlag; //SPACEBAR
    public bool interactionFlag; //E
    public bool attackFlag;
    public bool comboFlag;
    public bool canBuffer;

    //VARIABILE PATATERN
    public bool isInteracting; //Usata per impedire certi input


    //BUFFER
    public (string action, Vector3 direction) bufferedAction = (null, Vector3.zero);
    

    #endregion    
    [HideInInspector] public Vector2 inputDecoder;


    void Start()
    {
        attackHandler = GetComponent<NewPlayerAttackHandler>();
        manager = GetComponent<PlayerManager>();
        movementHandler = GetComponent<PlayerMovementHandler>();
    }

    public void OnEnable(){
        if(inputController == null){
            inputController = new PlayerInputController();
            SetupMovementInput();
            SetupRunInput();
            SetupRollInput();
            SetInteractInput();
            SetAttackInput();
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

    void SetInteractInput()
    {
        inputController.CharacterInputController.Interact.started += inputController => interactionFlag = inputController.ReadValueAsButton();
        inputController.CharacterInputController.Interact.canceled += inputController => interactionFlag = inputController.ReadValueAsButton();
    }

    void SetAttackInput()
    {
        inputController.CharacterInputController.Attack.started += inputController => attackFlag = inputController.ReadValueAsButton();
        inputController.CharacterInputController.Attack.canceled += inputController => attackFlag = inputController.ReadValueAsButton();
    }

    private void AdaptInputDirection()
    {
        adaptedInput = new Vector3(inputDecoder.x, 0, inputDecoder.y);
        adaptedInput = Quaternion.Euler(0,45,0)*adaptedInput;
        adaptedInput = adaptedInput.normalized;
    }

    //Da usare negli update ogni volte ci sia bisogno degli input
    public void TickInput(float delta)
    {
        if(!isInteracting) 
            MoveInput(delta);
        HandleAttackInput(delta);
        HandleRoll(delta);
    }

    //Serve ad aggiornare tutte le variabili di input usate dagli altri script
    //per creare movimento
    void MoveInput(float delta)
    {
        AdaptInputDirection();
        horizontal = adaptedInput.x;
        vertical = adaptedInput.z;
        inputMagnitude = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }

    void HandleRoll(float delta)
    {
        if(!rollFlag)
            return;
        //Salta l'azione se è già impegnato
        if(isInteracting){
            if(inputDecoder == Vector2.zero)
                return;

            if(!canBuffer)
                return;
                
            AdaptInputDirection();
            bufferedAction = ("dodge", adaptedInput);
            return;
        }

        if(inputMagnitude != 0) StartCoroutine(movementHandler.RollAction());
    }

    void HandleAttackInput(float delta)
    {
        if(!attackFlag)
            return;

        if(!manager.canDoCombo)
        {
            if(!isInteracting) attackHandler.HandleAttack();
            return;
        }       
        
        comboFlag = true;
        attackHandler.HandleCombo();
        comboFlag = false;
        
    }

    public void CheckActionBuffer()
    {
        if(bufferedAction.action == null)
            return;

        if(bufferedAction.action == "dodge")
        {
            bufferedAction.action = null;
            StartCoroutine(movementHandler.RollAction(bufferedAction.direction));
        }
            
            
    }
}
