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
    public string weaponAnimation02;
    public string weaponAnimation03;
    public string weaponIdleAnimation;

    [SerializeField] private ParticleSystem hitEffect;


    // Start is called before the first frame update
    void Start()
    {
        itemName = transform.name;
        itemDescription = "dummy";
        try
        {
            GetComponent<BoxCollider>().size = transform.lossyScale;
        }catch{
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
            Vector3 enemyPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + other.bounds.extents.y ,other.gameObject.transform.position.z);
            if(hitEffect != null) Instantiate(hitEffect, enemyPos, other.gameObject.transform.rotation);
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(damage);            
        }
    }
}
