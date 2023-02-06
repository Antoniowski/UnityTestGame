using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionHandler : MonoBehaviour
{

    //Sarabbe utile definire dei singleton per gli arti e le giunture
    private GameObject rightHand;
    private PlayerInputController playerInputController;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        //Riferimento al playerinputcontroller del movimento. E' preferibile creare una classe per gli inpout ad hoc
        playerInputController = FindObjectOfType<PlayerMovement>().GetInputController(); 
       //playerInputController.CharacterInputController.Interact.canceled += OnInteraction;

        animator = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Weapon"){
            //Check per settarlo solo una volta
            if (other.gameObject.GetComponent<WeaponInfo>().GetPickableState() != true) other.gameObject.GetComponent<WeaponInfo>().SetPickableState(true);
            //Azione fatta una sola volta
            if(playerInputController.CharacterInputController.Interact.IsPressed()){
                print("Preso");
                if (other.gameObject.GetComponent<WeaponInfo>().GetPickableState()){
                    other.gameObject.GetComponent<WeaponInfo>().SetPickableState(false);
                    other.gameObject.GetComponent<BoxCollider>().enabled = false; //Per evitare altri trigger
                    other.gameObject.GetComponent<BoxCollider>().enabled = false; //Utile per ottimizzare le prestazioni
                    Destroy(other.gameObject.GetComponent<Rigidbody>());

                    //PER AGGIUNGERLO ALLA MANO
                    //other.transform.forward = rightHand.transform.forward;
                    other.gameObject.transform.parent = rightHand.transform;
                    other.gameObject.transform.position = rightHand.transform.position;
                    
                    //Aggiustamenti
                    other.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0,0,-90));
                    other.gameObject.transform.localPosition = new Vector3(0,0,-1f);

                } 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Weapon"){
            other.gameObject.GetComponent<WeaponInfo>().SetPickableState(false);
        }
    }


    //INPUT FUNCTIONS
    //private void OnInteraction(InputAction.CallbackContext context){

    //}
}
