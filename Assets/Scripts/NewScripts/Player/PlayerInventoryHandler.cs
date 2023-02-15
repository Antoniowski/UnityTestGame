using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour
{

    PlayerCollisionHandler collisionHandler;
    PlayerInputHandler inputHandler;
    NewAnimationHandler animationHandler;

    [HideInInspector]
    public GameObject rightHand;
    public WeaponInfo rightHandWeapon;

    [HideInInspector]
    public GameObject leftHand;

    private string equippedWeapon;
    public bool isEquipped;

    void Start()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");

        collisionHandler = GetComponent<PlayerCollisionHandler>();
        inputHandler = GetComponent<PlayerInputHandler>();
        animationHandler = GetComponent<NewAnimationHandler>();
    }




        #region Weapon Handle
    public void WeaponInteractionHandle(float delta)
    {
        if(!collisionHandler.isPickable)
            return;

        if(!inputHandler.interactionFlag)
            return;

        animationHandler.animator.SetBool("isInteracting", true);
        if(isEquipped) DropWeapon();
        PickUpweapon(collisionHandler.weaponCollider);
    }

    void PickUpweapon(GameObject weapon)
    {
        weapon.GetComponent<BoxCollider>().enabled = false; //Per evitare altri trigger
        weapon.GetComponent<MeshCollider>().enabled = false; //Utile per ottimizzare le prestazioni
        Destroy(weapon.GetComponent<Rigidbody>());

        weapon.GetComponent<BoxCollider>().size = weapon.GetComponent<WeaponInfo>().hitBox;

        //PER AGGIUNGERLO ALLA MANO
        //weapon.transform.forward = rightHand.transform.forward;
        
        weapon.transform.parent = rightHand.transform;
        weapon.transform.position = rightHand.transform.position;
        
        //Aggiustamenti
        weapon.transform.localRotation = Quaternion.Euler(new Vector3(0,0,90));
        weapon.transform.localPosition = new Vector3(0,0,-1f);
        equippedWeapon = weapon.name;

        isEquipped = true;
        rightHandWeapon = weapon.GetComponent<WeaponInfo>();
        animationHandler.animator.SetBool("isInteracting", false);
    }

    void DropWeapon()
    {
        Transform oldWeapon = rightHand.transform.Find(equippedWeapon); //rimosso dalla mano
        oldWeapon.parent = null;
        oldWeapon.gameObject.AddComponent<Rigidbody>();
        oldWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
        oldWeapon.GetComponent<BoxCollider>().size = oldWeapon.GetComponent<WeaponInfo>().interactionCollider;
        oldWeapon.gameObject.GetComponent<MeshCollider>().enabled = true;
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
