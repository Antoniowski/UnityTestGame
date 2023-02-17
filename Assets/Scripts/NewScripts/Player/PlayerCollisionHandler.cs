using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    PlayerAnimationHandler animationHandler;

    public bool collisionDetected;
    //public bool isCollectible;
    public bool isPickable;

    public GameObject weaponCollider;

    void Start()
    {
        animationHandler = GetComponentInParent<PlayerAnimationHandler>();
    }

    void OnTriggerEnter(Collider other)
    {

    }

    void OnTriggerStay(Collider other)
    {
        //Aggiunta la condizione per richiamarlo solo una volta
        if(!isPickable) WeaponCollisionHandle(other, true);
    }

    void OnTriggerExit(Collider other)
    {
        WeaponCollisionHandle(other, false);
    }



    void WeaponCollisionHandle(Collider collider, bool isCol)
    {
        
        if(collider.gameObject.tag != "Weapon")
            return;

        if(!collider.gameObject.GetComponent<WeaponInfo>().GetPickableState())
            return;
        
        isPickable = isCol;
        weaponCollider = isCol ? collider.gameObject : null;

    }
}
