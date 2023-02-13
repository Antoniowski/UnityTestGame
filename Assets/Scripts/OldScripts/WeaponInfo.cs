using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int damage;
    public bool isPlayer;
    private bool pickable = true;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        itemName = transform.name;
        itemDescription = "dummy";
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
