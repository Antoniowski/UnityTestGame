using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject rightHand;

    PlayerInputHandler inputHandler;
    Animator animator;
    PlayerCollisionHandler collisionHandler;

    //WEAPON
    private string equippedWeapon;
    public bool isEquipped;


    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand");

        inputHandler = GetComponent<PlayerInputHandler>();
        collisionHandler = GetComponent<PlayerCollisionHandler>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.isInteracting = animator.GetBool("isInteracting");
        if(!inputHandler.isInteracting) WeaponInteractionHandle(delta);
    }


    void LateUpdate()
    {
        //Un possibile metodo per resettare i pulsanti dopo l'update
        inputHandler.interactionFlag = false;
    }





    #region Weapon Handle
    void WeaponInteractionHandle(float delta)
    {
        if(!collisionHandler.isPickable)
            return;

        if(!inputHandler.interactionFlag)
            return;

        animator.SetBool("isInteracting", true);
        if(isEquipped) DropWeapon();
        PickUpweapon(collisionHandler.weaponCollider);
    }

    void PickUpweapon(GameObject weapon)
    {
        weapon.GetComponent<BoxCollider>().enabled = false; //Per evitare altri trigger
        weapon.GetComponent<BoxCollider>().enabled = false; //Utile per ottimizzare le prestazioni
        Destroy(weapon.GetComponent<Rigidbody>());

        //PER AGGIUNGERLO ALLA MANO
        //weapon.transform.forward = rightHand.transform.forward;
        
        weapon.transform.parent = rightHand.transform;
        weapon.transform.position = rightHand.transform.position;
        
        //Aggiustamenti
        weapon.transform.localRotation = Quaternion.Euler(new Vector3(0,0,90));
        weapon.transform.localPosition = new Vector3(0,0,-1f);
        equippedWeapon = weapon.name;

        isEquipped = true;
        animator.SetBool("isInteracting", false);
    }

    void DropWeapon()
    {
        Transform oldWeapon = rightHand.transform.Find(equippedWeapon); //rimosso dalla mano
        oldWeapon.parent = null;
        oldWeapon.gameObject.AddComponent<Rigidbody>();
        oldWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
        oldWeapon.gameObject.GetComponent<WeaponInfo>().SetPickableState(false);
        StartCoroutine(PickingStatusCooldown(oldWeapon));
    }

    IEnumerator PickingStatusCooldown(Transform weapon)
    {
        yield return new WaitForSeconds(2);
        weapon.gameObject.GetComponent<WeaponInfo>().SetPickableState(true);
    }
    #endregion
}
