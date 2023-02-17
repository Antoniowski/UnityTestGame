using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int damage;

    public Vector3 interactionCollider;
    public Vector3 hitBox;

    public bool isPlayer;
    private bool pickable = true;
    public LayerMask layerMask;


    public string weaponAnimation;
    public string weaponIdleAnimation;


    // Start is called before the first frame update
    void Start()
    {
        itemName = transform.name;
        itemDescription = "dummy";
        try
        {
            GetComponent<BoxCollider>().size = transform.lossyScale;
        }catch{
            print("Il collider non esiste");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetPickableState(){return pickable;}
    public void SetPickableState(bool newState){pickable = newState;}


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);
        }
    }
}
