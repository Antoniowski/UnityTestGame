using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
