using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    PlayerInputHandler inputHandler;
    NewAnimationHandler animationHandler;

    public int currentHealth;
    private int maxHealth;

    
    void Start()
    {
        animationHandler = GetComponent<NewAnimationHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    public void Init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    public void SetHealth(int currentHealth, int maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }

    public void SetCurrentHealth(int health)
    {
        currentHealth = health;
    }

    void SetMaxHealth(int health)
    {
        maxHealth = health;
    }


    public void TakeDamage(int damage)
    {
        if(!inputHandler.isInteracting) animationHandler.PlayAnimationTarget("GetHit", true);
        currentHealth = currentHealth - damage;

        if(currentHealth <= 0)
        {

            currentHealth = 0;

            Death();
        }
    }

    private void Death()
    {
        animationHandler.PlayAnimationTarget("Death", true);
        //INSERIRE COSA DEVE FARE QUANDO MUORE
    }

}
