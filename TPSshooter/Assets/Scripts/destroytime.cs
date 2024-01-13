using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroytime : MonoBehaviour
{
    public float Destroytime = 0.8f;
    void Start()
    {
        Destroy(gameObject, Destroytime);
    }

   
}
