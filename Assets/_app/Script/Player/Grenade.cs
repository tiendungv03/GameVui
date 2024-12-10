using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;
    public float delay = 3f;

    public float explosionForce = 10f;
    public float radius = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", delay);
    }

    //Function make grenade explode
    private void Explode()
    {
        //check nearby collider
        Collider[] collider = Physics.OverlapSphere(transform.position,radius);

        //apply force to enemy
        foreach (Collider near in collider)
        {
            Rigidbody rb = near.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius, 0.5f, ForceMode.Impulse);
            }
        }
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
