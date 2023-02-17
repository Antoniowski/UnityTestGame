using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private Collider ownCollider;

    public int damage;

    void Awake()
    {
        ownCollider = GetComponent<CapsuleCollider>();
        damage = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
