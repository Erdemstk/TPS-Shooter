using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    public ParticleSystem wallDestroyParticle;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(wallDestroyParticle,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
