using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Animation handler per gestire i valori dell'animator
public class NewAnimationHandler : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    private int vertical;
    private int horizontal;

    public void Init(){
        animator = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    //V = 0 IDLE
    //V = 0.5 CAMMINATA
    //V = 1 CORSA
    public void UpdateAnimatorMovementValues(float verticalMovement, float horizontalMovement, bool isRunning)
    {
        #region Vertical
        float v;

        if(verticalMovement > 0){
            v = 0.5f;
        }else if(verticalMovement < 0){
            v = -0.5f;
        }else{
            v = 0;
        }
        #endregion


        #region Horizontal
        float h;
        if(horizontalMovement > 0){
            h = 0.5f;
        }else if (horizontalMovement < 0){
            h = -0.5f;
        }else{
            h = 0;
        }
        #endregion

        if(isRunning)
        {
            //Raddoppiamo la velocita' per attivare l'animazione di corsa
            v = v*2;
            h = h*2;
        }

        animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

    }

    public void PlayAnimationTarget(string targetAnim, bool isInteracting)
    {
        animator.applyRootMotion = isInteracting;
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnim, 0.2f);
        
    }

    public void EnableCombo()
    {
        animator.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("canDoCombo", false);
    }
}
