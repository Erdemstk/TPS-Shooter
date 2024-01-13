using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba1 : MonoBehaviour
{
   
    public GameObject explode;
    public Transform explosionTransform;
    AudioSource audio;
    public float blastRadius = 5f;
    public float force = 700f;

    private static bomba1 instance = null;
    public static bomba1 Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<bomba1>();
            }
            return instance;
        }
    }


    void Start()
    {
      
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Explosion()
    {
        Debug.Log("explosion");
        GameObject newObj = Instantiate(explode, explosionTransform.position, Quaternion.identity);
        newObj.transform.localScale *= 10f;
        Collider[] colliderstoDestroy = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObjects in colliderstoDestroy) 
        {
            Destructible dest = nearbyObjects.GetComponent<Destructible>();
            if(dest != null)
            {
                dest.Destroy();
            }
            

        }
        Collider[] colliderstoMove = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (Collider nearbyObjects in colliderstoMove)
        {
            Rigidbody rb = nearbyObjects.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }
        }
        Destroy(newObj, 2f);   
    }
    
}
