using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackHandler : MonoBehaviour
{
    private PlayerInputController playerInputController;
    private PlayerMovement playerStatus;
    private Animator animator;

    void Awake()
    {
        playerInputController = FindObjectOfType<PlayerMovement>().GetInputController(); 
        playerStatus = GetComponentInChildren<PlayerMovement>();
        animator = FindObjectOfType<Animator>();

        playerInputController.CharacterInputController.Attack.performed += OnAttack;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnAttack(InputAction.CallbackContext context){
        if(!playerStatus.GetStatus().isAttacking)
        {
            playerStatus.SetStatus(PlayerMovement.PlayerStatusEnum.IS_ATTACKING, true);
            playerStatus.SetStatus(PlayerMovement.PlayerStatusEnum.ATTACK_STATE, true);
            StartCoroutine(Attack());
        }
        if(playerStatus.GetStatus().isAttacking && animator.GetNextAnimatorStateInfo(0).IsName("PunchRight"))
        {
            playerStatus.SetStatus(PlayerMovement.PlayerStatusEnum.IS_ATTACKING, true);
            playerStatus.SetStatus(PlayerMovement.PlayerStatusEnum.ATTACK_STATE, true);
            animator.Play("PunchLeft");
        }
    
    }

    IEnumerator Attack(){
        //Primo attacco
        animator.SetTrigger("Attack"); 
        yield return null;
    }
    IEnumerator AttackStateTimer(){
        float timer = 5f;
        while(timer > 0){
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
