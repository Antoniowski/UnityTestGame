using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider slider;
    public bool updateValue;
    public int newValue;


    void LateUpdate()
    {
        if(updateValue)
            setCurrentHealth(newValue);
    }

    public void SetToMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void setCurrentHealth(int currentHealth)
    {   
        //Rapida
        //slider.value = currentHealth


        //Smooth
        if(slider.value > currentHealth)
        {
            slider.value = Mathf.MoveTowards(slider.value, currentHealth, 5f*Time.deltaTime);
            return;
        }
        
        updateValue = false;
    }

}
